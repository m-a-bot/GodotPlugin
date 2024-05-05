using System;
using System.Diagnostics;
using System.IO;
using Godot;

internal class Model
{
    public Model() 
    {
            
    }

    public static void ExecProcess(EditableSprite2D sprite2d, string pathToPython, string pathToPickle, string seed)
    {
        string current_directory = Directory.GetCurrentDirectory();

        //GD.Print(current_directory);

        // 1) Create Process Info
        var psi = new ProcessStartInfo();
        psi.FileName = pathToPython;


        // 2) Provide script and arguments
        var script = current_directory + @"\addons\dragplugin\source\" + pathToPickle +".py";

        psi.Arguments = $"\"{script}\" \"{current_directory + @"\addons\dragplugin\resources\"} {seed}\"";

        // 3) Process configuration
        psi.UseShellExecute = false;
        psi.CreateNoWindow = true;
        psi.RedirectStandardOutput = true;
        psi.RedirectStandardError = true;

        // 4) Execute process and get output
        var errors = "";
        var results = "";
        using (var process = Process.Start(psi))
        {
            errors = process.StandardError.ReadToEnd();
            results = process.StandardOutput.ReadToEnd();
        }

        if (errors.Length > 0)
            return;

        try
        {
            // "res://addons/dragplugin/resources/" + 
            //ResourceLoader.Load<CompressedTexture2D>(@"res://addons/dragplugin/resources/" + results)

            sprite2d.Texture = GD.Load<CompressedTexture2D>(@"res://addons/dragplugin/resources/" + results);
            // GD.Load<CompressedTexture2D>(@"res://addons/dragplugin/resources/" + results);
        }
        catch {

        }
        //GD.Print(results);
        //GD.Print(errors);
    }   

}

