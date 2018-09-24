using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VV_ProjetoLobosOvelhas
{
    class SimuladorTela : Form
    {
        private static readonly Color COR_VAZIA = Color.FromArgb(0,255,255,255);
        private static readonly Color COR_INDEFINIDA = Color.FromArgb(0,128,128,128);

        private readonly String PREFIXO_ETAPA = "Etapa: ";
        private readonly String PREFIXO_POPULACAO = "Populacao: ";
        private Label rotuloEtapa, populacao;
        private VisaoCampo visaoCampo;

        private Dictionary<Type, Color> cores;
        private CampoEstatistica estatisticas;

        public SimuladorTela(int height, int width)
        {
            estatisticas = new CampoEstatistica();
            cores = new LinkedHashMap<Type, Color>();

            
            this.Text = "Simulacao Coelhos and Raposas";
            rotuloEtapa.Text = PREFIXO_ETAPA;
            rotuloEtapa.AutoSize = false;
            rotuloEtapa.TextAlign = ContentAlignment.MiddleCenter;
            rotuloEtapa.Dock = DockStyle.None;
            populacao.Text = PREFIXO_POPULACAO;
            populacao.AutoSize = false;
            populacao.TextAlign = ContentAlignment.MiddleCenter;
            populacao.Dock = DockStyle.None;

            setLocation(100, 50);

            visaoCampo = new VisaoCampo(height, width);

            Container conteudos = getContentPane();
            conteudos.add(rotuloEtapa, BorderLayout.NORTH);
            conteudos.add(visaoCampo, BorderLayout.CENTER);
            conteudos.add(populacao, BorderLayout.SOUTH);
            pack();
            setVisible(true);
        }

        public void SetCor(Type animalClass, Color color)
        {
            if(cores.ContainsKey(animalClass))
            {
                cores.Remove(animalClass);
                cores.Add(animalClass, color);
            }
        }

        private Color getCor(Type animalClass)
        {
            Color coluna = cores[animalClass];
            if (coluna == null)
            {
                return COR_INDEFINIDA;
            }
            else
            {
                return coluna;
            }
        }

        public void mostraStatus(int etapa, Campo campo)
        {
            if (!isVisible())
            {
                setVisible(true);
            }

            rotuloEtapa.setText(PREFIXO_ETAPA + etapa);
            estatisticas.redefine();

            visaoCampo.preparePaint();

            for (int row = 0; row < campo.getProfundidade(); row++)
            {
                for (int col = 0; col < campo.getLargura(); col++)
                {
                    Object animal = campo.getObjectAt(row, col);
                    if (animal != null)
                    {
                        estatisticas.incrementaContador(animal.GetType());
                        visaoCampo.drawMark(col, row, getCor(animal.GetType()));
                    }
                    else
                    {
                        visaoCampo.drawMark(col, row, COR_VAZIA);
                    }
                }
            }
            estatisticas.contadorFinalizado();

            populacao.setText(PREFIXO_POPULACAO + estatisticas.getPopulationDetails(campo));
            visaoCampo.repaint();
        }

        public bool ehViavel(Campo campo)
        {
            return estatisticas.ehViavel(campo);
        }

        private class VisaoCampo
        {
        private readonly int GRID_VIEW_SCALING_FACTOR = 6;

        private int gridWidth, gridHeight;
        private int xScale, yScale;
        Point size;
        private Graphics g;
        private Image fieldImage;

        public VisaoCampo(int height, int width)
        {
            gridHeight = height;
            gridWidth = width;
            size = new Point(0, 0);
        }

        public Point getPreferredSize()
        {
            return new Point(gridWidth * GRID_VIEW_SCALING_FACTOR,
                                 gridHeight * GRID_VIEW_SCALING_FACTOR);
        }

        public void preparePaint()
        {
            if (!size.Equals(getSize))
            {
                size = getSize();
                fieldImage = visaoCampo.createImage(size.width, size.height);
                g = fieldImage.getGraphics();

                xScale = size.width / gridWidth;
                if (xScale < 1)
                {
                    xScale = GRID_VIEW_SCALING_FACTOR;
                }
                yScale = size.height / gridHeight;
                if (yScale < 1)
                {
                    yScale = GRID_VIEW_SCALING_FACTOR;
                }
            }
        }

        public void drawMark(int x, int y, Color color)
        {
            g.setColor(color);
            g.fillRect(x * xScale, y * yScale, xScale - 1, yScale - 1);
        }

        public void paintComponent(Graphics g)
        {
            if (fieldImage != null)
            {
                Dimension currentSize = getSize();
                if (size.equals(currentSize))
                {
                    g.drawImage(fieldImage, 0, 0, null);
                }
                else
                {
                    g.drawImage(fieldImage, 0, 0, currentSize.width, currentSize.height, null);
                }
            }
        }
    }
}
}
