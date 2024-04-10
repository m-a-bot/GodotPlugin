using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

public partial class EditorInspector : EditorInspectorPlugin
{
    private int seed = 1000;
    private float step_size = 0.001f;
    private int mask_radius = 50;
    private int lambda = 20;
    private bool show_mask = false;

    private List<string> name_networks = new()
    {
        "2d_game_scenes",
        "2d_game_scenes_da",
        "2d_game_scenes_da_generated",
        "pretrained_imagenet_2d_game_scenes"
    };

    private LineEdit seedEdit;
    private LineEdit stepSizeEdit;
    private LineEdit maskRadiusEdit;
    private LineEdit lambdaEdit;

    public bool Perfect { get; set; } = true;
    public int Steps { get; set; } = 0;

    public bool Add { get; set; } = false;
    public bool Remove { get; set; } = false;

    public override bool _CanHandle(GodotObject @object)
    {
        return @object.GetType() == typeof(EditableSprite2D);
    }

    public override void _ParseBegin(GodotObject @object)
    {
        EditableSpriteHolder.EditableObject = @object as EditableSprite2D;

        //GD.Print(EditableObject.GetClass());

        seedEdit = new LineEdit() { Text = $"{seed}" };
        seedEdit.TextChanged += SeedEdit_TextChanged;

        stepSizeEdit = new LineEdit() { Text = $"{step_size}" };
        maskRadiusEdit = new LineEdit() { Text = $"{mask_radius}" };
        lambdaEdit = new LineEdit() { Text = $"{lambda}" };

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

        Label __stepSize = new Label() { Text = "Step_size" };
        Button reset = new Button() { Text = "Reset" };
        OptionButton latent_variants = new OptionButton();
        latent_variants.AddItem("W");
        latent_variants.AddItem("W+");

        BoxContainer boxContainer1 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer1.AddChild(__seed);
        boxContainer1.AddChild(new VSeparator());
        boxContainer1.AddChild(seedEdit);
        boxContainer1.AddChild(new VSeparator());
        boxContainer1.AddChild(downSeed);
        boxContainer1.AddChild(new VSeparator());
        boxContainer1.AddChild(upSeed);

        BoxContainer boxContainer2 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer2.AddChild(__stepSize);
        boxContainer2.AddChild(new VSeparator());
        boxContainer2.AddChild(stepSizeEdit);

        BoxContainer boxContainer3 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer3.AddChild(reset);
        boxContainer3.AddChild(new VSeparator());
        boxContainer3.AddChild(latent_variants);

        // Drag
        Label drag_section = new Label() { Text = "Drag" };
        Button add_point = new Button() { Text = "Add point" };
        Button reset_point = new Button() { Text = "Reset point" };
        Button start = new Button() { Text = "Start" };
        Button stop = new Button() { Text = "Stop" };
        Label currentSteps = new Label() { Text = $"Steps: {Steps}" };

        BoxContainer boxContainer4 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer4.AddChild(add_point);
        boxContainer4.AddChild(new VSeparator());
        boxContainer4.AddChild(reset_point);

        BoxContainer boxContainer5 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer5.AddChild(start);
        boxContainer5.AddChild(new VSeparator());
        boxContainer5.AddChild(stop);

        BoxContainer boxContainer6 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer6.AddChild(currentSteps);

        // Mask
        Label mask_section = new Label() { Text = "Mask" };
        CheckButton use_mask = new CheckButton() { Text = "Use mask" };
        Button reset_mask = new Button() { Text = "Reset mask" };
        CheckButton show_mask = new CheckButton() { Text = "Show mask" };
        Label __radius = new Label() { Text = "Radius" };
        Label __lambda = new Label() { Text = "Lambda" };

        BoxContainer boxContainer7 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer7.AddChild(use_mask);
        boxContainer7.AddChild(new VSeparator());
        boxContainer7.AddChild(reset_mask);
        boxContainer7.AddChild(new VSeparator());
        boxContainer7.AddChild(show_mask);

        BoxContainer boxContainer8 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer8.AddChild(__radius);
        boxContainer8.AddChild(new VSeparator());
        boxContainer8.AddChild(maskRadiusEdit);
        boxContainer8.AddChild(new VSeparator());
        boxContainer8.AddChild(__lambda);
        boxContainer8.AddChild(new VSeparator());
        boxContainer8.AddChild(lambdaEdit);

        BoxContainer mainContainer = new BoxContainer();
        mainContainer.Vertical = true;

        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(__title);
        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(pickle_section);
        mainContainer.AddChild(boxContainer);

        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(latent_section);
        mainContainer.AddChild(boxContainer1);

        Button generateButton = new Button() { Text = " Generate " };
        generateButton.Pressed += GenerateButton_Pressed;

        BoxContainer centerBoxContainer = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        centerBoxContainer.AddChild(generateButton);
        mainContainer.AddChild(new HSeparator());
        mainContainer.AddChild(centerBoxContainer);

        if (Perfect)
        {
            mainContainer.AddChild(boxContainer2);
            mainContainer.AddChild(boxContainer3);

            mainContainer.AddChild(new HSeparator());
            mainContainer.AddChild(drag_section);
            mainContainer.AddChild(boxContainer4);
            mainContainer.AddChild(boxContainer5);
            mainContainer.AddChild(boxContainer6);

            mainContainer.AddChild(new HSeparator());
            mainContainer.AddChild(mask_section);
            mainContainer.AddChild(boxContainer7);
            mainContainer.AddChild(boxContainer8);
        }
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
