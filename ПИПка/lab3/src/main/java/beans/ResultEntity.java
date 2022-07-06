package beans;
import org.hibernate.annotations.Type;

import javax.persistence.*;

@Entity(name = "result")
@Table(name = "POINTS", schema = "S225116")
public class ResultEntity
{
    @Id
    @GeneratedValue(strategy = GenerationType.SEQUENCE, generator = "SEQ")
    @SequenceGenerator(name = "SEQ", sequenceName = "points_seq")
    @Column(name = "ID")
    private Integer id;

    @Basic
    @Column(name = "X", nullable = false)
    private Double x;

    @Basic
    @Column(name = "Y", nullable = false)
    private Double y;

    @Basic
    @Column(name = "R", nullable = false)
    private Double r;

    @Basic
    @Column(name = "HIT", nullable = false)
    private Boolean hit;

    public long getId()
    {
        return id;
    }
    public void setId(int id)
    {
        this.id = id;
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

    public boolean isHit()
    {
        return hit;
    }
    public void setHit(boolean hit)
    {
        this.hit = hit;
    }
}
