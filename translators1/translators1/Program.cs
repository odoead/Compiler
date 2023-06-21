using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using translators1.LexAnalyzer;
using translators1.LLGen;
using translators1.SyntaxAnalyzer;
using translators1.SyntaxAnalyzer.Interfaces;

class Trans
{
    /*static List<string> reservedWords = new List<string> { "if", "else", "while", "do", "for", "int", "char", "bool","string",
        "double", "decimal", "true", "false","Select","From","Where","OrdBy","Top","Distinct","Between","GroupBy" };//1
    static List<string> separators = new List<string> { "(", ")", "{", "}", ",", ";", "\n", "\r", " ", "\t", ".","\"" };//
    static List<string> operators = new List<string> { "+", "-", "*", "/", "=","!=","<>",">=","<=", "<", ">" ,"AutoInc","AutoDec","!","||","&&"};
    static List<string> literals = new List<string> { "true", "false" };

    static void Main(string[] args)
    {
        Console.WriteLine("Enter code:");

        string code = "while ( +-=currentIndex ){\nint ttrt = (\"dbff.DB);\n}"
            *//*"while ( }int\n  (};."currentIndex <str.Length){\r\nint \r\n}"*//*;

        List<Tuple<int, int>> lexemes = new List<Tuple<int, int>>();
        int currentIndex = 0;

        while (currentIndex < code.Length)
        {
            char currentChar = code[currentIndex];

            if (currentChar == '#')
            {
                // comment
                currentIndex = code.IndexOf('\n', currentIndex);

                if (currentIndex == -1)
                {
                    currentIndex = code.Length;
                }

                continue;
            }

            if (Char.IsLetter(currentChar)*//*|| currentChar=='+'*//*)
            {
                // reserved word or identifier

                string lexeme = GetLexeme(code, currentIndex, c => !Char.IsLetterOrDigit(c));

                if (reservedWords.Contains(lexeme))
                {
                    lexemes.Add(new Tuple<int, int>(1, reservedWords.IndexOf(lexeme)));
                }
                else if (literals.Contains(lexeme))
                {
                    lexemes.Add(new Tuple<int, int>(2, literals.IndexOf(lexeme)));
                }
              *//*  else if (operators.Contains(lexeme))*//**//*
                {
                    lexemes.Add(new Tuple<int, int>(3, operators.IndexOf(lexeme)));
                }*//*

                else
                {
                    lexemes.Add(new Tuple<int, int>(3, -1));
                }
                Console.WriteLine("A " + lexeme + "\t");
                Console.WriteLine("B " + lexeme.Length + "\t");
                currentIndex += lexeme.Length;
                continue;
            }

            if (Char.IsDigit(currentChar))
            {
                // numeric literal
                string lexeme = GetLexeme(code, currentIndex, c => !Char.IsDigit(c));

                lexemes.Add(new Tuple<int, int>(4, -1));

                currentIndex += lexeme.Length;
                Console.WriteLine("C|" + lexeme + "\t");
                Console.WriteLine("D|" + lexeme.Length + "\t");
                continue;
            }

            if (separators.Contains(currentChar.ToString()))
            {
                // separator
                string lexeme = GetLexeme(code, currentIndex, c => !separators.Contains(c.ToString()));

                lexemes.Add(new Tuple<int, int>(6, separators.IndexOf(lexeme[0].ToString())));
                currentIndex++;
               
                continue;

            }

            if (operators.Contains(currentChar.ToString()))
            {
                // operator
                string lexeme = GetLexeme(code, currentIndex, c => !operators.Contains(c.ToString()));

                lexemes.Add(new Tuple<int, int>(5, operators.IndexOf(lexeme[0].ToString())));

                currentIndex += lexeme.Length;
              
                continue;
            }

            // unknown lexeme
            string unknownLexeme = GetLexeme(code, currentIndex, c => separators.Contains(c.ToString()) || operators.Contains(c.ToString()));

            Console.WriteLine($"Unknown lexeme: {unknownLexeme}");
            currentIndex += unknownLexeme.Length;
        }

        Console.WriteLine("Lexemes:");

        foreach (Tuple<int, int> lexeme in lexemes)
        {
            Console.WriteLine($"Type: {lexeme.Item1}, Value: {lexeme.Item2}" +"\t"+ lexeme);
        }

       
    }

    static string GetLexeme(string code, int startIndex, Func<char, bool> isStopCharacter)
    {
        
        int length = 1;
        Console.WriteLine("\nGetlexeme lenght before: " + length+" startindx: "+startIndex);
        while (startIndex + length <= code.Length && !isStopCharacter(code[startIndex + length - 1]))
        {
            length++;
        }
        Console.WriteLine("Getlexeme lenght after: "+length);
        return code.Substring(startIndex, length - 1);
    }*/

    static void Main()
    {
        //Console.Write("Enter expression: ");

        string text ;
        /*text = Console.ReadLine();*/
         //text = "11-5-10*2/9-15";
        text = "5/10*2";
        //text = "Max(5,10)";
       // Console.WriteLine("Source code: ");
        Console.WriteLine(text);
        Console.WriteLine("---------------------------------------------------------");
        LexicalAnalyzer analyzer = new LexicalAnalyzer(text);
        lexicalController lexicalController = new lexicalController(analyzer.Analyze());

        Console.WriteLine("_________________________________________________________________________________________________");

        Console.WriteLine( lexicalController.toStringWithFormating());
        Console.WriteLine("_________________________________________________________________________________________________");
        try
        {
            Parser interpreter = new Parser(text);
            Expression node = interpreter.Parse();
            Console.WriteLine("1");

            node.Accept(new ValueCalculator());
            LLConverter converter = new LLConverter(@"C:\Users\Kirill\Desktop\result.txt");
            Console.Write(converter.Convert());

        }
        catch (InvalidSyntaxException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        Console.ReadLine();
    }


}
