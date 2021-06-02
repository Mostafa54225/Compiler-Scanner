
namespace Scanner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.Line_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lexeme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.matchability = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Return_Token = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lexeme_NO_in_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.noerrors = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.saveCodeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.table)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.richTextBox1, this.autocompleteMenu1);
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.richTextBox1.Location = new System.Drawing.Point(12, 61);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1089, 603);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(43, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(409, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "Compile";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(641, 8);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(409, 43);
            this.button2.TabIndex = 2;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.AllowsTabKey = true;
            this.autocompleteMenu1.Colors = ((AutocompleteMenuNS.Colors)(resources.GetObject("autocompleteMenu1.Colors")));
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[] {
        "Yesif-Otherwise",
        "Omw",
        "SIMww",
        "Chji",
        "Seriestl",
        "IMwf",
        "SIMwf",
        "NOReturn ",
        "Reiterate",
        "RepeatWhen",
        "GetBack",
        "OutLoop",
        "Loli ",
        "Include",
        "Start",
        "Last"};
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // Line_NO
            // 
            this.Line_NO.HeaderText = "Line NO";
            this.Line_NO.MinimumWidth = 6;
            this.Line_NO.Name = "Line_NO";
            this.Line_NO.ReadOnly = true;
            this.Line_NO.Width = 200;
            // 
            // Lexeme
            // 
            this.Lexeme.HeaderText = "Lexeme";
            this.Lexeme.MinimumWidth = 6;
            this.Lexeme.Name = "Lexeme";
            this.Lexeme.ReadOnly = true;
            this.Lexeme.Width = 200;
            // 
            // matchability
            // 
            this.matchability.HeaderText = "matchability";
            this.matchability.MinimumWidth = 6;
            this.matchability.Name = "matchability";
            this.matchability.ReadOnly = true;
            this.matchability.Width = 200;
            // 
            // table
            // 
            this.table.AllowUserToAddRows = false;
            this.table.AllowUserToDeleteRows = false;
            this.table.AllowUserToOrderColumns = true;
            this.table.AllowUserToResizeColumns = false;
            this.table.AllowUserToResizeRows = false;
            this.table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Return_Token,
            this.Lexeme_NO_in_Line,
            this.dataGridViewTextBoxColumn3});
            this.table.Location = new System.Drawing.Point(1113, 61);
            this.table.Name = "table";
            this.table.RowHeadersWidth = 51;
            this.table.RowTemplate.Height = 29;
            this.table.Size = new System.Drawing.Size(676, 603);
            this.table.TabIndex = 4;
            this.table.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.table_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Line NO";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.Frozen = true;
            this.dataGridViewTextBoxColumn2.HeaderText = "Lexeme";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // Return_Token
            // 
            this.Return_Token.Frozen = true;
            this.Return_Token.HeaderText = "Return Token";
            this.Return_Token.MinimumWidth = 6;
            this.Return_Token.Name = "Return_Token";
            this.Return_Token.ReadOnly = true;
            this.Return_Token.Width = 125;
            // 
            // Lexeme_NO_in_Line
            // 
            this.Lexeme_NO_in_Line.Frozen = true;
            this.Lexeme_NO_in_Line.HeaderText = "Lexeme NO in Line";
            this.Lexeme_NO_in_Line.MinimumWidth = 6;
            this.Lexeme_NO_in_Line.Name = "Lexeme_NO_in_Line";
            this.Lexeme_NO_in_Line.ReadOnly = true;
            this.Lexeme_NO_in_Line.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Frozen = true;
            this.dataGridViewTextBoxColumn3.HeaderText = "Matchability";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1113, 681);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 23);
            this.label1.TabIndex = 5;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // noerrors
            // 
            this.noerrors.AutoSize = true;
            this.noerrors.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.noerrors.ForeColor = System.Drawing.Color.Red;
            this.noerrors.Location = new System.Drawing.Point(1113, 758);
            this.noerrors.Name = "noerrors";
            this.noerrors.Size = new System.Drawing.Size(146, 20);
            this.noerrors.TabIndex = 6;
            this.noerrors.Text = "Total NO Of Errors: ";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(43, 698);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(409, 43);
            this.clearButton.TabIndex = 7;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // saveCodeButton
            // 
            this.saveCodeButton.Location = new System.Drawing.Point(641, 698);
            this.saveCodeButton.Name = "saveCodeButton";
            this.saveCodeButton.Size = new System.Drawing.Size(409, 43);
            this.saveCodeButton.TabIndex = 8;
            this.saveCodeButton.Text = "Save Code";
            this.saveCodeButton.UseVisualStyleBackColor = true;
            this.saveCodeButton.Click += new System.EventHandler(this.saveCodeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1801, 851);
            this.Controls.Add(this.saveCodeButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.noerrors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.table);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lexeme;
        private System.Windows.Forms.DataGridViewTextBoxColumn matchability;
        private System.Windows.Forms.DataGridView table;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Return_Token;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lexeme_NO_in_Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label noerrors;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button saveCodeButton;
    }
}

