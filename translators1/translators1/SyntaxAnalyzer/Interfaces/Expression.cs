using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.SyntaxAnalyzer.Interfaces
{
     abstract public class Expression : INode
    {
        abstract public object Accept(INodeVisitor visitor);
        abstract public override string ToString();

    }
}
