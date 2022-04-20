using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SuperMarioWPF.Utils;

namespace SuperMarioWPF.Objects;

class Terrain : LevelObject
{
    private Image sprite;
    private Vec2<double> Box { get; set; } 

    public Terrain(Game game)
        : base(game)
    {
        Box = new Vec2<double>(2120, 667);
        Position.X = -100;
        Position.Y = 300;
        sprite = new Image
        {
            Width = Box.X,
            Height = Box.Y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\terreno.png", UriKind.Absolute)),
        };
    }

    public override void Draw()
    {
        Canvas.SetLeft(sprite, Position.X);
        Canvas.SetTop(sprite, Position.Y - 87);
    }

    public override void Load(Canvas canvas)
    {
        canvas.Children.Add(sprite);
    }

    public override bool Tick(double deltaSeconds)
    {
        Position.X = -game.scroll;
        return true;
    }
}