import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;

public class ControllerServlet extends HttpServlet
{
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException
    {
        RequestDispatcher dispatcher;

        String rString = request.getParameter("r"),
               xString = request.getParameter("x"),
               yString = request.getParameter("y");

        float r;
        try
        {
            assert (rString != null && xString != null && yString != null);
            r = Float.parseFloat(rString);
            Float.parseFloat(xString);
            Float.parseFloat(yString);
            assert (r >= 0);
        }
        catch (Exception exception)
        {
            dispatcher = request.getRequestDispatcher("/index.jsp");
            dispatcher.forward(request, response);
            return;
        }

        dispatcher = request.getRequestDispatcher("/check");
        dispatcher.forward(request, response);
    }
}
