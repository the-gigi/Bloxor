using System.Drawing;
using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace Bloxor.Game
{
    public class GameObject : IGameObject
    {
        protected GameObject()
        {
            Visible = true;
            Enabled = true;
        }
        
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public Rectangle Bounds { get; set; }
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public Point Speed { get; set; }
        public string BackgroundColor { get; set; }
        public string BoarderColor { get; set; }
        
        public int Left => Bounds.Left;
        public int Top => Bounds.Top;
        public int Right => Bounds.Right;
        public int Bottom => Bounds.Bottom;
        
        public int Width => Bounds.Width;
        public int Height => Bounds.Width;

        public int DX => Speed.X;
        public int DY => Speed.Y;

        public virtual void Update(int screenWidth, int screenHeight, float timeStamp)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public virtual async ValueTask Render(Canvas2DContext canvas)
        {
            if (BackgroundColor != "")
            {
                await canvas.SetFillStyleAsync(BackgroundColor);
                await canvas.FillRectAsync(Left, Top, Width, Height);
            }

            if (BoarderColor != "")
            {
                await canvas.SetStrokeStyleAsync(BoarderColor);
                await canvas.FillRectAsync(Left, Top, Width, Height);
            }
        }        
    }
}