using Godot;
using System;

public partial class Checkpoint_Trigger : Area2D
{
	public CheckpointManager checkpointManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		checkpointManager = GetParent() as CheckpointManager;
	}

	private void OnBodyEntered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Player"))
		{
			checkpointManager.lastLocation = GetNode<Marker2D>("RespawnPoint").GlobalPosition;
		}
	}
}
