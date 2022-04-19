using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SuperMarioWPF;

internal class Player : GameObject
{
    private readonly Image sprite;
    private bool falling = true;
    private bool facingRight = true;
    private bool usingWalkingImageOne = true;
    
    public Player(Game game)
        : base(game)
    {
        box = new Vec2<double>(32, 32);
        pos.x = 100;
        sprite = new Image
        {
            Width = box.x,
            Height = box.y,
            Stretch = Stretch.Fill,
            Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario1.png", UriKind.Absolute)),
        };
    }

    public override bool Tick(double deltaTS)
    {
        if (game.keyboard.Get(Key.Left) == KeyStates.Down && game.keyboard.Get(Key.Right) != KeyStates.Down)
        {
            vel.x = -256;
            facingRight = false;
        }
        else if (game.keyboard.Get(Key.Left) != KeyStates.Down && game.keyboard.Get(Key.Right) == KeyStates.Down)
        {
            vel.x = 256;
            facingRight = true;
        }
        else
            vel.x = 0;
        var deltaPosX = vel.x * deltaTS;
        pos.x += deltaPosX;
        var collision = false;
        foreach (var o in game.CollidingObjects(this))
            if (o is Enemy)
                return false;
            else if (o != this)
                collision = true;
        if (collision)
        {
            pos.x -= deltaPosX;
            vel.x = 0;
        }
        if (pos.x > 360)
        {
            game.scroll += pos.x - 360;
            pos.x = 360;
        }
        else if (pos.x < 0)
        {
            pos.x = 0;
        }
        vel.y += 1000 * deltaTS;
        if (!falling && game.keyboard.Get(Key.Up) == KeyStates.Down)
        {
            vel.y -= 512;
            pos.y -= 8;
            falling = true;
        }
        var deltaPosY = vel.y * deltaTS;
        pos.y += deltaPosY;
        collision = false;
        GameObject? collider = null;
        foreach (var o in game.CollidingObjects(this))
            if (o is Enemy)
                return false;
            else if (o != this)
            {
                collision = true;
                collider = o;
            }
        if (collision && collider != null)
        {
            pos.y -= (pos.y + box.y) - collider.pos.y;
            vel.y = 0;
            falling = false;
        }
        return true;
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
            if (facingRight)
                sprite.LayoutTransform = new ScaleTransform() { ScaleX = 1 };
            else
                sprite.LayoutTransform = new ScaleTransform() { ScaleX = -1 };
            if (falling)
                sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario6.png", UriKind.Absolute));
            else if (vel.x != 0 && usingWalkingImageOne)
                sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario2.png", UriKind.Absolute));
            else if (vel.x != 0 && !usingWalkingImageOne)
                sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario4.png", UriKind.Absolute));
            else
                sprite.Source = new BitmapImage(new Uri(@"Z:\opgaver-eud\SuperMarioWPF\assets\supermario\textures\mario1.png", UriKind.Absolute));
            usingWalkingImageOne = !usingWalkingImageOne;
        });
    }

}