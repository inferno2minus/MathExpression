using I2M.MathExpression.Tokenizers;

namespace I2M.MathExpression.Interfaces
{
    public interface ITokenizer
    {
        Token CurrentToken { get; }
        void NextToken();
        void Init();
    }
}
