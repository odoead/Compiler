using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using translators1.SyntaxAnalyzer.Interfaces;
using translators1.SyntaxAnalyzer.Models;

namespace translators1.SyntaxAnalyzer
{
    public  class Parser
    {
        private Token curToken;
        private int curPos;
        private int charCount;
        private string curChar;
        public string Text { get; private set; }

        public Parser(string text)
        {
            Text = string.IsNullOrEmpty(text) ? string.Empty : text;
            charCount = Text.Length;
            curToken = Token.None();

            curPos = -1;
            Advance();
        }

        /// <summary>
        /// начало рекурсии 
        /// </summary>
        /// <returns></returns>
        public Expression Parse()
        {
            NextToken();
            Expression node = GrabExpr();
            ExpectToken(TokenType.None);
            return node;
        }

        /// <summary>
        /// принимает токен и сравнивает с общим списком токенов
        /// </summary>
        /// <param  ></param>
        /// <returns>токен</returns>
        /// <exception cref="InvalidSyntaxException"></exception>
        private Token ExpectToken(TokenType tokenType)
        {
            if (curToken.Type == tokenType)
            {
                return curToken;
            }
            else
            {
                throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Expected {1} but {2} is given.{3}", curPos, tokenType, curToken.Type.ToString(), curChar));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Expression GrabExpr()
        {
            Expression left;
            Expression right;
            while (curToken.Type == TokenType.Identifier)//если первый токен - имя функции 
            {
                Token op = curToken;// запись опер. в токен 
                NextToken();
                if (curToken.Type == TokenType.LeftBracket)
                {
                    NextToken();// пропускаем (
                    left = GrabFactor();//берем левое значение 
                    if (curToken.Type == TokenType.RightBracket)//если ф-я с одним параметром 
                    {
                        left = new UnaryOp(op, left);

                        ExpectToken(TokenType.RightBracket);
                        NextToken();
                        return left;

                    }
                    else//если бинарный
                    {
                        NextToken();//пропускаем ,
                        right = GrabFactor();
                        ExpectToken(TokenType.RightBracket);
                        NextToken();

                        left = new BinOp(op, left, right);
                        return left;
                    }

                }


                right = GrabTerm();

            }

            left = GrabTerm();

            while (curToken.Type == TokenType.Zap)
            {
                // NextToken();
                Token op = curToken;
                NextToken();
                right = GrabTerm();
                left = new BinOp(op, left, right);
            }

            while (curToken.Type == TokenType.Plus
                || curToken.Type == TokenType.Minus /*|| curToken.Type == TokenType.Zap*/)//а+б+с-с итд
            {
                Token op = curToken;
                NextToken();
                right = GrabTerm();
                left = new BinOp(op, left, right);
            }

            return left;
        }

        private Expression GrabTerm()
        {
            Expression left = GrabFactor();

            while (curToken.Type == TokenType.Multiply
                || curToken.Type == TokenType.Divide)//а*б*с/д
            {
                Token op = curToken;
                NextToken();
                Expression right = GrabFactor();
                left = new BinOp(op, left, right);
            }
            while (curToken.Type == TokenType.Zap)////!!!!!!!!!!!!!
            {
                NextToken();
                Expression right = GrabFactor();
                // left = new BinOp(op, left, right);
            }
            while (curToken.Type == TokenType.Identifier/*|| curToken.Type == TokenType.Zap*/)///макс(а,б)
            {
                Token op = curToken;
                NextToken();
                while (curToken.Type == TokenType.Zap)
                {
                    NextToken();
                }
                Expression right = GrabFactor();
                left = new Function(op, left, right);
            }

            return left;
        }

        private Expression GrabFactor()///!!!!!!!!!
        {
            while (curToken.Type == TokenType.Zap)
            {
                NextToken();
            }
            if (curToken.Type == TokenType.Plus
            || curToken.Type == TokenType.Minus /*|| curToken.Type == TokenType.String*/)
            {
                Expression node = GrabUnaryExpr();
                return node;
            }
            else if (curToken.Type == TokenType.LeftBracket)
            {
                Expression node = GrabBracketExpr();
                return node;
            }
            else if (curToken.Type == TokenType.Identifier)
            {
                Expression node = GrabFunctionExpr();
                return node;
            }

            else
            {
                Token token = ExpectToken(TokenType.Number);
                NextToken();
                return new Num(token);
            }
        }

        private Expression GrabUnaryExpr()
        {
            Token op;

            if (curToken.Type == TokenType.Plus)
            {
                op = ExpectToken(TokenType.Plus);
            }
            else
            {
                op = ExpectToken(TokenType.Minus);
            }

            NextToken();

            if (curToken.Type == TokenType.Plus
                || curToken.Type == TokenType.Minus)
            {
                Expression expr = GrabUnaryExpr();
                return new UnaryOp(op, expr);
            }
            else
            {
                Expression expr = GrabFactor();
                return new UnaryOp(op, expr);
            }
        }

        private Expression GrabBracketExpr()
        {
            ExpectToken(TokenType.LeftBracket);
            NextToken();
            Expression node = GrabExpr();
            ExpectToken(TokenType.RightBracket);
            NextToken();
            return node;
        }
        private Expression GrabFunctionExpr()
        {


            ExpectToken(TokenType.Identifier);
            NextToken();
            ExpectToken(TokenType.LeftBracket);
            NextToken();
            Expression node = GrabFactor();//!!!!

            ExpectToken(TokenType.RightBracket);
            NextToken();
            return node;

        }

        private void NextToken()
        {


            if (curChar == "\0")
            {
                curToken = Token.None();
                return;
            }

            if (curChar == " ")
            {
                while ((curChar != "\0" && curChar == " ") || (curChar != "\0" && curChar == ","))
                {
                    Advance();
                }

                if (curChar == "\0")
                {
                    curToken = Token.None();
                    return;
                }
            }


            if (curChar == "+")
            {
                curToken = new Token(TokenType.Plus, curChar.ToString());
                Advance();
                return;
            }

            if (curChar == "-")
            {
                curToken = new Token(TokenType.Minus, curChar.ToString());
                Advance();
                return;
            }

            if (curChar == ",")
            {
                curToken = new Token(TokenType.Zap, curChar.ToString());
                Advance();
                return;
            }

            if (curChar == "*")
            {
                curToken = new Token(TokenType.Multiply, curChar.ToString());
                Advance();
                return;
            }

            if (curChar == "/")
            {
                curToken = new Token(TokenType.Divide, curChar.ToString());
                Advance();
                return;
            }

            if (curChar == "(")
            {
                curToken = new Token(TokenType.LeftBracket, curChar.ToString());
                Advance();
                return;
            }

            if (curChar == ")")
            {
                curToken = new Token(TokenType.RightBracket, curChar.ToString());
                Advance();
                return;
            }


            if ((Convert.ToChar(curChar) >= 'a' && Convert.ToChar(curChar) <= 'z') || (Convert.ToChar(curChar) >= 'A' && Convert.ToChar(curChar) <= 'Z'))
            {

                string str = string.Empty;
                while ((Convert.ToChar(curChar) >= 'a' && Convert.ToChar(curChar) <= 'z') || (Convert.ToChar(curChar) >= 'A' && Convert.ToChar(curChar) <= 'Z'))
                {
                    str += curChar.ToString();
                    Advance();
                }


                curToken = new Token(TokenType.Identifier, str);
                return;
            }

            if (Convert.ToChar(curChar) >= '0' && Convert.ToChar(curChar) <= '9')
            {
                string num = string.Empty;
                while (Convert.ToChar(curChar) >= '0' && Convert.ToChar(curChar) <= '9')
                {
                    num += curChar.ToString();
                    Advance();
                }

                if (curChar == ".")
                {
                    num += curChar.ToString();
                    Advance();

                    if (Convert.ToChar(curChar) >= '0' && Convert.ToChar(curChar) <= '9')
                    {
                        while (Convert.ToChar(curChar) >= '0' && Convert.ToChar(curChar) <= '9')
                        {
                            num += curChar.ToString();
                            Advance();
                        }
                    }
                    else
                    {
                        throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Unexpected symbol {1}.", curPos, curChar));
                    }
                }

                curToken = new Token(TokenType.Number, num);

                return;
            }

            throw new InvalidSyntaxException(string.Format("Invalid syntax at position {0}. Unexpected symbol {1}.", curPos, curChar));
        }

        /// <summary>
        /// проход по всему выражению и чтение след. элемента
        /// </summary>
        private void Advance()//i++
        {
            curPos += 1;

            if (curPos < charCount)
            {
                curChar = Text[curPos].ToString();
            }
            else
            {
                curChar = "\0";
            }
        }
    }
}
