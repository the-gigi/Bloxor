using System.Drawing;
using System.Threading.Tasks;
using Bloxor.Glazor;

namespace Bloxor.Game
{
    public class Grid : GameObject
    {
        private string LineColor { get;  }
        public int Rows { get; }
        public int Columns { get; }

        public Grid(Rectangle bounds, int rows, int columns, string lineColor)
        {
            Bounds = bounds;
            LineColor = lineColor;
            Rows = rows;
            Columns = columns;
        }

        public override async ValueTask Render(ICanvas canvas)
        {
            await base.Render(canvas);
            var cellWidth =  Width / Columns;
            var cellHeight = Height / Rows;
            
            for (var row = 0; row <= Rows; ++row)
            {
                var y = row * cellHeight;
                await canvas.DrawLine(Left, Top + y, Left + Width, Top + y, LineColor);
            }
            
            for (var col = 0; col <= Columns; ++col)
            {
                var x = col * cellWidth;
                await canvas.DrawLine(Left + x, Top, Left + x, Top + Height, LineColor);
            }
        }
    }
}