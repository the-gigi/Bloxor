using System.Drawing;
using Microsoft.AspNetCore.Components;

namespace Bloxor
{
    public class Sprite
    {
        public Size Size { get; set; }
        public ElementReference SpriteSheet { get; set; }
    }
}