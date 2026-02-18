using Godot;
using System;
namespace CRTGame; 

public partial class PauseCanvas : CanvasLayer 
{
	[Export]
	public CheckpointManager checkpointManager;
	[Export]
	public CanvasLayer shaderCanvas;
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Pause"))
		{
			UnPause();
		}
	}

	public void UnPause()
	{
		GetTree().Paused = false; 
		GD.Print("unpaused");
		
		Hide();
		
		checkpointManager = GetParent<CheckpointManager>();
		checkpointManager.isPaused = false;
		
		// THIS makes is so unpause input action (currently space) doesn't persist when the game is unpaused
		GetViewport().SetInputAsHandled();
	}
	
			
}
