using Godot;
using System;

public partial class Win_Trigger : Area2D
{
	[Export]
	Win_Screen winScreen;

	private void OnBodyEntered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Player"))
		{
			winScreen.Showtime();
		}
	}
}
