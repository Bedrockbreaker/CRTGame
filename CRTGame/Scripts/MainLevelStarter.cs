using Godot;

namespace CRTGame;

public partial class MainLevelStarter : Node2D
{
	[Export]
	public Node2D Player;

	[Export]
	public Node2D SpawnStart;

	[Export]
	public Node2D LevelWin;

	[Export]
	public Node2D LevelWinSpawn;

	[Export]
	public float TimeToPlayerSpawn = 2.0f;

	private bool spawnedPlayer = false;

	public override void _Process(double delta)
	{
		TimeToPlayerSpawn -= (float)delta;
		if (TimeToPlayerSpawn < 0 && !spawnedPlayer)
		{
			Player.Position = SpawnStart.GlobalPosition;
			spawnedPlayer = true;
		}

		if (Input.IsActionJustPressed("Click"))
		{
			LevelWin.Position = LevelWinSpawn.Position;
		}
	}
}
