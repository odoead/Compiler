using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.LexAnalyzer
{
    /// <summary>
    /// определяет вывод результата 
    /// </summary>
    public class lexicalController
    {
        public lexicalController(List<Tuple<int, int>> tuples)
        {
            lexemes = tuples;
        }
        public List<Tuple<int, int>> lexemes;
        public string toStringNoFormating()// return with no line accounting
        {
            StringBuilder str = new StringBuilder("");
            int currentIndex = 0;
            while (currentIndex < lexemes.Count())
            {
                str.Append("[" + lexemes[currentIndex].Item1 + "|" + lexemes[currentIndex].Item2 + "]"+ Environment.NewLine);
                currentIndex++;
            }


            return str.ToString();

        }
        public string toStringWithFormating()// return with line accounting
        {
            StringBuilder str = new StringBuilder("");
            int currentIndex = 0;
            while (currentIndex < lexemes.Count())
            {

                if (lexemes[currentIndex].Item1 == 6 && lexemes[currentIndex].Item2 == 6 && currentIndex != 0)//if lexeme is not in first line and   lex=\n 
                {
                    str.Append("\r\n" + "[" + lexemes[currentIndex].Item1 + "|" + lexemes[currentIndex].Item2 + "]" + Environment.NewLine);

                }
                else if (lexemes[currentIndex].Item1 == 6 && lexemes[currentIndex].Item2 == 6 && currentIndex == 0)//if lexeme is in first line and is lex=\n  
                {
                    str.Append("[" + lexemes[currentIndex].Item1 + "|" + lexemes[currentIndex].Item2 + "]" + Environment.NewLine);

                }
                else
                {
                    str.Append("[" + lexemes[currentIndex].Item1 + "|" + lexemes[currentIndex].Item2 + "]" + Environment.NewLine);
                }
                currentIndex++;
            }
            return str.ToString();


        }

        /*public string toString*/

    }

}
