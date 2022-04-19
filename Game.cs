using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SuperMarioWPF.Objects;
using SuperMarioWPF.Utils;

namespace SuperMarioWPF;
public class Game
{
    private readonly Scoreboard scoreboard;
    public readonly GameKeyboard keyboard;
    public readonly LinkedList<GameObject> objects;
    public double scroll;

    public Game()
    {
        scoreboard = new Scoreboard();
        keyboard = new GameKeyboard();
        objects = new LinkedList<GameObject>();
        var t = new Terrain(this);
        var p = new Player(this);
        var b1 = new Block(this, new Vec2<double>(500, 200));
        var b2 = new Block(this, new Vec2<double>(532, 200));
        var b3 = new Block(this, new Vec2<double>(532, 200 - 32));
        var g1 = new Goomba(this, new Vec2<double>(1332, 268), p);
        var g2 = new Goomba(this, new Vec2<double>(1400, 268), p);
        objects.AddLast(t);
        objects.AddLast(p);
        objects.AddLast(b1);
        objects.AddLast(b2);
        objects.AddLast(b3);
        objects.AddLast(g1);
        objects.AddLast(g2);
        scroll = 0;
    }

    public bool Tick(double deltaSeconds)
    {
        if (!scoreboard.Tick(deltaSeconds))
            return false;
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

    public IEnumerable<ICollidable> CollidingObjects(ICollidable caller)
    {
        var collidingObjects = new LinkedList<ICollidable>();
        foreach (var o in objects)
            if (o is ICollidable collidable && collidable.DoesCollide(caller))
                collidingObjects.AddLast(collidable);
        return collidingObjects.ToArray();
    }
}

