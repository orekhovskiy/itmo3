import database.PointsEntity;

import javax.annotation.PostConstruct;
import javax.annotation.PreDestroy;
import javax.faces.bean.ManagedBean;
import javax.faces.bean.ManagedProperty;
import java.util.ArrayList;
import java.util.List;
public class CheckArea implements java.io.Serializable {
    Double x;
    Double y;
    Double r;
    DataBase dataBase;
    public CheckArea(){
        dataBase = new DataBase();
    }
    public void setX(Double x){
        this.x=x;
    }
    public void setY(Double y){
        this.y=y;
    }
    public void setR(Double r){
        this.r=r;
    }
    public Double getX(){
        return x;
    }
    public Double getY(){
        return y;
    }
    public Double getR(){
        return r;
    }
    public String check(Double x,Double y,Double r)
    {
        if((y<=-x+r&&y>=0&&x>=0)||(y<=0&&y>=-r&&x>=0&&x<=r/2)||(x*x+y*y<=(r/2)*(r/2)&&x<=0&&y<=0))
            return "in the area";
        return "not in the area";
    }
    public String addToDb(){
        String res = check(this.x,this.y,this.r);
        dataBase.add(x,y,r,res);
        return "add";
    }
    public List getResponse(){
        List response;
        response = dataBase.getAll();
        if(response!=null) {
            for (int i = 0; i < response.size(); i++) {
                PointsEntity pe = (PointsEntity) response.get(i);
                pe.setR(getR());
                pe.setResult(check(pe.getX(),pe.getY(),pe.getR()));
                dataBase.update(pe.getId(),pe.getR(),pe.getResult());
            }
            response = dataBase.getAll();
        }
        return response;
    }
    public void clear(){
        dataBase.clear();
    }

}
