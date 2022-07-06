package database;

import javax.persistence.*;

@Entity
@Table(name = "POINTS", schema = "S223868", catalog = "")
public class PointsEntity {
    private long id;
    private Double x;
    private Double y;
    private Double r;
    private String result;
    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE,generator = "points_gen")
    @SequenceGenerator(name = "points_gen",sequenceName = "Points_seq")
    @Column(name = "ID")
    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Basic
    @Column(name = "X")
    public Double getX() {
        return x;
    }

    public void setX(Double x) {
        this.x = x;
    }

    @Basic
    @Column(name = "Y")
    public Double getY() {
        return y;
    }

    public void setY(Double y) {
        this.y = y;
    }

    @Basic
    @Column(name = "R")
    public Double getR() {
        return r;
    }

    public void setR(Double r) {
        this.r = r;
    }

    @Basic
    @Column(name = "RESULT")
    public String getResult() {
        return result;
    }

    public void setResult(String result) {
        this.result = result;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        PointsEntity that = (PointsEntity) o;

        if (id != that.id) return false;
        if (x != null ? !x.equals(that.x) : that.x != null) return false;
        if (y != null ? !y.equals(that.y) : that.y != null) return false;
        if (r != null ? !r.equals(that.r) : that.r != null) return false;
        if (result != null ? !result.equals(that.result) : that.result != null) return false;

        return true;
    }

    @Override
    public int hashCode() {
        int result1 = (int) (id ^ (id >>> 32));
        result1 = 31 * result1 + (x != null ? x.hashCode() : 0);
        result1 = 31 * result1 + (y != null ? y.hashCode() : 0);
        result1 = 31 * result1 + (r != null ? r.hashCode() : 0);
        result1 = 31 * result1 + (result != null ? result.hashCode() : 0);
        return result1;
    }
}
