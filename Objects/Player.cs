using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SuperMarioWPF.Utils;

namespace SuperMarioWPF.Objects;

internal class Player : PhysicsObject
{
    private readonly Image sprite;
    private bool falling = true;
    private bool facingRight = true;
    private bool usingWalkingImageOne = true;
    
    public Player(Game game)
        : base(game)
    {
        Box = new Vec2<double>(32, 32);
        Position.X = 100;
        sprite = new Image
        {
            Width = Box.X,
            Height = Box.Y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario1.png", UriKind.Absolute)),
        };
    }

    public override bool Tick(double deltaSeconds)
    {
        if (game.keyboard.Get(Key.Left) == KeyStates.Down && game.keyboard.Get(Key.Right) != KeyStates.Down)
        {
            Velocity.X = -256;
            facingRight = false;
        }
        else if (game.keyboard.Get(Key.Left) != KeyStates.Down && game.keyboard.Get(Key.Right) == KeyStates.Down)
        {
            Velocity.X = 256;
            facingRight = true;
        }
        else
            Velocity.X = 0;
        var deltaPosX = Velocity.X * deltaSeconds;
        Position.X += deltaPosX;
        var collision = false;
        foreach (var o in game.CollidingObjects(this))
            if (o is Enemy)
                return false;
            else if (o != this)
                collision = true;
        if (collision)
        {
            Position.X -= deltaPosX;
            Velocity.X = 0;
        }
        if (Position.X > 360)
        {
            game.scroll += Position.X - 360;
            Position.X = 360;
        }
        else if (Position.X < 0)
        {
            Position.X = 0;
        }
        Velocity.Y += 1000 * deltaSeconds;
        if (!falling && game.keyboard.Get(Key.Up) == KeyStates.Down)
        {
            Velocity.Y -= 512;
            Position.Y -= 8;
            falling = true;
        }
        var deltaPosY = Velocity.Y * deltaSeconds;
        Position.Y += deltaPosY;
        collision = false;
        ICollidable? collider = null;
        foreach (var o in game.CollidingObjects(this))
            if (o is Enemy)
                return false;
            else if (o != this)
            {
                collision = true;
                collider = o;
            }

        if (!collision || collider == null) return true;
        Position.Y -= (Position.Y + Position.Y) - collider.Position.Y;
        Velocity.Y = 0;
        falling = false;
        return true;
    }

    public override void Load(Canvas canvas)
    {
        canvas.Children.Add(sprite);
    }

    public override void Draw()
    { 
        Canvas.SetLeft(sprite, Position.X);
        Canvas.SetTop(sprite, Position.Y);
        sprite.LayoutTransform = facingRight ? new ScaleTransform() { ScaleX = 1 } : new ScaleTransform() { ScaleX = -1 };
        if (falling)
            sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario6.png", UriKind.Absolute));
        else if (Velocity.X != 0 && usingWalkingImageOne)
            sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario2.png", UriKind.Absolute));
        else if (Velocity.X != 0 && !usingWalkingImageOne)
            sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario4.png", UriKind.Absolute));
        else
            sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario1.png", UriKind.Absolute));
        usingWalkingImageOne = !usingWalkingImageOne;
    }

}