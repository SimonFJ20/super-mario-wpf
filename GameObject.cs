using System.Windows.Controls;
using SuperMarioWPF.Utils;

namespace SuperMarioWPF;

public abstract class GameObject 
{
    protected readonly Game game;
    public Vec2<double> Position { get; set; }

    protected GameObject(Game game)
    {
        this.game = game; 
        Position = new Vec2<double>(0, 0);
    }

    public abstract void Load(Canvas canvas);
    public abstract void Draw();
    public abstract bool Tick(double deltaSeconds);
}