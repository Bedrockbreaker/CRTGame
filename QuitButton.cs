using Godot;
using System;

public partial class QuitButton : Button
{
	private void OnPressed()
	{
		GetTree().Quit();
		GD.Print("press");
	}
}
