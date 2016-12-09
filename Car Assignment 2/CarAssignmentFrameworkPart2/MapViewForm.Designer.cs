namespace CarAssignmentFrameworkPart2
{
    partial class MapViewForm
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
            this.components = new System.ComponentModel.Container();
            this.tmrAutodrive = new System.Windows.Forms.Timer(this.components);
            this.tmrProjectiles = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrAutodrive
            // 
            this.tmrAutodrive.Interval = 200;
            this.tmrAutodrive.Tick += new System.EventHandler(this.tmrAutodrive_Tick);
            // 
            // tmrProjectiles
            // 
            this.tmrProjectiles.Interval = 20;
            this.tmrProjectiles.Tick += new System.EventHandler(this.tmrProjectiles_Tick);
            // 
            // MapViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 570);
            this.Name = "MapViewForm";
            this.Text = "Cars";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MapViewForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MapViewForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrAutodrive;
        private System.Windows.Forms.Timer tmrProjectiles;
    }
}

