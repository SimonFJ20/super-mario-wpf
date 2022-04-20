using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace SuperMarioWPF;

public class ObjectHandler
{
    private readonly LinkedList<GameObject> objects;

    public ObjectHandler()
    {
        objects = new LinkedList<GameObject>();
    }
    
    public bool Tick(double deltaSeconds)
    {
        return objects.ToArray().All(o => o.Tick(deltaSeconds));
    }
    
    public void Load(Canvas canvas)
    {
        foreach (var o in objects)
            o.Load(canvas);
    }
    
    public void Draw()
    {
        foreach (var o in objects)
            o.Draw();
    }
}