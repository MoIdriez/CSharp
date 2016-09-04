namespace TankControl
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textCom = new System.Windows.Forms.TextBox();
            this.textBaud = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textResults = new System.Windows.Forms.TextBox();
            this.textMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textCom
            // 
            this.textCom.Location = new System.Drawing.Point(15, 25);
            this.textCom.Name = "textCom";
            this.textCom.Size = new System.Drawing.Size(100, 20);
            this.textCom.TabIndex = 0;
            this.textCom.Text = "COM3";
            // 
            // textBaud
            // 
            this.textBaud.Location = new System.Drawing.Point(121, 25);
            this.textBaud.Name = "textBaud";
            this.textBaud.Size = new System.Drawing.Size(100, 20);
            this.textBaud.TabIndex = 1;
            this.textBaud.Text = "115200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Comm Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Baud Rate";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(227, 22);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textResults
            // 
            this.textResults.Location = new System.Drawing.Point(15, 76);
            this.textResults.Multiline = true;
            this.textResults.Name = "textResults";
            this.textResults.Size = new System.Drawing.Size(287, 247);
            this.textResults.TabIndex = 5;
            // 
            // textMessage
            // 
            this.textMessage.Location = new System.Drawing.Point(15, 50);
            this.textMessage.Name = "textMessage";
            this.textMessage.Size = new System.Drawing.Size(206, 20);
            this.textMessage.TabIndex = 6;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(227, 48);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 353);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textMessage);
            this.Controls.Add(this.textResults);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBaud);
            this.Controls.Add(this.textCom);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textCom;
        private System.Windows.Forms.TextBox textBaud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textResults;
        private System.Windows.Forms.TextBox textMessage;
        private System.Windows.Forms.Button buttonSend;
    }
}

