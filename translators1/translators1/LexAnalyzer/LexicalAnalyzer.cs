using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.LexAnalyzer
{
    using System;
    using System.Collections.Generic;

    /* public enum objects
     {
         reservedWords=1,
         literals=2,
         word_variables=3,
         numericLiterals=4,
         operators=5,
         separators=6,
         notExists=-1

     }*/

    public class LexicalAnalyzer
    {
        /*//1-reservedWords
        //2-literals
        //3-word variable
        //4-numeric literal
        //5-operators
        //6-separators
        static List<string> reservedWords = new List<string> { "if", "else", "while", "do", "for", "int", "char", "bool","string",
        "double", "decimal", "true", "false","Select","From","Where","OrdBy","Top","Distinct","Between","GroupBy" };//1
        static List<string> separators = new List<string> { "(", ")", "{", "}", ",", ";", "\n", "\r", " ", "\t", ".", "\"" };//
        static List<string> operators = new List<string> { "+", "-", "*", "/", "=", "!=", ">=", "<=", 
            "<", ">", "AutoInc", "AutoDec", "!", "||", "&&" };
        static List<string> literals = new List<string> { "true", "false" };*/
        string code;
        public LexicalAnalyzer(string str)
        {
            code = str;
        }
        public List<Tuple<int, int>> Analyze()
        {
            List<Tuple<int, int>> lexemes = new List<Tuple<int, int>>();
            int currentIndex = 0;

            while (currentIndex < code.Length)
            {
                char currentChar = code[currentIndex];

                if (currentChar == '#')
                {
                    // коментарий
                    currentIndex = code.IndexOf('\n', currentIndex);

                    if (currentIndex == -1)
                    {
                        currentIndex = code.Length;
                    }

                    continue;
                }

                if (char.IsLetter(currentChar))
                {
                    // резервированое слово или идентификатор

                    string lexeme = GetLexeme(code, currentIndex, c => !char.IsLetterOrDigit(c));

                    if (AnalyzerReservedTerms.Words.Contains(lexeme))
                    {
                        lexemes.Add(new Tuple<int, int>((int)objects.reservedWords, AnalyzerReservedTerms.Words.IndexOf(lexeme)));
                    }
                    else if (AnalyzerReservedTerms.literals.Contains(lexeme))
                    {
                        lexemes.Add(new Tuple<int, int>((int)objects.literals, AnalyzerReservedTerms.literals.IndexOf(lexeme)));
                    }


                    else
                    {
                        lexemes.Add(new Tuple<int, int>((int)objects.word_variables, -1));
                    }

                    currentIndex += lexeme.Length;
                    continue;
                }

                if (char.IsDigit(currentChar))
                {
                    // число
                    string lexeme = GetLexeme(code, currentIndex, c => !char.IsDigit(c));

                    lexemes.Add(new Tuple<int, int>((int)objects.numericLiterals, -1));

                    currentIndex += lexeme.Length;

                    continue;
                }

                if (AnalyzerReservedTerms.separators.Contains(currentChar.ToString()))
                {
                    // разделитель
                    string lexeme = GetLexeme(code, currentIndex, c => !AnalyzerReservedTerms.separators.Contains(c.ToString()));

                    lexemes.Add(new Tuple<int, int>((int)objects.separators, AnalyzerReservedTerms.separators.IndexOf(lexeme[0].ToString())));
                    currentIndex++;

                    continue;

                }
                 if(AnalyzerReservedTerms.invisibleS.Contains(currentChar.ToString()))
                {
                    //невидимый разделитель
                    currentIndex++;
                    continue;
                }

                if (AnalyzerReservedTerms.operators.Contains(currentChar.ToString()))
                {
                    // оператор
                    string lexeme = GetLexeme(code, currentIndex, c => !AnalyzerReservedTerms.operators.Contains(c.ToString()));

                    lexemes.Add(new Tuple<int, int>((int)objects.operators, AnalyzerReservedTerms.operators.IndexOf(lexeme[0].ToString())));

                    currentIndex += lexeme.Length;

                    continue;
                }

                // неизвестная лексема
                string unknownLexeme = GetLexeme(code, currentIndex, c => AnalyzerReservedTerms.separators.Contains(c.ToString()) || AnalyzerReservedTerms.operators.Contains(c.ToString()));
                lexemes.Add(new Tuple<int, int>((int)objects.notExists, -1));
                currentIndex += unknownLexeme.Length;
            }

            Console.WriteLine("Lexemes:");

            foreach (Tuple<int, int> lexeme in lexemes)
            {
                Console.WriteLine($"Type: {lexeme.Item1}, Value: {lexeme.Item2}" + "\t" + lexeme);
            }

            return lexemes;
        }

        static string GetLexeme(string code, int startIndex, Func<char, bool> isStopCharacter)
        {

            int length = 1;
            while (startIndex + length <= code.Length && !isStopCharacter(code[startIndex + length - 1]))
            {
                length++;
            }
            return code.Substring(startIndex, length - 1);
        }


    }

}
