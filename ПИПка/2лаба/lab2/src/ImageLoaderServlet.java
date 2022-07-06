import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.io.OutputStream;

import javax.imageio.ImageIO;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

public class ImageLoaderServlet extends HttpServlet
{
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws IOException
    {
        response.setContentType("image/png");

        String pathToWeb = getServletContext().getRealPath(File.separator);
        File f = new File(pathToWeb + "axis1.png");
        BufferedImage image = ImageIO.read(f);
        OutputStream out = response.getOutputStream();
        ImageIO.write(image, "png", out);
        out.close();
    }
}