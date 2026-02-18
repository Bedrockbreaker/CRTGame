using Godot;
using System;

public partial class SettingsButton : Button
{
	
	[Export]
	public ColorRect settingsMenu;
	[Export]
	public ColorRect pauseMenu;
	private bool isOpen = false;
	
	private void OnPressed()
	{
		if (isOpen == false)
		{
		isOpen = true;
		settingsMenu.Show();
		pauseMenu.Hide();
		}
		
		else 
		{
		isOpen = false;
		settingsMenu.Hide();
		pauseMenu.Show();
		}
	}
}
