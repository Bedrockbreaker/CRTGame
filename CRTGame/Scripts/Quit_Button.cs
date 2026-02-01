using Godot;
using System;

public partial class Quit_Button : Button
{
    private void OnPressed()
    {
        GetTree().Quit();
    }
}
