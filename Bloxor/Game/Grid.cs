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

        public int CellWidth => Width / Columns;
        public int CellHeight => Height / Rows;
        
        public override async ValueTask Render(ICanvas canvas)
        {
            await base.Render(canvas);
            for (var row = 0; row <= Rows; ++row)
            {
                var y = row * CellHeight;
                await canvas.DrawLine(Left, Top + y, Left + Width, Top + y, LineColor);
            }
            
            for (var col = 0; col <= Columns; ++col)
            {
                var x = col * CellWidth;
                await canvas.DrawLine(Left + x, Top, Left + x, Top + Height, LineColor);
            }
        }

        public Point FindCellByCoordinates(int x, int y)
        {
            var cellX = (x - Left) / CellWidth;
            var cellY = (y - Top) / CellHeight;

            if (cellX < 0 || cellX > Columns - 1 || cellY < 0 || cellY > Rows - 1)
            {
                cellX = -1;
                cellY = -1;
            }

            return new Point(cellX, cellY);
        }
    }
}