using Godot;
using System;
namespace CRTGame;

public partial class ExitButton : Button
{
	[Export]
	public PauseCanvas canvasLayer;
	[Export]
	public ColorRect settingsMenu;
	[Export]
	public ColorRect pauseMenu;
	
	
	private void OnPressed()
	{
		settingsMenu = GetParent<ColorRect>();
		settingsMenu.Hide();
		pauseMenu = GetParent<ColorRect>();
		pauseMenu.Show();
		canvasLayer.UnPause();
		
	}
	
}
