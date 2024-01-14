namespace ParticleLife
{
    partial class Simulation
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
            this.fpsLabel = new System.Windows.Forms.Label();
            this.Multi = new System.Windows.Forms.Button();
            this.Single = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpsLabel.Location = new System.Drawing.Point(0, 746);
            this.fpsLabel.Margin = new System.Windows.Forms.Padding(10);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(32, 15);
            this.fpsLabel.TabIndex = 0;
            this.fpsLabel.Text = "Hola";
            // 
            // Multi
            // 
            this.Multi.Location = new System.Drawing.Point(681, 726);
            this.Multi.Name = "Multi";
            this.Multi.Size = new System.Drawing.Size(91, 23);
            this.Multi.TabIndex = 1;
            this.Multi.Text = "MultiThread";
            this.Multi.UseVisualStyleBackColor = true;
            this.Multi.Click += new System.EventHandler(this.Auto_Click);
            // 
            // Single
            // 
            this.Single.Location = new System.Drawing.Point(681, 697);
            this.Single.Name = "Single";
            this.Single.Size = new System.Drawing.Size(91, 23);
            this.Single.TabIndex = 3;
            this.Single.Text = "SingleThread";
            this.Single.UseVisualStyleBackColor = true;
            this.Single.Click += new System.EventHandler(this.Single_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.WindowText;
            this.trackBar1.LargeChange = 4;
            this.trackBar1.Location = new System.Drawing.Point(668, 12);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(104, 45);
            this.trackBar1.TabIndex = 5;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.BackColor = System.Drawing.SystemColors.WindowText;
            this.trackBar2.Location = new System.Drawing.Point(668, 63);
            this.trackBar2.Minimum = 1;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(104, 45);
            this.trackBar2.TabIndex = 6;
            this.trackBar2.Value = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBar3
            // 
            this.trackBar3.BackColor = System.Drawing.SystemColors.WindowText;
            this.trackBar3.Location = new System.Drawing.Point(668, 114);
            this.trackBar3.Maximum = 100;
            this.trackBar3.Minimum = 1;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(104, 45);
            this.trackBar3.TabIndex = 7;
            this.trackBar3.Value = 10;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.Single);
            this.Controls.Add(this.Multi);
            this.Controls.Add(this.fpsLabel);
            this.Name = "Simulation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Particle Life";
            this.Load += new System.EventHandler(this.Simulation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Label fpsLabel;
        private Button Multi;
        private Button Single;
        private TrackBar trackBar1;
        private TrackBar trackBar2;
        private TrackBar trackBar3;
    }
}