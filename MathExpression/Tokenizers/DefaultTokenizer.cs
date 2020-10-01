using I2M.MathExpression.Extensions;
using System.Globalization;
using System.IO;
using System.Text;

namespace I2M.MathExpression.Tokenizers
{
    public class DefaultTokenizer : BaseTokenizer
    {
        private const char Eof = '\0';
        private const char Add = '+';
        private const char Subtract = '-';
        private const char Multiply = '*';
        private const char Divide = '/';
        private const char LeftBracket = '(';
        private const char RightBracket = ')';

        private readonly TextReader _reader;

        public DefaultTokenizer(TextReader reader)
        {
            _reader = reader;
        }

        protected override void NextTokenCore()
        {
            switch (CurrentChar)
            {
                case Eof:
                    CurrentToken = new Token(TokenType.Eof);
                    return;
                case Add:
                    NextCharCore();
                    CurrentToken = new Token(TokenType.Add);
                    return;
                case Subtract:
                    NextCharCore();
                    CurrentToken = new Token(TokenType.Subtract);
                    return;
                case Multiply:
                    NextCharCore();
                    CurrentToken = new Token(TokenType.Multiply);
                    return;
                case Divide:
                    NextCharCore();
                    CurrentToken = new Token(TokenType.Divide);
                    return;
                case LeftBracket:
                    NextCharCore();
                    CurrentToken = new Token(TokenType.LeftBracket);
                    return;
                case RightBracket:
                    NextCharCore();
                    CurrentToken = new Token(TokenType.RightBracket);
                    return;
            }

            if (char.IsDigit(CurrentChar) || CurrentChar.IsDecimalPoint())
            {
                CurrentToken = new Token(TokenType.Number, double.Parse(GetStringNumber(), CultureInfo.InvariantCulture));
                return;
            }

            CurrentToken = new Token(TokenType.Unknown);
        }

        protected override void NextCharCore()
        {
            var currentChar = _reader.Read();

            CurrentChar = currentChar < 0 ? Eof : (char)currentChar;
        }

        private string GetStringNumber()
        {
            var stringBuilder = new StringBuilder();

            var haveDecimalPoint = false;

            while (char.IsDigit(CurrentChar) || !haveDecimalPoint && CurrentChar.IsDecimalPoint())
            {
                stringBuilder.Append(CurrentChar);
                haveDecimalPoint = CurrentChar.IsDecimalPoint();
                NextCharCore();
            }

            return stringBuilder.ToString();
        }
    }
}
