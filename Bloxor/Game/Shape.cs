using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

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

        public override async ValueTask Render(ICanvas canvas)
        {
            foreach (var cell in Cells)
            {
                var left = Left + offset + cell.X * CellWidth;
                var top = Top + offset + cell.Y * CellHeight;
                
                await canvas.DrawRectangle(left, top, CellWidth - 2 * offset, CellHeight - 2 * offset, fillColor: Color);
            }
        }
    }
}