namespace SuperMarioWPF;

class Vec2<T> where T : notnull
{
    public T x { get; set; }
    public T y { get; set; }

    public Vec2(T x, T y)
    {
        this.x = x;
        this.y = y;
    }

    public Vec2(T v)
    {
        this.x = v;
        this.y = v;
    }

    public Vec2<T> Copy()
    {
        return new Vec2<T>(x, y);
    }

    public Vec2<T> Add(Vec2<T> other)
    {
        dynamic tx = x, ty = y, ox = other.x, oy = other.y;
        tx += ox;
        ty += oy;
        x = tx;
        y = ty;
        return Copy();
    }

    public Vec2<T> Subtract(Vec2<T> other)
    {
        dynamic tx = x, ty = y, ox = other.x, oy = other.y;
        tx -= ox;
        ty -= oy;
        x = tx;
        y = ty;
        return Copy();
    }

    public Vec2<T> Multiply(Vec2<T> other)
    {
        dynamic tx = x, ty = y, ox = other.x, oy = other.y;
        tx *= ox;
        ty *= oy;
        x = tx;
        y = ty;
        return Copy();
    }
}