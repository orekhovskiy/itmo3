package beans;

import com.google.gson.Gson;
import database.DataAccess;
import database.PointEntity;
import org.hibernate.annotations.Check;
import org.icefaces.ace.json.JSONArray;
import org.icefaces.ace.json.JSONObject;

import javax.faces.bean.ApplicationScoped;
import javax.faces.bean.ManagedBean;
import javax.faces.event.ValueChangeEvent;
import java.util.ArrayList;
import java.util.List;

@ManagedBean
@ApplicationScoped
public class ResultsBean {
    private List<CheckAreaBean> resultList;

    public ResultsBean(){
        resultList = new ArrayList<CheckAreaBean>();
    }
    public List<CheckAreaBean> getResultList() {
        return resultList;
    }
    public void addToResultList(CheckAreaBean point){
        this.resultList.add(point);
    }
}
