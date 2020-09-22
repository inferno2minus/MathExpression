﻿using I2M.MathExpression.Interfaces;
using I2M.MathExpression.Tokenizers;
using System;
using System.IO;

namespace I2M.MathExpression.Helpers
{
    public static class MathExpressionHelper
    {
        public static IExpression Parse(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            using var stringReader = new StringReader(value);
            var tokenizer = new DefaultTokenizer(stringReader);
            var engine = new MathExpressionEngine();

            return engine.ParseExpression(tokenizer);
        }
    }
}
