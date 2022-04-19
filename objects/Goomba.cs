using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SuperMarioWPF;

class Goomba : Enemy
{
    private Image sprite;
    public double offsetx;
    private Player player;
    private bool usingImageOne = true;

    public Goomba(Game game, Vec2<double> p, Player pl)
        : base(game)
    {
        box = new Vec2<double>(32, 32);
        pos.y = p.y;
        offsetx = p.x;
        player = pl;
        sprite = new Image
        {
            Width = box.x,
            Height = box.y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\nemico1.png", UriKind.Absolute)),
        };
    }

    public override void Load(Canvas canvas)
    {
        canvas.Children.Add(sprite);
    }

    public override void Draw(Dispatcher dispatcher)
    {
        dispatcher.Invoke(() =>
        {
            Canvas.SetLeft(sprite, pos.x);
            Canvas.SetTop(sprite, pos.y);
            if (usingImageOne)
                sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\nemico1.png", UriKind.Absolute));
            else
                sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\nemico2.png", UriKind.Absolute));
            usingImageOne = !usingImageOne;
            if (shouldRemoveSelf)
                game.objects.Remove(this);
        });
    }

    public override bool Tick(double deltaTS)
    {
        pos.x = -game.scroll + offsetx;
        if (player.pos.x < pos.x)
            vel.x = -128;
        else
            vel.x = 128;
        offsetx += vel.x * deltaTS;
        return true;
    }
}