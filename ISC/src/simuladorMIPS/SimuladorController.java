package simuladorMIPS;

import java.io.File;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.ResourceBundle;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.image.Image;
import javafx.scene.image.ImageView;

public class SimuladorController implements Initializable {

    @FXML
    private ImageView painelImagem;

    @FXML
    private Button buttonAnterior;

    @FXML
    private Button buttonProximo;

    private List<Image> listaImagens;

    private int imgAtual;

    public SimuladorController() {
        this.listaImagens = new ArrayList();
        this.imgAtual = 0;
    }

    @FXML
    void gerarCodigoButtonAnterior(ActionEvent event) {
        if (this.imgAtual > 0) {
            this.imgAtual -= 1;
            painelImagem.setImage(listaImagens.get(imgAtual));
        }
    }

    @FXML
    void gerarCodigoButtonProximo(ActionEvent event) {
        if (this.imgAtual < listaImagens.size() - 1) {
            this.imgAtual += 1;
            painelImagem.setImage(listaImagens.get(imgAtual));
        }
    }

    public void initialize(URL location, ResourceBundle resources) {
        this.carregarImagens();
        painelImagem.setImage(listaImagens.get(imgAtual));
    }

    private void carregarImagens() {
        File file;
        int ite = 0;
        do {
            file = new File("img" + ite + ".jpg");
            Image image = new Image(file.toURI().toString());
            this.listaImagens.add(image);

            ite += 1;
            file = new File("img" + ite + ".jpg");
        } while (file.exists());
    }
    
}
