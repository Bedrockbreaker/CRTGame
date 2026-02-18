using Godot;
using System.Collections.Generic;
using System.Linq;

namespace CRTGame;

public partial class CheckpointManager : Node2D
{
	public Vector2 lastPlayerLocation;
	public CharacterBody2D player;
	public bool isPaused = false;
	
	[Export]
	public CanvasLayer PauseMenu { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetNode("P_Player_Obj") as CharacterBody2D;
		lastPlayerLocation = player.GlobalPosition;
	}
	
	/*
	PAUSE SCRIPT---TEMPORARY!!!!!!!!!!!!!!!!
	*/
	 public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Pause"))
		{
			Pause();
		}
	}
	public void Pause()
	{
		if (isPaused == false)
		{
			GetTree().Paused = true;
			isPaused = true;
			GD.Print("paused");
			
			PauseMenu.Visible = true;
		}
	}
	
	/*
	PAUSE SCRIPT---TEMPORARY!!!!!!!!!!!!!!!!
	*/
	
	
	
	public void ResetAtCheckpoint()
	{
		GetTree().ReloadCurrentScene();
	}
}
