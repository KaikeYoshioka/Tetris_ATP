using System;
using System.Collections.Generic;
using System.IO;

namespace Tetris_ATP
{
    public class Programa
    {
        static void Main(string[] args)
        {
            JogoTetris jogo = new JogoTetris();
            jogo.Iniciar();
        }
    }

    public class JogoTetris
    {
        private const int Largura = 10;
        private const int Altura = 20;
        private int[,] tabuleiro = new int[Altura, Largura];
        private Tetromino pecaAtual;
        private Random aleatorio = new Random();
        private int pontuacao = 0;
        private bool jogoEncerrado = false;

        public void Iniciar()
        {
            Console.WriteLine("Bem-vindo ao Tetris!");
            Console.Write("Digite seu nome: ");
            string nomeJogador = Console.ReadLine() ?? "jogador";

            GerarNovaPeca();

            while (!jogoEncerrado)
            {
                DesenharTabuleiro();
                Console.WriteLine($"Pontuação: {pontuacao}");
                Console.WriteLine("Controles: A (esquerda), D (direita), W (rotacionar), S (descer)");

                var tecla = Console.ReadKey(true).Key;
                switch (tecla)
                {
                    case ConsoleKey.A:
                        MoverEsquerda();
                        break;
                    case ConsoleKey.D:
                        MoverDireita();
                        break;
                    case ConsoleKey.W:
                        Rotacionar();
                        break;
                    case ConsoleKey.S:
                        MoverParaBaixo();
                        break;
                }

                // Verifica se a peça chegou ao fundo
                if (!PodeMoverParaBaixo())
                {
                    FundirPecaNoTabuleiro();
                    LimparLinhasCompletas();
                    GerarNovaPeca();
                    if (!PodePosicionarPeca())
                    {
                        jogoEncerrado = true;
                    }
                }
            }

            Console.WriteLine("Fim de Jogo!");
            Console.WriteLine($"Pontuação final: {pontuacao}");
            SalvarPontuacao(nomeJogador, pontuacao);
        }

