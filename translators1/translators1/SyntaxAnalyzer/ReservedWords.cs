using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.SyntaxAnalyzer
{
    public enum TokenType
    {
        None,
        Plus,
        Minus,
        Multiply,
        Divide,
        Number,
        LeftBracket,
        RightBracket,
        Identifier,
        Zap
    }
}
