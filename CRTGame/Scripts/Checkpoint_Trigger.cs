using Godot;

namespace CRTGame;

public partial class Checkpoint_Trigger : Area2D
{
	public CheckpointManager checkpointManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		checkpointManager = GetParent().GetNode("P_CheckpointManager") as CheckpointManager;
		checkpointManager.lastLocation = GetNode<Marker2D>("RespawnPoint").GlobalPosition;
	}
}
