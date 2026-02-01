using System;
using Godot;

public partial class Win_Screen : ColorRect
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
