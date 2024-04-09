using Godot;
using System;

public partial class EditorInspector : EditorInspectorPlugin
{
    private int seed = 1000;
    private float step_size = 0.001f;
    private int mask_radius = 50;
    private int lambda = 20;
    private bool show_mask = false;

    private LineEdit seedEdit;
    private LineEdit stepSizeEdit;
    private LineEdit maskRadiusEdit;
    private LineEdit lambdaEdit;

    public bool Perfect { get; set; } = true;
    public int Steps { get; set; } = 0;

    public override bool _CanHandle(GodotObject @object)
    {
        return @object.GetType() == typeof(EditableSprite2D);
    }

    public override void _ParseBegin(GodotObject @object)
    {
        seedEdit = new LineEdit() { Text = $"{seed}" };
        stepSizeEdit = new LineEdit() { Text = $"{step_size}" };
        maskRadiusEdit = new LineEdit() { Text = $"{mask_radius}" };
        lambdaEdit = new LineEdit() { Text = $"{lambda}" };

        Label title = new Label() { Text = "Generator" };
        BoxContainer __title = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        __title.AddChild(title);

        Label pickle_section = new Label() { Text = "Pickle" };
        OptionButton pickle_variants = new OptionButton();
        pickle_variants.AddItem("2d_game_scenes");
        pickle_variants.AddItem("2d_game_scenes_da");
        pickle_variants.AddItem("2d_game_scenes_da_generated");
        pickle_variants.AddItem("pretrained_imagenet_2d_game_scenes");
        pickle_variants.AddItem("abstract_scenes");
        pickle_variants.AddItem("pretrained_wikiart_abstract_scenes");
        pickle_variants.AddItem("Mario_da");
        pickle_variants.AddItem("pretrained_imagenet_Mario_da");

        BoxContainer boxContainer = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center};
        boxContainer.AddChild(pickle_variants);

        // Latent
        Label latent_section = new Label() { Text = "Latent" };
        Label __seed = new Label() { Text = "Seed" };
        Label __stepSize = new Label() { Text = "Step_size" };
        Button reset = new Button() { Text = "Reset" };
        OptionButton latent_variants = new OptionButton();
        latent_variants.AddItem("W");
        latent_variants.AddItem("W+");

        BoxContainer boxContainer1 = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        boxContainer1.AddChild(__seed);
        boxContainer1.AddChild(new VSeparator());
        boxContainer1.AddChild(seedEdit);

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

        Button generateButton = new Button() { Text = "Generate" };
        generateButton.Pressed += GenerateButton_Pressed;

        BoxContainer centerBoxContainer = new BoxContainer() { Alignment = BoxContainer.AlignmentMode.Center };
        centerBoxContainer.AddChild(generateButton);
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

    private void GenerateButton_Pressed()
    {
        Model.ExecProcess(seed);
    }
}
