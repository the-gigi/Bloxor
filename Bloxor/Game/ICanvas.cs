using System.Drawing;
using System.Threading.Tasks;

namespace Bloxor.Game
{
    public interface ICanvas
    {
        ValueTask DrawLine(int x1, int y1, int x2, int y2, string color);
        ValueTask DrawRectangle(int left, int top, int width, int height, string color = "", string fillColor = "");
    }
}