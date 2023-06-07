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
            this.Auto = new System.Windows.Forms.Button();
            this.Multi = new System.Windows.Forms.Button();
            this.Single = new System.Windows.Forms.Button();
            this.SIMD = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fpsLabel
            // 
            this.fpsLabel.AutoSize = true;
            this.fpsLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fpsLabel.Location = new System.Drawing.Point(0, 946);
            this.fpsLabel.Margin = new System.Windows.Forms.Padding(10);
            this.fpsLabel.Name = "fpsLabel";
            this.fpsLabel.Size = new System.Drawing.Size(32, 15);
            this.fpsLabel.TabIndex = 0;
            this.fpsLabel.Text = "Hola";
            // 
            // Auto
            // 
            this.Auto.Location = new System.Drawing.Point(881, 897);
            this.Auto.Name = "Auto";
            this.Auto.Size = new System.Drawing.Size(91, 23);
            this.Auto.TabIndex = 1;
            this.Auto.Text = "AutoThread";
            this.Auto.UseVisualStyleBackColor = true;
            this.Auto.Click += new System.EventHandler(this.Auto_Click);
            // 
            // Multi
            // 
            this.Multi.Location = new System.Drawing.Point(881, 868);
            this.Multi.Name = "Multi";
            this.Multi.Size = new System.Drawing.Size(91, 23);
            this.Multi.TabIndex = 2;
            this.Multi.Text = "MultiThread";
            this.Multi.UseVisualStyleBackColor = true;
            this.Multi.Click += new System.EventHandler(this.Multi_Click);
            // 
            // Single
            // 
            this.Single.Location = new System.Drawing.Point(881, 839);
            this.Single.Name = "Single";
            this.Single.Size = new System.Drawing.Size(91, 23);
            this.Single.TabIndex = 3;
            this.Single.Text = "SingleThread";
            this.Single.UseVisualStyleBackColor = true;
            this.Single.Click += new System.EventHandler(this.Single_Click);
            // 
            // SIMD
            // 
            this.SIMD.Location = new System.Drawing.Point(881, 926);
            this.SIMD.Name = "SIMD";
            this.SIMD.Size = new System.Drawing.Size(91, 23);
            this.SIMD.TabIndex = 4;
            this.SIMD.Text = "SIMD";
            this.SIMD.UseVisualStyleBackColor = true;
            this.SIMD.Click += new System.EventHandler(this.SIMD_Click);
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 961);
            this.Controls.Add(this.SIMD);
            this.Controls.Add(this.Single);
            this.Controls.Add(this.Multi);
            this.Controls.Add(this.Auto);
            this.Controls.Add(this.fpsLabel);
            this.Name = "Simulation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Particle Life";
            this.Load += new System.EventHandler(this.Simulation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private Label fpsLabel;
        private Button Auto;
        private Button Multi;
        private Button Single;
        private Button SIMD;
    }
}