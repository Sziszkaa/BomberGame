using Game.BomberGame.Persistence;
using BomberGame.Winforms;
using Game.BomberGame.Model;
//using Timer = System.Windows.Forms.Timer;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Game.BomberGame.View
{
    public partial class GameForm : Form
    {

        #region Fields 

        private BomberGameModel _model;
        private PictureBox[,] _grid;
        private Timer _timer;
        private Timer _enemyTimer;
        private Timer _bombTimer;

        #endregion
        #region Constructor

        public GameForm()
        {
            InitializeComponent();

            _model = new BomberGameModel();
            _model.GameAdvanced += new EventHandler<GameEventArgs>(Game_GameAdvanced);
            _model.GameOver += new EventHandler<GameEventArgs>(Game_GameOver);

            _timer = new Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);

            _enemyTimer = new Timer();
            _enemyTimer.Interval = 500;
            _enemyTimer.Elapsed += new ElapsedEventHandler(EnemyMove);

            _bombTimer = new Timer();
            _bombTimer.Interval = 100;
            _bombTimer.Elapsed += new ElapsedEventHandler(BombExplosion);

            SetupMenus();
            _model.NewGame();

            FormBorderStyle = FormBorderStyle.FixedDialog;
            GenerateTable();
            SetTable();

            _timer.Start();
            _enemyTimer.Start();
            _bombTimer.Start();
        }

        #endregion
        #region Game Event Handler

        private void Game_GameAdvanced(Object? sender, GameEventArgs e)
        {
            toollabelGameTime.Text = e.GameTime.ToString("g");
            toolLabelExplodedEnemies.Text = e.ExplodedEnemies.ToString();
        }

        private void Game_GameOver(Object? sender, GameEventArgs e)
        {
            _timer.Stop();
            _enemyTimer.Stop();
            _bombTimer.Stop();
            _menuPauseGame.Enabled = false;
            if (e.IsWon)
            {
                MessageBox.Show("Congratulation, you won!" + Environment.NewLine +
                               "You exploded " + e.ExplodedEnemies + " emeny and " +
                               "your playtime was: "+e.GameTime.ToString("g") + " second.",
                               "Bomber Game",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("You lost, You died!",
                                "Bomber Game",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }

        }

        #endregion
        #region Menu Event Handlers

        private void MenuNewGame_Click(Object sender, EventArgs e)
        {
            _menuPauseGame.Enabled = true;
            _model.NewGame();
            gamePanel.Controls.Clear();
            GenerateTable();
            SetTable();
            SetupMenus();

            _timer.Start();
            _enemyTimer.Start();
            _bombTimer.Start();
            _model.Running = true;
        }

        private void MenuPauseGame_Click(object sender, EventArgs e)
        {
            if(_model.Running==true)
            {
                _timer.Stop();
                _enemyTimer.Stop();
                _bombTimer.Stop();
                _model.Running = false;
            }
            else
            {
                _timer.Start();
                _enemyTimer.Start();
                _bombTimer.Start();
                _model.Running = true;
            }
        }

        private void MenuExit_Click(Object sender, EventArgs e)
        {
            bool restartTimer = _timer.Enabled;
            _timer.Stop();
            _enemyTimer.Stop();
            _bombTimer.Stop();

            if (MessageBox.Show("Are You sure you want to exit the game?","Bomber Game",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Close();
            }
            else
            {
                if (restartTimer)
                {
                    _timer.Start();
                    _enemyTimer.Start();
                    _bombTimer.Start();

                }
            }
        }

        private void MenuGameEasy_Click(Object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Easy;
        }

        private void MenuGameMedium_Click(Object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Medium;
        }

        private void MenuGameHard_Click(Object sender, EventArgs e)
        {
            _model.GameDifficulty = GameDifficulty.Hard;
        }
        #endregion
        #region Time Events Handlers

        private void Timer_Elapsed(Object? sender,EventArgs e)
        {
            _model.AdvanceTime();
        }

        private void BombExplosion(Object? sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
            for(int i= 0;i < _model.Table.Bombs.Count;i++)
                {
                    _model.Table.Bombs[i].TimePassed();
                    if (_model.Table.Bombs[i].GetTime == 50)
                    {
                        List<int> explodedPoints = _model.BombExplosion();

                        _grid[explodedPoints[0], explodedPoints[1]].BackgroundImage = null;
                        for (int j = 2; j < explodedPoints.Count; j += 2)
                        {
                            _grid[explodedPoints[j], explodedPoints[j + 1]].Image = null;
                            _grid[explodedPoints[j], explodedPoints[j + 1]].BackgroundImage = Pictures.blood;
                        }
                    }
                }
            }));
        }

        private void EnemyMove(Object? sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                List<int> beforeLocation = new List<int>();
                List<int> afterLocation = new List<int>();
                _model.EnemyMove(beforeLocation, afterLocation);
                int j = 0;
                for (int i = 0; i < _model.Table.Enemies.Count; i++)
                {
                    _grid[beforeLocation[j], beforeLocation[j + 1]].Image = null;
                    switch (_model.Table.Enemies[i].Direction)
                    {
                        case Direction.Right: _grid[afterLocation[j], afterLocation[j + 1]].Image = Pictures.enemy_right; break;
                        case Direction.Left: _grid[afterLocation[j], afterLocation[j + 1]].Image = Pictures.enemy_left; break;
                        case Direction.Up: _grid[afterLocation[j], afterLocation[j + 1]].Image = Pictures.enemy_up; break;
                        case Direction.Down: _grid[afterLocation[j], afterLocation[j + 1]].Image = Pictures.enemy_down; break;
                    }
                    j += 2;
                }
            }));
        }

        #endregion
        #region KeyDown Event Handler

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    if (_model.PlayerMove(Direction.Right))
                    {
                        _grid[_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition - 1].Image = null;
                        _grid[_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition].Image = Pictures.player_right;
                    }
                    break;
                case Keys.Left:
                    if (_model.PlayerMove(Direction.Left))
                    {
                        _grid[_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition + 1].Image = null;
                        _grid[_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition].Image = Pictures.player_left;
                    }
                    break;
                case Keys.Up:
                    if (_model.PlayerMove(Direction.Up))
                    {
                        _grid[_model.Table.Player.GetXPosition + 1, _model.Table.Player.GetYPosition].Image = null;
                        _grid[_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition].Image = Pictures.player_up;
                    }
                    break;
                case Keys.Down:
                    if (_model.PlayerMove(Direction.Down))
                    {
                        _grid[_model.Table.Player.GetXPosition - 1, _model.Table.Player.GetYPosition].Image = null;
                        _grid[_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition].Image = Pictures.player_down;
                    }
                    break;
                case Keys.B:
                    if (_model.PlantBomb(_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition))
                    {
                        _grid[_model.Table.Player.GetXPosition, _model.Table.Player.GetYPosition].BackgroundImage = Pictures.bomb;
                    }
                    break;
            }
        }

            #endregion
            #region Private methods

        private void GenerateTable()
        {
            switch (_model.GameDifficulty)
            {
                case GameDifficulty.Easy:
                    Size=new Size(480, 560);
                    gamePanel.Size = new Size(450, 450);
                    break;
                case GameDifficulty.Medium:
                    Size = new Size(630, 720);
                    gamePanel.Size = new Size(600, 600);
                    break;
                case GameDifficulty.Hard:
                    Size = new Size(780, 870);
                    gamePanel.Size = new Size(750, 750);
                    break;
            }
            _grid = new PictureBox[_model.Table.Size, _model.Table.Size];
            for (int i = 0; i < _model.Table.Size; i++)
            {
                for (int j = 0; j < _model.Table.Size; j++)
                {
                    _grid[i, j] = new PictureBox();
                    _grid[i,j].Location = new Point(30*j,30*i);
                    _grid[i, j].Size = new Size(30, 30);
                    _grid[i, j].SizeMode = PictureBoxSizeMode.StretchImage;
                    _grid[i, j].BackColor = Color.Transparent;
                    _grid[i, j].BorderStyle = BorderStyle.FixedSingle;
                    _grid[i,j].BackgroundImageLayout = ImageLayout.Stretch;
                    gamePanel.Controls.Add(_grid[i, j]);
                }
            }
        }

        private void SetTable()
        {
            for (int i = 0; i < _model.Table.Size; i++)
            {
                for(int j=0;j < _model.Table.Size; j++)
                {
                    if(_model.Table.GetValue(i,j)==9)
                    {
                        _grid[i, j].Image = Pictures.wall;
                    }
                    else if(_model.Table.GetValue(i,j)==2)
                    {
                        _grid[i, j].Image = Pictures.enemy_right;
                    }
                    else if(_model.Table.GetValue(i,j)==1)
                    {
                        _grid[i, j].Image = Pictures.player_right;
                    }
                }
            }
        }

        private void SetupMenus()
        {
            _menuEasy.Checked = (_model.GameDifficulty == GameDifficulty.Easy);
            _menuMedium.Checked = (_model.GameDifficulty == GameDifficulty.Medium);
            _menuHard.Checked = (_model.GameDifficulty == GameDifficulty.Hard);
        }

        #endregion
    }
}