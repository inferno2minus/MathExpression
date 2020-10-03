using I2M.MathExpression.Extensions;
using I2M.MathExpression.Interfaces;

namespace I2M.MathExpression.Tokenizers
{
    public abstract class BaseTokenizer : ITokenizer
    {
        public Token CurrentToken { get; protected set; }
        protected char CurrentSymbol { get; set; }

        public void NextToken()
        {
            while (CurrentSymbol.IsWhiteSpace())
            {
                NextSymbolCore();
            }

            NextTokenCore();
        }

        public void Init()
        {
            NextSymbolCore();
            NextTokenCore();
        }

        protected abstract void NextTokenCore();
        protected abstract void NextSymbolCore();
    }
}
