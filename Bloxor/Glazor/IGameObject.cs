using System.Drawing;
using System.Threading.Tasks;

namespace Bloxor.Glazor
{
    public interface IGameObject
    {
        public Rectangle Bounds { get; set; }
        public bool Visible { get; set; }
        public bool Enabled { get; set; }
        public Point Speed { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }
        public int Width { get; }
        public int Height { get; }
        public int ZIndex { get; }

        public int DX { get; }
        public int DY { get; }

        public void OnMouseMove(int x, int y);
        public void OnMouseDown();
        public void OnMouseUp();

        public void CenterInRect(Rectangle rect);
        
        public void Update(int screenWidth, int screenHeight, float timeStamp);
        
        public ValueTask Render(ICanvas canvas);
        
    }
}