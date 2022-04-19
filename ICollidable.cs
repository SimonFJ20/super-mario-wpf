using SuperMarioWPF.Utils;

namespace SuperMarioWPF;

public interface ICollidable
{
    Vec2<double> Position { get; set; }
    Vec2<double> Velocity { get; set; }
    Vec2<double> Box { get; set; }
    bool DoesCollide(ICollidable other);
}