
namespace MyFirstNXManager
{
    partial class NXManagerDemo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NewPartNoButton = new System.Windows.Forms.Button();
            this.OpenItemButton = new System.Windows.Forms.Button();
            this.CreateItemButton = new System.Windows.Forms.Button();
            this.PartNameInput = new System.Windows.Forms.TextBox();
            this.PartNoInput = new System.Windows.Forms.TextBox();
            this.PartRevInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.QueryInput = new System.Windows.Forms.TextBox();
            this.QueryButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.NewPartNoButton);
            this.groupBox1.Controls.Add(this.OpenItemButton);
            this.groupBox1.Controls.Add(this.CreateItemButton);
            this.groupBox1.Controls.Add(this.PartNameInput);
            this.groupBox1.Controls.Add(this.PartNoInput);
            this.groupBox1.Controls.Add(this.PartRevInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Part Util";
            // 
            // NewPartNoButton
            // 
            this.NewPartNoButton.Location = new System.Drawing.Point(10, 45);
            this.NewPartNoButton.Name = "NewPartNoButton";
            this.NewPartNoButton.Size = new System.Drawing.Size(77, 23);
            this.NewPartNoButton.TabIndex = 6;
            this.NewPartNoButton.Text = "New Part No";
            this.NewPartNoButton.UseVisualStyleBackColor = true;
            this.NewPartNoButton.Click += new System.EventHandler(this.NewPartNoButton_Click);
            // 
            // OpenItemButton
            // 
            this.OpenItemButton.Location = new System.Drawing.Point(189, 45);
            this.OpenItemButton.Name = "OpenItemButton";
            this.OpenItemButton.Size = new System.Drawing.Size(89, 23);
            this.OpenItemButton.TabIndex = 5;
            this.OpenItemButton.Text = "Open";
            this.OpenItemButton.UseVisualStyleBackColor = true;
            this.OpenItemButton.Click += new System.EventHandler(this.OpenItemButton_Click);
            // 
            // CreateItemButton
            // 
            this.CreateItemButton.Location = new System.Drawing.Point(93, 45);
            this.CreateItemButton.Name = "CreateItemButton";
            this.CreateItemButton.Size = new System.Drawing.Size(90, 23);
            this.CreateItemButton.TabIndex = 4;
            this.CreateItemButton.Text = "Create!";
            this.CreateItemButton.UseVisualStyleBackColor = true;
            this.CreateItemButton.Click += new System.EventHandler(this.CreateItemButton_Click);
            // 
            // PartNameInput
            // 
            this.PartNameInput.Location = new System.Drawing.Point(136, 18);
            this.PartNameInput.Name = "PartNameInput";
            this.PartNameInput.Size = new System.Drawing.Size(128, 20);
            this.PartNameInput.TabIndex = 3;
            // 
            // PartNoInput
            // 
            this.PartNoInput.Location = new System.Drawing.Point(9, 19);
            this.PartNoInput.Name = "PartNoInput";
            this.PartNoInput.Size = new System.Drawing.Size(78, 20);
            this.PartNoInput.TabIndex = 1;
            // 
            // PartRevInput
            // 
            this.PartRevInput.Location = new System.Drawing.Point(93, 19);
            this.PartRevInput.Name = "PartRevInput";
            this.PartRevInput.Size = new System.Drawing.Size(36, 20);
            this.PartRevInput.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(278, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "CheckTCInfoButton";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CheckTCInfoButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.QueryButton);
            this.groupBox2.Controls.Add(this.QueryInput);
            this.groupBox2.Location = new System.Drawing.Point(12, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 102);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Query";
            // 
            // QueryInput
            // 
            this.QueryInput.Location = new System.Drawing.Point(10, 19);
            this.QueryInput.Name = "QueryInput";
            this.QueryInput.Size = new System.Drawing.Size(148, 20);
            this.QueryInput.TabIndex = 0;
            // 
            // QueryButton
            // 
            this.QueryButton.Location = new System.Drawing.Point(164, 17);
            this.QueryButton.Name = "QueryButton";
            this.QueryButton.Size = new System.Drawing.Size(75, 23);
            this.QueryButton.TabIndex = 1;
            this.QueryButton.Text = "Query";
            this.QueryButton.UseVisualStyleBackColor = true;
            this.QueryButton.Click += new System.EventHandler(this.QueryButton_Click);
            // 
            // NXManagerDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(309, 245);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "NXManagerDemo";
            this.Text = "NXManagerDemo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CreateItemButton;
        private System.Windows.Forms.TextBox PartNameInput;
        private System.Windows.Forms.TextBox PartNoInput;
        private System.Windows.Forms.TextBox PartRevInput;
        private System.Windows.Forms.Button OpenItemButton;
        private System.Windows.Forms.Button NewPartNoButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button QueryButton;
        private System.Windows.Forms.TextBox QueryInput;
    }
}