using I2M.MathExpression.Interfaces;

namespace I2M.MathExpression.Tokenizers
{
    public abstract class BaseTokenizer : ITokenizer
    {
        public Token CurrentToken { get; } = new Token();

        protected char CurrentChar { get; set; }

        public void NextToken()
        {
            while (char.IsWhiteSpace(CurrentChar))
            {
                NextChar();
            }

            NextTokenCore();
        }

        public void Init()
        {
            NextChar();
            NextToken();
        }

        private void NextChar()
        {
            NextCharCore();
        }

        protected abstract void NextTokenCore();

        protected abstract void NextCharCore();
    }
}
