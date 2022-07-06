package database;

import org.hibernate.annotations.Type;

import javax.persistence.*;

@Entity
@Table(name = "POINT", schema = "ETS")
public class PointEntity {

    @Basic
    @Column(name = "X", nullable = false)
    private double x;

    @Basic
    @Column(name = "Y", nullable = false)
    private double y;

    @Basic
    @Column(name = "R", nullable = false)
    private double r;

    @Basic
    @Type(type = "true_false")
    @Column(name = "STRIKE", nullable = false   )
    private boolean strike;

    @Id
    @SequenceGenerator(name = "SEQ", sequenceName = "ID_SEQUENCE", schema = "ETS", allocationSize=1)
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "SEQ")
    @Column(name = "ID")
    private long id;

    public long getId(){
        return id;
    }

    public double getX() {
        return x;
    }

    public void setX(double x) {
        this.x = x;
    }


    public double getY() {
        return y;
    }

    public void setY(double y) {
        this.y = y;
    }


    public double getR() {
        return r;
    }

    public void setR(double r) {
        this.r = r;
    }


    public boolean getStrike() {
        return strike;
    }

    public void setStrike(boolean strike) {
        this.strike = strike;
    }

}
