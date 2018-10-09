/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package simuladorMIPS;


import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 *
 * @author Lucas
 */
public class GerenciadorProcessos {

    private List<Processo> processosEmExecucao;
    private List<Processo> processosEmEspera;
    private List<Processo> processosEntrada;
    private List<Instrucao> instrucao;
    private Processador execucao;
    //private final Memoria memoria;

    public GerenciadorProcessos() {

        
       // this.memoria = new Memoria(tamanhoMemoria);

    }
    public void identificarPosicaoProcesso(ArrayList processos){
         for (int i = 0; i < processosEntrada.size() ; i++) {
            if(processos.get(i).equals(processos.get(i+5))){     
            }
         }
    }
    public void percorreProcessos() {
        for (int i = 0; i < execucao.getEtapas().length ; i++) {
            
            
            
        
         }
        
    }

    public void insereProcesso(Instrucao i, Processo processo) {
      //  memoria.inserirProcesso(processo, posicao);
        processosEmExecucao.add(processo);
        
    }

    public void executarProcessos() {

        

    }


    public List<Processo> getProcessosEmEspera() {
        return processosEmEspera;
    }

    public Processador getExecucao() {
        return execucao;
    }

    public void processoFinalizado(Processo processoRegistrado) {

        if (execucao.getEtapas()[5].equals(processoRegistrado)) {
            for(int i = 0; i < processosEntrada.size(); i++){
                if(processosEntrada.get(i).equals(processoRegistrado)){
                   processosEntrada.get(i).setRegistrado(true);
                   execucao.removerProcesso(i);
                   processosEmExecucao.remove(i);
                }
            }
           
        }
        
    }

}
