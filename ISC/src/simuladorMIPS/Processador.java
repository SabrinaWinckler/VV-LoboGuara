package simuladorMIPS;

public class Processador {

   

    private final Processo[] etapas;

    public Processador() {
        this.etapas = new Processo[5];
       
    }
    public Processo[] getEtapas(){
        return this.etapas;
    }
    public Processo get(int indice) {

        try {
            return etapas[indice];
        } catch (IndexOutOfBoundsException e) {
            System.err.println("Caught IndexOutOfBoundsException: " + e.getMessage());
        }
        return null;
        
    }

    public void inserirProcesso(Processo processo, int posicao) {
        etapas[posicao] = processo;        
 
    }
    public void removerProcesso(int posicao) {
        etapas[posicao] = null;
       
    }
        
    @Override
    public String toString() {

        String processosEmExecucao = "";

        for (Processo processo : etapas) {
            if(processo != null){
                 processosEmExecucao  += " " + processo.toString();
            }else{
                processosEmExecucao += " -";
            }
           
        }

        return processosEmExecucao ;

    }
    
}
