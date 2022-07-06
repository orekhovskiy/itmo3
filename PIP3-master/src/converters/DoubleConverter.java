package converters;

import javax.faces.application.FacesMessage;
import javax.faces.component.UIComponent;
import javax.faces.context.FacesContext;
import javax.faces.convert.Converter;
import javax.faces.convert.ConverterException;
import javax.faces.convert.FacesConverter;

@FacesConverter("converters.DoubleConverter")
public class DoubleConverter implements Converter {

    @Override
    public Object getAsObject(FacesContext context, UIComponent component, String value) {
        String tempStr;
        Double y = 0.0;
        try {
            tempStr = (String) value;
            y = Double.parseDouble(tempStr);
        } catch (NumberFormatException e) {
            FacesMessage msg = new FacesMessage("Значение должно быть вещественным числом (дробная часть отделяется запятой)");
            throw new ConverterException(msg);
        } catch (NullPointerException e) {
            FacesMessage msg = new FacesMessage("Строка ввода не может быть пустой");
            throw new ConverterException(msg);
        }
        return y;
    }

    @Override
    public String getAsString(FacesContext context, UIComponent component, Object value) {
        return value.toString();
    }

}
