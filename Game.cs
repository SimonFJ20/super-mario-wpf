using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SuperMarioWPF;
class Game
{
    private Scoreboard scoreboard;
    public GameKeyboard keyboard;
    public LinkedList<GameObject> objects;
    public Pair<int, int> worldLevel;
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
        worldLevel = new Pair<int, int>(0, 0);
        scroll = 0;
    }

    public bool Tick(double deltaTS)
    {
        if (!scoreboard.Tick(deltaTS))
            return false;
        return objects.ToArray().All(o => o.Tick(deltaTS));
    }

    public void Load(Canvas canvas)
    {
        foreach (var o in objects)
            o.Load(canvas);
    }

    public void Draw(Dispatcher dispatcher)
    {
        foreach (var o in objects)
            o.Draw(dispatcher);
    }

    public GameObject[] CollidingObjects(GameObject caller)
    {
        return objects.Where(o => o.DoesCollide(caller)).ToArray();
    }
}

