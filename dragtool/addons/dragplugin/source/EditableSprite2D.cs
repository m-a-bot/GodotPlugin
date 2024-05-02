using Godot;
using System;
using System.Linq;

[Tool]
public partial class EditableSprite2D : Sprite2D
{

    private float width;
    private float height;


    [Export]
    public float Width
    {
        get 
        {
            return Texture != null ? Texture.GetWidth() * Scale[0] : 0;
        }
        private set {
            width = value;
        }
    }
    [Export]
    public float Height
    {
        get 
        {
            return Texture != null ? Texture.GetHeight() * Scale[1] : 0;
        }
        private set {
            height = value;
        }
    }

    public void Resize(float newWidth, float newHeight)
    {
        
        Width = newWidth;
        Height = newHeight;
    }

    public override void _EnterTree()
    {

        base._EnterTree();

    }

    public override void _ExitTree()
    {
        base._ExitTree();

    }

    public override void _Ready()
    {
        
    }
}
