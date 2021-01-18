using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Bloxor.Glazor;


namespace Bloxor.Game
{
    public class BloxorStagingArea : GameObject
    {
        readonly List<Shape> _shapes;
        
        public BloxorStagingArea(Rectangle bounds, string borderColor)
        {
            _shapes = new List<Shape>();
            Bounds = bounds;
            BorderColor = borderColor;
        }

        public int CellWidth { get; set; }
        public int CellHeight { get; set; }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void Clear(Shape shape)
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
                var rect = new Rectangle(Left + i * w, Top, w, Height);
                shape.CenterInRect(rect);                
            }
        }
        public override async ValueTask Render(ICanvas canvas)
        {
            await base.Render(canvas);
            var shapeCount = _shapes.Count(shape => shape != null);
            await canvas.DrawText(Left + 10, Top + 10, $"{Bounds}", Config.Font, "black", "green");
            foreach (var shape in _shapes.Where(shape => shape != null))
            {
                shape.CellWidth = CellWidth;
                shape.CellHeight = CellHeight;
                await shape.Render(canvas);
            }
        }
    }
}