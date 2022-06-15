using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace curso
{
    class Tela
    {
        public static void impress(Partida partida)
        {
            Views(partida.tab);
            Captureds(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            if (!partida.terminada)
            {
                Console.WriteLine("Agurdando jogador: " + partida.Player);
                if (partida.xeque)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE !");
                Console.WriteLine("Vencedor: " + partida.Player);
            }
        }
        public static void Captureds(Partida partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas");
            impressConj(partida.CapturedWhite(Color.Branca));
            Console.Write("Pretas");
            impressConj(partida.CapturedWhite(Color.Preto));
            Console.WriteLine();
        }
        public static void impressConj(HashSet<Peca> conjunto)
        {
            Console.Write("  ["); 
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.Write("]  ");
        }
        public static void Views(Tabuleiro tab)
        {
            for(int i = 0; i<tab.linha; i++)
            {
                Console.Write(8 - i + "| ");

                for (int j = 0; j < tab.coluna; j++)
                {
                    impressPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("   A B C D E F G H");
            Console.WriteLine();
        }
        public static void Views(Tabuleiro tab, bool[,] positionPossible)
        {
            ConsoleColor original = Console.BackgroundColor;
            ConsoleColor Alterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.linha; i++)
            {
                Console.Write(8 - i + "| ");
                for (int j = 0; j < tab.coluna; j++)
                {
                    if(positionPossible[i, j])
                    {
                        Console.BackgroundColor = Alterado;
                    }
                    else
                    {
                        Console.BackgroundColor = original;
                    }
                    impressPeca(tab.peca(i, j));
                    Console.BackgroundColor = original;

                }
                Console.WriteLine();
            }
            Console.WriteLine("   A B C D E F G H");
            Console.BackgroundColor = original;
        }
        public static PositionXadrez lerPositionXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PositionXadrez(coluna, linha);
        }
        public static void impressPeca(Peca peca)
        {
            if(peca == null)
            {
                Console.Write(" -");
            }
            else
            {
                if (peca.color == Color.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
