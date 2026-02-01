using Godot;
using System;

public partial class Win_Screen : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hide();
	}

	public void Showtime()
	{
		Show();
	}
}
