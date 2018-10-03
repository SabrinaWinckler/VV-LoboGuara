/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package simuladorMIPS;


import java.util.ArrayList;
import java.util.HashMap;

/**
 *
 * @author Lucas
 */
public class GerenciadorProcessos {

    private final HashMap<Instrucao, Processo> entradaDeProcessos;
    private final HashMap<Instrucao, Processo> processosEmEspera;
    private final HashMap<Integer, Processo> processosEmExecucao;
    private ArrayList<Processo> processo;
    private ArrayList<Instrucao> instrucao;
    //private final Memoria memoria;

    public GerenciadorProcessos(HashMap<Instrucao, Processo> listaEntrada, int tamanhoMemoria) {

        this.entradaDeProcessos = listaEntrada;
        this.processosEmEspera = new HashMap();
        this.processosEmExecucao = new HashMap();
       // this.memoria = new Memoria(tamanhoMemoria);

    }
    public void identificarProcesso(ArrayList processos){
         for (int i = 0; i < processo.size(); i++) {
            if(processos.get(i).equals(processos.get(i+1))){
                
            }
         }
    }
    public void percorreProcessos() {

        for (int i = 0; i < processo.size(); i++) {
            Processo processoTest = processo.get(i) ;
            processosEmExecucao.put(i, processoTest);
            if (processo.getTempoCheg() == tempoClock) {
                processosEmEspera.add(processo);
                processosDeEntrada.remove(i);
                //Retorna um no contador para não pular nenhum processo da lista
                i--;
            } else {
                /*A lista de entrada está sempre ordenada por tempo de chegada
                então para otimização se o tempo não é igual ao tempo de clock 
                não é necessário percorrer toda a lista*/
                break;
            }
        }

    }

    public void insereProcesso(Instrucao i, Processo processo, Etapa posicao) {
      //  memoria.inserirProcesso(processo, posicao);
        processosEmExecucao.put(i, processo);
        
    }

    public boolean executarProcessos() {

        boolean removeuAlgumProcesso = false;

        for (int i = processosEmExecucao.size() - 1; i >= 0; i--) {
            Processo processo = processosEmExecucao.get(i);
            processo.executar();
            if (processo.getTempoExec() == 0) {
                removerProcessoFinalizado(processo);
                removeuAlgumProcesso = true;
            }
        }

        return removeuAlgumProcesso;

    }

    public void removerProcessoFinalizado(Processo processo) {

        memoria.removerProcesso(processo, processo.getPosicaoMemoria());
        processosEmExecucao.remove(processo);

    }

    public ArrayList<Processo> getProcessosEmEspera() {
        return processosEmEspera;
    }

    public Memoria getMemoria() {
        return memoria;
    }

    public boolean finalizou() {

        if (processosDeEntrada.size() == 0 && processosEmEspera.size() == 0
                && processosEmExecucao.size() == 0) {
            return true;
        }
        return false;
    }

}
