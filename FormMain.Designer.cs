using System;

namespace NewPogodi
{
    partial class FormMain
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
            this.panelGame = new System.Windows.Forms.Panel();
            this.labelTip = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.pictureCatcher = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.panelMainMenu = new System.Windows.Forms.Panel();
            this.labelHighscoreDesc = new System.Windows.Forms.Label();
            this.labelHighscore = new System.Windows.Forms.Label();
            this.buttonAI = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.timerAI = new System.Windows.Forms.Timer(this.components);
            this.panelGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCatcher)).BeginInit();
            this.panelMainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelGame
            // 
            this.panelGame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGame.BackColor = System.Drawing.Color.White;
            this.panelGame.Controls.Add(this.labelTip);
            this.panelGame.Controls.Add(this.labelScore);
            this.panelGame.Controls.Add(this.pictureCatcher);
            this.panelGame.Location = new System.Drawing.Point(12, 12);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(773, 680);
            this.panelGame.TabIndex = 0;
            this.panelGame.Visible = false;
            this.panelGame.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGame_Paint);
            // 
            // labelTip
            // 
            this.labelTip.BackColor = System.Drawing.Color.Transparent;
            this.labelTip.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTip.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelTip.Location = new System.Drawing.Point(6, 254);
            this.labelTip.Name = "labelTip";
            this.labelTip.Size = new System.Drawing.Size(764, 75);
            this.labelTip.TabIndex = 2;
            this.labelTip.Text = "Подсказка";
            this.labelTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTip.Visible = false;
            // 
            // labelScore
            // 
            this.labelScore.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelScore.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.Location = new System.Drawing.Point(6, -15);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(767, 57);
            this.labelScore.TabIndex = 1;
            this.labelScore.Tag = "Untouched";
            this.labelScore.Text = "Тут будет показан счёт :)";
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureCatcher
            // 
            this.pictureCatcher.Image = global::NewPogodi.Properties.Resources.igra_nu_pogodi;
            this.pictureCatcher.Location = new System.Drawing.Point(0, 457);
            this.pictureCatcher.Name = "pictureCatcher";
            this.pictureCatcher.Size = new System.Drawing.Size(193, 176);
            this.pictureCatcher.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureCatcher.TabIndex = 0;
            this.pictureCatcher.TabStop = false;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 50;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // timerAnimation
            // 
            this.timerAnimation.Enabled = true;
            this.timerAnimation.Interval = 17;
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            // 
            // panelMainMenu
            // 
            this.panelMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMainMenu.BackgroundImage = global::NewPogodi.Properties.Resources.Nupogodi;
            this.panelMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMainMenu.Controls.Add(this.labelHighscoreDesc);
            this.panelMainMenu.Controls.Add(this.labelHighscore);
            this.panelMainMenu.Controls.Add(this.buttonAI);
            this.panelMainMenu.Controls.Add(this.buttonExit);
            this.panelMainMenu.Controls.Add(this.buttonNewGame);
            this.panelMainMenu.Location = new System.Drawing.Point(12, 12);
            this.panelMainMenu.Name = "panelMainMenu";
            this.panelMainMenu.Size = new System.Drawing.Size(773, 680);
            this.panelMainMenu.TabIndex = 1;
            // 
            // labelHighscoreDesc
            // 
            this.labelHighscoreDesc.BackColor = System.Drawing.Color.Transparent;
            this.labelHighscoreDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHighscoreDesc.Location = new System.Drawing.Point(-3, 74);
            this.labelHighscoreDesc.Name = "labelHighscoreDesc";
            this.labelHighscoreDesc.Size = new System.Drawing.Size(773, 34);
            this.labelHighscoreDesc.TabIndex = 2;
            this.labelHighscoreDesc.Tag = "";
            this.labelHighscoreDesc.Text = "Сможешь набрать больше?";
            this.labelHighscoreDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHighscore
            // 
            this.labelHighscore.BackColor = System.Drawing.Color.Transparent;
            this.labelHighscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHighscore.Location = new System.Drawing.Point(0, 32);
            this.labelHighscore.Name = "labelHighscore";
            this.labelHighscore.Size = new System.Drawing.Size(773, 42);
            this.labelHighscore.TabIndex = 2;
            this.labelHighscore.Tag = "Рекорд: {0}";
            this.labelHighscore.Text = "Рекорд: 0";
            this.labelHighscore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonAI
            // 
            this.buttonAI.BackColor = System.Drawing.Color.Transparent;
            this.buttonAI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAI.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAI.Location = new System.Drawing.Point(222, 356);
            this.buttonAI.Name = "buttonAI";
            this.buttonAI.Size = new System.Drawing.Size(312, 45);
            this.buttonAI.TabIndex = 1;
            this.buttonAI.Text = "Наблюдать за ИИ";
            this.buttonAI.UseVisualStyleBackColor = false;
            this.buttonAI.Click += new System.EventHandler(this.buttonAI_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(222, 399);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(312, 43);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Выход";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.BackColor = System.Drawing.Color.Transparent;
            this.buttonNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewGame.Location = new System.Drawing.Point(222, 225);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(312, 135);
            this.buttonNewGame.TabIndex = 0;
            this.buttonNewGame.Text = "Играть!";
            this.buttonNewGame.UseVisualStyleBackColor = false;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // timerAI
            // 
            this.timerAI.Tick += new System.EventHandler(this.timerAI_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 704);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelMainMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.Text = " ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.handleKeyDown);
            this.panelGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureCatcher)).EndInit();
            this.panelMainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.PictureBox pictureCatcher;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Panel panelMainMenu;
        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Timer timerAnimation;
        private System.Windows.Forms.Label labelTip;
        private System.Windows.Forms.Label labelHighscore;
        private System.Windows.Forms.Label labelHighscoreDesc;
        private System.Windows.Forms.Button buttonAI;
        private System.Windows.Forms.Timer timerAI;
    }
}

