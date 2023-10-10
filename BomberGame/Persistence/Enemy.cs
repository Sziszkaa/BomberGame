  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.BomberGame.Persistence
{
    public class Enemy
    {
        #region Fields
        private int _xPosition;
        private int _yPosition;
        Direction _direction;
        #endregion
        #region Properties

        public int GetXPosition { get { return _xPosition; } }

        public int GetYPosition { get { return _yPosition; } }

        public Direction Direction { get { return _direction; } }

        #endregion
        #region Constructor

        public Enemy(int Xpostition,int Yposition,Direction driection)
        {
            _xPosition = Xpostition;
            _yPosition = Yposition;
            _direction = driection;
        }

        #endregion
        #region Private Methods

        public void MoveRight()
        {
            ++_yPosition;
        }

        public void MoveLeft()
        {
            --_yPosition;
        }

        public void MoveUp()
        {
            --_xPosition;
        }

        public void MoveDown()
        {
            ++_xPosition;
        }

        public void ChangeDirection()
        {
            Random random = new Random();
            Direction oldDirection = _direction;
            do
            {
                int randomNumber = random.Next(4);
                _direction = (Direction)randomNumber;
            } while (_direction == oldDirection);
        }

        #endregion
    }
}
