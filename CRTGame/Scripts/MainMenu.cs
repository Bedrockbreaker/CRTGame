using Godot;
using System;

public partial class MainMenu : Node2D
{
	[Export] public Button startButton;
	[Export] public Button optionsButton;
	[Export] public Button creditsButton;
	[Export] public Button quitButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	// START
	public void _on_button_start_button_up()
	{
		// TODO: Scene management, start the game
	}
	
	// OPTIONS
	public void _on_button_options_button_up()
	{
		// TODO: open options menu (keep this menu loaded)
	}
	
	// CREDITS
	public void _on_button_credits_button_up()
	{
		// TODO: open credits menu
	}
	
	// QUIT
	public void _on_button_quit_button_up()
	{
		// TODO quit
		GetTree().Quit();
	}
}
