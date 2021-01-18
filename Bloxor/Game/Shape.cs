using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Bloxor.Glazor;

namespace Bloxor.Game
{
    public class Shape : GameObject
    {
        const int offset = 2;        
        private string Color { get;  }
        private Point[] Cells { get; }
        
        public int CellWidth  { get; set; }
        public int CellHeight { get; set; }
        
        public Shape(Point[] cells, string color)
        {
            Cells = cells;
            Color = color;
        }

        public new int Width => (Cells.Select(c => c.X).Max() + 1) * CellWidth;
        
        public override async ValueTask Render(ICanvas canvas)
        {
            foreach (var cell in Cells)
            {
                var left = Left + offset + cell.X * CellWidth;
                var top = Top + offset + cell.Y * CellHeight;

                var w = CellWidth - 2 * offset;
                var h = CellHeight - 2 * offset;
                await canvas.DrawRectangle(left, top, w ,h , Config.ShapeBorderColor, Color);
            }
        }
    }
}