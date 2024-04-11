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

    // // TODO
    // // save changes
    // // get mouse position in inspector

    public override void _EnterTree()
    {

     
    }

    public override void _ExitTree()
    {
    }

    //public override void _Draw()
    //{
    //    var center = new Vector2(0, 0);
    //    float radius = 45;
    //    var color = new Color(1, 0, 0);
    //    DrawCircle(center, radius, color);

        
    //}

    public override void _Input(InputEvent @event)
    {
        if (Engine.IsEditorHint())
        {
            Vector2 localMousePos = GetLocalMousePosition();

            if (!GetRect().HasPoint(localMousePos))
            {
                EditableSpriteHolder.EditableObject = null;
                return;
            }

            Vector2 new_position = new Vector2(localMousePos[0] / 2, localMousePos[1] / 2);

            HandlePoint point = new HandlePoint(new_position);
            point.Visible = true;

            EditableSprite2D sprite = EditableSpriteHolder.EditableObject;

            sprite.AddChild(point, false, InternalMode.Back);

//#if TOOLS
//            VisualServer.
//#endif

            //sprite.AddChild(point);
            GD.Print(localMousePos);
        }
    }

    public override void _Process(double delta)
    {
        QueueRedraw();

        //GD.Print(GetViewport().GetMousePosition());

        base._Process(delta);
    }

    public override void _Ready()
    {
        
    }
}
