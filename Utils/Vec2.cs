using System;

namespace SuperMarioWPF.Utils;

public class Vec2<T> where T : notnull
{
    public T X { get; set; }
    public T Y { get; set; }

    public Vec2(T x, T y)
    {
        this.X = x;
        this.Y = y;
    }

    public Vec2(T v)
    {
        this.X = v;
        this.Y = v;
    }

    public Vec2<T> Copy()
    {
        return new Vec2<T>(X, Y);
    }

    public Vec2<T> Add(Vec2<T> other)
    {
        dynamic tx = X, ty = Y, ox = other.X, oy = other.Y;
        tx += ox;
        ty += oy;
        X = tx;
        Y = ty;
        return Copy();
    }

    public Vec2<T> Subtract(Vec2<T> other)
    {
        dynamic tx = X, ty = Y, ox = other.X, oy = other.Y;
        tx -= ox;
        ty -= oy;
        X = tx;
        Y = ty;
        return Copy();
    }

    public Vec2<T> Multiply(Vec2<T> other)
    {
        dynamic tx = X, ty = Y, ox = other.X, oy = other.Y;
        tx *= ox;
        ty *= oy;
        X = tx;
        Y = ty;
        return Copy();
    }
    
    public Vec2<T> DivideBoth(T divider)
    {
        dynamic tx = X, ty = Y;
        tx *= divider;
        ty *= divider;
        X = tx;
        Y = ty;
        return Copy();
    }

    public T Length()
    {
        dynamic tx = X, ty = Y;
        return Math.Sqrt(Math.Pow(tx, 2) + Math.Pow(ty, 2));
    }

}