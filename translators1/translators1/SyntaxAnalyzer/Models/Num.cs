using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translators1.SyntaxAnalyzer.Interfaces;

namespace translators1.SyntaxAnalyzer.Models
{
    public class Num : Expression
    {

        internal Token Token { get; private set; }

        public Num(Token token)
        {
            Token = token;
        }
        public override string ToString()
        {
            return Token.Value.ToString();
        }
        override public object Accept(INodeVisitor visitor)
        {
            return visitor.VisitNum(Token);
        }
    }
}
