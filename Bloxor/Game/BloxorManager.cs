using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Bloxor.Glazor;


namespace Bloxor.Game
{
    public class BloxorManager : GameObject
    {
        private ShapeFactory _shapeFactory = new ShapeFactory();
        private IGameEngine _gameEngine;
        private IGameObject _header; 
        BloxorGrid  _grid = new BloxorGrid(new Rectangle(), Config.GridColor);
        BloxorStagingArea  _stagingArea = new BloxorStagingArea(new Rectangle(), Config.StagingAreaColor);

        public BloxorManager(IGameEngine engine)
        {
            _gameEngine = engine;
            BackgroundColor = Config.BackgroundColor;
            
            _grid.Bounds = new Rectangle(0, 0, 0, 0);
            _stagingArea.Bounds = new Rectangle(0, 0, 0, 0);
            
            _gameEngine.AddObject(_grid);
            _gameEngine.AddObject(_stagingArea);
            GenerateShapes();
        }

        void GenerateShapes()
        {
            for (var i = 0; i < 3; ++i)
            {
                var shape = _shapeFactory.ChooseRandomShape();
                _stagingArea.AddShape(shape);
            }
        }
    
        /// <summary>
        /// Make sure that each cell is exactly the same size
        /// and leave room for the lines
        /// </summary>
        private int CalcGridSide(int screenWidth, int screenHeight)
        {
            var gridSide = (int)(Config.GridRatio * Math.Min(screenWidth, screenHeight));

            while ((gridSide - 11) % 10 != 0)
                gridSide++;
            return gridSide;
        }
        public override void Update(int screenWidth, int screenHeight, float timeStamp)
        {
            base.Update(screenWidth, screenHeight, timeStamp);
            
            var screen = new Rectangle(0, 0, screenWidth, screenHeight);
            var gridSide = CalcGridSide(screenWidth, screenHeight);

            _grid.Width = gridSide;
            _grid.Height = gridSide;
            
            _stagingArea.Width = gridSide;
            _stagingArea.Height = gridSide / 2;

            // Center horizontally (will override the vertical centering)
            _grid.CenterInRect(screen);
            _stagingArea.CenterInRect(screen);
            
            // center grid + staging area vertically
            var offset = (screenHeight - _grid.Height - _stagingArea.Height - Config.GridStagingAreaSpacing) / 2;
            Console.WriteLine($"screen: {screenWidth}, {screenHeight}");
            Console.WriteLine($"offset: {offset}");
            _grid.Top = offset;
            Console.WriteLine($"BEFORE stagingArea bounds: {_stagingArea.Bounds}");
            _stagingArea.Top = _grid.Bottom + Config.GridStagingAreaSpacing;
            Console.WriteLine($"AFTER stagingArea bounds: {_stagingArea.Bounds}");
            
            // To update the shapes
            // Why 19? longest shape is 5 cells across 3 areas + leave a cell spacing before and after each area
            // 1 + 5 + 1 + 5 + 1 + 5 + 1 = 19
            var cellSize = _stagingArea.Width / 19;
            _stagingArea.CellWidth = cellSize;
            _stagingArea.CellHeight = cellSize;
            
            
            _stagingArea.Update(screenWidth, screenHeight, timeStamp);
        }
    }
}