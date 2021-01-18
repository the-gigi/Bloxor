using System.Drawing;

namespace Bloxor.Glazor
{
    public interface IGameEngine
    {
        void AddObject(IGameObject o);
        void RemoveObject(IGameObject o);
        IGameObject FindObjectAt(Point p);

        void Subscribe(IGameEngineEvents sink);
        void Unsubscribe(IGameEngineEvents sink);
    }
}