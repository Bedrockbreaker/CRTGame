using Godot;
using System.Collections.Generic;
using System.Linq;

namespace CRTGame;

public partial class CheckpointManager : Node2D
{
	public Vector2 lastPlayerLocation;
	public CharacterBody2D player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetParent().GetNode("P_Player_Obj") as CharacterBody2D;
		lastPlayerLocation = player.GlobalPosition;
    }

	public void ResetAtCheckpoint()
	{
        GetTree().ReloadCurrentScene();
    }

	public void LoadScene(string sceneName)
	{
        GetTree().ChangeSceneToFile(sceneName);
    }
}
