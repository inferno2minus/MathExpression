namespace I2M.MathExpression.Extensions
{
    public static class CharExtensions
    {
        public static bool IsDecimalPoint(this char value)
        {
            return value == '.';
        }

        public static bool IsDigit(this char value)
        {
            return char.IsDigit(value);
        }

        public static bool IsWhiteSpace(this char value)
        {
            return char.IsWhiteSpace(value);
        }
    }
}
