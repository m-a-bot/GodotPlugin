#if TOOLS
using Godot;
using System;

[Tool]
public partial class Plugin : EditorPlugin
{
	private EditorInspector _plugin;

	public override void _EnterTree()
	{
		{
			var script1 = GD.Load<Script>("res://addons/dragplugin/source/EditableSprite2D.cs");

            AddCustomType("SceneFrame", "Sprite2D", script1, null);

        }
        _plugin = new EditorInspector();

        AddInspectorPlugin(_plugin);

    }

	public override void _ExitTree()
	{
		RemoveCustomType("SceneFrame");

        RemoveCustomType("EditableSprite2D");

        RemoveCustomType("TargetPoint");

        RemoveCustomType("HandlePoint");

        RemoveInspectorPlugin(_plugin);

    }
}
#endif
