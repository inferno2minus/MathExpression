using I2M.MathExpression.Extensions;
using System.Globalization;
using System.IO;
using System.Text;

namespace I2M.MathExpression.Tokenizers
{
    public class DefaultTokenizer : BaseTokenizer
    {
        private readonly TextReader _reader;

        public DefaultTokenizer(TextReader reader)
        {
            _reader = reader;
        }

        protected override void NextTokenCore()
        {
            switch (CurrentChar)
            {
                case '\0':
                    CurrentToken.Type = TokenType.Eof;
                    return;
                case '+':
                    NextCharCore();
                    CurrentToken.Type = TokenType.Add;
                    return;
                case '-':
                    NextCharCore();
                    CurrentToken.Type = TokenType.Subtract;
                    return;
                case '*':
                    NextCharCore();
                    CurrentToken.Type = TokenType.Multiply;
                    return;
                case '/':
                    NextCharCore();
                    CurrentToken.Type = TokenType.Divide;
                    return;
                case '(':
                    NextCharCore();
                    CurrentToken.Type = TokenType.LeftBracket;
                    return;
                case ')':
                    NextCharCore();
                    CurrentToken.Type = TokenType.RightBracket;
                    return;
            }

            if (char.IsDigit(CurrentChar) || CurrentChar.IsDecimalPoint())
            {
                var stringBuilder = new StringBuilder();

                var haveDecimalPoint = false;

                while (char.IsDigit(CurrentChar) || !haveDecimalPoint && CurrentChar.IsDecimalPoint())
                {
                    stringBuilder.Append(CurrentChar);
                    haveDecimalPoint = CurrentChar.IsDecimalPoint();
                    NextCharCore();
                }

                CurrentToken.Value = double.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture);
                CurrentToken.Type = TokenType.Number;
            }
        }

        protected override void NextCharCore()
        {
            var currentChar = _reader.Read();

            CurrentChar = currentChar < 0 ? '\0' : (char)currentChar;
        }
    }
}
