namespace SuperMarioWPF.Utils;

public class CollisionCheckers
{
    private class Box2d : ICollidable
    {
        public Vec2<double> Position { get; set; }
        public Vec2<double> Velocity { get; set; }
        public Vec2<double> Box { get; set; }
        public bool DoesCollide(ICollidable other)
        {
            return CheckBoxCollision(this, other);
        }
    }

    public static bool CheckBoxCollision(ICollidable first, ICollidable second)
    {
        return first.Position.X + first.Box.X > second.Position.X
               && first.Position.X < second.Position.X + second.Box.X
               && first.Position.Y + first.Box.Y > second.Position.Y
               && first.Position.Y < second.Position.Y + second.Box.Y;
    }

    public static bool CheckMovingBoxCollision(ICollidable first, ICollidable second)
    {
        // This algorithm assumes objects are moved before checking collisions.
        // And that velocity is unchanged from moving.
        // tick() -> move objects -> check collisions -> handle collisions -> change velocity

        if (first.DoesCollide(second))
            return true;
        
        if (!AreDeltasColliding(first, second))
            return false;

        var firstStep = first.Velocity.Copy().DivideBoth(2);
        var secondStep = second.Velocity.Copy().DivideBoth(2);

        while (firstStep.Length() > 1 && secondStep.Length() > 1)
        {
            var firstBoxOnPath = new Box2d
            {
                Position = first.Position.Copy().Subtract(first.Velocity).Add(firstStep),
                Box = first.Box,
                Velocity = new Vec2<double>(0, 0),
            };
            var secondBoxOnPath = new Box2d
            {
                Position = second.Position.Copy().Subtract(second.Velocity).Add(secondStep),
                Box = second.Box,
                Velocity = new Vec2<double>(0, 0),
            };
        }
        return false;
    }

    private static bool AreDeltasColliding(ICollidable first, ICollidable second)
    {
        var firstDeltaBox = new Box2d
        {
            Position = first.Position.Copy(),
            Box = first.Box.Copy().Add(first.Velocity),
            Velocity = new Vec2<double>(0, 0),
        };
        var secondDeltaBox = new Box2d
        {
            Position = second.Position.Copy(),
            Box = second.Box.Copy().Add(second.Velocity),
            Velocity = new Vec2<double>(0, 0),
        };
        return firstDeltaBox.DoesCollide(secondDeltaBox);
    }
}