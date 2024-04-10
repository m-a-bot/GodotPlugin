using System;
using Godot;


internal partial class HandlePoint : Node2D
{
    private float _radius;

    private Color? _color;

    public HandlePoint()
    {

    }

    public HandlePoint(Vector2 position, float radius = 3, Color? color = null)
    {
        Position = position;

        _radius = radius;

        if (color is null)
            _color = new Color(1, 0, 0);
        else
            _color = color;
    }

    public override void _Draw()
    {
        DrawCircle(Position, _radius, _color.Value);
    }
}

