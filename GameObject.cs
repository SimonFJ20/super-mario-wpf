using System.Windows.Controls;
using System.Windows.Threading;

namespace SuperMarioWPF;

abstract class GameObject 
{
    protected Game game;
    public Vec2<double> pos, vel, box;
    public bool rigid;
    public bool shouldRemoveSelf = false;

    public GameObject(Game game)
    {
        this.game = game; 
        pos = new Vec2<double>(0, 0);
        vel = new Vec2<double>(0, 0);
        box = new Vec2<double>(0, 0);
        this.rigid = true;
    }

    public abstract void Load(Canvas canvas);
    public abstract void Draw(Dispatcher dispatcher);
    public abstract bool Tick(double deltaTS);

    public bool DoesCollide(GameObject other)
    {
        return pos.x + box.x > other.pos.x
               && pos.x < other.pos.x + other.box.x
               && pos.y + box.y > other.pos.y
               && pos.y < other.pos.y + other.box.y;
    }
}