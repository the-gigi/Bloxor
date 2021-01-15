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
            BorderColor = "red";
            BackgroundColor = "";
        }
        
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public Point Speed { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        
        public Rectangle Bounds { get; set; }
        public int Left
        {
            get => Bounds.Left;
            set => Bounds = new Rectangle(value, Bounds.Top, Bounds.Width, Bounds.Height);
        }

        public int Top
        {
            get => Bounds.Top;
            set => Bounds = new Rectangle( Bounds.Left, value, Bounds.Width, Bounds.Height);
        }

        public int Right => Bounds.Right;
        public int Bottom => Bounds.Bottom;
        
        public int Width
        {
            get => Bounds.Width;
            set => Bounds = new Rectangle( Bounds.Left, Bounds.Top, value, Bounds.Height);
        }

        public int Height
        {
            get => Bounds.Width;
            set => Bounds = new Rectangle( Bounds.Left, Bounds.Top, Bounds.Width, value);
        }
        

        public int DX => Speed.X;
        public int DY => Speed.Y;

        
        public void OnMouseMove(int x, int y)
        {
        }
        
        public void OnMouseDown()
        {
        }        

        public void OnMouseUp()
        {
        }        
        
        public virtual void Update(int screenWidth, int screenHeight, float timeStamp)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }

        public virtual async ValueTask Render(ICanvas canvas)
        {
            if (BorderColor == "" && BackgroundColor == "")
            {
                return;
            }
            await canvas.DrawRectangle(Left, Top, Width, Height, BorderColor, BackgroundColor);
        }        
    }
}