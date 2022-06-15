using System;
using tabuleiro;
using xadrez;
namespace curso
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Partida partida = new Partida();

                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.impress(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origem = Tela.lerPositionXadrez().toPosition();
                        partida.ValidResult(origem);

                        bool[,] positionPossible = partida.tab.peca(origem).moviesPossible();

                        Console.Clear();
                        Tela.Views(partida.tab, positionPossible);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position ends = Tela.lerPositionXadrez().toPosition();
                        partida.ValidResultEnd(origem, ends);

                        partida.JoinGame(origem, ends);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.impress(partida);

            }catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
