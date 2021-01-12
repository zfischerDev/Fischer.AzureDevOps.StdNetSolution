
namespace Fischer.AzureDevOps.StdNetSolution.TestHarness
{
    partial class TestHarnessForm
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
            this.btnExecute01 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProjectPAToken = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProjectURI = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.adoMainTreeView = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExecute01
            // 
            this.btnExecute01.Location = new System.Drawing.Point(9, 21);
            this.btnExecute01.Name = "btnExecute01";
            this.btnExecute01.Size = new System.Drawing.Size(458, 36);
            this.btnExecute01.TabIndex = 0;
            this.btnExecute01.Text = "Get Results";
            this.btnExecute01.UseVisualStyleBackColor = true;
            this.btnExecute01.Click += new System.EventHandler(this.btnExecute01_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtProjectPAToken);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtProjectURI);
            this.groupBox1.Location = new System.Drawing.Point(10, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(941, 71);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // txtProjectPAToken
            // 
            this.txtProjectPAToken.Location = new System.Drawing.Point(480, 35);
            this.txtProjectPAToken.Name = "txtProjectPAToken";
            this.txtProjectPAToken.Size = new System.Drawing.Size(451, 22);
            this.txtProjectPAToken.TabIndex = 6;
            this.txtProjectPAToken.Text = "z6ompgpmhkd43yrcxj2pmo3lemszr5nkd3q37e3wzkoddkaoftqq";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Azure DevOps Project URI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(482, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(305, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Azure DevOps Project  Personal Access Token";
            // 
            // txtProjectURI
            // 
            this.txtProjectURI.Location = new System.Drawing.Point(8, 35);
            this.txtProjectURI.Name = "txtProjectURI";
            this.txtProjectURI.Size = new System.Drawing.Size(451, 22);
            this.txtProjectURI.TabIndex = 4;
            this.txtProjectURI.Text = "https://dev.azure.com/zfischer";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(482, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(446, 36);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // adoMainTreeView
            // 
            this.adoMainTreeView.Location = new System.Drawing.Point(8, 33);
            this.adoMainTreeView.Name = "adoMainTreeView";
            this.adoMainTreeView.Size = new System.Drawing.Size(923, 337);
            this.adoMainTreeView.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.adoMainTreeView);
            this.groupBox2.Location = new System.Drawing.Point(13, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(938, 386);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Click the button below to get the results";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnExecute01);
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Location = new System.Drawing.Point(13, 485);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(939, 70);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // TestHarnessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 571);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "TestHarnessForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExecute01;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProjectPAToken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProjectURI;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TreeView adoMainTreeView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

