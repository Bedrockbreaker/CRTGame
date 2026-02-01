using Godot;
using System;

public partial class CheckpointManager : Node2D
{
	public Vector2 lastLocation;
	public CharacterBody2D player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetNode("P_Player_Obj") as CharacterBody2D;
		lastLocation = player.GlobalPosition;
	}
}
