using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;
using Bloxor.Glazor;

namespace Bloxor.Game
{
    public class Shape : GameObject
    {
        const int Offset = 5;        
        public string Color { get;  }
        public Point[] Cells { get; }
        
        public int CellWidth  { get; set; }
        public int CellHeight { get; set; }

        private List<Rectangle> RenderedRectangles = new List<Rectangle>();
        
        public Shape(Point[] cells, string color)
        {
            Cells = cells;
            Color = color;
            ZIndex = Config.DefaultZIndex * 2;
        }
        
        public override string ToString()
        {
            var cells = string.Join(", ", Cells.ToList().Select(p => $"({p.X}, {p.Y})"));
            return $"Shape: [{cells}]";
        }
        
        private List<Rectangle> Rectangles
        {
            get
            {
                var rectangles = new List<Rectangle>();
                foreach (var cell in Cells)
                {
                    var left = Left + cell.X * CellWidth;
                    var top = Top + cell.Y * CellHeight;
                    rectangles.Add(new Rectangle(left, top, CellWidth, CellHeight));
                }

                return rectangles;
            }
        }
        public override bool Contains(Point p)
        {
            return Rectangles.Any(r => r.Contains(p));
        }
        
        public override async ValueTask Render(ICanvas canvas)
        {
            RenderedRectangles = Rectangles;
            foreach (var r in RenderedRectangles)
            {
                // var left = r.Left + Offset;
                // var top = r.Top + Offset;
                // var w = r.Width - 2 * Offset;
                // var h = r.Height - 2 * Offset;
                // await canvas.DrawRectangle(left, top, w ,h, null, Color);
                // left = r.Left + 2;
                // top = r.Top + 2;
                // w = r.Width - 4;
                // h = r.Height - 4;
                // await canvas.DrawRectangle(left, top, w , h, Config.ShapeBorderColor);
                await canvas.DrawRectangle(r.Left, r.Top, r.Width, r.Height, Config.ShapeBorderColor, Color);                                                
            }
        }
    }
}