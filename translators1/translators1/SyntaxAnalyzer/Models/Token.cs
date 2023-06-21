using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.SyntaxAnalyzer.Models
{
    public  class Token
    {
        public static int? LLI;
        public TokenType Type { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType type, string value)
        {
            LLI++;
            Type = type;
            Value = value;
        }

        internal static Token None()
        {
            if (LLI == null)
            {
                LLI = 0;
            }

            return new Token(TokenType.None, "");
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
