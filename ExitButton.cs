using Godot;
using System;
namespace CRTGame;

public partial class ExitButton : Button
{
	[Export]
	public PauseCanvas canvasLayer;
	
	
	private void OnPressed()
	{
		canvasLayer.UnPause();
		GD.Print("closed?"); 
	}
	
}
