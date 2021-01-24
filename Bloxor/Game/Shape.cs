using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Bloxor.Glazor;

namespace Bloxor.Game
{
    public class Shape : GameObject
    {
        const int Offset = 2;        
        public string Color { get;  }
        public Point[] Cells { get; }
        
        public int CellWidth  { get; set; }
        public int CellHeight { get; set; }
        
        public Shape(Point[] cells, string color)
        {
            Cells = cells;
            Color = color;
            ZIndex = Config.DefaultZIndex * 2;
        }

        //public new int Width => (Cells.Select(c => c.X).Max() + 1) * CellWidth;
        
        public override async ValueTask Render(ICanvas canvas)
        {
            foreach (var cell in Cells)
            {
                var left = Left + Offset + cell.X * CellWidth;
                var top = Top + Offset + cell.Y * CellHeight;

                var w = CellWidth - 2 * Offset;
                var h = CellHeight - 2 * Offset;
                await canvas.DrawRectangle(left, top, w ,h , Config.ShapeBorderColor, Color);
            }
        }
    }
}