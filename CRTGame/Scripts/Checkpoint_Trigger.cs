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
		if (@event.IsActionPressed("ResetLevel"))
		{
			ResetLevel();
		}
		else if (@event.IsActionPressed("ResetGame"))
		{
			// hardcoded to the title screen, please change later (:
			checkpointManager.LoadScene("res://CRTGame/Scenes/Levels/S_Level_00.tscn");
		}
		else return;
	}

	public void ResetLevel()
	{        
		checkpointManager.ResetAtCheckpoint();
	}
}
