using System;
using System.Windows.Forms;
using System.IO;
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
        public static int LineNumber = 1;
        public int NoOfLexeme = 1;
       

       
        public string line;  // a string to load lines into external file

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

                //save output in external file

                saveOutputFromTable(token);

                //end of save output

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

                saveOutputFromTable(token);


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

                if (tokenInclude.Kind == SyntaxKind.EndOfFileToken) 
                {
                    LineNumber = 1;
                    LineNumberInclude = 1;
                    NoOfLexeme = 1;
                    NoOfLexemeInclude = 1;
                    break; 
                }

                saveOutputFromTable(tokenInclude);

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
            line = null;
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
            saveCode();
        }


        private void saveCode() 
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
            if (NoOfErrors > 0)
            {
                messageBoxShow("Remove the errors to be able to save the code", "Erorrs in Code", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.RestoreDirectory = true;
            //saveFile.InitialDirectory = @"G:\CS\Level 3\Second Semester\Compiler\Project\Project";
            //saveFile.FileName = String.Format("{0}.txt", code);
            saveFile.DefaultExt = "*.txt*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName))
                    sw.WriteLine(code);
            }
        }

        private void saveOutputFromTable(SyntaxToken token)
        {

            line += $"{token.Kind}: '{token.Text}' #Lexeme No: {NoOfLexeme} in Line Number: {LineNumber}  matchability: {match}";
            line += Environment.NewLine;
        }


        private void messageBoxShow(string text, string title, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageIcon)
        {
            MessageBox.Show(text, title, messageBoxButtons, messageIcon);
        }

        private void saveOutputButton_Click(object sender, EventArgs e)
        {
            if(line == null)
            {
                messageBoxShow("There is no output to save", "Output is empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName))
                    sw.WriteLine(line);
            }
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
}