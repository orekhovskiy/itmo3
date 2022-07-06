import javax.servlet.*;
import javax.servlet.http.*;
import java.io.IOException;

public class HistoryCleanerServlet extends HttpServlet
{
    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException
    {
        ServletContext context = getServletContext();
        context.removeAttribute("r");
        context.removeAttribute("x");
        context.removeAttribute("y");
        context.removeAttribute("result");
    }
}
