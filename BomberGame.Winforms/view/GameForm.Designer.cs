namespace Game.BomberGame.View
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._menuGame = new System.Windows.Forms.ToolStripMenuItem();
            this._menuNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this._menuPauseGame = new System.Windows.Forms.ToolStripMenuItem();
            this._menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this._menuEasy = new System.Windows.Forms.ToolStripMenuItem();
            this._menuMedium = new System.Windows.Forms.ToolStripMenuItem();
            this._menuHard = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolLabelExplodedEnemies = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toollabelGameTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.gamePanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuGame,
            this._menuSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(982, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // _menuGame
            // 
            this._menuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuNewGame,
            this._menuPauseGame,
            this._menuExit});
            this._menuGame.Name = "_menuGame";
            this._menuGame.Size = new System.Drawing.Size(62, 24);
            this._menuGame.Text = "Game";
            // 
            // _menuNewGame
            // 
            this._menuNewGame.Name = "_menuNewGame";
            this._menuNewGame.Size = new System.Drawing.Size(209, 26);
            this._menuNewGame.Text = "New Game";
            this._menuNewGame.Click += new System.EventHandler(this.MenuNewGame_Click);
            // 
            // _menuPauseGame
            // 
            this._menuPauseGame.Name = "_menuPauseGame";
            this._menuPauseGame.Size = new System.Drawing.Size(209, 26);
            this._menuPauseGame.Text = "Pause/Start Game";
            this._menuPauseGame.Click += new System.EventHandler(this.MenuPauseGame_Click);
            // 
            // _menuExit
            // 
            this._menuExit.Name = "_menuExit";
            this._menuExit.Size = new System.Drawing.Size(209, 26);
            this._menuExit.Text = "Exit";
            this._menuExit.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // _menuSettings
            // 
            this._menuSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuEasy,
            this._menuMedium,
            this._menuHard});
            this._menuSettings.Name = "_menuSettings";
            this._menuSettings.Size = new System.Drawing.Size(76, 24);
            this._menuSettings.Text = "Settings";
            // 
            // _menuEasy
            // 
            this._menuEasy.Name = "_menuEasy";
            this._menuEasy.Size = new System.Drawing.Size(147, 26);
            this._menuEasy.Text = "Easy";
            this._menuEasy.Click += new System.EventHandler(this.MenuGameEasy_Click);
            // 
            // _menuMedium
            // 
            this._menuMedium.Name = "_menuMedium";
            this._menuMedium.Size = new System.Drawing.Size(147, 26);
            this._menuMedium.Text = "Medium";
            this._menuMedium.Click += new System.EventHandler(this.MenuGameMedium_Click);
            // 
            // _menuHard
            // 
            this._menuHard.Name = "_menuHard";
            this._menuHard.Size = new System.Drawing.Size(147, 26);
            this._menuHard.Text = "Hard";
            this._menuHard.Click += new System.EventHandler(this.MenuGameHard_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolLabelExplodedEnemies,
            this.toolStripStatusLabel3,
            this.toollabelGameTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 727);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(982, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(134, 20);
            this.toolStripStatusLabel1.Text = "Exploded Enemies:";
            // 
            // toolLabelExplodedEnemies
            // 
            this.toolLabelExplodedEnemies.Name = "toolLabelExplodedEnemies";
            this.toolLabelExplodedEnemies.Size = new System.Drawing.Size(17, 20);
            this.toolLabelExplodedEnemies.Text = "0";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(45, 20);
            this.toolStripStatusLabel3.Text = "Time:";
            // 
            // toollabelGameTime
            // 
            this.toollabelGameTime.Name = "toollabelGameTime";
            this.toollabelGameTime.Size = new System.Drawing.Size(55, 20);
            this.toollabelGameTime.Text = "0:00:00";
            // 
            // gamePanel
            // 
            this.gamePanel.Location = new System.Drawing.Point(5, 35);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(958, 684);
            this.gamePanel.TabIndex = 2;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.Controls.Add(this.gamePanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "BomberGame";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem _menuGame;
        private ToolStripMenuItem _menuNewGame;
        private ToolStripMenuItem _menuPauseGame;
        private ToolStripMenuItem _menuExit;
        private ToolStripMenuItem _menuSettings;
        private ToolStripMenuItem _menuEasy;
        private ToolStripMenuItem _menuMedium;
        private ToolStripMenuItem _menuHard;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolLabelExplodedEnemies;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toollabelGameTime;
        private Panel gamePanel;
    }
}