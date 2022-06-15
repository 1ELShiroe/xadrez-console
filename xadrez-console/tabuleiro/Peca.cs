namespace tabuleiro
{
    abstract class Peca
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movies { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Color color)
        {
            this.position = null;
            this.tab = tab;
            this.color = color;
            this.movies = 0;
        }
        public void incrementeMoview()
        {
            movies++;
        }
        public void decrementeMoview()
        {
            movies--;
        }
        public bool ExistMoviewPossible()
        {
            bool[,] mat = moviesPossible();
            for (int i = 0; i < tab.linha; i++)
            {
                for(int x = 0; x < tab.coluna; x++)
                {
                    if (mat[i, x])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool MovePermission(Position pos)
        {
            return moviesPossible()[pos.linha, pos.coluna];
        }
        public abstract bool[,] moviesPossible();
    }
}
