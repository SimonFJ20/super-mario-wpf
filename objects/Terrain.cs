using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SuperMarioWPF;

class Terrain : LevelObject
{
    private Image sprite;

    public Terrain(Game game)
        : base(game)
    {
        box = new Vec2<double>(2120, 667);
        pos.x = -100;
        pos.y = 300;
        sprite = new Image
        {
            Width = box.x,
            Height = box.y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\terreno.png", UriKind.Absolute)),
        };
    }

    public override void Draw(Dispatcher dispatcher)
    {
        dispatcher.Invoke(() =>
        {
            Canvas.SetLeft(sprite, pos.x);
            Canvas.SetTop(sprite, pos.y - 87);
        });
    }

    public override void Load(Canvas canvas)
    {
        canvas.Children.Add(sprite);
    }

    public override bool Tick(double deltaTS)
    {
        pos.x = -game.scroll;
        return true;
    }
}