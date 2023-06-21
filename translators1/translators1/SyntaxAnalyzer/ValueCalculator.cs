using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using translators1.SyntaxAnalyzer.Interfaces;
using translators1.SyntaxAnalyzer.Models;

namespace translators1.SyntaxAnalyzer
{
    public class ValueCalculator : INodeVisitor
    {
        public ValueCalculator()
        {
            LLI = 1;
            //ShowResult();
        }

        static string filepath = @"C:\Users\Kirill\Desktop\result.txt";

        private string result;
        public static int LLI;
        public static StringBuilder ResultFunction = new StringBuilder();
        private Stack<int> LLIList = new Stack<int>();//масив итераторов где присутствует expression
        private Stack<string> Tokenlist = new Stack<string>();//масив знаков операций
        private Stack<double> NumberRList = new Stack<double>(); //масив ЧИСЕЛ 
        private Stack<double> NumberLList = new Stack<double>();
        private Stack<bool> IsNumberL = new Stack<bool>();// масив для хранения порядка число- для финального построения результата 
        private Stack<bool> IsNumberR = new Stack<bool>();
        public void AddToDictionary(Token op, INode left, INode right, int lli)
        {


            Tokenlist.Push(op.Value);
            if (left is Num)// если левое выражение - число то записываем в стеки  значение и факт существования числа
            {
                IsNumberL.Push(true);
                NumberLList.Push(Convert.ToInt32(left.ToString()));
            }
            else if (left is Interfaces.Expression)
            {
                IsNumberL.Push(false);
            }

            if (right is Num)
            {
                IsNumberR.Push(true);
                NumberRList.Push(Convert.ToInt32(right.ToString()));
            }
            else if (right is Interfaces.Expression)
            {
                IsNumberR.Push(false);
            }
            if (!(right is Num || left is Num))//если в выражениях есть хоть одно не число то записываем  
            {
                LLIList.Push(lli);
            }
            LLI++;
            /*LLIList.Add(lli);
            Tokenlist.Push(op.ToString());
            if(left is Num)
            {
               NodeLList.Add(left.ToString());
            }else
                {
                NodeLList.Add("%" + lli.ToString());
            }
            if (right is Num)
            {
                NodeRList.Add(right.ToString());
            }
            else
            {
                NodeRList.Add("%"+lli.ToString());
            }*/

        }
        public void ShowResult()
        {

            string temp;

            if (Tokenlist.Count > 0)
            {
                temp = Tokenlist.Pop().ToString() + " ";
                byte[] buffer = Encoding.Default.GetBytes(temp);
                //запись в файл
                string content = File.ReadAllText(filepath);
                content = temp + content;
                File.WriteAllText(filepath, content);

                result += temp;
                //Console.Write(temp);
            } 
            if (IsNumberR.Count > 0)
                if (IsNumberR.Pop() == true)
                {
                    temp = NumberRList.Pop().ToString() + " ";
                    byte[] buffer = Encoding.Default.GetBytes(temp);
                    //запись в файл
                    string content = File.ReadAllText(filepath);
                    content = temp + content /*+ temp*/;
                    File.WriteAllText(filepath, content);

                    result += temp;
                    //Console.Write(temp);
                }
            if (IsNumberL.Count > 0)
                if (IsNumberL.Pop() == true)
                {
                    temp = NumberLList.Pop().ToString() + " ";
                    byte[] buffer = Encoding.Default.GetBytes(temp);
                    //запись в файл
                    string content = File.ReadAllText(filepath);
                    content = temp + content;
                    File.WriteAllText(filepath, content);
                    //File.AppendAllText(filepath, temp );

                    result += temp;
                    // Console.Write(temp);
                }
           

        }
        /// <summary>
        /// геттер для бинарного сивола 
        /// </summary>
        /// <param name="op"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Object VisitBinOp(Token op, INode left, INode right)
        {
            /*if (LLI == 0)
            {
                //LLI = 0;
                ResultFunction.Append("declare dso_local double " + op.ToString() + LLI + "\n");
            }
            else
            {
                LLI++;
                ResultFunction.Append("%" + LLI + " = fadd double");

            }
            //Console.WriteLine("Left: {0}|Right: {1}|Op{2}",left.ToString(),right.ToString(),op.Value.ToString()+"\n");
            //Console.WriteLine("%" + LLI + " = " + op.Value.ToString()+" double " + left.ToString() + " " + right.ToString());
            //LLI++;
            //right.*/
            switch (op.Type)
            {
                case TokenType.Plus:
                    { //if() 
                        AddToDictionary(op, left, right, LLI);
                        // Console.WriteLine("%" + LLI + " = " + "fadd double " + left.ToString() + ", " + right.ToString());
                        // LLI++;
                        ShowResult();
                        return (string)left.Accept(this) + "+" + (string)right.Accept(this);
                        //return LLI-1;

                    }

                case TokenType.Minus:
                    {
                        AddToDictionary(op, left, right, LLI);
                        // Console.WriteLine("%" + LLI + " = " + "fsub double " + left.ToString() + ", " + right.ToString());
                        // LLI++;
                        ShowResult();
                        return (string)left.Accept(this) + "-" + (string)right.Accept(this);

                    }

                case TokenType.Multiply:
                    {
                        AddToDictionary(op, left, right, LLI);
                        //Console.WriteLine("%" + LLI + " = " + "fmul double " + left.ToString() + ", " + right.ToString());
                        // LLI++;
                        //return LLI - 1;
                        ShowResult();
                        return (string)left.Accept(this) + "*" + (string)right.Accept(this);
                        break;

                    }

                case TokenType.Divide:
                    {
                        AddToDictionary(op, left, right, LLI);
                        // Console.WriteLine("%" + LLI + " = " + "fdiv double " + left.ToString() + ", " + right.ToString());
                        // LLI++;
                        ShowResult();
                        return (string)left.Accept(this) + "/" + (string)right.Accept(this);
                        break;
                    }

                case TokenType.Identifier:
                    if (op.Value == "Avg")
                    {
                        AddToDictionary(op, left, right, LLI);
                        ShowResult();
                        return (string)left.Accept(this) + "+" + (string)right.Accept(this);
                    }
                    else if (op.Value == "Max")
                    {
                        AddToDictionary(op, left, right, LLI);
                        ShowResult();
                        return (string)left.Accept(this) + "+" + (string)right.Accept(this);
                    }
                    else if (op.Value == "Min")
                    {
                        AddToDictionary(op, left, right, LLI);
                        ShowResult();
                        return (string)left.Accept(this) + "+" + (string)right.Accept(this);
                    }
                   
                    else
                    {
                        AddToDictionary(op, left, right, LLI);
                        ShowResult();
                        throw new Exception(string.Format("Token of type {0} cannot be evaluated.", op.Type.ToString()));
                    }


                default:
                    throw new Exception(string.Format("Token of type {0} cannot be evaluated.", op.Type.ToString()));
            }
        }


