using System.Drawing;

namespace Bloxor.Glazor
{
    public interface IGameEngineEvents
    {
        void OnMouseMove(int x, int y);
        void OnMouseDown(IGameObject o, int x, int y);
        void OnMouseUp(IGameObject o);
    }
}