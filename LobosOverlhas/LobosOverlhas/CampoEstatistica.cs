using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV_ProjetoLobosOvelhas
{
    class CampoEstatistica
    {
        private Dictionary<Type, Contador> contadores;
        private bool contadoresValidos;

        public CampoEstatistica()
        {
            contadores = new Dictionary<Type, Contador>();
            contadoresValidos = true;
        }

        public String getPopulationDetails(Campo campo)
        {
            StringBuilder buffer = new StringBuilder();
            if (!contadoresValidos)
            {
                geraContadores(campo);
            }
            foreach (Type chave in contadores.Keys)
            {
                Contador info = contadores.get(chave);
                buffer.Append(info.getName());
                buffer.Append(": ");
                buffer.Append(info.getCount());
                buffer.Append(' ');
            }
            return buffer.ToString();
        }

        public void redefine()
        {
            contadoresValidos = false;
            foreach (Type chave in contadores.Keys)
            {
                Contador contador = contadores.get(chave);
                contador.reset();
            }
        }

        public void incrementaContador(Type animalClass)
        {
            Contador contador = contadores.get(animalClass);
            if (contador == null)
            {
                contador = new Contador(animalClass.getName());
                contadores[animalClass] = contador;
            }
            contador.increment();
        }

        public void contadorFinalizado()
        {
            contadoresValidos = true;
        }

        public bool ehViavel(Campo campo)
        {
            int nonZero = 0;
            if (!contadoresValidos)
            {
                geraContadores(campo);
            }
            foreach (Type key in contadores.Keys)
            {
                Contador info = contadores.get(key);
                if (info.getCount() > 0)
                {
                    nonZero++;
                }
            }
            return nonZero > 1;
        }

        private void geraContadores(Campo campo)
        {
            redefine();
            for (int linha = 0; linha < campo.getProfundidade(); linha++)
            {
                for (int coluna = 0; coluna < campo.getLargura(); coluna++)
                {
                    Object animal = campo.getObjectAt(linha, coluna);
                    if (animal != null)
                    {
                        incrementaContador(animal.GetType());
                    }
                }
            }
            contadoresValidos = true;
        }
    }
}
