namespace Game.BomberGame.Persistence
{
    public class Player
    {
        #region Fields
        private int _xPosition;
        private int _yPosition;
        #endregion
        #region Properties

        public int GetXPosition { get { return _xPosition; }  }

        public int GetYPosition { get { return _yPosition; } }

        #endregion
        #region Constructor

        public Player()
        {
            _xPosition = 0;
            _yPosition = 0;
        }

        #endregion
        #region Public Methods

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


        public void Reset()
        {
            _xPosition = 0;
            _yPosition = 0;
        }

        #endregion
    }
}
