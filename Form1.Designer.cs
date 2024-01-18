namespace Sample_AutoTiltCorrection
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
            listBox1 = new ListBox();
            controllerList = new CheckedListBox();
            Add = new Button();
            textBox1 = new TextBox();
            Remove = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            Connect = new Button();
            label4 = new Label();
            label5 = new Label();
            targetAngle = new Label();
            Disconnect = new Button();
            Correction = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(427, 29);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(317, 229);
            listBox1.TabIndex = 0;
            // 
            // controllerList
            // 
            controllerList.FormattingEnabled = true;
            controllerList.Location = new Point(46, 144);
            controllerList.Name = "controllerList";
            controllerList.Size = new Size(339, 112);
            controllerList.TabIndex = 1;
            // 
            // Add
            // 
            Add.Location = new Point(46, 92);
            Add.Name = "Add";
            Add.Size = new Size(77, 46);
            Add.TabIndex = 2;
            Add.Text = "Add";
            Add.UseVisualStyleBackColor = true;
            Add.Click += Add_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(77, 39);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(47, 23);
            textBox1.TabIndex = 3;
            textBox1.Text = "192";
            // 
            // Remove
            // 
            Remove.Location = new Point(129, 92);
            Remove.Name = "Remove";
            Remove.Size = new Size(77, 46);
            Remove.TabIndex = 4;
            Remove.Text = "Remove";
            Remove.UseVisualStyleBackColor = true;
            Remove.Click += Remove_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(137, 39);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(47, 23);
            textBox2.TabIndex = 5;
            textBox2.Text = "168";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(197, 39);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(47, 23);
            textBox3.TabIndex = 6;
            textBox3.Text = "0";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(257, 39);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(47, 23);
            textBox4.TabIndex = 7;
            textBox4.Text = "1";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(76, 65);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(48, 23);
            textBox5.TabIndex = 8;
            textBox5.Text = "24691";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(126, 44);
            label1.Name = "label1";
            label1.Size = new Size(10, 15);
            label1.TabIndex = 9;
            label1.Text = ".";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(186, 44);
            label2.Name = "label2";
            label2.Size = new Size(10, 15);
            label2.TabIndex = 10;
            label2.Text = ".";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(246, 44);
            label3.Name = "label3";
            label3.Size = new Size(10, 15);
            label3.TabIndex = 11;
            label3.Text = ".";
            // 
            // Connect
            // 
            Connect.Location = new Point(46, 262);
            Connect.Name = "Connect";
            Connect.Size = new Size(77, 46);
            Connect.TabIndex = 12;
            Connect.Text = "Connect";
            Connect.UseVisualStyleBackColor = true;
            Connect.Click += Connect_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 43);
            label4.Name = "label4";
            label4.Size = new Size(17, 15);
            label4.TabIndex = 13;
            label4.Text = "IP";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(43, 65);
            label5.Name = "label5";
            label5.Size = new Size(30, 15);
            label5.TabIndex = 14;
            label5.Text = "Port";
            // 
            // targetAngle
            // 
            targetAngle.AutoSize = true;
            targetAngle.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            targetAngle.Location = new Point(46, 348);
            targetAngle.Name = "targetAngle";
            targetAngle.Size = new Size(102, 20);
            targetAngle.TabIndex = 21;
            targetAngle.Text = "TargetAngle";
            // 
            // Disconnect
            // 
            Disconnect.Location = new Point(308, 262);
            Disconnect.Name = "Disconnect";
            Disconnect.Size = new Size(77, 46);
            Disconnect.TabIndex = 28;
            Disconnect.Text = "Disconnect";
            Disconnect.UseVisualStyleBackColor = true;
            Disconnect.Click += Disconnect_Click;
            // 
            // Correction
            // 
            Correction.Location = new Point(502, 348);
            Correction.Name = "Correction";
            Correction.Size = new Size(147, 78);
            Correction.TabIndex = 29;
            Correction.Text = "Correction";
            Correction.UseVisualStyleBackColor = true;
            Correction.Click += Correction_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 487);
            Controls.Add(Correction);
            Controls.Add(Disconnect);
            Controls.Add(targetAngle);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Connect);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(Remove);
            Controls.Add(textBox1);
            Controls.Add(Add);
            Controls.Add(controllerList);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "ReadAndGetMultiControllerSetting";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox1;
        private CheckedListBox controllerList;
        private Button Add;
        private TextBox textBox1;
        private Button Remove;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button Connect;
        private Label label4;
        private Label label5;
        private Label targetAngle;
        private Button Disconnect;
        private Button Correction;
    }
}