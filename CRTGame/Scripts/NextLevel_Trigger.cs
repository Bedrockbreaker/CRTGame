using Godot;
using System;

public partial class NextLevel_Trigger : Area2D
{
	[Export]
	public PackedScene nextLevel; 

    private void OnBodyEntered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Player"))
		{
			CharacterBody2D player = (CharacterBody2D)body;
			if (nextLevel == null)
			{
				GD.PushWarning("No Level Set in nextLevel variable");
				return;
			}

			/*
			WORKING ON GETTING IT TO REMEMBER POSITIONS
			Player_Data.lastPosition = player.Position;
			Player_Data.lastVelocity = player.Velocity;
			*/

			CallDeferred(nameof(ChangeSceneDeferred), nextLevel);
		}
	}

	private void ChangeSceneDeferred(PackedScene scene)
	{
		GetTree().ChangeSceneToPacked(scene);
	}
}
