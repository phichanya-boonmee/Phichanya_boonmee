﻿namespace WindowsFormsApp2
{
    partial class UserControl1
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
            this.SuspendLayout();
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Name = "UserControl1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserControl1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserControl1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UserControl1_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseDown_1);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseUp_1);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
