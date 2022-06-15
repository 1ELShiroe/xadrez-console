using System.Collections.Generic;
using System;
using tabuleiro;

namespace xadrez
{
    class Partida
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Color Player { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        public Partida()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            Player = Color.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            entryPeca();
        }

        public Peca executeMoview(Position origem, Position ends)
        {
            Peca p = tab.exitPeca(origem);
            p.incrementeMoview();
            Peca capture = tab.exitPeca(ends);
            tab.entryPeca(p, ends);
            if (capture != null)
            {
                capturadas.Add(capture);
            }
            // #JOGADAESPECIAL ROQUE Pequeno;
            if (p is Rei && ends.coluna == origem.coluna + 2)
            {
                Position OrigemT = new Position(origem.linha, origem.coluna + 3);
                Position DestinoT = new Position(origem.linha, origem.coluna + 1);
                Peca T = tab.exitPeca(OrigemT);
                T.incrementeMoview();
                tab.entryPeca(T, DestinoT);
            }
            // #JOGADAESPECIAL ROQUE grande;
            if (p is Rei && ends.coluna == origem.coluna - 2)
            {
                Position OrigemT = new Position(origem.linha, origem.coluna - 4);
                Position DestinoT = new Position(origem.linha, origem.coluna - 1);
                Peca T = tab.exitPeca(OrigemT);
                T.incrementeMoview();
                tab.entryPeca(T, DestinoT);
            }
            // #JOGADAESPECIAL EN PASSANT;
            if(p is Peao)
            {
                if(origem.coluna != ends.coluna && capture == null)
                {
                    Position posP;
                    if(p.color == Color.Branca)
                    {
                        posP = new Position(ends.linha + 1, ends.coluna);
                    }
                    else
                    {
                        posP = new Position(ends.linha - 1, ends.coluna);
                    }
                    capture = tab.exitPeca(posP);
                    capturadas.Add(capture);
                }
            }
            return capture;
        }
        public void desfazMovie (Position origem,Position ends, Peca capture)
        {
            Peca p = tab.exitPeca(ends);
            p.decrementeMoview();
            if(capture != null)
            {
                tab.entryPeca(capture, ends);
                capturadas.Remove(capture);
            }
            tab.entryPeca(p, origem);

            // #JOGADAESPECIAL ROQUE pequeno;
            if (p is Rei && ends.coluna == origem.coluna - 2)
            {
                Position OrigemT = new Position(origem.linha, origem.coluna - 4);
                Position DestinoT = new Position(origem.linha, origem.coluna + 1);
                Peca T = tab.exitPeca(DestinoT);
                T.decrementeMoview();
                tab.entryPeca(T, OrigemT);
            }
            // #JOGADAESPECIAL ROQUE grande;
            if (p is Rei && ends.coluna == origem.coluna + 2)
            {
                Position OrigemT = new Position(origem.linha, origem.coluna - 3);
                Position DestinoT = new Position(origem.linha, origem.coluna + 1);
                Peca T = tab.exitPeca(DestinoT);
                T.decrementeMoview();
                tab.entryPeca(T, OrigemT);
            }
            // #JOGADAESPECIAL EN PASSANT;
            if(p is Peao)
            {
                if(origem.coluna != ends.coluna && capture == vulneravelEnPassant)
                {
                    Peca peao = tab.exitPeca(ends);
                    Position posP;
                    if(p.color == Color.Branca)
                    {
                        posP = new Position(3, ends.coluna);
                    }
                    else
                    {
                        posP = new Position(4, ends.coluna);
                    }
                    tab.entryPeca(peao, posP);
                }
            }
        }
        public void JoinGame(Position origem, Position ends)
        {
            Peca capture = executeMoview(origem, ends);
            Peca p = tab.peca(ends);
            if (p is Peao)
            {
                if ((p.color == Color.Branca && ends.linha == 0) || (p.color == Color.Preto && ends.linha == 7))
                {
                    p = tab.exitPeca(ends);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.color);
                    tab.entryPeca(dama, ends);
                    pecas.Add(dama);
                }
            }
            if (InCheck(Player))
            {
                desfazMovie(origem, ends, capture);
                throw new TabuleiroException("Você não pode se colocar em check !");
            }
            if (InCheck(Adversario(Player)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testXequeMate(Adversario(Player)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                switchPlayer();
            }
            if (p is Peao && (ends.linha == origem.linha - 2 || ends.linha == origem.linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
        }
        public void ValidResult(Position pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (Player != tab.peca(pos).color)
            {
                throw new TabuleiroException("Peça de origem escolhida, não te pertence!");
            }
            if (!tab.peca(pos).ExistMoviewPossible())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }
        public void ValidResultEnd(Position origem, Position ends)
        {
            if (!tab.peca(origem).MovePermission(ends))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
        private void switchPlayer()
        {
            if (Player == Color.Branca)
            {
                Player = Color.Preto;
            }
            else
            {
                Player = Color.Branca;
            }
        }



        public HashSet<Peca> PecaInGame(Color color)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedWhite(color));
            return aux;
        }
        public HashSet<Peca> CapturedWhite(Color color)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        private Color Adversario(Color color)
        {
            if(color == Color.Branca)
            {
                return Color.Preto;
            }else
            {
                return Color.Branca;
            }
        }

        private Peca rei (Color color)
        {
            foreach (Peca x in PecaInGame(color))
            {
                if(x is Rei){
                    return x;
                }
            }
            return null;
        }

        public bool InCheck(Color color)
        {
            Peca R = rei(color);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + color + " no tabuleiro !");
            }
            foreach (Peca x in PecaInGame(Adversario(color)))
            {
                bool[,] mat = x.moviesPossible();

                if(mat[R.position.linha, R.position.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool testXequeMate(Color color)
        {
            if (!InCheck(color))
            {
                return false;
            }
            foreach(Peca x in PecaInGame(color))
            {
                bool[,] mat = x.moviesPossible();
                for(int i = 0; i < tab.linha; i++)
                {
                    for(int j = 0; j < tab.coluna; j++)
                    {
                        if(mat[i, j])
                        {
                            Position origem = x.position;
                            Position destino = new Position(i, j);
                            Peca capture = executeMoview(origem, destino);
                            bool testXeque = InCheck(color);
                            desfazMovie(origem, destino, capture);
                            if (!testXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void EntryNewPeca(char coluna, int linha, Peca peca)
        {
            tab.entryPeca(peca, new PositionXadrez(coluna, linha).toPosition());
            pecas.Add(peca);
        }
        private void entryPeca()
        {
            EntryNewPeca('a', 1, new Torre(tab, Color.Branca));
            EntryNewPeca('b', 1, new Cavalo(tab, Color.Branca));
            EntryNewPeca('c', 1, new Bispo(tab, Color.Branca));
            EntryNewPeca('d', 1, new Dama(tab, Color.Branca));
            EntryNewPeca('e', 1, new Rei(tab, Color.Branca, this));
            EntryNewPeca('f', 1, new Bispo(tab, Color.Branca));
            EntryNewPeca('g', 1, new Cavalo(tab, Color.Branca));
            EntryNewPeca('h', 1, new Torre(tab, Color.Branca));
            EntryNewPeca('a', 2, new Peao(tab, Color.Branca, this));
            EntryNewPeca('b', 2, new Peao(tab, Color.Branca, this));
            EntryNewPeca('c', 2, new Peao(tab, Color.Branca, this));
            EntryNewPeca('d', 2, new Peao(tab, Color.Branca, this));
            EntryNewPeca('e', 2, new Peao(tab, Color.Branca, this));
            EntryNewPeca('f', 2, new Peao(tab, Color.Branca, this));
            EntryNewPeca('g', 2, new Peao(tab, Color.Branca, this));
            EntryNewPeca('h', 2, new Peao(tab, Color.Branca, this));

            EntryNewPeca('a', 8, new Torre(tab, Color.Preto));
            EntryNewPeca('b', 8, new Cavalo(tab, Color.Preto));
            EntryNewPeca('c', 8, new Bispo(tab, Color.Preto));
            EntryNewPeca('d', 8, new Dama(tab, Color.Preto));
            EntryNewPeca('e', 8, new Rei(tab, Color.Preto, this));
            EntryNewPeca('f', 8, new Bispo(tab, Color.Preto));
            EntryNewPeca('g', 8, new Cavalo(tab, Color.Preto));
            EntryNewPeca('h', 8, new Torre(tab, Color.Preto));
            EntryNewPeca('a', 7, new Peao(tab, Color.Preto, this));
            EntryNewPeca('b', 7, new Peao(tab, Color.Preto, this));
            EntryNewPeca('c', 7, new Peao(tab, Color.Preto, this));
            EntryNewPeca('d', 7, new Peao(tab, Color.Preto, this));
            EntryNewPeca('e', 7, new Peao(tab, Color.Preto, this));
            EntryNewPeca('f', 7, new Peao(tab, Color.Preto, this));
            EntryNewPeca('g', 7, new Peao(tab, Color.Preto, this));
            EntryNewPeca('h', 7, new Peao(tab, Color.Preto, this));
        }
    }
}
