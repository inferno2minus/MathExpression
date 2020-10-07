namespace I2M.MathExpression.Interfaces
{
    public interface IMathExpressionParser
    {
        IExpression Parse(ITokenizer tokenizer);
    }
}
