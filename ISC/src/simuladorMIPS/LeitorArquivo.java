package simuladorMIPS;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */


import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.JFileChooser;
import javax.swing.JOptionPane;
import javax.swing.filechooser.FileFilter;
import javax.swing.filechooser.FileNameExtensionFilter;


/**
 *
 * @author Lucas
 */
public class LeitorArquivo {//REFATORAR!

    private static File arquivo;
    private static int quantidadeProcessos;

    public static File carregarArquivo() {

        JFileChooser chooser = new JFileChooser();
        
        FileFilter filter = new FileNameExtensionFilter("Arquivo JSON", "json");
        chooser.addChoosableFileFilter(filter);
        chooser.setAcceptAllFileFilterUsed(false);

        int retorno = chooser.showOpenDialog(null);

        if (retorno == JFileChooser.APPROVE_OPTION) {
            arquivo = chooser.getSelectedFile();
            JOptionPane.showMessageDialog(null, "Arquivo importado com sucesso");
            return arquivo;
        }

        if (arquivo == null) {
            JOptionPane.showMessageDialog(null, "Arquivo Não Selecionado");
        } else {
            JOptionPane.showMessageDialog(null, "Nenhum arquivo foi selecionado será mantido o anterior");
        }

        return arquivo;
    }

    public static ArrayList montarLista(File arquivo) {

        FileReader reader;
        BufferedReader br;

        ArrayList<Processo> listaProcessos = new ArrayList();

        try {
            reader = new FileReader(arquivo);
            br = new BufferedReader(reader);

            String[] entradaArray;
            String entradaString;
            String nomeProcesso;
            String nomeInstrucao; 
            ArrayList listaDeInstrucoesEProcessos = new ArrayList();
            
            
            while ((entradaString = br.readLine()) != null) {
                entradaArray = entradaString.split(":");
                nomeInstrucao = entradaArray[0];
                entradaArray = entradaString.split(",");
                nomeProcesso = entradaArray[0];
                

                listaProcessos.add(new Processo(nomeProcesso));
            }

        } catch (IOException ex) {
            Logger.getLogger(LeitorArquivo.class.getName()).log(Level.SEVERE, null, ex);
        }

       
        LeitorArquivo.quantidadeProcessos = listaProcessos.size();
        return listaProcessos;
    }

    public static int getQuantidadeProcessos() {
        return quantidadeProcessos;
    }
}
