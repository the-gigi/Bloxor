using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Bloxor.Glazor;


namespace Bloxor.Game
{
    public class BloxorHeader : GameObject
    {
        
        public BloxorHeader(Rectangle bounds, string borderColor)
        {
            Bounds = bounds;
            BorderColor = borderColor;
        }

        public int Score { get; set; }
        public int HighScore { get; set; }
        
        public override async ValueTask Render(ICanvas canvas)
        {
            const int offset = 25;
            await base.Render(canvas);
            await canvas.DrawText(Left + 10, Top + offset, $"High Score: {HighScore:000}", Config.Font, "black", "green");
            await canvas.DrawText(Right - 100, Top + offset, $"Score: {Score:000}", Config.Font, "black", "green");
        }
    }
}