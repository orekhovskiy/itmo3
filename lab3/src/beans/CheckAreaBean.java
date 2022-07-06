package beans;

import javax.faces.bean.ManagedBean;
import javax.faces.bean.RequestScoped;
import javax.faces.context.FacesContext;

import database.DataAccess;


@ManagedBean
@RequestScoped
public class CheckAreaBean {
    private Double x;
    private Double y;
    private Double r;
    private Boolean strike;

    public Double getX() {
        return this.x;
    }

    public void setX(Double x) {
        this.x = x;
    }

    public Double getY() {
        return this.y;
    }

    public void setY(Double y) {
        this.y = y;
    }

    public Double getR() {
        return this.r;
    }

    public void setR(Double r) {
        this.r = r;
    }

    public Boolean getStrike() {
        return this.strike;
    }



    public void checkArea() {
        if (!(x == null || y == null || r == null)) {
            if (x < 0) {
                if (y > 0) strike = false;
                else {
                    if (x * x + y * y <= r * r / 4) strike = true;
                    else strike = false;
                }
            } else {
                if (y > 0) {
                    if (-x / 2 + r / 2 >= y) strike = true;
                    else strike = false;
                } else {
                    if (x <= r && y >= -r) strike = true;
                    else strike = false;
                }
            }
        }
    }

    public String getOutput() {
        checkArea();
        if (strike == null) return "";

        //Закомментить 2 строчки ниже, чтобы затестить без подключения к бд.
        //DataAccess dataAccess = new DataAccess();
        //dataAccess.addNewPoint(x, y, r, strike);

        FacesContext context = FacesContext.getCurrentInstance();
        ResultsBean bean = (ResultsBean)context.getExternalContext().getApplicationMap().get("resultsBean");
        bean.addToResultList(this);

        String outputMessage = "Точка (" + this.x + "; " + this.y + ") ";
        if (!strike) outputMessage += "не ";
        outputMessage += "попадает в область радиусом " + this.r;
        return outputMessage;
    }
}
