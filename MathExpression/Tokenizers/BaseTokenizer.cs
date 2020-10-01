using I2M.MathExpression.Interfaces;

namespace I2M.MathExpression.Tokenizers
{
    public abstract class BaseTokenizer : ITokenizer
    {
        public Token CurrentToken { get; protected set; }
        protected char CurrentChar { get; set; }

        public void NextToken()
        {
            while (char.IsWhiteSpace(CurrentChar))
            {
                NextCharCore();
            }

            NextTokenCore();
        }

        public void Init()
        {
            NextCharCore();
            NextTokenCore();
        }

        protected abstract void NextTokenCore();
        protected abstract void NextCharCore();
    }
}
