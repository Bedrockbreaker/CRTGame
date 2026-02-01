using Godot;

namespace CRTGame;

public partial class Death_Trigger : Area2D
{
	public CheckpointManager checkpointManager;
	public CharacterBody2D player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		checkpointManager = GetParent().GetNode("P_CheckpointManager") as CheckpointManager;
		player = GetParent().GetNode("P_Player_Obj") as CharacterBody2D;
	}

	private void OnBodyEntered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Player"))
		{
			KillPlayer();
		}
	}

	private void KillPlayer()
	{
		player.Position = checkpointManager.lastLocation;
	}
}
