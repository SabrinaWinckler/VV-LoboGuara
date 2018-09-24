package simuladorMIPS;

import java.io.IOException;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.stage.Stage;

public class InicialController {

    @FXML
    private Button buttonStart;

    @FXML
    void gerarCodigoButton(ActionEvent event) throws IOException {
        Parent menu_parent = FXMLLoader.load(getClass().getResource("/simuladorMIPS/Simulador.fxml"));
        Scene menu_scene = new Scene(menu_parent);
        Stage app_stage = (Stage) ((Node) event.getSource()).getScene().getWindow();
        app_stage.hide();
        app_stage.setScene(menu_scene);
        app_stage.show();
    }

}
