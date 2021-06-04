using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Scanner
{
    class Lexer
    {
        private const char CR = '\r';
        private const char LF = '\n';
        private readonly string _text;
        private int _position;

        public Lexer(string text)
        {
            _text = text;
        }
        private char Current
        {
            get
            {
                if (_position >= lengthOfString(_text))
                    return '\0';
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }
        public SyntaxToken NextToken()
        {

            int i = 0;
            if (_position >= lengthOfString(_text))
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0");
            }
            // Condition for new line (Pressing Enter Key)
            // CR => moves the cursor to the beginning of the line without advancing to the next line
            // LF => moves the cursor down to the next line without returning to the beginning of the line
            if (Current == CR)
                Next();
            if (Current == LF)
            {
                Next();
                return new SyntaxToken(SyntaxKind.NewLine, _position, "");
            }

            if (isTokenDelimiter(Current) == 1)
                return new SyntaxToken(SyntaxKind.TokenDelimiter, _position++, "$");
            if (isTokenDelimiter(Current) == 2)
            {
                return new SyntaxToken(SyntaxKind.LineDelimiter, _position++, ".");
            }


            if (isWhiteSpace(Current))
            {
                var start = _position;
                while (isWhiteSpace(Current))
                    Next();

                var text = subString(_text, start, _position);
                //int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text);
            }

            if (isDigit(Current))
            {
                var start = _position;
                var flag = false;
                while (isDigit(Current) || (isTokenDelimiter(Current) == 2 && !flag))
                {
                    Next();
                    if (isTokenDelimiter(Current) == 2)
                    {
                        flag = true;
                        Next();
                        if (isDigit(Current))
                        {
                            Next();
                            continue;
                        }
                        else
                        {
                            _position--;
                            break;
                        }
                    }

                }


                var text = subString(_text, start, _position);
                //int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.Constant, start, text);
            }


            // For Single Line Comment
            if (isKeyWord(Current, i, "/^"))
            {
                var start = _position;
                while (isKeyWord(Current, i, "/^"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "/^")
                {
                    while (Current != LF)
                    {
                        Next();
                        if (Current == '\0') break;
                    }

                    return new SyntaxToken(SyntaxKind.Comment, start, text);
                }
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "/@"))
            {
                var start = _position;
                while (isKeyWord(Current, i, "/@"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "/@")
                {

                    while (Char.ToString(Current) != "@")
                    {
                        Next();
                        if (Current == LF) Form1.LineNumber++;
                        if (Current == '\0') break;
                    }
                    Next();
                    if (Char.ToString(Current) == "/")
                    {
                        Next();
                        return new SyntaxToken(SyntaxKind.Comment, start, text + " @/");
                    }
                    else
                    {
                        _position = start + 2;
                        return new SyntaxToken(SyntaxKind.BadToken, start, text);
                    }
                }
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isArithmeticOperation(Current))
                return new SyntaxToken(SyntaxKind.ArithmeticOperation, _position++, subString(_text, _position - 1, _position));

            if (isBraces(Current))
                return new SyntaxToken(SyntaxKind.Braces, _position++, subString(_text, _position - 1, _position));


            if (isKeyWord(Current, i, "Include"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "Include"))
                {
                    Next();
                    i++;
                }
                var text = subString(_text, start, _position);
                if (text == "Include")
                {
                    string filePath = "";
                    while (Current != LF)
                    {
                        if (Current == ' ')
                        {
                            Next();
                            continue;
                        }
                        if (Current == '\0') break;
                        filePath += Char.ToString(Current);
                        Next();

                    }
                    filePath = Regex.Replace(filePath, @"\s", "");
                    return new SyntaxToken(SyntaxKind.Inclusion, start, filePath);
                }
                else
                {
                    _position = start;
                    i = 0;
                }
            }


            if (isKeyWord(Current, i, "Yesif-Otherwise"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "Yesif-Otherwise"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "Yesif-Otherwise")
                    return new SyntaxToken(SyntaxKind.Condition, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "Omw"))
            {
                var start = _position;
                while (isKeyWord(Current, i, "Omw"))
                {
                    Next();
                    i++;
                }
                //var length = _position - start;
                var text = subString(_text, start, _position);
                if (text == "Omw")
                    return new SyntaxToken(SyntaxKind.Integer, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "SIMww"))
            {
                var start = _position;
                while (isKeyWord(Current, i, "SIMww"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "SIMww")
                    return new SyntaxToken(SyntaxKind.SInteger, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "Chji"))
            {
                var start = _position;
                while (isKeyWord(Current, i, "Chji"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "Chji")
                    return new SyntaxToken(SyntaxKind.Character, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "Seriestl"))
            {
                var start = _position;
                while (isKeyWord(Current, i, "Seriestl"))
                {
                    Next();
                    i++;
                }


                var text = subString(_text, start, _position);
                if (text == "Seriestl")
                    return new SyntaxToken(SyntaxKind.String, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }

            }

            if (isKeyWord(Current, i, "IMwf"))
            {
                var start = _position;
                while (isKeyWord(Current, i, "IMwf"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "IMwf")
                    return new SyntaxToken(SyntaxKind.Float, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }

            }

            if (isKeyWord(Current, i, "SIMwf"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "SIMwf"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "SIMwf")
                    return new SyntaxToken(SyntaxKind.SFloat, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "NOReturn"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "NOReturn"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "NOReturn")
                    return new SyntaxToken(SyntaxKind.Void, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "RepeatWhen"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "RepeatWhen"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "RepeatWhen")
                    return new SyntaxToken(SyntaxKind.Loop, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "Reiterate"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "Reiterate"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "Reiterate")
                    return new SyntaxToken(SyntaxKind.Loop, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "GetBack"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "GetBack"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "GetBack")
                    return new SyntaxToken(SyntaxKind.Return, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "OutLoop"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "OutLoop"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "OutLoop")
                    return new SyntaxToken(SyntaxKind.Break, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "Loli"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "Loli"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "Loli")
                    return new SyntaxToken(SyntaxKind.Struct, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "&&"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "&&"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "&&")
                    return new SyntaxToken(SyntaxKind.LogicOperators, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "||"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "||"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "||")
                    return new SyntaxToken(SyntaxKind.LogicOperators, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "->"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "->"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "->")
                    return new SyntaxToken(SyntaxKind.AccessOperator, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "Start"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "Start"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "Start")
                    return new SyntaxToken(SyntaxKind.Start, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }

            if (isKeyWord(Current, i, "Last"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "Last"))
                {
                    Next();
                    i++;
                }

                var text = subString(_text, start, _position);
                if (text == "Last")
                    return new SyntaxToken(SyntaxKind.End, start, text);
                else
                {
                    _position = start;
                    i = 0;
                }
            }


            if (isKeyWord(Current, i, "="))
            {
                Next();
                if (Current == '=')
                {
                    string matchRlationalOp = "=" + Current;
                    if (isRelationalOp(matchRlationalOp))
                        return new SyntaxToken(SyntaxKind.RelationalOperators, _position++, matchRlationalOp);
                }
                else _position--;
            }

            if (isKeyWord(Current, i, "!"))
            {
                Next();
                if (Current == '=')
                {
                    string matchRlationalOp = "!" + Current;
                    if (isRelationalOp(matchRlationalOp))
                        return new SyntaxToken(SyntaxKind.RelationalOperators, _position++, matchRlationalOp);
                }
                else _position--;
            }

            if (isKeyWord(Current, i, "<"))
            {
                Next();
                if (Current == '=')
                {
                    string matchRlationalOp = "<" + Current;
                    if (isRelationalOp(matchRlationalOp))
                        return new SyntaxToken(SyntaxKind.RelationalOperators, _position++, matchRlationalOp);
                }
                else _position--;
            }

            if (isKeyWord(Current, i, ">"))
            {
                Next();
                if (Current == '=')
                {
                    string matchRlationalOp = ">" + Current;
                    if (isRelationalOp(matchRlationalOp))
                        return new SyntaxToken(SyntaxKind.RelationalOperators, _position++, matchRlationalOp);
                }
                else _position--;
            }

            if (isKeyWord(Current, i, "~"))
                return new SyntaxToken(SyntaxKind.LogicOperators, _position++, "~");

            if (isKeyWord(Current, i, "="))
            {
                return new SyntaxToken(SyntaxKind.AssignmentOperator, _position++, "=");
            }

            if (isidentifier(Char.ToString(Current)))
            {
                var start = _position;
                while (isidentifier(Char.ToString(Current)) || isDigit(Current))
                    Next();


                var text = subString(_text, start, _position);

                return new SyntaxToken(SyntaxKind.IDENTIFIER, start, text);
            }



            if (isRelationalOp(Char.ToString(Current)))
                return new SyntaxToken(SyntaxKind.RelationalOperators, _position++, subString(_text, _position - 1, _position));

            if (isQuotationMark(Char.ToString(Current)))
                return new SyntaxToken(SyntaxKind.QuotationMark, _position++, subString(_text, _position - 1, _position));

            return new SyntaxToken(SyntaxKind.BadToken, _position++, subString(_text, _position - 1, _position));
        }

        private static int lengthOfString(string text)
        {
            int counter = 0;
            foreach (char c in text) counter++;
            return counter;
        }
        private static string subString(string text, int startIndex, int length)
        {
            string word = "";
            for (int i = startIndex; i < length; i++)
            {
                word += text[i];
            }
            return word;
        }

        private static bool isDigit(char text)
        {
            Regex regex = new Regex(@"([0-9]*[.])?[0-9]+");
            string test = Char.ToString(text);
            if (regex.IsMatch(test)) return true;
            return false;
        }
        private static int isTokenDelimiter(char text)
        {
            if (text == '$') return 1;
            else if (text == '.') return 2;
            return 0;
        }
        private static bool isWhiteSpace(char text)
        {
            if (text == ' ') return true;
            return false;
        }
        private static bool isArithmeticOperation(char text)
        {
            Regex regex = new Regex(@"[-+*\/]");
            string test = Char.ToString(text);
            if (regex.IsMatch(test)) return true;
            return false;
        }
        private static bool isBraces(char text)
        {
            //Regex regex = new Regex(@"(\{.*?\}|\[.*?\]|\(.*?\))");
            Regex regex = new Regex(@"(\{|\}|\[|\]|\(|\))");
            string test = Char.ToString(text);
            if (regex.IsMatch(test)) return true;
            return false;
        }

        private static bool isRelationalOp(string text)
        {
            Regex regex = new Regex(@"(?:<=?|>=?|==|!=)");
            if (regex.IsMatch(text)) return true;
            return false;
        }

        private static bool isQuotationMark(string text)
        {
            Regex regex = new Regex(@"(""|')");
            if (regex.IsMatch(text)) return true;
            return false;
        }
        private static bool isKeyWord(char character, int location, string word)
        {
            string str = word;
            if (location < lengthOfString(str) && character == str[location]) return true;
            return false;
        }

        private static bool isidentifier(string text)
        {
            Regex regex = new Regex(@"[a-zA-Z_][0-9a-zA-Z_]*");
            if (regex.IsMatch(text)) return true;
            return false;
        }
    }
}