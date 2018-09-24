using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV_ProjetoLobosOvelhas
{
    class Ovelha
    {
        private static readonly int IDADE_PROCRIACAO = 5;
        private static readonly int IDADE_MAXIMA = 40;
        private static readonly double PROBABILIDADE_PROCRIACAO = 0.15;
        private static readonly int TAMANHO_MAXIMO_NINHADA = 4;
        private static readonly Random rand = Randomizador.getRandom();
    
    private int idade;
        private bool vivo;
        private Localizacao localizacao;
        private Campo campo;

        public Ovelha(bool randomAge, Campo campo, Localizacao localizacao)
        {
            idade = 0;
            vivo = true;
            this.campo = campo;
            setLocalizacao(localizacao);
            if (randomAge)
            {
                idade = rand.Next(IDADE_MAXIMA);
            }
        }

        public void corre(List<Ovelha> novasOvelhas)
        {
            incrementaIdade();
            if (vivo)
            {
                daALuz(novasOvelhas);
                Localizacao newLocalizacao = campo.localizacaoAdjacenteLivre(localizacao);
                if (newLocalizacao == null)
                {
                    setLocalizacao(newLocalizacao);
                }
                else
                {
                    setMorte();
                }
            }
        }

        public bool estaViva()
        {
            return vivo;
        }

        public void setMorte()
        {
            vivo = false;
            if (localizacao != null)
            {
                campo.limpa(localizacao);
                localizacao = null;
                campo = null;
            }
        }

        public Localizacao getLocalizacao()
        {
            return localizacao;
        }

        private void setLocalizacao(Localizacao newLocalizacao)
        {
            if (localizacao != null)
            {
                campo.limpa(localizacao);
            }
            localizacao = newLocalizacao;
            campo.lugar(this, newLocalizacao);
        }

        private void incrementaIdade()
        {
            idade++;
            if (idade > IDADE_MAXIMA)
            {
                idade--;
            }
        }

        private void daALuz(List<Ovelha> novasOvelhas)
        {
            List<Localizacao> livre = campo.localizacoesAdjacentesLivres(localizacao);
            int nascimentos = procria();
            for (int b = 0; b < nascimentos; b++)
            {
                Localizacao loc = livre.RemoveAt(0);
                Ovelha jovem = new Ovelha(false, campo, loc);
                novasOvelhas.Add(jovem);
            }
        }

        private int procria()
        {
            int nascimentos = 0;
            if (podeProcriar() && rand.NextDouble() == PROBABILIDADE_PROCRIACAO)
            {
                nascimentos = rand.Next(TAMANHO_MAXIMO_NINHADA) + 1;
            }
            return nascimentos;
        }

        private bool podeProcriar()
        {
            return idade >= IDADE_PROCRIACAO;
        }
    }
}
