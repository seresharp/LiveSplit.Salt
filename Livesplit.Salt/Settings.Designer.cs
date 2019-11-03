namespace LiveSplit.Salt
{
    partial class Settings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rndSkinsBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rndSkinsBox
            // 
            this.rndSkinsBox.AutoSize = true;
            this.rndSkinsBox.Location = new System.Drawing.Point(4, 4);
            this.rndSkinsBox.Name = "rndSkinsBox";
            this.rndSkinsBox.Size = new System.Drawing.Size(108, 17);
            this.rndSkinsBox.TabIndex = 0;
            this.rndSkinsBox.Text = "Randomize Skins";
            this.rndSkinsBox.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rndSkinsBox);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(115, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox rndSkinsBox;
    }
}
