using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using SuperMarioWPF.Utils;

namespace SuperMarioWPF;
public class Game
{
    private readonly Scoreboard scoreboard;
    public readonly GameKeyboard keyboard;
    public readonly ObjectHandler objectHandler;
    public Game()
    {
        scoreboard = new Scoreboard();
        keyboard = new GameKeyboard();
        objectHandler = new ObjectHandler();
    }

    public void Load(Canvas canvas)
    {
        objectHandler.Load(canvas);
    }

    public bool Tick(double deltaSeconds)
    {
        if (!scoreboard.Tick(deltaSeconds))
            return false;
        if (!objectHandler.Tick(deltaSeconds))
            return false;
        return true;
    }

    public void Draw()
    {
        objectHandler.Draw();
    }
}

