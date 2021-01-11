using System.Drawing;
using System.Threading.Tasks;


namespace Bloxor.Game
{
    interface IGameObject
    {
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
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
        public int DX { get; }
        public int DY { get; }

        public void Update(int screenWidth, int screenHeight, float timeStamp);
        
        public ValueTask Render(ICanvas canvas);
        
    }
}