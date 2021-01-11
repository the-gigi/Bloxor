using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace Bloxor.Game
{
    public class Grid : GameObject
    {
        private string Color { get;  }
        private int Rows { get; }
        private int Columns { get; }

        
        public Grid(int rows, int columns, string color)
        {
            Color = color;
            Rows = rows;
            Columns = columns;
        }

        public override async ValueTask Render(Canvas2DContext canvas)
        {
            await base.Render(canvas);
            var cellWidth =  Width / Columns;
            var cellHeight = Height / Rows;

            await canvas.SetStrokeStyleAsync(Color);
            for (var row = 0; row < Rows; ++row)
            {
                var top = Top + row * cellHeight;
                for (var col = 0; col < Columns; ++col)
                {
                    var left = Left + col * cellWidth;
                    await canvas.StrokeRectAsync(left, top, cellWidth, cellHeight);
                }
            }
        }
    }
}