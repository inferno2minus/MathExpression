namespace I2M.MathExpression.Interfaces
{
    public interface IMathExpressionEngine
    {
        IExpression ParseExpression(ITokenizer tokenizer);
    }
}
