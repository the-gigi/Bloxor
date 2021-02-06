using Bloxor.Game;

namespace Bloxor.Glazor
{
    public interface IGameEngineEvents
    {
        void OnMouseMove(int x, int y);
        void OnMouseDown(GameObject o, int x, int y);
        void OnMouseUp(GameObject o);
    }
}