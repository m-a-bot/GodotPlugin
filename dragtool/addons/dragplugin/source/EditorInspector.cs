using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

public partial class EditorInspector : EditorInspectorPlugin
{
    private int seed = 1000;

    private List<string> name_networks = new()
    {
        "2d_game_scenes",
    };

    private LineEdit seedEdit;

    public override bool _CanHandle(GodotObject @object)
    {
        return @object.GetType() == typeof(EditableSprite2D);
    }

    public override void _ParseBegin(GodotObject @object)
    {

        //GD.Print(EditableObject.GetClass());

        seedEdit = new LineEdit() { Text = $"{seed}" };
        seedEdit.TextChanged += SeedEdit_TextChanged;


        Label title = new Label() { Text = "2D scene generator" };
        BoxContainer __title = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        __title.AddChild(title);

        Label pickle_section = new Label() { Text = "Pickle" };
        OptionButton pickle_variants = new OptionButton();
        foreach (string name_net in name_networks)
        {
            pickle_variants.AddItem(name_net);
        }
      
        pickle_variants.ItemSelected += Pickle_variants_ItemSelected;

        BoxContainer boxContainer = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer.AddChild(pickle_variants);

        // Latent
        Label latent_section = new Label() { Text = "Latent" };
        Label __seed = new Label() { Text = " Seed " };

        Button upSeed = new Button() { Text = "+" };
        upSeed.Pressed += UpSeed_Pressed;

        Button downSeed = new Button() { Text = "-" };
        downSeed.Pressed += DownSeed_Pressed;

        BoxContainer rowBoxSeed = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        rowBoxSeed.AddChild(__seed);
        rowBoxSeed.AddChild(new VSeparator());
        rowBoxSeed.AddChild(seedEdit);
        rowBoxSeed.AddChild(new VSeparator());
        rowBoxSeed.AddChild(downSeed);
        rowBoxSeed.AddChild(new VSeparator());
        rowBoxSeed.AddChild(upSeed);


        BoxContainer mainContainer = new BoxContainer();
        mainContainer.Vertical = true;

        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(__title);
        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(pickle_section);
        mainContainer.AddChild(boxContainer);

        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(latent_section);
        mainContainer.AddChild(rowBoxSeed);

        Button generateButton = new Button() { Text = " Generate image" };
        generateButton.Pressed += GenerateButton_Pressed;

        BoxContainer centerBoxContainer = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        centerBoxContainer.AddChild(generateButton);
        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(centerBoxContainer);


        mainContainer.AddChild(new HSeparator());

        AddCustomControl(mainContainer);

    }

    private void Pickle_variants_ItemSelected(long index)
    {
        GD.Print(Directory.GetCurrentDirectory());
        GD.Print(name_networks[(int) index]);
    }

    private void DownSeed_Pressed()
    {
        if (seed < 1)
            return;
        seed -= 1;
        seedEdit.Text = $"{seed}";
    }

    private void UpSeed_Pressed()
    {
        seed += 1;
        seedEdit.Text = $"{seed}";
    }

    private void SeedEdit_TextChanged(string newText)
    {
        int new_seed;
        bool success = int.TryParse(newText, out new_seed);

        if (!success || new_seed < 0)
        {
            seedEdit.Text = $"{seed}";
            return;
        }

        seed = new_seed;
    }

    private void GenerateButton_Pressed()
    {
        //if (targetObject is not EditableSprite2D sprite)
        //    return;

        //sprite.Texture = GD.Load<Texture2D>("res://addons/dragplugin/resources/texture.png");

        Model.ExecProcess(seed);
    }
}
