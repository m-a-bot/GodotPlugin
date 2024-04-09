using System;
using System.Diagnostics;
using Godot;

internal class Model
{
    public Model() 
    {
            
    }

    public static void ExecProcess(int seed)
    {
        // 1) Create Process Info
        var psi = new ProcessStartInfo();
        psi.FileName = @"C:\Users\TM\AppData\Local\Programs\Python\Python39\python.exe";


        // 2) Provide script and arguments
        var script = @"C:\Users\TM\Documents\GodotPlugin\dragtool\addons\dragplugin\source\run_python_from_c#.py";

        var test = $"Hello, Python - {seed}";

        psi.Arguments = $"\"{script}\" \"{test}\"";

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

        GD.Print(results);
    }   

}

