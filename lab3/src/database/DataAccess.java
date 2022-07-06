package database;

import beans.CheckAreaBean;
import org.hibernate.Session;
import org.hibernate.SessionException;
import org.hibernate.Transaction;
import org.hibernate.criterion.Projections;
import org.hibernate.query.Query;

import java.util.List;

public class DataAccess {
    private Session session;
    public DataAccess(){
        try {
            session = PointUtil.getSessionFactory().openSession();
        }
        catch (Throwable ex){
            System.err.println("Opening session failed" + ex.getMessage());
            throw new SessionException(ex.getMessage());
        }
    }
    public void closeSession(){

        session.close();
    }

    public void addNewPoint(double x, double y, double r, boolean strike){
        Transaction tx = session.beginTransaction();
        PointEntity point = new PointEntity();
        point.setStrike(strike);
        point.setR(r);
        point.setY(y);
        point.setX(x);
        session.save(point);
        tx.commit();
    }
    public List<PointEntity> getListOfPoints(){
        return (List<PointEntity>)session.createQuery("from PointEntity order by id").list();
    }
    public void deleteAllFromTable(){

        session.createQuery("delete from PointEntity");
    }

    public List<PointEntity> getLastNPoints(int n){
        return session.createQuery(
                "FROM PointEntity ORDER BY id DESC"
        ).setMaxResults(n).list();
    }
    public void changeRadius(double r, PointEntity point){
        session.beginTransaction();
        CheckAreaBean check = new CheckAreaBean();
        check.setX(point.getX()*100);
        check.setY(point.getY());
        check.setR(point.getR());
        check.checkArea();
        Boolean strike = check.getStrike();
        point.setStrike(strike);
        point.setR(r);
        session.update(point);
        session.getTransaction().commit();
    }

}
