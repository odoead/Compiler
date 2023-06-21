using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.SyntaxAnalyzer.Interfaces
{
    public interface INode
    {
        public object Accept(INodeVisitor visitor);
    }
}
