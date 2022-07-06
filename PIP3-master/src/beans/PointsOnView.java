package beans;


import database.DataAccess;
import database.PointEntity;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.SessionScoped;
import javax.faces.event.ValueChangeEvent;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

@ManagedBean
@SessionScoped
public class PointsOnView{

    private int numberOfPoints;

    public void updateRadiusInDB(ValueChangeEvent e) {
        DataAccess dataAccess = new DataAccess();
        double r;
        try {
            r = Double.parseDouble(e.getNewValue().toString());
        } catch (NullPointerException exception) {
            System.err.println("Empty string:" + exception.getMessage());
            return;
        } catch (NumberFormatException exception) {
            System.err.println("Not number:" + exception.getMessage());
            return;
        }
        System.out.println(numberOfPoints);
        List<PointEntity> list = dataAccess.getLastNPoints(numberOfPoints);
        for (int i = 0; i < list.size(); i++) {
            dataAccess.changeRadius(r, list.get(i));
        }
    }
    public int getNumberOfPoints() {
        return numberOfPoints;
    }
    public List<String> getLastNPoints(){
        DataAccess dataAccess = new DataAccess();
        List<PointEntity> javaList = dataAccess.getLastNPoints(numberOfPoints);
        List<String> jsList= new ArrayList<String>();
        for(int i = 0; i < javaList.size() - 1; i++){
            jsList.add(Double.toString(javaList.get(i).getX()));
            jsList.add(Double.toString(javaList.get(i).getY()));
            jsList.add(Double.toString(javaList.get(i).getR()));
            jsList.add(Boolean.toString(javaList.get(i).getStrike()));
        }
        return jsList;
    }
    public void setNumberOfPoints(int numberOfPoints) {
        numberOfPoints++;
        this.numberOfPoints = numberOfPoints;
    }

}
