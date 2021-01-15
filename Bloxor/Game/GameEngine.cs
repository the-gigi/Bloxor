using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace Bloxor.Game
{
    public class GameEngine
    {
        Canvas2DContext _canvas;
        Point _mousePosition;
        bool _mouseButtonDown;
        int _screenWidth;
        int _screenHeight;
        GameObject[] _objects;
        GameObject _clickedObject;
        GameTime _gameTime = new GameTime();

        public async ValueTask InitAsync(Canvas2DContext canvas)
        {
            _canvas = canvas;
        }

        public void AddObject()
        {
        }

        public void RemoveObject()
        {
            
        }

        private GameObject FindObjectAt(Point p)
        {
            for (var i = _objects.Length - 1; i >= 0; i--)
            {
                if (_objects[i].Bounds.Contains(p))
                {
                    return _objects[i];
                }
            }

            return null;
        }        
        public void OnMouseMove(int x, int y)
        {
            _mousePosition.X = x;
            _mousePosition.Y = y;
        }
        
        public void OnMouseDown()
        {
            _mouseButtonDown = true;
            _clickedObject = FindObjectAt(_mousePosition);
            
        }        

        public void OnMouseUp()
        {
            _mouseButtonDown = false;
            _clickedObject = null;
        }        

        public void Update(int screenWidth, int screenHeight, float timeStamp)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _gameTime.TotalTime = timeStamp;
        }
        public async ValueTask Render()
        {
            await _canvas.ClearRectAsync(0, 0, _screenWidth, _screenHeight);
            await _canvas.SetFillStyleAsync("azure");
            await _canvas.FillRectAsync(0, 0, _screenWidth, _screenHeight);

            var canvas = new Canvas(_canvas);
            var grid = new Grid(new Rectangle(350, 200, 500, 500), 10, 10, "purple");

            var points = new Point[]
            {
                new Point(0, 0),
                new Point(0, 1),
                new Point(1, 1),
                new Point(1, 2),
            };
            
            var cellWidth =  grid.Width / grid.Columns;
            var cellHeight = grid.Height / grid.Rows;

            var shape = new Shape(points, "yellow")
            {
                Left = grid.Left, 
                Top = grid.Top, 
                CellWidth = cellWidth, 
                CellHeight = cellHeight
            };
            
            var points2 = new Point[]
            {
                new Point(5, 5),
                new Point(5, 6),
                new Point(5, 7),
                new Point(6, 7),
                new Point(7, 7),
            };
            
            var shape2 = new Shape(points2, "red")
            {
                Left = grid.Left,
                Top = grid.Top, 
                CellWidth = cellWidth, 
                CellHeight = cellHeight
            };
            
            

            await grid.Render(canvas);
            await shape.Render(canvas);
            await shape2.Render(canvas);
            // const int y = 200;
            // const int width = 300;
            // const int height = 100;
            //
            // await _canvas.SetFillStyleAsync("azure");
            // await _canvas.FillRectAsync(0, 0, _screenWidth, _screenHeight);
            //
            // await _canvas.SetStrokeStyleAsync("blue");
            // await _canvas.StrokeRectAsync(0, y, width, height);
            // await _canvas.SetStrokeStyleAsync("red");
            // await _canvas.StrokeRectAsync(width, y, width, height);
            //
            await _canvas.SetFontAsync("24px verdana");
            //await _canvas.StrokeTextAsync($"time: {_gameTime.ElapsedTime}", width / 6, y + height / 3);
            await _canvas.StrokeTextAsync($"x: {_mousePosition.X}, y: {_mousePosition.Y} pressed: {_mouseButtonDown}", 10, 30);
            //
            // await _canvas.SetFillStyleAsync("red");
            // await _canvas.FillRectAsync(2 * width, y, width, height);
            // await _canvas.StrokeTextAsync("Yeah, it works!!!", width * 7 / 6, y + height / 3);
            //
            // await _canvas.SetFillStyleAsync("green");
            // await _canvas.FillRectAsync(0, 0, 50, 50);
            // await _canvas.DrawImageAsync(_sprite.SpriteSheet, _spritePosition.X, _spritePosition.Y, _sprite.Size.Width,  _sprite.Size.Height);
        }
    }
}