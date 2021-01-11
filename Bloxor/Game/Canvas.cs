using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace Bloxor.Game
{
    public class Canvas : ICanvas
    {
        readonly Canvas2DContext _canvas;
        public Canvas(Canvas2DContext canvas)
        {
            _canvas = canvas;
        }

        public async ValueTask DrawLine(int x1, int y1, int x2, int y2, string color)
        {
            await _canvas.SetStrokeStyleAsync(color);
            await _canvas.BeginPathAsync();
            await _canvas.MoveToAsync(x1, y1);
            await _canvas.LineToAsync(x2, y2);
            await _canvas.ClosePathAsync();
            await _canvas.StrokeAsync();
        }

        public async ValueTask DrawRectangle(int left, int top, int width, int height, string color = "", string fillColor = "")
        {
            if (fillColor != "")
            {
                await _canvas.SetFillStyleAsync(fillColor);
                await _canvas.FillRectAsync(left, top, width, height);
            }
            
            if (color != "")
            {
                await _canvas.SetStrokeStyleAsync(color);
                await _canvas.StrokeRectAsync(left, top, width, height);
            }
        }
    }
}