namespace Surfly
{
    partial class FormProfiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProfiles));
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCustomProfile = new System.Windows.Forms.TextBox();
            this.buttonCustomProfle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxCustomProfile
            // 
            resources.ApplyResources(this.textBoxCustomProfile, "textBoxCustomProfile");
            this.textBoxCustomProfile.Name = "textBoxCustomProfile";
            // 
            // buttonCustomProfle
            // 
            resources.ApplyResources(this.buttonCustomProfle, "buttonCustomProfle");
            this.buttonCustomProfle.Name = "buttonCustomProfle";
            this.buttonCustomProfle.UseVisualStyleBackColor = true;
            this.buttonCustomProfle.Click += new System.EventHandler(this.buttonCustomProfle_Click);
            // 
            // FormProfiles
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCustomProfle);
            this.Controls.Add(this.textBoxCustomProfile);
            this.Controls.Add(this.label1);
            this.Name = "FormProfiles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCustomProfile;
        private System.Windows.Forms.Button buttonCustomProfle;
    }
}