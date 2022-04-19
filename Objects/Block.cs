using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SuperMarioWPF.Utils;

namespace SuperMarioWPF.Objects;

class Block : LevelObject, ICollidable
{
    private Image sprite;
    private double offsetx;

    public Vec2<double> Velocity { get; set; }
    public Vec2<double> Box { get; set; }
    public Block(Game game, Vec2<double> p)
        : base(game)
    {
        Box = new Vec2<double>(32, 32);
        Position.Y = p.Y;
        offsetx = p.X;
        sprite = new Image
        {
            Width = Box.X,
            Height = Box.Y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\block.png", UriKind.Absolute)),
        };
    }

    public override void Draw()
    {
        Canvas.SetLeft(sprite, Position.X);
        Canvas.SetTop(sprite, Position.Y);
    }

    public override void Load(Canvas canvas)
    {
        canvas.Children.Add(sprite);
    }

    public override bool Tick(double deltaSeconds)
    {
        Position.X = -game.scroll + offsetx;
        return true;
    }
    public bool DoesCollide(ICollidable other)
    {
        throw new NotImplementedException();
    }
}