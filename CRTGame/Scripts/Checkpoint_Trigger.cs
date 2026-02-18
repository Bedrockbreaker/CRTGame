using Godot;

namespace CRTGame;

public partial class Checkpoint_Trigger : Area2D
{
	[Export]
	public CheckpointManager checkpointManager;

	public override void _EnterTree()
	{
		Player_Data.Instance.lastPosition = GlobalPosition;
	}


	public override void _Ready()
	{
		checkpointManager = GetParent<CheckpointManager>(); // previous implementation was always making this null
		checkpointManager.lastPlayerLocation = GetNode<Marker2D>("RespawnPoint").GlobalPosition;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Reset"))
		{
			GD.Print("Checkpoint triggered.");
			TriggerCheckpoint();
		}
		else return;
	}

	public void TriggerCheckpoint()
	{        
		checkpointManager.ResetAtCheckpoint();
	}
}
