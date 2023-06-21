using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translators1.SyntaxAnalyzer.Interfaces;

namespace translators1.SyntaxAnalyzer.Models
{
    public class UnaryOp : Expression
    {

        internal Token Op { get; private set; }
        internal Expression Node { get; private set; }


        public override string ToString()
        {
            return Op.Value.ToString() + Node.ToString();
        }
        public UnaryOp(Token op, Expression node)
        {
            Op = op;
            Node = node;
        }

        override public object Accept(INodeVisitor visitor)
        {
            return visitor.VisitUnaryOp(Op, Node);
        }
    }
}
