using SuperMarioWPF.Utils;

namespace SuperMarioWPF.Objects;

public abstract class PhysicsObject : GameObject, ICollidable
{

    public Vec2<double> Velocity { get; set; }
    public Vec2<double> Box { get; set; }

    protected PhysicsObject(Game game)
        : base(game)
    {
        Velocity = new Vec2<double>(0, 0);
        Box = new Vec2<double>(0, 0);
    }

    public bool DoesCollide(ICollidable other)
    {
        return Position.X + Box.X > other.Position.X
               && Position.X < other.Position.X + other.Box.X
               && Position.Y + Box.Y > other.Position.Y
               && Position.Y < other.Position.Y + other.Box.Y;
    }
}