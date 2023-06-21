namespace translatorGraphicForms1
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
            buttonClean = new Button();
            buttonTranslate = new Button();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBoxInputCode = new TextBox();
            textBoxLiterals = new TextBox();
            textBoxOutputCode = new TextBox();
            SuspendLayout();
            // 
            // buttonClean
            // 
            buttonClean.Location = new Point(472, 322);
            buttonClean.Name = "buttonClean";
            buttonClean.Size = new Size(65, 23);
            buttonClean.TabIndex = 0;
            buttonClean.Text = "Очистка";
            buttonClean.UseVisualStyleBackColor = true;
            buttonClean.Click += buttonClean_Click;
            // 
            // buttonTranslate
            // 
            buttonTranslate.Location = new Point(337, 322);
            buttonTranslate.Name = "buttonTranslate";
            buttonTranslate.Size = new Size(75, 23);
            buttonTranslate.TabIndex = 1;
            buttonTranslate.Text = "Переклад";
            buttonTranslate.UseVisualStyleBackColor = true;
            buttonTranslate.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 2;
            label1.Text = "Вхідний код";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 326);
            label3.Name = "label3";
            label3.Size = new Size(65, 15);
            label3.TabIndex = 4;
            label3.Text = "Синтаксис";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(472, 9);
            label4.Name = "label4";
            label4.Size = new Size(79, 15);
            label4.TabIndex = 5;
            label4.Text = "Вихідний код";
            // 
            // textBoxInputCode
            // 
            textBoxInputCode.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxInputCode.Location = new Point(12, 27);
            textBoxInputCode.Multiline = true;
            textBoxInputCode.Name = "textBoxInputCode";
            textBoxInputCode.Size = new Size(400, 290);
            textBoxInputCode.TabIndex = 7;
            // 
            // textBoxLiterals
            // 
            textBoxLiterals.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxLiterals.Location = new Point(12, 349);
            textBoxLiterals.Multiline = true;
            textBoxLiterals.Name = "textBoxLiterals";
            textBoxLiterals.Size = new Size(860, 200);
            textBoxLiterals.TabIndex = 8;
            // 
            // textBoxOutputCode
            // 
            textBoxOutputCode.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxOutputCode.Location = new Point(472, 27);
            textBoxOutputCode.Multiline = true;
            textBoxOutputCode.Name = "textBoxOutputCode";
            textBoxOutputCode.Size = new Size(400, 290);
            textBoxOutputCode.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(textBoxOutputCode);
            Controls.Add(textBoxLiterals);
            Controls.Add(textBoxInputCode);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(buttonTranslate);
            Controls.Add(buttonClean);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonClean;
        private Button buttonTranslate;
        private Label label1;
        private Label label3;
        private Label label4;
        private TextBox textBoxInputCode;
        private TextBox textBoxLiterals;
        private TextBox textBoxOutputCode;
    }
}