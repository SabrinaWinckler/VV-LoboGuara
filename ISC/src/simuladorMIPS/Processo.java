package simuladorMIPS;

public class Processo {

    private String nome;
    private boolean registrado;
  

    public Processo(String nome) {
        this.nome = nome;
       
    }

    public String getNome() {
        return nome;
    }

    @Override
    public String toString() {
        return nome;
    }

    @Override
    public boolean equals(Object o) {
        Processo outroProcesso = (Processo) o;
        return this.nome.equals(outroProcesso.nome);
    }

    public void setRegistrado(boolean registrado) {
        this.registrado = registrado;
    }
    
    

}
