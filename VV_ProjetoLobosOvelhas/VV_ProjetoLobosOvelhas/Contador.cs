namespace VV_ProjetoLobosOvelhas
{
    internal class Contador
    {
        private string name;
        private int count;

        public Contador(string name)
        {
            this.name = name;
            count = 0;
        }

        public string getName()
        {
            return name;
        }

        public int getCount()
        {
            return count;
        }

        public void increment()
        {
            count++;
        }

        public void reset()
        {
            count = 0;
        }
    }
}