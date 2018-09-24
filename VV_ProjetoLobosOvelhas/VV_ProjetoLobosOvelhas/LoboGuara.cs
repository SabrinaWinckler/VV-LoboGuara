using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VV_ProjetoLobosOvelhas
{
    class LoboGuara
    {
        private static readonly int IDADE_PROCRIACAO = 10;
        private static readonly int IDADE_MAXIMA = 150;
        private static readonly double PROBABILIDADE_PROCRIACAO = 0.75;
        private static readonly int TAMANHO_MAXIMO_NINHADA = 5;
        private static readonly int VALOR_FOME_OVELHA = 7;
        private static readonly Random rand = Randomizador.getRandom();
    
    private int idade;
        private bool vivo;
        private Localizacao localizacao;
        private Campo campo;
        private int nivelFome;

        public LoboGuara(bool idadeRandomica, Campo campo, Localizacao localizacao)
        {
            idade = 0;
            vivo = true;
            this.campo = campo;
            setLocalizacao(localizacao);
            if (idadeRandomica)
            {
                idade = rand.Next(IDADE_MAXIMA);
                nivelFome = rand.Next(VALOR_FOME_OVELHA);
            }
            else
            {
                nivelFome = VALOR_FOME_OVELHA;
            }
        }

        public void caca(List<LoboGuara> novosLobos)
        {
            incrementaIdade();
            if (vivo)
            {
                daALuz(novosLobos);
                Localizacao newLocalizacao = procuraComida(localizacao);
                if (newLocalizacao == null)
                {
                    newLocalizacao = campo.localizacaoAdjacenteLivre(localizacao);
                }
                if (newLocalizacao != null)
                {
                    setLocalizacao(newLocalizacao);
                }
                else
                {
                    setMorte();
                }
            }
        }

        public bool estaVivo()
        {
            return vivo;
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
            if (idade >= IDADE_MAXIMA)
            {
                setMorte();
            }
        }

        private void incrementaFome()
        {
            nivelFome--;
            if (nivelFome == 0)
            {
                setMorte();
            }
        }

        private Localizacao procuraComida(Localizacao localizacao)
        {
            List<Localizacao> adjacente = campo.localizacoesAdjacentes(localizacao);
            IEnumerator<Localizacao> it = adjacente.GetEnumerator();
            while (it.MoveNext())
            {
                Localizacao onde = it.Current;
                Object animal = campo.getObjectAt(onde);
                Ovelha ovelha = (Ovelha)animal;
                ovelha.setMorte();
                nivelFome = VALOR_FOME_OVELHA;
                return onde;
            }
            return null;
        }

        private void daALuz(List<LoboGuara> novosLobos)
        {
            List<Localizacao> livre = campo.localizacoesAdjacentesLivres(localizacao);
            int nascimentos = procria();
            for (int b = 0; b < nascimentos; b++)
            {
                Localizacao loc = livre.RemoveAt(0);
                LoboGuara jovem = new LoboGuara(false, campo, loc);
                novosLobos.Add(jovem);
            }
        }

        private int procria()
        {
            int nascimentos = 0;
            if (podeProcriar() && rand.NextDouble() < PROBABILIDADE_PROCRIACAO)
            {
                nascimentos = rand.Next(TAMANHO_MAXIMO_NINHADA) + 1;
            }
            return nascimentos;
        }

        private bool podeProcriar()
        {
            return idade > IDADE_PROCRIACAO;
        }

        private void setMorte()
        {
            vivo = false;
            if (localizacao != null)
            {
                campo.limpa(localizacao);
                localizacao = null;
                campo = null;
            }
        }

    }
}
