using System.Threading.Tasks;
using Blazor.Extensions.Canvas.Canvas2D;

namespace Bloxor.Game
{
    public class GameRenderer
    {
        public static async ValueTask Render(Canvas2DContext canvas, float timeStamp)
        {
            const int y = 200;
            const int width = 300;
            const int height = 100;
            
            await canvas.SetFillStyleAsync("azure");
            await canvas.FillRectAsync(0, 0, 1000, 600);
        
            await canvas.SetFontAsync("24px verdana");
        
            await canvas.SetFillStyleAsync("green");
            await canvas.FillRectAsync(0, y, width, height);
        
            await canvas.StrokeTextAsync($"time: {timeStamp}", width / 6, y + height / 3);
        
            await canvas.SetFillStyleAsync("red");
            await canvas.FillRectAsync(width, y, width, height);
            await canvas.StrokeTextAsync("Yeah, it works!!!", width * 7 / 6, y + height / 3);
        }
    }
    
}