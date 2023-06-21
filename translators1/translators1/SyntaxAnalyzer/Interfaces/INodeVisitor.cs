using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translators1.SyntaxAnalyzer.Models;

namespace translators1.SyntaxAnalyzer.Interfaces
{
    public interface INodeVisitor
    {
        //object
        object VisitNum(Token num);
        object VisitUnaryOp(Token op, INode node);
        object VisitBinOp(Token op, INode left, INode right);
    }
}
