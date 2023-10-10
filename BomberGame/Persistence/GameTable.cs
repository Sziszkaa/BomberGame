using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game.BomberGame.Persistence
{
    public enum Direction { Right, Left, Up, Down }

    public class GameTable
    {
        #region Fields
        private int[,] _fieldValues;
        private Player _player;
        private List<Enemy> _enemies;
        private List<Bomb> _bombs;

        #endregion
        #region Properties

        public Player Player { get { return _player; } }

        public int Size { get { return _fieldValues.GetLength(0); } }

        public List<Enemy> Enemies { get { return _enemies; } }

        public Bomb Bomb { get { Bomb b = _bombs[0]; _bombs.RemoveAt(0); return b; } }

        public List<Bomb> Bombs {get{return _bombs;}}

        #endregion
        #region Constructor

        public GameTable(int tableSize)
        {
            if (tableSize < 0) throw new ArgumentOutOfRangeException(nameof(tableSize), "The table size is less than 0.");
            _fieldValues = new int[tableSize, tableSize];
            _player = new Player();
            _enemies = new List<Enemy>();
            _bombs = new List<Bomb>();
        }

        //Test
        public GameTable(int[,]fieldValues)
        {
            _player = new Player();
            _enemies = new List<Enemy>();
            _bombs = new List<Bomb>();
            _fieldValues = fieldValues;
            for (int i = 0; i < _fieldValues.GetLength(0); i++)
            {
                for (int j = 0; j < _fieldValues.GetLength(0); j++)
                {
                    if(_fieldValues[i, j] == 2)
                    {
                        _enemies.Add(new Enemy(i, j,Direction.Right));
                    }
                    else if(_fieldValues[i, j] == 3)
                    {
                        _bombs.Add(new Bomb(i, j));
                    }
                }
            }
        }

        #endregion
        #region Public methods

        public int GetValue(int x, int y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0)) throw new ArgumentOutOfRangeException(nameof(x), "The X cordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1)) throw new ArgumentOutOfRangeException(nameof(y), "The Y cordinate is out of range.");

            return _fieldValues[x, y];
        }

        public void SetValue(int x, int y, int value)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0)) throw new ArgumentOutOfRangeException(nameof(x), "The X cordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1)) throw new ArgumentOutOfRangeException(nameof(y), "The Y cordinate is out of range.");

            _fieldValues[x, y] = value;
        }   

        public void AddEnemy(int xposition,int yposition)
        {
            _enemies.Add(new Enemy(xposition, yposition, Direction.Right));
        }


        public void AddBomb(int xposition,int yposition)
        {
            _fieldValues[xposition, yposition] = 3;
            _bombs.Add(new Bomb(xposition, yposition));
        }

        #endregion
    }
}
