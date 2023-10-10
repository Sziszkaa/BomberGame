using Game.BomberGame.Model;
using Game.BomberGame.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BomberGameTest
{
    [TestClass]
    public class BomberGameModelTest
    {
        private BomberGameModel _model=null!;
        private GameTable _mockedGameTable=null!;
        private int[,] _fieldValues = null!;

        [TestInitialize]
        public void Initialize()
        {
            int[,] fieldValues = new int[10,10] 
            {
                {1,0,0,9,0,0,0,0,0,0},
                {0,0,0,9,0,0,0,0,0,0},
                {2,0,0,9,0,0,0,0,0,0},
                {0,0,0,9,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0}
            };
            _mockedGameTable = new GameTable(fieldValues);

           /*_mock = new Mock<BomberFileDataAccess>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                .Returns(() => Task.FromResult(_mockedGameTable));

            _model=new BomberGameModel(_mock.Object);*/

            _model = new BomberGameModel();

            _model.GameAdvanced += new EventHandler<GameEventArgs>(Model_GameAdvanced);
            _model.GameOver += new EventHandler<GameEventArgs>(Model_GameOver);

        }

        [TestMethod]
        public void BomberGameModelNewGameEasyTest()
        {
            _model.NewGame();

            Assert.AreEqual(1, _model.Table.GetValue(0, 0));
            Assert.AreEqual(2, _model.Table.GetValue(11,0));
            Assert.AreEqual(9, _model.Table.GetValue(0, 7));
            Assert.AreEqual(0, _model.GameTime);
            Assert.AreEqual(0, _model.ExplodedEnemies);
            Assert.AreEqual(GameDifficulty.Easy,_model.GameDifficulty);
            Assert.AreEqual(3, _model.Table.Enemies.Count);
            Assert.AreEqual(_model.Running, true);
        }

        [TestMethod]
        public void BomberGameModelNewGameMediumTest()
        {
            _model.GameDifficulty=GameDifficulty.Medium;
            _model.NewGame();

            Assert.AreEqual(1, _model.Table.GetValue(0, 0));
            Assert.AreEqual(2, _model.Table.GetValue(0, 7));
            Assert.AreEqual(9, _model.Table.GetValue(3, 0));
            Assert.AreEqual(0, _model.GameTime);
            Assert.AreEqual(0, _model.ExplodedEnemies);
            Assert.AreEqual(GameDifficulty.Medium, _model.GameDifficulty);
            Assert.AreEqual(6, _model.Table.Enemies.Count);
            Assert.AreEqual(_model.Running, true);
        }

        [TestMethod]
        public void BomberGameModelNewGameHardTest()
        {
            _model.GameDifficulty = GameDifficulty.Hard;
            _model.NewGame();

            Assert.AreEqual(1, _model.Table.GetValue(0, 0));
            Assert.AreEqual(2, _model.Table.GetValue(0, 8));
            Assert.AreEqual(9, _model.Table.GetValue(4, 0));
            Assert.AreEqual(0, _model.GameTime);
            Assert.AreEqual(0, _model.ExplodedEnemies);
            Assert.AreEqual(GameDifficulty.Hard, _model.GameDifficulty);
            Assert.AreEqual(9, _model.Table.Enemies.Count);
            Assert.AreEqual(_model.Running, true);
        }


        [TestMethod]
        public void BomberGameModelPlayerMoveTest()
        {
            _model = new BomberGameModel(_mockedGameTable);
            _model.PlayerMove(Direction.Up);
            Assert.AreEqual(0, _model.Table.Player.GetXPosition);
            _model.PlayerMove(Direction.Left);
            Assert.AreEqual(0, _model.Table.Player.GetYPosition);

            for(int i=0;i<3;i++)
            {
                _model.PlayerMove(Direction.Right);
            }
            Assert.AreEqual(2, _model.Table.Player.GetYPosition);
            Assert.AreEqual(1, _model.Table.GetValue(0,2));
            _model.PlayerMove(Direction.Down);
            Assert.AreEqual(1, _model.Table.Player.GetXPosition);
            _model.PlayerMove(Direction.Up);
            Assert.AreEqual(0, _model.Table.Player.GetXPosition);
            _model.PlayerMove(Direction.Left);
            Assert.AreEqual(1, _model.Table.Player.GetYPosition);
        }

        [TestMethod]
        public void BomberGameModelEnemyMoveTest()
        {
            _model = new BomberGameModel(_mockedGameTable);
            List<int> beforeLocations = new List<int>();
            List<int> afterLocations = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                _model.EnemyMove(beforeLocations, afterLocations);
            }
            Assert.AreEqual(2,_model.Table.Enemies[0].GetYPosition);
            Assert.AreEqual(2, _model.Table.GetValue(2,2));
            Assert.AreNotEqual(Direction.Right,_model.Table.Enemies[0].Direction);
            Direction d = _model.Table.Enemies[0].Direction;
            _model.Table.SetValue(2, 1, 9);
            if(d== Direction.Left)
            {
                _model.EnemyMove(beforeLocations, afterLocations);
                Assert.AreNotEqual(Direction.Left, _model.Table.Enemies[0].Direction);
            }
            else
            {
                _model.EnemyMove(beforeLocations, afterLocations);
                Assert.AreNotEqual(2, _model.Table.Enemies[0].GetXPosition);
            }
        }

        [TestMethod]
        public void BomberGameModelPlantAndExplodeBombTest()
        {
            _mockedGameTable.Enemies.Add(new Enemy(8, 5,Direction.Right));
            _model = new BomberGameModel(_mockedGameTable);
            _model.PlantBomb(5, 0);
            _model.PlantBomb(8, 8);
            Assert.AreEqual(3, _model.Table.GetValue(5,0));
            Assert.AreEqual(3, _model.Table.GetValue(8, 8));
            _model.BombExplosion();
            Assert.AreEqual(1, _model.Table.Enemies.Count);
            Assert.AreEqual(1, _model.Table.Bombs.Count);
            Assert.AreEqual(1, _model.ExplodedEnemies);
            _model.BombExplosion();
            Assert.AreEqual(0, _model.Table.Enemies.Count);
            Assert.AreEqual(0, _model.Table.Bombs.Count);
            Assert.AreEqual(2, _model.ExplodedEnemies);
        }

        [TestMethod]
        public void BomberGameModelAdvancedTimeTest()
        {
            for (int i = 1; i <= 5; i++)
            {
                _model.AdvanceTime();
                Assert.AreEqual(i, _model.GameTime);
            }
        }

        private void Model_GameAdvanced(object? sender, GameEventArgs e)
        {
            Assert.AreEqual(e.ExplodedEnemies, _model.ExplodedEnemies); 
            Assert.AreEqual(e.GameTime, _model.GameTime); 
            Assert.IsFalse(e.IsWon);
        }

        private void Model_GameOver(object? sender, GameEventArgs e)
        {
            Assert.IsFalse(_model.Running);
            Assert.AreEqual(_model.GameTime, e.GameTime);
            Assert.IsFalse(e.IsWon);
        }
    }
}