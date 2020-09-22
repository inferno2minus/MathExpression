namespace I2M.MathExpression.Extensions
{
    public static class CharExtensions
    {
        public static bool IsDecimalPoint(this char value)
        {
            return value == '.';
        }
    }
}
