using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


static internal class EditableSpriteHolder
{
    public static EditableSprite2D EditableObject { get; set; }

    public static bool EditPoints { get; set; } = false;

    public static bool AddPoint { get; set; } = false;

    public static bool RemovePoint { get; set; } = false;
}

