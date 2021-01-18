using System.Threading.Tasks;

namespace Bloxor.Glazor
{
    public interface ICanvas
    {
        ValueTask DrawLine(int x1, int y1, int x2, int y2, string color);
        ValueTask DrawRectangle(int left, int top, int width, int height, string color = "", string fillColor = "");
        ValueTask DrawText(int left, int top, string text, string font, string color = "", string fillColor = "");
    }
}