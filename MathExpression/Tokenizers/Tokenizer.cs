using I2M.MathExpression.Extensions;
using I2M.MathExpression.Infrastructure;
using System;
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
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        protected override void NextTokenCore()
        {
            if (CurrentSymbol.IsDigitOrDecimalPoint())
            {
                var numberString = GetNumberString();

                var number = double.Parse(numberString, CultureInfo.InvariantCulture);

                CurrentToken = new Token(numberString.ToCharArray(), number);

                return;
            }

            CurrentToken = new Token(new[] { CurrentSymbol });

            NextSymbolCore();
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