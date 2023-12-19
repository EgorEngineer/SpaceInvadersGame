using System.Drawing;

namespace SpaceInvaders
{
    partial class Client
    {
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Client));
            this.scoreLabel = new System.Windows.Forms.Label();
            this.numberOfLivesLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.PauseLabel = new System.Windows.Forms.Label();
            this.RestartLabel = new System.Windows.Forms.Label();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.BackColor = System.Drawing.Color.Transparent;
            this.scoreLabel.Font = new System.Drawing.Font("Russo One", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreLabel.ForeColor = System.Drawing.Color.Red;
            this.scoreLabel.Location = new System.Drawing.Point(9, 63);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Padding = new System.Windows.Forms.Padding(17, 16, 17, 16);
            this.scoreLabel.Size = new System.Drawing.Size(314, 61);
            this.scoreLabel.TabIndex = 3;
            this.scoreLabel.Text = "Количество очков = 0";
            // 
            // numberOfLivesLabel
            // 
            this.numberOfLivesLabel.AutoSize = true;
            this.numberOfLivesLabel.BackColor = System.Drawing.Color.Transparent;
            this.numberOfLivesLabel.Font = new System.Drawing.Font("Russo One", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfLivesLabel.ForeColor = System.Drawing.Color.Yellow;
            this.numberOfLivesLabel.Location = new System.Drawing.Point(12, 9);
            this.numberOfLivesLabel.Name = "numberOfLivesLabel";
            this.numberOfLivesLabel.Padding = new System.Windows.Forms.Padding(17, 16, 17, 16);
            this.numberOfLivesLabel.Size = new System.Drawing.Size(171, 61);
            this.numberOfLivesLabel.TabIndex = 2;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            //
            //HelpLabel
            //
            this.HelpLabel.AutoSize = true;
            this.HelpLabel.BackColor = System.Drawing.Color.Transparent;
            this.HelpLabel.Font = new System.Drawing.Font("Russo One", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpLabel.ForeColor = System.Drawing.Color.LightGreen;
            this.HelpLabel.Location = new System.Drawing.Point(250, 150);
            this.HelpLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.HelpLabel.Name = "HelpLabel";
            this.HelpLabel.Padding = new System.Windows.Forms.Padding(24);
            this.HelpLabel.Size = new System.Drawing.Size(500, 120);
            this.HelpLabel.TabIndex = 1;
            this.HelpLabel.Text = "                      SPACE CONQUERORS\n\n\n ПЕРЕМЕЩЕНИЕ - A(ВЛЕВО) D(ВПРАВО)\n ПРОБЕЛ - выстрел торпедой,  E - выстрел ракетой\n ESC - меню паузы" +
                "\n\nДля изменения разрешения нажмите клавиши\n                        PG UP или PG DN";
            // 
            // PauseLabel
            // 
            this.PauseLabel.AutoSize = true;
            this.PauseLabel.BackColor = System.Drawing.Color.Transparent;
            this.PauseLabel.Font = new System.Drawing.Font("Russo One", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseLabel.ForeColor = System.Drawing.Color.LightGreen;
            this.PauseLabel.Location = new System.Drawing.Point(300, 230);
            this.PauseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PauseLabel.Name = "PauseLabel";
            this.PauseLabel.Padding = new System.Windows.Forms.Padding(24);
            this.PauseLabel.Size = new System.Drawing.Size(500, 120);
            this.PauseLabel.TabIndex = 1;
            // 
            // RestartLabel
            // 
            this.RestartLabel.AutoSize = true;
            this.RestartLabel.BackColor = System.Drawing.Color.Transparent;
            this.RestartLabel.Font = new System.Drawing.Font("Russo One", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RestartLabel.ForeColor = System.Drawing.Color.LightGreen;
            this.RestartLabel.Location = new System.Drawing.Point(320, 280);
            this.RestartLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RestartLabel.Name = "RestartLabel";
            this.RestartLabel.Padding = new System.Windows.Forms.Padding(24);
            this.RestartLabel.Size = new System.Drawing.Size(500, 120);
            this.RestartLabel.TabIndex = 3;
            this.RestartLabel.Text = "ИГРА ЗАВЕРШЕНА! НАЖМИТЕ ENTER ДЛЯ ПЕРЕЗАПУСКА...";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SpaceInvaders.Properties.Resources.backspace;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1396, 703);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.numberOfLivesLabel);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Client";
            this.Text = "Space Conquerors";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label numberOfLivesLabel;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label PauseLabel;
        private System.Windows.Forms.Label RestartLabel;
        private System.Windows.Forms.Label HelpLabel;
    }
}

