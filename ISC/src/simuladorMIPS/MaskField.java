/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package simuladorMIPS;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import javafx.application.Platform;
import javafx.event.EventHandler;
import javafx.scene.control.TextField;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;

/**
 *
 * @author roliv
 */
public class MaskField {

    private static List<KeyCode> ignoreKeyCodes = new ArrayList<>();

    public static void ignoreKeys(TextField textField) {
        textField.addEventFilter(KeyEvent.KEY_PRESSED, (EventHandler) new EventHandler<KeyEvent>() {

            @Override
            public void handle(KeyEvent keyEvent) {
                if (ignoreKeyCodes.contains(keyEvent.getCode())) {
                    keyEvent.consume();
                }
            }
        });
    }

    public static void foneField(TextField textField) {
        MaskField.maxField(textField, 14);
        textField.lengthProperty().addListener((observableValue, number, number2) -> {
            try {
                String value = textField.getText();
                value = value.replaceAll("[^0-9]", "");
                int tam = value.length();
                value = value.replaceFirst("(\\d{2})(\\d)", "($1)$2");
                value = value.replaceFirst("(\\d{4})(\\d)", "$1-$2");
                if (tam > 10) {
                    value = value.replaceAll("-", "");
                    value = value.replaceFirst("(\\d{5})(\\d)", "$1-$2");
                }
                textField.setText(value);
                MaskField.positionCaret(textField);

            } catch (Exception ex) {
            }
        }
        );
    }

    public static void cpfField(TextField textField) {
        MaskField.maxField(textField, 14);
        textField.lengthProperty().addListener((observableValue, number, number2) -> {
            String value = textField.getText();
            value = value.replaceAll("[^0-9]", "");
            value = value.replaceFirst("(\\d{3})(\\d)", "$1.$2");
            value = value.replaceFirst("(\\d{3})(\\d)", "$1.$2");
            value = value.replaceFirst("(\\d{3})(\\d)", "$1-$2");
            try {
                textField.setText(value);
                MaskField.positionCaret(textField);
            } catch (Exception ex) {
            }
        }
        );
    }

    private static void positionCaret(TextField textField) {
        Platform.runLater(() -> {
            if (textField.getText().length() != 0) {
                textField.positionCaret(textField.getText().length());
            }
        }
        );
    }

    public static void maxField(TextField textField, Integer length) {
        textField.textProperty().addListener((observableValue, oldValue, newValue) -> {
            if (newValue == null || newValue.length() > length) {
                textField.setText(oldValue);
            }
        }
        );
    }

    static {
        Collections.addAll(ignoreKeyCodes, new KeyCode[]{KeyCode.F1, KeyCode.F2, KeyCode.F3, KeyCode.F4,
            KeyCode.F5, KeyCode.F6, KeyCode.F7, KeyCode.F8, KeyCode.F9, KeyCode.F10, KeyCode.F11, KeyCode.F12});
    }

}
