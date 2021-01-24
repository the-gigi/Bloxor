using System;
using System.Drawing;
using System.Linq;
using Bloxor.Glazor;
using Color = Bloxor.Glazor.Color;


namespace Bloxor.Game
{
    public class BloxorManager : GameObject, IGameEngineEvents
    {
        private ShapeFactory _shapeFactory = new ShapeFactory();
        private IGameEngine _gameEngine;
        private IGameObject _header;
        BloxorGrid _grid = new BloxorGrid(new Rectangle(), Config.GridColor);
        BloxorStagingArea _stagingArea = new BloxorStagingArea(new Rectangle(), Config.StagingAreaColor);
        private Shape _phantomShape;

        private int _prevMouseX = -1;
        private int _prevMouseY = -1;
        private int _currMouseX;
        private int _currMouseY;

        public BloxorManager(IGameEngine engine)
        {
            _gameEngine = engine;
            BackgroundColor = Config.BackgroundColor;

            _grid.Bounds = new Rectangle(0, 0, 0, 0);
            _stagingArea.Bounds = new Rectangle(0, 0, 0, 0);

            _gameEngine.AddObject(_grid);
            _gameEngine.AddObject(_stagingArea);
            GenerateShapes();
            _gameEngine.Subscribe(this);
        }

        void GenerateShapes()
        {
            for (var i = 0; i < 3; ++i)
            {
                var shape = _shapeFactory.ChooseRandomShape();
                _gameEngine.AddObject(shape);
                _stagingArea.AddShape(shape);
            }
        }

        /// <summary>
        /// Make sure that each cell is exactly the same size
        /// and leave room for the lines
        /// </summary>
        private int CalcGridSide(int screenWidth, int screenHeight)
        {
            var gridSide = (int) (Config.GridRatio * Math.Min(screenWidth, screenHeight));

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
            _grid.Top = offset;

            UpdatePhantom();

            _stagingArea.Top = _grid.Bottom + Config.GridStagingAreaSpacing;

            // To update the shapes
            // Why 19? longest shape is 5 cells across 3 areas + leave a cell spacing before and after each area
            // 1 + 5 + 1 + 5 + 1 + 5 + 1 = 19
            var cellSize = _stagingArea.Width / 19;
            _stagingArea.CellWidth = cellSize;
            _stagingArea.CellHeight = cellSize;
            _stagingArea.Update(screenWidth, screenHeight, timeStamp);
        }

        public void OnMouseDown(IGameObject o, int x, int y)
        {
            _prevMouseX = x;
            _prevMouseY = y;
            var shapes = _stagingArea.Shapes;
            var shapeCount = _stagingArea.Shapes.Count();
            for (var i = 0; i < shapeCount; ++i)
            {
                var shape = shapes[i];
                if (shape != o || shape == null)
                    continue;

                _stagingArea.RemoveShape(shape);
                _phantomShape = shape;
                _phantomShape.CellWidth = _grid.CellWidth;
                _phantomShape.CellHeight = _grid.CellHeight;
            }
        }

        public void UpdatePhantom()
        {
            if (_phantomShape == null)
                return;

            var dx = _currMouseX - _prevMouseX;
            var dy = _currMouseY - _prevMouseY;

            var firstTime = _prevMouseX == -1 && _prevMouseY == -1;

            var prevCell = _grid.FindCellByCoordinates(_prevMouseX, _prevMouseY);
            var currCell = _grid.FindCellByCoordinates(_currMouseX, _currMouseY);

            _prevMouseX = _currMouseX;
            _prevMouseY = _currMouseY;

            if (firstTime)
                return;

            // Update phantom location
            _phantomShape.Left += dx;
            _phantomShape.Top += dy;

            // update shadow
            // shadow didn't move. just bail out
            if (prevCell == currCell)
                return;

            Console.WriteLine("[GGG] clean up previous shadow if exists");
            // clean up previous shadow if exists 
            if (prevCell.X != -1)
            {
                foreach (var p in _phantomShape.Cells)
                {
                    var row = p.Y + prevCell.Y;
                    var col = p.X + prevCell.X;
                    _grid.Cells[row, col] = null;
                }
            }

            // if any part of the shape is out of the grid bail out
            Console.WriteLine("[GGG] if any part of the shape is out of the grid bail out");
            foreach (var p in _phantomShape.Cells)
            {
                var row = p.Y + currCell.Y;
                var col = p.X + currCell.X;
                if (row < 0 || row > _grid.Rows - 1 || col < 0 || col > _grid.Columns - 1)
                    return;
            }

            // Set new shadow
            Console.WriteLine("[GGG] Set new shadow");
            var shadowColor = Color.AdjustBrightness(_phantomShape.Color, Config.ShadowBrightness);
            Console.WriteLine($"[GGG] Setting new shadow: {shadowColor}");
            foreach (var p in _phantomShape.Cells)
            {
                var row = p.Y + currCell.Y;
                var col = p.X + currCell.X;
                _grid.Cells[row, col] = shadowColor;
            }
        }

        public void OnMouseMove(int x, int y)
        {
            _currMouseX = x;
            _currMouseY = y;
        }

        public void OnMouseUp(IGameObject o)
        {
            if (o != _phantomShape)
            {
                throw new Exception($"mouse up on wrog object. o: {o}, phantom shape: {_phantomShape}");
            }

            _gameEngine.RemoveObject(_phantomShape);
            _phantomShape = null;
        }
    }
}