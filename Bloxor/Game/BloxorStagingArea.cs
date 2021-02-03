using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Bloxor.Glazor;


namespace Bloxor.Game
{
    public class BloxorStagingArea : Grid
    {
        readonly List<Shape> _shapes;
        
        public BloxorStagingArea(Rectangle bounds, string borderColor) : base(bounds, 1, 3, "purple")
        {
            _shapes = new List<Shape>();
            Bounds = bounds;
            BorderColor = borderColor;
        }

        public int CellWidth { get; set; }
        public int CellHeight { get; set; }

        public List<Shape> Shapes => _shapes;

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void RemoveShape(Shape shape)
        {
            for (var i = 0; i < _shapes.Count; ++i)
            {
                if (shape != _shapes[i])
                    continue;
                
                _shapes[i] = null;
                return;
            }
        }
        
        public void Clear()
        {
            _shapes.Clear();
        }

        public override void Update(int screenWidth, int screenHeight, float timeStamp)
        {
            base.Update(screenWidth, screenHeight, timeStamp);
            for (var i = 0; i < 3; ++i)
            {
                var shape = _shapes[i];
                if (shape == null)
                    continue;

                var w = Width / 3;
                shape.Width = (shape.Cells.Select(c => c.X).Max() + 1) * CellWidth;
                shape.Height = (shape.Cells.Select(c => c.Y).Max() + 1) * CellHeight;
                var rect = new Rectangle(Left + i * w, Top, w, Height);
                shape.CenterInRect(rect);                
            }
        }
        public override async ValueTask Render(ICanvas canvas)
        {
            await base.Render(canvas);
            //await canvas.DrawText(Left + 10, Top + 10, $"{Bounds}", Config.Font, "black", "green");
            foreach (var shape in _shapes.Where(shape => shape != null))
            {
                shape.CellWidth = CellWidth;
                shape.CellHeight = CellHeight;
                await shape.Render(canvas);
            }
        }
    }
}