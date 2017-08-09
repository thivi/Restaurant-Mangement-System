namespace ResMan
{
    partial class addCat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(addCat));
            this.addc = new System.Windows.Forms.Button();
            this.catname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addc
            // 
            this.addc.BackColor = System.Drawing.Color.SeaGreen;
            this.addc.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
            this.addc.FlatAppearance.BorderSize = 0;
            this.addc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addc.ForeColor = System.Drawing.Color.White;
            this.addc.Location = new System.Drawing.Point(243, 122);
            this.addc.Name = "addc";
            this.addc.Size = new System.Drawing.Size(123, 33);
            this.addc.TabIndex = 2;
            this.addc.Text = "Add &Category";
            this.addc.UseVisualStyleBackColor = false;
            this.addc.Click += new System.EventHandler(this.addc_Click);
            // 
            // catname
            // 
            this.catname.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.catname.Location = new System.Drawing.Point(200, 62);
            this.catname.Name = "catname";
            this.catname.Size = new System.Drawing.Size(166, 30);
            this.catname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Category Name";
            // 
            // addCat
            // 
            this.AcceptButton = this.addc;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuPopup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(445, 212);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.catname);
            this.Controls.Add(this.addc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "addCat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Category";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button addc;
        private System.Windows.Forms.TextBox catname;
        private System.Windows.Forms.Label label1;
    }
}