package beans;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import javax.faces.context.FacesContext;
import javax.servlet.ServletContext;
import java.io.File;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import java.sql.*;
import java.util.logging.*;

@ManagedBean
@RequestScoped
public class ResultManagerBean implements Serializable
{
    private double x, y, r;

    public ResultManagerBean()
    {
        r = 1;
        try
        {
            Connection connection = null;
            String url = "yebaniy url";
            String name = "yebanoye name";
            String password = "yebaniy password";

            connection = DriverManager.getConnection(url, name, password);

        }
        catch (Throwable ex)
        {
            System.err.println("Failed to create SessionFactory: " + ex);
            //throw new SessionException(ex.getMessage());
        }
    }

    public double getX()
    {
        return x;
    }
    public void setX(double x)
    {
        this.x = x;
    }

    public double getY()
    {
        return y;
    }
    public void setY(double y)
    {
        this.y = y;
    }

    public double getR()
    {
        return r;
    }
    public void setR(double r)
    {
        this.r = r;
    }

    public boolean checkArea(double x, double y)
    {/*
        //Point p = new Point(x, y);
        //Point center = new Point(0, 0);

        //четверть окружности радиуса R/2 в III четверти
        //if (p.x <= 0 && p.y <= 0 && Utils.distance(center, p) <= r/2)
            return true;

        //Квадрат R x R в I четверти
        //if (p.x >= 0 && p.x <= r && p.y >= 0 && p.y <= r)
            return true;

        ///треугольник в IV четверти
        Point a = new Point(r/2, 0), b = new Point(0, -r);
        double abc = Utils.triangleArea(a, b, center),
                abp = Utils.triangleArea(a, b, p),
                acp = Utils.triangleArea(a, p, center),
                bcp = Utils.triangleArea(p, b, center);

        return Utils.doubleEquals(abc, abp + acp + bcp);
    */
    return false;
    }

    public void submit()
    {
        boolean hit = checkArea(x, y);

        ResultEntity result = new ResultEntity();
        result.setX(x);
        result.setY(y);
        result.setR(r);
        result.setHit(hit);
        try {
            Connection connection = null;
            String url = "yebaniy url";
            String name = "yebanoye name";
            String password = "yebaniy password";

            PreparedStatement preparedStatement = null;
            preparedStatement = connection.prepareStatement("insert into hits(x, y, r, hit) values (?,?,?,?)");
            preparedStatement.setInt(x,y,r,hit);
        }
        catch (Throwable e)
        {
            e.printStackTrace();
        }
    }

    public List getResultList()
    {
        Session session;
        List resultList = new ArrayList<ResultEntity>();

        try
        {
            session = sessionFactory.openSession();
            Query query = session.createQuery("FROM ResultEntity ORDER BY id DESC");
            resultList = query.list();
        }
        catch (Throwable e)
        {
            e.printStackTrace();
        }

        return resultList;
    }

    public void clearHistory()
    {
        Transaction tx = null;
        try
        {
            Session session = sessionFactory.openSession();
            tx = session.beginTransaction();
            Query query = session.createQuery("DELETE FROM ResultEntity");
            query.executeUpdate();
            tx.commit();
        }
        catch (Throwable e)
        {
            if (tx != null) tx.rollback();
            e.printStackTrace();
        }*/
    }
}
