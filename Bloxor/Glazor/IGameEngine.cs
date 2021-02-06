using System.Drawing;
using Bloxor.Game;

namespace Bloxor.Glazor
{
    public interface IGameEngine
    {
        void AddObject(GameObject o);
        void RemoveObject(GameObject o);
        GameObject FindObjectAt(Point p);

        void Subscribe(IGameEngineEvents sink);
        void Unsubscribe(IGameEngineEvents sink);
    }
}