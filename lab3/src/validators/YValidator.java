package validators;

import javax.faces.application.FacesMessage;
import javax.faces.component.UIComponent;
import javax.faces.context.FacesContext;
import javax.faces.validator.FacesValidator;
import javax.faces.validator.Validator;
import javax.faces.validator.ValidatorException;


@FacesValidator("validators.YValidator")
public class YValidator implements Validator {

    @Override
    public void validate(FacesContext context, UIComponent component, Object value) throws ValidatorException {
        String tempStr;
        Double y = 0.0;
        tempStr = value.toString();
        y = Double.parseDouble(tempStr);
        if (y < -5.0 || y > 3.0) {
            FacesMessage msg = new FacesMessage("Y должен находиться в пределах (-5; 3)");
            throw new ValidatorException(msg);
        }
    }

}
