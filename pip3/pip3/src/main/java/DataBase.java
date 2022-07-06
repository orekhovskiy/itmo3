import database.PointsEntity;
import org.hibernate.HibernateException;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;

import javax.annotation.PostConstruct;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import javax.persistence.GeneratedValue;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.zip.DeflaterOutputStream;

public class DataBase implements java.io.Serializable {
    public DataBase(){}

    public void add(Double x,Double y,Double r,String result) {
        Session session = null;
        Transaction tx=null;
        try {
            session = HibernateUtil.getSessionFactory().openSession();
            tx = session.beginTransaction();
            PointsEntity points = new PointsEntity();
            points.setX(x);
            points.setY(y);
            points.setR(r);
            points.setResult(result);
            session.flush();
            session.save(points);
            tx.commit();
        } catch (HibernateException e) {
            if (tx!=null) tx.rollback();
            e.printStackTrace();
        } finally {
            if (session != null && session.isOpen()) {

                session.close();
            }
        }
    }
    public void clear(){
        Session session = null;
        Transaction tx=null;
        try {
            session = HibernateUtil.getSessionFactory().openSession();
            tx=session.beginTransaction();
            Query query =session.createQuery("delete from PointsEntity p");
            query.executeUpdate();
            tx.commit();
        } catch (HibernateException e) {
            if (tx!=null) tx.rollback();
            e.printStackTrace();
        } finally {
            if (session != null && session.isOpen()) {

                session.close();
            }
        }
    }
    public void update(long id,Double r,String result){
        Session session = null;
        try {
            session = HibernateUtil.getSessionFactory().openSession();
            Query q = session.createQuery("update PointsEntity set r=:r_param,result=:res_param where id=:id_param");
            q.setParameter("r_param",r);
            q.setParameter("res_param",result);
            q.setParameter("id_param",id);
            q.executeUpdate();
        }  catch (HibernateException e) {
            e.printStackTrace();
        } finally {
            if (session != null && session.isOpen()) {
                session.close();
            }
        }
    }
    public List getAll() {
        Session session = null;
        List points = new ArrayList<PointsEntity>();
        try {
            session = HibernateUtil.getSessionFactory().openSession();
            Query q = session.createQuery("from PointsEntity p order by p.id");
            points = q.list();
        }  catch (HibernateException e) {
            e.printStackTrace();
        } finally {
            if (session != null && session.isOpen()) {
                session.close();
            }
        }
        return points;
    }
}
