using Godot;
using System;
using System.Linq;

[Tool]
public partial class EditableSprite2D : Sprite2D
{

    [Export]
    public float Width
    {
        get 
        {
            return Texture != null ? Texture.GetWidth() * Scale[0] : 0;
        }
        set
        {
            Vector2 vector = Vector2.One;
            if (Texture != null)
            {
                vector = new Vector2(value / Texture.GetWidth(), Scale[1]);
            }
            Scale = vector;
        }
    }
    [Export]
    public float Height
    {
        get 
        { 
            return Texture != null ? Texture.GetHeight() * Scale[1] : 0; 
        }
        set
        {
            Vector2 vector = Vector2.One;
            if (Texture != null)
            {
                Scale = new Vector2(Scale[0], value / Texture.GetHeight());
            }
            Scale = vector;
        }
    }

    // TODO
    // save changes
    // get mouse position in inspector


    public void Resize(float newWidth, float newHeight)
    {
        if (Texture != null)
        {
            Width = newWidth;
            Height = newHeight;
        }
    }
    public override void _EnterTree()
    {
        base._EnterTree();
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }

    public override void _Input(InputEvent @event)
    {
        
        GD.Print(@event);
        GD.Print("sprite");

        //if (@event is InputEventMouseButton eventMouseButton)
        //    GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
        //else if (@event is InputEventMouseMotion eventMouseMotion)
        //    GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

        //Print the size of the viewport.
        //GD.Print("Viewport Resolution is: ", GetViewport().GetVisibleRect().Size);

        //GD.Print(GetViewport().GetMousePosition());
        //base._Input(@event);
    }

    public override void _Draw()
    {
        var center = new Vector2(0, 0);
        float radius = 45;
        var color = new Color(1, 0, 0);
        DrawCircle(center, radius, color);
    }

    public override void _Process(double delta)
    {
        QueueRedraw();

        //GD.Print(GetViewport().GetMousePosition());

        base._Process(delta);
    }

    public override void _Ready()
    {
        SetProcessInput(true);
    }

    //public override Godot.Collections.Dictionary<string, object> _Save()
    //{
    //    var saveData = base._Save();
    //    saveData["width"] = Width;
    //    saveData["height"] = Height;
    //    return saveData;
    //}

    //public override void _Load(Godot.Collections.Dictionary<string, object> data)
    //{
    //    base._Load(data);
    //    if (data.Contains("width"))
    //        Width = (float)data["width"];
    //    if (data.Contains("height"))
    //        Height = (float)data["height"];
    //}
}
