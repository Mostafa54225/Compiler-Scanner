using System;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace Scanner
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        public string code;
        public int LineNumber = 1;
        public int NoOfLexeme = 1;

        public int LineNumberInclude = 1;
        public int NoOfLexemeInclude = 1;
        public int NoOfErrors = 0;
        public string match = "";
        
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Open a file";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                using (StreamReader sr = new StreamReader(openFile.FileName))
                {
                    code = sr.ReadToEnd();
                    sr.Close();
                }
            }
            else return;
            var lexer = new Lexer(code);

            while (true)
            {

                var token = lexer.NextToken();
                // Condition if the token return null (Pressing Enter Key to enter new line)
                if (token.Kind == SyntaxKind.NewLine)
                {
                    LineNumber++;
                    NoOfLexeme = 1;
                    continue;
                }
                if (token.Kind == SyntaxKind.WhiteSpaceToken) continue;

                if (token.Kind == SyntaxKind.Inclusion) includeFiles(token.Text);

                if (token.Kind == SyntaxKind.BadToken)
                {
                    NoOfErrors++;
                    match = "Not Matched";
                }
                else match = "Matched";

                if (token.Kind == SyntaxKind.EndOfFileToken) break;

                table.Rows.Add(LineNumber, token.Text, token.Kind, NoOfLexeme++, match);
            }
            noerrors.Text += NoOfErrors;
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetInformations(false);


            code = richTextBox1.Text;
            var lexer = new Lexer(code);
            while (true)
            {
                var token = lexer.NextToken();
                // Condition if the token return null (Pressing Enter Key to enter new line)
                if (token.Kind == SyntaxKind.NewLine)
                {
                    LineNumber++;
                    NoOfLexeme = 1;
                    continue;
                }
                if (token.Kind == SyntaxKind.WhiteSpaceToken) continue;

                if (token.Kind == SyntaxKind.Inclusion) includeFiles(token.Text);

                if (token.Kind == SyntaxKind.EndOfFileToken) break;

                
                if (token.Kind == SyntaxKind.BadToken)
                {
                    NoOfErrors++;
                    match = "Not Matched";
                    richTextBox1.SelectionStart = token.Position;
                    richTextBox1.SelectionLength = 1;
                    richTextBox1.SelectionColor = Color.Red;
                    richTextBox1.SelectionBackColor = Color.Yellow;
                }
                else match = "Matched";

                
                table.Rows.Add(LineNumber, token.Text, token.Kind, NoOfLexeme++, match);
            }
            noerrors.Text += NoOfErrors;
        }

        private void includeFiles(string filePath)
        {
            string FilePath = @"G:\CS\Level 3\Second Semester\Compiler\Project\Project\" + filePath;

            // If the file doesn't exist
            if (!File.Exists(FilePath))
            {
                label1.Text += $"{filePath} doesn't exist";
                NoOfErrors++;
                return;
            }
            
            string codeFromInclude = System.IO.File.ReadAllText(FilePath);

            var lexerInclude = new Lexer(codeFromInclude);
            while (true)
            {
                var tokenInclude = lexerInclude.NextToken();
                // Condition if the token return null (Pressing Enter Key to enter new line)
                if (tokenInclude.Kind == SyntaxKind.NewLine)
                {
                    LineNumberInclude++;
                    NoOfLexemeInclude = 1;
                    continue;
                }
                if (tokenInclude.Kind == SyntaxKind.Inclusion) includeFiles(tokenInclude.Text);

                if (tokenInclude.Kind == SyntaxKind.WhiteSpaceToken) continue;

                if (tokenInclude.Kind == SyntaxKind.BadToken)
                {
                    NoOfErrors++;
                    match = "Not Matched";
                }
                else match = "Matched";

                if (tokenInclude.Kind == SyntaxKind.EndOfFileToken) break;


                table.Rows.Add(LineNumberInclude, tokenInclude.Text, tokenInclude.Kind, NoOfLexemeInclude++, match);
            }

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            resetInformations(true);
        }
        public void resetInformations(bool removeTextFromEditor)
        {
            if (removeTextFromEditor)
                richTextBox1.Text = "";
            
            table.Rows.Clear();

            LineNumber = 1;
            NoOfLexeme = 1;

            LineNumberInclude = 1;
            NoOfLexemeInclude = 1;
            NoOfErrors = 0;
            noerrors.Text = "Total NO Of Errors: ";
            label1.Text = "";
        }
        private void saveCodeButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "") 
            {
                messageBoxShow("You should write in the editor to be able to save", "Editor is empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (code == null)
            {
                messageBoxShow("You should Compile the code first", "Compilation Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(NoOfErrors > 0)
            {
                messageBoxShow("Remove the errors to be able to save the code", "Erorrs in Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.RestoreDirectory = true;
            saveFile.InitialDirectory = @"G:\CS\Level 3\Second Semester\Compiler\Project\Project";
            //saveFile.FileName = String.Format("{0}.txt", code);
            saveFile.DefaultExt = "*.txt*";
            if(saveFile.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName))
                    sw.WriteLine(code);
            }
        }
        private void messageBoxShow(string text, string title, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageIcon)
        {
            MessageBox.Show(text, title, messageBoxButtons, messageIcon);
        }
    }

    enum SyntaxKind
    {
        Constant,
        WhiteSpaceToken,
        ArithmeticOperation,
        Braces,
        Integer,
        SInteger,
        String,
        Float,
        SFloat,
        Condition,
        Void,
        Loop,
        Return,
        Break,
        Struct,
        LogicOperators,
        RelationalOperators,
        AssignmentOperator,
        AccessOperator,
        QuotationMark,
        Inclusion,
        Comment,
        TokenDelimiter,
        LineDelimiter,
        Start,
        End,
        Character,
        EndOfFileToken,
        BadToken,
        IDENTIFIER,
        NewLine
    }

    class SyntaxToken
    {
        public SyntaxToken(SyntaxKind kind, int position, string text)
        {
            Kind = kind;
            Position = position;
            Text = text;
        }
        public SyntaxKind Kind { get; }
        public int Position { get; }
        public string Text { get; }
    }

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
                if (_position >= _text.Length)
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
            if (_position >= _text.Length)
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

                var length = _position - start;
                var text = _text.Substring(start, length);
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

                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (text == "/^")
                {
                    while (Current != LF) Next();
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
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (text == "/@")
                {
                    while(Char.ToString(Current) != "@")
                    {
                        Next();
                        
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
                return new SyntaxToken(SyntaxKind.ArithmeticOperation, _position++, _text.Substring(_position - 1, 1));

            if (isBraces(Current))
                return new SyntaxToken(SyntaxKind.Braces, _position++, _text.Substring(_position - 1, 1));


            if (isKeyWord(Current, i, "Include"))
            {

                var start = _position;
                while (isKeyWord(Current, i, "Include"))
                {
                    Next();
                    i++;
                }
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                        filePath += Char.ToString(Current);
                        Next();

                    }
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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

                var length = _position - start;
                var text = _text.Substring(start, length);
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

                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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
                var length = _position - start;
                var text = _text.Substring(start, length);
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

                var length = _position - start;
                var text = _text.Substring(start, length);

                return new SyntaxToken(SyntaxKind.IDENTIFIER, start, text);
            }



            if (isRelationalOp(Char.ToString(Current)))
                return new SyntaxToken(SyntaxKind.RelationalOperators, _position++, _text.Substring(_position - 1, 1));

            if (isQuotationMark(Char.ToString(Current)))
                return new SyntaxToken(SyntaxKind.QuotationMark, _position++, _text.Substring(_position - 1, 1));

            return new SyntaxToken(SyntaxKind.BadToken, _position++, _text.Substring(_position - 1, 1));
        }



        private static bool isDigit(char text)
        {
            Regex regex = new Regex(@"^\d$");
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
            if (location < str.Length && character == str[location]) return true;
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
