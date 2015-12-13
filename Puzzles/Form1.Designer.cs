namespace Puzzles
{
    partial class Puzzles
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OrderedPictureBox = new System.Windows.Forms.PictureBox();
            this.UnorderPictureBox = new System.Windows.Forms.PictureBox();
            this.ClickLabal = new System.Windows.Forms.Label();
            this.Construct = new System.Windows.Forms.Button();
            this.Loading = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OrderedPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnorderPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // OrderedPictureBox
            // 
            this.OrderedPictureBox.Location = new System.Drawing.Point(493, 12);
            this.OrderedPictureBox.Name = "OrderedPictureBox";
            this.OrderedPictureBox.Size = new System.Drawing.Size(500, 500);
            this.OrderedPictureBox.TabIndex = 0;
            this.OrderedPictureBox.TabStop = false;
            // 
            // UnorderPictureBox
            // 
            this.UnorderPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UnorderPictureBox.Location = new System.Drawing.Point(10, 10);
            this.UnorderPictureBox.Name = "UnorderPictureBox";
            this.UnorderPictureBox.Size = new System.Drawing.Size(300, 300);
            this.UnorderPictureBox.TabIndex = 1;
            this.UnorderPictureBox.TabStop = false;
            this.UnorderPictureBox.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // ClickLabal
            // 
            this.ClickLabal.AutoSize = true;
            this.ClickLabal.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Italic);
            this.ClickLabal.ForeColor = System.Drawing.Color.Maroon;
            this.ClickLabal.Location = new System.Drawing.Point(63, 66);
            this.ClickLabal.Name = "ClickLabal";
            this.ClickLabal.Size = new System.Drawing.Size(164, 17);
            this.ClickLabal.TabIndex = 2;
            this.ClickLabal.Text = "Click here to load images";
            // 
            // Construct
            // 
            this.Construct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Construct.Location = new System.Drawing.Point(359, 105);
            this.Construct.Name = "Construct";
            this.Construct.Size = new System.Drawing.Size(150, 100);
            this.Construct.TabIndex = 3;
            this.Construct.Text = "Construct";
            this.Construct.UseVisualStyleBackColor = true;
            this.Construct.Visible = false;
            this.Construct.Click += new System.EventHandler(this.button1_Click);
            // 
            // Loading
            // 
            this.Loading.AutoSize = true;
            this.Loading.Font = new System.Drawing.Font("Meiryo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Loading.ForeColor = System.Drawing.Color.Maroon;
            this.Loading.Location = new System.Drawing.Point(411, 230);
            this.Loading.Name = "Loading";
            this.Loading.Size = new System.Drawing.Size(90, 24);
            this.Loading.TabIndex = 4;
            this.Loading.Text = "Loading...";
            this.Loading.Visible = false;
            // 
            // Puzzles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 459);
            this.Controls.Add(this.Loading);
            this.Controls.Add(this.Construct);
            this.Controls.Add(this.ClickLabal);
            this.Controls.Add(this.UnorderPictureBox);
            this.Controls.Add(this.OrderedPictureBox);
            this.Name = "Puzzles";
            this.Text = "Puzzles";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.OrderedPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnorderPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox OrderedPictureBox;
        private System.Windows.Forms.PictureBox UnorderPictureBox;
        private System.Windows.Forms.Label ClickLabal;
        private System.Windows.Forms.Button Construct;
        private System.Windows.Forms.Label Loading;
    }
}

