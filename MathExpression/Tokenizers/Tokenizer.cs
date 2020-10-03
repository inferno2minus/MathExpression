using I2M.MathExpression.Extensions;
using I2M.MathExpression.infrastructure;
using System.Globalization;
using System.IO;
using System.Text;

namespace I2M.MathExpression.Tokenizers
{
    public class Tokenizer : BaseTokenizer
    {
        private readonly TextReader _reader;

        public Tokenizer(TextReader reader)
        {
            _reader = reader;
        }

        protected override void NextTokenCore()
        {
            var symbol = CurrentSymbol;

            if (CurrentSymbol.IsDigit() || CurrentSymbol.IsDecimalPoint())
            {
                var value = double.Parse(GetStringNumber(), CultureInfo.InvariantCulture);

                CurrentToken = new Token(symbol, value);

                return;
            }

            NextSymbolCore();

            CurrentToken = new Token(symbol);
        }

        protected override void NextSymbolCore()
        {
            var symbol = _reader.Read();

            CurrentSymbol = symbol < 0 ? Symbols.Eof : (char)symbol;
        }

        private string GetStringNumber()
        {
            var stringBuilder = new StringBuilder();

            var haveDecimalPoint = false;

            while (CurrentSymbol.IsDigit() || CurrentSymbol.IsDecimalPoint() && !haveDecimalPoint)
            {
                stringBuilder.Append(CurrentSymbol);

                haveDecimalPoint = CurrentSymbol.IsDecimalPoint();

                NextSymbolCore();
            }

            return stringBuilder.ToString();
        }
    }
}
