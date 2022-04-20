using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SuperMarioWPF.Utils;

namespace SuperMarioWPF.Objects;

class Goomba : Enemy
{
    private Image sprite;
    public double offsetx;
    private Player player;
    private bool usingImageOne = true;

    public Goomba(Game game, Vec2<double> p, Player pl)
        : base(game)
    {
        Box = new Vec2<double>(32, 32);
        Position.Y = p.Y;
        offsetx = p.X;
        player = pl;
        sprite = new Image
        {
            Width = Box.X,
            Height = Box.Y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\nemico1.png", UriKind.Absolute)),
        };
    }

    public override void Load(Canvas canvas)
    {
        canvas.Children.Add(sprite);
    }

    public override void Draw()
    {
        Canvas.SetLeft(sprite, Position.X);
        Canvas.SetTop(sprite, Position.Y);
        if (usingImageOne)
            sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\nemico1.png", UriKind.Absolute));
        else
            sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\nemico2.png", UriKind.Absolute));
        usingImageOne = !usingImageOne;
    }

    public override bool Tick(double deltaSeconds)
    {
        Position.X = -game.scroll + offsetx;
        if (player.Position.X < Position.X)
            Velocity.X = -128;
        else
            Velocity.X = 128;
        offsetx += Velocity.X * deltaSeconds;
        return true;
    }
}