        private void DesenharTabuleiro()
        {
            Console.Clear();
            for (int y = 0; y < Altura; y++)
            {
                for (int x = 0; x < Largura; x++)
                {
                    if (tabuleiro[y, x] == 1)
                    {
                        Console.Write("■ ");
                    }
                    else if (pecaAtual != null && pecaAtual.EstaNaPosicao(x, y))
                    {
                        Console.Write("□ ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }

        private void GerarNovaPeca()
        {
            int tipo = aleatorio.Next(3);
            pecaAtual = new Tetromino(tipo, Largura / 2 - 1, 0);
        }

        private bool PodePosicionarPeca()
        {
            for (int i = 0; i < 4; i++)
            {
                int novoX = pecaAtual.X + pecaAtual.Formato[pecaAtual.Rotacao, i, 0];
                int novoY = pecaAtual.Y + pecaAtual.Formato[pecaAtual.Rotacao, i, 1];

                if (novoX < 0 || novoX >= Largura || novoY >= Altura || (novoY >= 0 && tabuleiro[novoY, novoX] == 1))
                {
                    return false;
                }
            }
            return true;
        }

        private bool PodeMoverParaBaixo()
        {
            for (int i = 0; i < 4; i++)
            {
                int novoY = pecaAtual.Y + pecaAtual.Formato[pecaAtual.Rotacao, i, 1] + 1;
                int x = pecaAtual.X + pecaAtual.Formato[pecaAtual.Rotacao, i, 0];

                if (novoY >= Altura || (novoY >= 0 && tabuleiro[novoY, x] == 1))
                {
                    return false;
                }
            }
            return true;
        }

        private void MoverParaBaixo()
        {
            if (PodeMoverParaBaixo())
            {
                pecaAtual.Y++;
            }
        }

        private void MoverEsquerda()
        {
            for (int i = 0; i < 4; i++)
            {
                int novoX = pecaAtual.X + pecaAtual.Formato[pecaAtual.Rotacao, i, 0] - 1;
                int y = pecaAtual.Y + pecaAtual.Formato[pecaAtual.Rotacao, i, 1];

                if (novoX < 0 || (y >= 0 && y < Altura && tabuleiro[y, novoX] == 1))
                {
                    return;
                }
            }
            pecaAtual.X--;
        }

        private void MoverDireita()
        {
            for (int i = 0; i < 4; i++)
            {
                int novoX = pecaAtual.X + pecaAtual.Formato[pecaAtual.Rotacao, i, 0] + 1;
                int y = pecaAtual.Y + pecaAtual.Formato[pecaAtual.Rotacao, i, 1];

                if (novoX >= Largura || (y >= 0 && y < Altura && tabuleiro[y, novoX] == 1))
                {
                    return;
                }
            }
            pecaAtual.X++;
        }

        private void Rotacionar()
        {
            int rotacaoOriginal = pecaAtual.Rotacao;
            pecaAtual.Rotacao = (pecaAtual.Rotacao + 1) % 4;

            if (!PodePosicionarPeca())
            {
                pecaAtual.Rotacao = rotacaoOriginal;
            }
        }

        private void FundirPecaNoTabuleiro()
        {
            for (int i = 0; i < 4; i++)
            {
                int x = pecaAtual.X + pecaAtual.Formato[pecaAtual.Rotacao, i, 0];
                int y = pecaAtual.Y + pecaAtual.Formato[pecaAtual.Rotacao, i, 1];

                if (y >= 0 && y < Altura && x >= 0 && x < Largura)
                {
                    tabuleiro[y, x] = 1;
                }
            }
        }

        private void LimparLinhasCompletas()
        {
            int linhasCompletas = 0;

            for (int y = Altura - 1; y >= 0; y--)
            {
                bool linhaCompleta = true;
                for (int x = 0; x < Largura; x++)
                {
                    if (tabuleiro[y, x] == 0)
                    {
                        linhaCompleta = false;
                        break;
                    }
                }

                if (linhaCompleta)
                {
                    linhasCompletas++;
                    // Move todas as linhas acima para baixo
                    for (int yy = y; yy > 0; yy--)
                    {
                        for (int x = 0; x < Largura; x++)
                        {
                            tabuleiro[yy, x] = tabuleiro[yy - 1, x];
                        }
                    }
                    // Limpa a linha do topo
                    for (int x = 0; x < Largura; x++)
                    {
                        tabuleiro[0, x] = 0;
                    }
                    y++; // Verifica a mesma linha novamente
                }
            }

            // Atualiza a pontuação
            if (linhasCompletas > 0)
            {
                pontuacao += linhasCompletas * 100;
                if (linhasCompletas > 1)
                {
                    pontuacao += (linhasCompletas - 1) * 50; // Bônus por múltiplas linhas
                }
            }
        }

        private void SalvarPontuacao(string nomeJogador, int pontuacao)
        {
            try
            {
                using (StreamWriter escritor = File.AppendText("pontuacoes.txt"))
                {
                    escritor.WriteLine($"{nomeJogador};{pontuacao}");
                }
                Console.WriteLine("Pontuação salva com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar pontuação: {ex.Message}");
            }
        }
    }

    public class Tetromino
    {
        public int[,,] Formato { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Rotacao { get; set; }

        public Tetromino(int tipo, int x, int y)
        {
            X = x;
            Y = y;
            Rotacao = 0;

            // Formato I
            int[,,] formatoI = new int[4, 4, 2]
            {
                { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 } },
                { { 1, -1 }, { 1, 0 }, { 1, 1 }, { 1, 2 } },
                { { 0, 1 }, { 1, 1 }, { 2, 1 }, { 3, 1 } },
                { { 2, -1 }, { 2, 0 }, { 2, 1 }, { 2, 2 } }
            };

            // Formato L
            int[,,] formatoL = new int[4, 4, 2]
            {
                { { 0, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 } },
                { { 1, 0 }, { 1, 1 }, { 1, 2 }, { 2, 0 } },
                { { 0, 1 }, { 1, 1 }, { 2, 1 }, { 2, 2 } },
                { { 0, 2 }, { 1, 0 }, { 1, 1 }, { 1, 2 } }
            };

            // Formato T
            int[,,] formatoT = new int[4, 4, 2]
            {
                { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 2, 1 } },
                { { 1, 0 }, { 1, 1 }, { 2, 1 }, { 1, 2 } },
                { { 0, 1 }, { 1, 1 }, { 2, 1 }, { 1, 2 } },
                { { 1, 0 }, { 0, 1 }, { 1, 1 }, { 1, 2 } }
            };

            switch (tipo)
            {
                case 0:
                    Formato = formatoI;
                    break;
                case 1:
                    Formato = formatoL;
                    break;
                case 2:
                    Formato = formatoT;
                    break;
                default:
                    Formato = formatoI;
                    break;
            }
        }

        public bool EstaNaPosicao(int x, int y)
        {
            for (int i = 0; i < 4; i++)
            {
                if (X + Formato[Rotacao, i, 0] == x && Y + Formato[Rotacao, i, 1] == y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}