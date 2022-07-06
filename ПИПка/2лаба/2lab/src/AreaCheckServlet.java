import javax.servlet.*;
import javax.servlet.http.*;
import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;

public class AreaCheckServlet extends HttpServlet
{
    private boolean checkArea(float r, Point p)
            throws IOException
    {

        if (Math.pow(p.y, 2) + Math.pow(p.x, 2) <= Math.pow(r, 2) && p.x >= 0 && p.y >= 0 ||
            p.y >= 0.5 * p.x - (double)(r / 2) &&  p.x >= 0 && p.y <= 0 ||
            Math.abs(p.y) <= r && Math.abs(p.x) <=((double) r / 2) && p.x <= 0 && p.y <=0)
            return true;
        else
            return false;
    }

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException
    {
        String rStr = request.getParameter("r_field"),
               xStr = request.getParameter("x"),
               yStr = request.getParameter("y");
        float r = Float.NaN, x = Float.NaN, y = Float.NaN;
        boolean error = false, result = false;
        try {
            r = Float.parseFloat(rStr.replace(',', '.'));
            x = Float.parseFloat(xStr.replace(',', '.'));
            y = Float.parseFloat(yStr.replace(',', '.'));

            Point p = new Point(x, y);
            result = checkArea(r, p);
        }
        catch (Exception e) { error = true; }

        if (r < 0 || Math.abs(x) > 5 || Math.abs(y) > 5) error = true;

        PrintWriter out = response.getWriter();

        //выводим страницу
        response.setContentType("text/html");
        out.print("<!DOCTYPE html>" +
                  "<html lang=\"en\">" +
                  "<style type=\"text/css\">" +
                  " div {" +
                  "     margin: 5% auto 5%" +
                  " }" +
                  " table {" +
                  "   border: 1px solid #0c181f;" +
                  "   border-collapse: collapse;" +
                  "   margin: inherit;" +
                  " }" +
                  " th, td {" +
                  "   border: 1px solid #0c181f;" +
                  "   border-collapse: collapse;" +
                  "   text-align: center;" +
                  "   padding: 15px;" +
                  " }" +
                  " button {" +
                  "   background-color: #0c181f;" +
                  "   width: 150px;" +
                  "   height: 40px;" +
                  "   color: white;" +
                  "   font-size: 16;" +
                  "   font-family: \"Fantasy\";" +
                  "   font-weight: bold;" +
                  "   border: none;" +
                  "   border-radius: 15px;" +
                  "}" +
                  "</style>" +
                  "<head>" +
                  "    <meta charset=\"UTF-8\">" +
                  "    <title>Results</title>" +
                  "</head>" +
                  "<body>" +
                  "    <div> " +
                  "       <table border=\"1\">" +
                  "            <tr>" +
                  "                <th>Radius</th>" +
                  "                <th>X</th>" +
                  "                <th>Y</th>" +
                  "                <th>Included?</th>" +
                  "            </tr>" +
                  "            <tr>" +
                  "                <td>" + (!Float.isNaN(r) ? r : rStr) + "</td>" +
                  "                <td>" + (!Float.isNaN(x) ? x : xStr) + "</td>" +
                  "                <td>" + (!Float.isNaN(y) ? y : yStr)  + "</td>" +
                  "                <td>" + (error ? "Error!" : result ? "Yes" : "No") + "</td>" +
                  "            </tr>" +
                  "       </table>" +
                  "    </div>" +
                  "    <div style=\"text-align: center\">" +
                  "       <button onclick=\"location.href='./';\">Return</button>" +
                  "    </div>" +
                  "</body>" +
                  "</html>");
        out.close();
        if (error) return;

        //сохраняем данные в контекст
        ServletContext context = getServletContext();

        ArrayList<Float> rList = (ArrayList)context.getAttribute("r");
        ArrayList<Float> xList = (ArrayList)context.getAttribute("x");
        ArrayList<Float> yList = (ArrayList)context.getAttribute("y");
        ArrayList<Boolean> resList = (ArrayList)context.getAttribute("result");

        if(rList == null || xList == null || yList == null || resList == null)
        {
            rList = new ArrayList<Float>(10);
            xList = new ArrayList<Float>(10);
            yList = new ArrayList<Float>(10);
            resList = new ArrayList<Boolean>(10);
        }

        rList.add(r);
        xList.add(x);
        yList.add(y);
        resList.add(result);

        context.setAttribute("r", rList);
        context.setAttribute("x", xList);
        context.setAttribute("y", yList);
        context.setAttribute("result", resList);
    }
}
