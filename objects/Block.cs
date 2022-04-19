using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SuperMarioWPF;

class Block : LevelObject
{
    private Image sprite;
    private double offsetx;

    public Block(Game game, Vec2<double> p)
        : base(game)
    {
        box = new Vec2<double>(32, 32);
        pos.y = p.y;
        offsetx = p.x;
        sprite = new Image
        {
            Width = box.x,
            Height = box.y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\block.png", UriKind.Absolute)),
        };
    }

    public override void Draw(Dispatcher dispatcher)
    {
        dispatcher.Invoke(() =>
        {
            Canvas.SetLeft(sprite, pos.x);
            Canvas.SetTop(sprite, pos.y);
        });
    }

    public override void Load(Canvas canvas)
    {
        canvas.Children.Add(sprite);
    }

    public override bool Tick(double deltaTS)
    {
        pos.x = -game.scroll + offsetx;
        return true;
    }
}