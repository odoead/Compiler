using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.LexAnalyzer
{
    public class AnalyzerReservedTerms
    {
        //1-reservedWords
        //2-literals
        //3-word variable
        //4-numeric literal
        //5-operators
        //6-separators
        //7-invisible separators
        public static List<string> Words = new List<string> { "if", "else", "while", "do", "for", "int", "char", "bool","string",
        "double", "decimal", "true", "false","Select","From","Where","OrdBy","Top","Distinct","Between","GroupBy" };//1
        public static List<string> separators = new List<string> { "(", ")"};//
        public static List<string> operators = new List<string> { "+", "-", "*", "/", "=", "!=", ">=", "<=",
            "<", ">", "AutoInc", "AutoDec", "!", "||", "&&" };
        public static List<string> literals = new List<string> { "true", "false" };
        public static List<string> invisibleS = new List<string> { ",", ";", "\n", "\r", " ", "\t","\"",  "."   };
        //var cew = 12;
    }
    public enum objects
    {
        reservedWords = 1,
        literals = 2,
        word_variables = 3,
        numericLiterals = 4,
        operators = 5,
        separators = 6,
        invisibleS=7,
        notExists = -1

    }
}
