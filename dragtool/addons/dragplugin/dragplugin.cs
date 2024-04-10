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
			var script1 = GD.Load<Script>("res://addons/dragplugin/EditableSprite2D.cs");
            var script2 = GD.Load<Script>("res://addons/dragplugin/HandlePoint.cs");
            var script3 = GD.Load<Script>("res://addons/dragplugin/TargetPoint.cs");
            AddCustomType("EditableSprite2D", "Sprite2D", script1, null);
            AddCustomType("HandlePoint", "Node2D", script2, null);
            AddCustomType("TargetPoint", "Node2D", script3, null);
        }
		_plugin = new EditorInspector();
		_plugin.Perfect = false;
		AddInspectorPlugin(_plugin);
    }

	public override void _ExitTree()
	{
		RemoveInspectorPlugin(_plugin);

        //GetViewport().SetProcessInput(false);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButtonEvent
            && mouseButtonEvent.Pressed
            && mouseButtonEvent.ButtonIndex == MouseButton.Left)
        {

            EditableSprite2D sprite = EditableSpriteHolder.EditableObject;

            if (sprite is null)
                return;

            GD.Print(sprite);

            EditableSpriteHolder.AddPoint = true;
            sprite._Input(@event);

            //Vector2 mousePos = GetViewport().GetMousePosition();
            //Camera2D camera = GetViewport().GetCamera2D();
            
            //GD.Print(camera);

            //Vector2 globalMousePos = camera.GetGlobalMousePosition();
            //Vector2 localMousePos = camera.GetLocalMousePosition();

            //GD.Print(sprite.GetRect().;
            //GD.Print(globalMousePos);
            //GD.Print(localMousePos);
        }
    }
}
#endif
