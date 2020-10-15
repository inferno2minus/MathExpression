namespace I2M.MathExpression.Extensions
{
    public static class CharExtensions
    {
        public static bool IsDigitOrDecimalPoint(this char value)
        {
            return char.IsDigit(value) || value == '.';
        }

        public static bool IsWhiteSpace(this char value)
        {
            return char.IsWhiteSpace(value);
        }
    }
}
