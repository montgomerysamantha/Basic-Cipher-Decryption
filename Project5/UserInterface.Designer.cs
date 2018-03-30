namespace Project5
{
    partial class UserInterface
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
            this.uxMessageLabel = new System.Windows.Forms.Label();
            this.uxTextbox = new System.Windows.Forms.TextBox();
            this.uxGetMessageButton = new System.Windows.Forms.Button();
            this.uxDecryptButton = new System.Windows.Forms.Button();
            this.uxSolvedTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // uxMessageLabel
            // 
            this.uxMessageLabel.AutoSize = true;
            this.uxMessageLabel.Location = new System.Drawing.Point(13, 3);
            this.uxMessageLabel.Name = "uxMessageLabel";
            this.uxMessageLabel.Size = new System.Drawing.Size(56, 13);
            this.uxMessageLabel.TabIndex = 0;
            this.uxMessageLabel.Text = "Message: ";
            // 
            // uxTextbox
            // 
            this.uxTextbox.Location = new System.Drawing.Point(10, 19);
            this.uxTextbox.Multiline = true;
            this.uxTextbox.Name = "uxTextbox";
            this.uxTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxTextbox.Size = new System.Drawing.Size(430, 110);
            this.uxTextbox.TabIndex = 1;
            // 
            // uxGetMessageButton
            // 
            this.uxGetMessageButton.Location = new System.Drawing.Point(64, 149);
            this.uxGetMessageButton.Name = "uxGetMessageButton";
            this.uxGetMessageButton.Size = new System.Drawing.Size(153, 23);
            this.uxGetMessageButton.TabIndex = 2;
            this.uxGetMessageButton.Text = "Get Message From File";
            this.uxGetMessageButton.UseVisualStyleBackColor = true;
            // 
            // uxDecryptButton
            // 
            this.uxDecryptButton.Location = new System.Drawing.Point(264, 149);
            this.uxDecryptButton.Name = "uxDecryptButton";
            this.uxDecryptButton.Size = new System.Drawing.Size(75, 23);
            this.uxDecryptButton.TabIndex = 3;
            this.uxDecryptButton.Text = "Decrypt";
            this.uxDecryptButton.UseVisualStyleBackColor = true;
            // 
            // uxSolvedTextbox
            // 
            this.uxSolvedTextbox.Location = new System.Drawing.Point(10, 245);
            this.uxSolvedTextbox.Multiline = true;
            this.uxSolvedTextbox.Name = "uxSolvedTextbox";
            this.uxSolvedTextbox.ReadOnly = true;
            this.uxSolvedTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.uxSolvedTextbox.Size = new System.Drawing.Size(424, 109);
            this.uxSolvedTextbox.TabIndex = 4;
            // 
            // UserInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 366);
            this.Controls.Add(this.uxSolvedTextbox);
            this.Controls.Add(this.uxDecryptButton);
            this.Controls.Add(this.uxGetMessageButton);
            this.Controls.Add(this.uxTextbox);
            this.Controls.Add(this.uxMessageLabel);
            this.Name = "UserInterface";
            this.Text = "Crypto Solver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label uxMessageLabel;
        private System.Windows.Forms.TextBox uxTextbox;
        private System.Windows.Forms.Button uxGetMessageButton;
        private System.Windows.Forms.Button uxDecryptButton;
        private System.Windows.Forms.TextBox uxSolvedTextbox;
    }
}