        /// <summary>
        ///  геттер для токена числа
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public object VisitNum(Token num)
        {
            //Console.WriteLine("Nmu: "+num.ToString()); //ЧИСЛО
            decimal result;
            decimal.TryParse(num.Value, out result);
            return result.ToString();
            /*return result decimal.TryParse(  *//*(num.Value)*//*;*/
        }

        /// <summary>
        /// геттер для токена унарного символа 
        /// </summary>
        /// <param name="op"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public object VisitUnaryOp(Token op, INode node)
        {
            //Console.WriteLine("Left: {0}|op: {1}", node.ToString(), op.Value.ToString());
            switch (op.Type)
            {

                case TokenType.Plus:
                    {
                        ShowResult();
                        return node.Accept(this).ToString();
                    }
                case TokenType.Minus:
                    {
                        ShowResult();
                        return "-" + node.Accept(this).ToString();
                    }
                /*case TokenType.Identifier:
                    if (op.Value == "Avg")
                    {
                        AddToDictionary(op, node,LLI);
                        ShowResult();
                        return (string)left.Accept(this) + "+" + (string)right.Accept(this);
                    }
                    else if (op.Value == "Max")
                    {
                        AddToDictionary(op, left, right, LLI);
                        ShowResult();
                        return (string)left.Accept(this) + "+" + (string)right.Accept(this);
                    }
                    else if (op.Value == "Min")
                    {
                        AddToDictionary(op, left, right, LLI);
                        ShowResult();
                        return (string)left.Accept(this) + "+" + (string)right.Accept(this);
                    }

                    else
                    {
                        AddToDictionary(op, left, right, LLI);
                        ShowResult();
                        throw new Exception(string.Format("Token of type {0} cannot be evaluated.", op.Type.ToString()));
                    }*/
                default:
                    throw new Exception(string.Format("Token of type {0} cannot be evaluated.", op.Type.ToString()));
            }
        }
    }
}
