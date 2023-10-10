using Game.BomberGame.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.BomberGame.Model
{

    public enum GameDifficulty { Easy, Medium, Hard}

    public class BomberGameModel
    {
        #region Fields

        private BomberFileDataAccess _dataAccess;
        private GameDifficulty _gameDifficulty;
        private GameTable _table;
        private int _gameTime;
        private int _explodedEnemies;
        private bool running;

        #endregion
        #region Properties

        public GameTable Table { get { return _table; } }

        public GameDifficulty GameDifficulty { get { return _gameDifficulty;} set { _gameDifficulty = value; } }

        public int GameTime { get { return _gameTime; } }

        public bool Running { get { return running; } set { running = value; } }

        public int ExplodedEnemies { get { return _explodedEnemies; } }

        #endregion
        #region Events

        public event EventHandler<GameEventArgs>? GameAdvanced;
        public event EventHandler<GameEventArgs>? GameOver;

        #endregion
        #region Constructor

        public BomberGameModel()
        {
            _gameDifficulty = GameDifficulty.Easy;
            _dataAccess = new BomberFileDataAccess();
            _gameTime = 0;
            _explodedEnemies = 0;
            running = true;
        }

        public BomberGameModel(GameTable table)
        {
            _table =table;
            _gameTime =0;
            _explodedEnemies=0;
            running = true;
        }

        #endregion
        #region Public Game methods

        public async Task NewGame()
        {
            _gameTime = 0;
            _explodedEnemies = 0;
            switch (_gameDifficulty)
            {
                case GameDifficulty.Easy:
                    _table = await _dataAccess.LoadAsync("easygame_table.txt");
                    _table.Player.Reset();
                    break;
                case GameDifficulty.Medium:
                    _table = await _dataAccess.LoadAsync("mediumgame_table.txt");
                    _table.Player.Reset();
                    break;
                case GameDifficulty.Hard:
                    _table = await _dataAccess.LoadAsync("hardgame_table.txt");
                    _table.Player.Reset();
                    break;
            }
        }

        public void AdvanceTime()
        {
            _gameTime++;
            OnGameAdvanced();
        }

        public bool PlayerMove(Direction d)
        {
            if (!running) return false;

            if (d==Direction.Right && _table.Player.GetYPosition < _table.Size - 1)
            {
                int nextPos = _table.GetValue(_table.Player.GetXPosition, _table.Player.GetYPosition + 1);
                if (nextPos!= 9 && nextPos != 3)
                {
                    if (_table.GetValue(_table.Player.GetXPosition, _table.Player.GetYPosition) != 3)
                        _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 0);
                    _table.Player.MoveRight();
                    _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 1);
                    if (SamePosition()) OnGameOver(false);
                    return true;
                }
            }
            else if (d ==Direction.Left && _table.Player.GetYPosition > 0)
            {
                int nextPos = _table.GetValue(_table.Player.GetXPosition, _table.Player.GetYPosition - 1);
                if (nextPos != 9 && nextPos != 3 )
                {
                    if (_table.GetValue(_table.Player.GetXPosition, _table.Player.GetYPosition) != 3)
                        _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 0);
                    _table.Player.MoveLeft();
                    _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 1);
                    if (SamePosition()) OnGameOver(false);
                    return true;
                }
            }
            else if (d == Direction.Up && _table.Player.GetXPosition > 0)
            {
                int nextPos = _table.GetValue(_table.Player.GetXPosition - 1, _table.Player.GetYPosition);
                if (nextPos != 9 && nextPos != 3 )
                {
                    if (_table.GetValue(_table.Player.GetXPosition, _table.Player.GetYPosition) != 3)
                        _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 0);
                    _table.Player.MoveUp();
                    _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 1);
                    if (SamePosition()) OnGameOver(false);
                    return true;
                }
            }
            else if(d == Direction.Down && _table.Player.GetXPosition < _table.Size - 1)
            {
                int nextPos = _table.GetValue(_table.Player.GetXPosition + 1, _table.Player.GetYPosition);
                if (nextPos != 9 && nextPos != 3)
                {
                    if(_table.GetValue(_table.Player.GetXPosition, _table.Player.GetYPosition) !=3)
                    _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 0);
                    _table.Player.MoveDown();
                    _table.SetValue(_table.Player.GetXPosition, _table.Player.GetYPosition, 1);
                    if (SamePosition()) OnGameOver(false);
                    return true;
                }
            }
            return false;
        }

        public void EnemyMove(List<int> beforeLocations, List<int> afterLocations)
        {
            bool moved = false;
            for (int i = 0; i < _table.Enemies.Count; i++)
            {
                beforeLocations.Add(_table.Enemies[i].GetXPosition);
                beforeLocations.Add(_table.Enemies[i].GetYPosition);
                if (_table.Enemies[i].Direction == Direction.Right &&
                    _table.Enemies[i].GetYPosition < _table.Size - 1)
                {
                    int nextPos = _table.GetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition + 1);
                    if (nextPos != 9 && nextPos != 2 && nextPos != 3)
                    {
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 0);
                        _table.Enemies[i].MoveRight();
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 2);
                        moved = true;
                    }
                }
                else if (_table.Enemies[i].Direction == Direction.Left &&
                    _table.Enemies[i].GetYPosition > 0)
                {
                    int nextPos = _table.GetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition - 1);
                    if (nextPos!= 9 && nextPos!= 2 && nextPos != 3)
                    {
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 0);
                        _table.Enemies[i].MoveLeft();
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 2);
                        moved = true;
                    }
                }
                else if (_table.Enemies[i].Direction == Direction.Up &&
                    _table.Enemies[i].GetXPosition > 0)
                {
                    int nextPos = _table.GetValue(_table.Enemies[i].GetXPosition - 1, _table.Enemies[i].GetYPosition);
                    if (nextPos!= 9 && nextPos != 2 && nextPos != 3)
                    {
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 0);
                        _table.Enemies[i].MoveUp();
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 2);
                        moved = true;
                    }
                }
                else if (_table.Enemies[i].Direction == Direction.Down &&
                    _table.Enemies[i].GetXPosition < _table.Size - 1)
                {
                    int nextPos = _table.GetValue(_table.Enemies[i].GetXPosition + 1, _table.Enemies[i].GetYPosition);
                    if (nextPos!= 9 && nextPos!= 2 && nextPos != 3)
                    {
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 0);
                        _table.Enemies[i].MoveDown();
                        _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 2);
                        moved = true;
                    }
                }
                if (!moved) _table.Enemies[i].ChangeDirection();
                afterLocations.Add(_table.Enemies[i].GetXPosition);
                afterLocations.Add(_table.Enemies[i].GetYPosition);
                moved = false;
            }
            if (SamePosition()) OnGameOver(false);
        }

        public bool PlantBomb(int xposition,int yPosition)
        {
            if (!running) return false;
            if (_table.GetValue(xposition,yPosition)!=3)
            _table.AddBomb(xposition, yPosition);
            return true;
   
            return false;
        }

        public List<int> BombExplosion()
        {
            Bomb bomb = _table.Bomb;
            _table.SetValue(bomb.GetXPosition, bomb.GetYPosition, 0);
            List<int> explodedPoints = new List<int>();
            explodedPoints.Add(bomb.GetXPosition);
            explodedPoints.Add(bomb.GetYPosition);
            int startX = bomb.GetXPosition - 3;
            int endX = bomb.GetXPosition + 3;
            int startY = bomb.GetYPosition - 3;
            int endY = bomb.GetYPosition + 3;

            for (int i = 0; i < _table.Enemies.Count; i++)
            {
                if (_table.Player.GetXPosition >= startX && _table.Player.GetXPosition <= endX &&
                    _table.Player.GetYPosition >= startY && _table.Player.GetYPosition <= endY)
                {
                    explodedPoints.Add(_table.Player.GetXPosition);
                    explodedPoints.Add(_table.Player.GetYPosition);
                    OnGameOver(false);
                    return explodedPoints;
                }
                if (_table.Enemies[i].GetXPosition >= startX && _table.Enemies[i].GetXPosition <= endX &&
                    _table.Enemies[i].GetYPosition >= startY && _table.Enemies[i].GetYPosition <= endY)
                {
                    explodedPoints.Add(_table.Enemies[i].GetXPosition);
                    explodedPoints.Add(_table.Enemies[i].GetYPosition);
                    _table.SetValue(_table.Enemies[i].GetXPosition, _table.Enemies[i].GetYPosition, 0);
                    EnemyDeath(_table.Enemies[i]);
                    if (_table.Enemies.Count == 0) OnGameOver(true);
                }
            }
            OnGameAdvanced();
            return explodedPoints;
        }

        #endregion
        #region Private Game Methods

        private bool SamePosition()
        {
            for(int i= 0; i < _table.Enemies.Count; i++)
            {
                if(_table.Player.GetXPosition == _table.Enemies[i].GetXPosition &&
                   _table.Player.GetYPosition == _table.Enemies[i].GetYPosition)
                {
                    return true;
                }
            }
            return false;
        }

        private void EnemyDeath(Enemy enemy)
        {
            _table.Enemies.Remove(enemy);
            _explodedEnemies++;
        }

        #endregion
        #region Private Events Methods

        private void OnGameAdvanced()
        {
            GameAdvanced?.Invoke(this, new GameEventArgs(false,_explodedEnemies,GameTime));
        }

        private void OnGameOver(bool isWon)
        {
            running = false;
            GameOver?.Invoke(this, new GameEventArgs(isWon,_explodedEnemies,GameTime));
        }

        #endregion

    }
}
