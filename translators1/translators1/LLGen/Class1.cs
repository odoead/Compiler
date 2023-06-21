using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translators1.LLGen
{
   
    public class LLConverter
    {
       /* enum BINOperatorts
        {

            max,
            min,
            avg

        }
        enum UNAROperatorts
        {
            sin,
            cos,
            tan


        }*/
      
        string filepath;
        private string rawstr;
        public LLConverter(string fpath)
        {
            filepath = fpath;
        }

        public string Convert()
        {
            string result = "";
            foreach (string str in File.ReadLines(filepath))
            {
                rawstr = str;

                /*//переворачиваем строку
                char[] charArray = rawstr.ToCharArray();
                Array.Reverse(charArray);
                rawstr = new string(charArray);*/

                //Console.WriteLine(rawstr);
                result += ConvertToLL();
            }
            File.WriteAllText(filepath, "");
            return result;

        }


        string ConvertToLL()
        {
            int i = 0;
            string Fstr = "";
            string temp;
            Stack<string> numStack = new Stack<string>(); //стек для врем. хранения чисел  
            Stack<string> FuncNameStack = new Stack<string>();//стек для врем. хранения названий ф-й 
            LinkedList<string> splitString = new LinkedList<string>();//хранит в себе все элементы
            foreach (string str in rawstr.Split(" "))
                splitString.AddFirst(str);



            while (splitString.Count > 0)
            {
                temp = splitString.Last.Value;
                splitString.RemoveLast();

                if (temp == "+" || temp == "-" || temp == "*" || temp == "/")
                {   string b = numStack.Pop();
                    string a = numStack.Pop();
                    
                    i++;//прибавляем число операций при нахождении знака 
                    switch (temp)
                    {
                        case "+":
                            {
                                Fstr += "%" + i + " = fadd double " + a + ", " + b + Environment.NewLine ;
                                //Console.WriteLine("%" + i + "= fadd double " + a + " " + b);
                                // splitString.AddLast("%"+i.ToString());
                                break;
                            }
                        case "-":
                            {
                                Fstr += "%" + i + " = fsub double " + a + ", " + b +";"+ Environment.NewLine;
                                //Console.WriteLine("%" + i + "= fsub double " + a + " " + b);
                                // splitString.AddLast("%" + i.ToString());
                                break;
                            }
                        case "*":
                            {
                                Fstr += "%" + i + " = fmul double " + a + ", " + b +";"+ Environment.NewLine;
                                //Console.WriteLine("%" + i + "= fmul double " + a + " " + b);
                                // splitString.AddLast("%" + i.ToString());
                                break;
                            }
                        case "/":
                            {
                                Fstr += "%" + i + " = fdiv double " + a + ", " + b +";"+ Environment.NewLine;
                                //Console.WriteLine("%" + i + "= fdiv double " + a + " " + b);
                                //splitString.AddLast("%" + i.ToString());
                                break;
                            }


                    }

                    splitString.AddLast("%" + i.ToString());

                }
                else if (temp == "Min" || temp == "Max" || temp == "Avg")
                {
                    FuncNameStack.Push(temp);
                    string a = numStack.Pop();
                    string b = numStack.Pop();
                    i++;
                    Fstr += "%" + i + " = call double @" + temp + "(double " + a + ", " + b + ");" + Environment.NewLine;
                    splitString.AddLast("%" + i.ToString());
                }
                else if (temp == "Sin" || temp == "Cos" || temp == "Tan")
                {
                    FuncNameStack.Push(temp);
                    string a = numStack.Pop();
                    i++;
                    Fstr += "%" + i + " = call double @" + temp + "(double " + a + ");" + Environment.NewLine;
                    splitString.AddLast("%" + i.ToString());
                }
                else
                {
                    numStack.Push(temp);
                }

            }


            Fstr += "ret double %" + i + ";" + Environment.NewLine;
            while (FuncNameStack.Count > 0)
                Fstr += "declare dso_local double @" + FuncNameStack.Pop() + "(double noudef) local_unamed_addr+" + Environment.NewLine;

            return Fstr;
        }


        /* public double Counting(string input)
         {
             double result = 0; //Результат
             Stack<double> temp = new Stack<double>(); //Dhtvtyysq стек для решения

             for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
             {
                 //Если символ - цифра, то читаем все число и записываем на вершину стека
                 if (Char.IsDigit(input[i]))
                 {
                     string a = string.Empty;

                     while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                     {
                         a += input[i]; //Добавляем
                         i++;
                         if (i == input.Length) break;
                     }
                     temp.Push(double.Parse(a)); //Записываем в стек
                     i--;
                 }
                 else if (IsOperator(input[i])) //Если символ - оператор
                 {
                     //Берем два последних значения из стека
                     double a = temp.Pop();
                     double b = temp.Pop();

                     switch (input[i]) //И производим над ними действие, согласно оператору
                     {
                         case '+': result = b + a; break;
                         case '-': result = b - a; break;
                         case '*': result = b * a; break;
                         case '/': result = b / a; break;
                         case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                     }
                     temp.Push(result); //Результат вычисления записываем обратно в стек
                 }
             }
             return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
         }
 */


    }
}
