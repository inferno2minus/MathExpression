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

            if (CurrentSymbol.IsDigitOrDecimalPoint())
            {
                var value = double.Parse(GetNumberString(), CultureInfo.InvariantCulture);

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

        private string GetNumberString()
        {
            var stringBuilder = new StringBuilder();

            while (CurrentSymbol.IsDigitOrDecimalPoint())
            {
                stringBuilder.Append(CurrentSymbol);

                NextSymbolCore();
            }

            return stringBuilder.ToString();
        }
    }
}