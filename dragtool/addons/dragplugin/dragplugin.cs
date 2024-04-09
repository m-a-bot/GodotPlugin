#if TOOLS
using Godot;
using System;

[Tool]
public partial class dragplugin : EditorPlugin
{
	private EditorInspector _plugin;
	public override void _EnterTree()
	{
		{
			var script = GD.Load<Script>("res://addons/dragplugin/EditableSprite2D.cs");
			AddCustomType("EditableSprite2D", "Sprite2D", script, null);
		}
		_plugin = new EditorInspector();
		_plugin.Perfect = false;
		AddInspectorPlugin(_plugin);

        
    }

	public override void _ExitTree()
	{
		RemoveInspectorPlugin(_plugin);

        GetViewport().SetProcessInput(false);
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        GD.Print(@event);

        if (@event is InputEventMouseButton eventMouseButton)
            GD.Print("Mouse Click/Unclick at: ", eventMouseButton.Position);
        else if (@event is InputEventMouseMotion eventMouseMotion)
            GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

        // Print the size of the viewport.
        GD.Print("Viewport Resolution is: ", GetViewport().GetVisibleRect().Size);

        GD.Print(GetViewport().GetMousePosition());
    }
}
#endif
