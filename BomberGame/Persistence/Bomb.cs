using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.BomberGame.Persistence
{
    public class Bomb
    {
        #region Fileds

        int _xPosition;
        int _yPosition;
        int _time;

        #endregion
        #region Proerties

        public int GetXPosition { get { return _xPosition; }}

        public int GetYPosition { get { return _yPosition; }}

        public int GetTime { get { return _time; } }

        #endregion
        #region Constructor

        public Bomb(int xposition,int yposition)
        {
            _xPosition = xposition;
            _yPosition = yposition;
            _time = 0;
        }

        #endregion
        #region Public Methods

        public void TimePassed()
        {
            ++_time;
        }

        #endregion
    }
}
