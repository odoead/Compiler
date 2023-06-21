using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translators1.SyntaxAnalyzer.Interfaces;

namespace translators1.SyntaxAnalyzer.Models
{
    public class BinOp : Expression
    {
        internal Token Op { get; private set; }
        internal Expression Left { get; private set; }
        internal Expression Right { get; private set; }

        public BinOp(Token op, Expression left, Expression right)
        {
            Op = op;
            Left = left;
            Right = right;
        }
        public override string ToString()
        {
            return Left.ToString() + Op.Value.ToString() + Right.ToString();
        }
        override public object Accept(INodeVisitor visitor)
        {
            return visitor.VisitBinOp(Op, Left, Right);
        }
    }
}
