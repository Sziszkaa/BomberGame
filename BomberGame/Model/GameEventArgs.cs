using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.BomberGame.Model
{
    public class GameEventArgs : EventArgs
    {
        private bool _isWon;
        private int _explodedEnemies;
        private int _gameTime;

        public bool IsWon { get { return _isWon; } }

        public int ExplodedEnemies { get { return _explodedEnemies; } }

        public int GameTime { get { return _gameTime; } }

        public GameEventArgs(bool isWon,int explodedEnemies, int gameTime)
        {
            _isWon = isWon;
            _explodedEnemies = explodedEnemies;
            _gameTime = gameTime;
        }
    }
}
