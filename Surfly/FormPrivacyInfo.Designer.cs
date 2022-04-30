namespace Surfly
{
    partial class FormPrivacyInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrivacyInfo));
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelPrivateSession = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelOf100 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTrackers = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.labelPrivateSession);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelPrivateSession
            // 
            resources.ApplyResources(this.labelPrivateSession, "labelPrivateSession");
            this.labelPrivateSession.Name = "labelPrivateSession";
            // 
            // labelScore
            // 
            resources.ApplyResources(this.labelScore, "labelScore");
            this.labelScore.Name = "labelScore";
            // 
            // labelOf100
            // 
            resources.ApplyResources(this.labelOf100, "labelOf100");
            this.labelOf100.Name = "labelOf100";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTrackers);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // labelTrackers
            // 
            resources.ApplyResources(this.labelTrackers, "labelTrackers");
            this.labelTrackers.Name = "labelTrackers";
            // 
            // FormPrivacyInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelOf100);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormPrivacyInfo";
            this.Load += new System.EventHandler(this.FormPrivacyInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelPrivateSession;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelOf100;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelTrackers;
    }
}