using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

namespace Bloxor.Game
{
    public class GameEngine
    {
        public ElementReference Spritesheet;
        
        Canvas2DContext _canvas;
        Sprite _sprite;

        int _screenWidth;
        int _screenHeight;
        Point _spritePosition = Point.Empty;
        Point _spriteDirection = new Point(1, 1);
        float _spriteSpeed = 0.25f;
        readonly GameTime _gameTime = new GameTime();

        public async ValueTask InitAsync(Canvas2DContext canvas)
        {
            _canvas = canvas;
            _sprite = new Sprite()
            {
                Size = new Size(200, 200),
                SpriteSheet = Spritesheet
            };
        }

        public void Update(int screenWidth, int screenHeight, float timeStamp)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            
            _gameTime.TotalTime = timeStamp;
            
            if (_spritePosition.X + _sprite.Size.Width >= _screenWidth || _spritePosition.X < 0)
                _spriteDirection.X = -_spriteDirection.X;

            if (_spritePosition.Y +  _sprite.Size.Height >= _screenHeight || _spritePosition.Y < 0)
                _spriteDirection.Y = -_spriteDirection.Y;

            _spritePosition.X += (int)(_spriteDirection.X * _spriteSpeed * _gameTime.ElapsedTime);
            _spritePosition.Y += (int)(_spriteDirection.Y * _spriteSpeed * _gameTime.ElapsedTime);
        }
        public async ValueTask Render()
        {
            const int y = 200;
            const int width = 300;
            const int height = 100;
            
            await _canvas.SetFillStyleAsync("azure");
            await _canvas.FillRectAsync(0, 0, 1000, 600);
        
            await _canvas.SetFontAsync("24px verdana");
        
            await _canvas.SetFillStyleAsync("green");
            await _canvas.FillRectAsync(0, y, width, height);
        
            await _canvas.StrokeTextAsync($"time: {_gameTime.ElapsedTime}", width / 6, y + height / 3);
        
            await _canvas.SetFillStyleAsync("red");
            await _canvas.FillRectAsync(width, y, width, height);
            await _canvas.StrokeTextAsync("Yeah, it works!!!", width * 7 / 6, y + height / 3);
            
            await _canvas.ClearRectAsync(0, 0, width, height);
            await _canvas.SetFillStyleAsync("green");
            await _canvas.FillRectAsync(0, 0, 50, 50);
            await _canvas.DrawImageAsync(_sprite.SpriteSheet, _spritePosition.X, _spritePosition.Y, _sprite.Size.Width,  _sprite.Size.Height);
        }
    }
}