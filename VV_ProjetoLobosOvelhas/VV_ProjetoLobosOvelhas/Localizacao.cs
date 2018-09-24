namespace VV_ProjetoLobosOvelhas
{
    internal class Localizacao
    {
        private int linha;
        private int coluna;

        public Localizacao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Localizacao)) {
                Localizacao outra = (Localizacao)obj;
                return linha == outra.getLinha() && coluna == outra.getColuna();
            }
        else {
                return false;
            }
        }

        public string ToString()
        {
            return linha + "," + coluna;
        }

        public int hashCode()
        {
            return (linha << 16) + coluna;
        }

        public int getLinha()
        {
            return linha;
        }

        public int getColuna()
        {
            return coluna;
        }
    }
}