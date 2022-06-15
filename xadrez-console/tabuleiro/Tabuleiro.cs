namespace tabuleiro
{
    class Tabuleiro
    {
        public int linha { get; set; }
        public int coluna { get; set; }
        private Peca[,] pecas;
        public Tabuleiro(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
            pecas = new Peca[linha, coluna];
        }
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
        public Peca peca(Position pos)
        {
            return pecas[pos.linha, pos.coluna];
        }
        public bool ExistPeca(Position pos)
        {
            ValidPositon(pos);
            return peca(pos) != null; 
        }
        public void entryPeca(Peca p, Position pos)
        {
            if (ExistPeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            pecas[pos.linha, pos.coluna] = p;
            p.position = pos;
        }
        public Peca exitPeca(Position pos)
        {
            if(peca(pos) == null)
            {
                return null;
            }
            Peca aux = peca(pos);
            aux.position = null;
            pecas[pos.linha, pos.coluna] = null;
            return aux;
        }
        public bool PositionVal(Position pos)
        {
            if (pos.linha < 0 || pos.linha >= linha || pos.coluna < 0 || pos.coluna >= coluna)
            {
                return false;
            }
            return true;
        }
        public void ValidPositon(Position pos)
        {
            if (!PositionVal(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
