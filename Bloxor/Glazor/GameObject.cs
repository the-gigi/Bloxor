using System.Drawing;
using System.Threading.Tasks;
using Bloxor.Glazor;

namespace Bloxor.Game
{
    public class GameObject
    {
        protected GameObject()
        {
            Visible = true;
            Enabled = true;
            BorderColor = "red";
            BackgroundColor = "";
            ZIndex = Config.DefaultZIndex;
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
        public int Bottom
        {
            get => Bounds.Bottom;
            set => Bounds = new Rectangle( Bounds.Left, value - Bounds.Height, Bounds.Width, Bounds.Height);
        }

        public int Width
        {
            get => Bounds.Width;
            set => Bounds = new Rectangle( Bounds.Left, Bounds.Top, value, Bounds.Height);
        }

        public int Height
        {
            get => Bounds.Height;
            set => Bounds = new Rectangle( Bounds.Left, Bounds.Top, Bounds.Width, value);
        }

        public int ZIndex { get; set; }

        public int DX => Speed.X;
        public int DY => Speed.Y;
        
        public void CenterInRect(Rectangle rect)
        {
            if (rect.Width < Width || rect.Height < Height)
            {
                return;
            }
            
            Left = rect.Left + (rect.Width - Width) / 2;
            Top= rect.Top + (rect.Height - Height) / 2;
        }

        public virtual bool Contains(Point p)
        {
            return Bounds.Contains(p);
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