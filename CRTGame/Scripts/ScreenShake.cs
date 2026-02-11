using Godot;
using System;

public partial class ScreenShake : Node2D
{
	[Export]
	
	public float ShakeDecay = 1.0f;
	
	[Export]
	public float ShakeStrength = 0.0f;
	
	private Vector2 _originalPosition;

//reset position
	public override void _Ready()
	{
		_originalPosition = Position;
	}
	
	//shake strength process
	public override void _Process(double delta)
	{
		if (ShakeStrength > 0.0f)
		{
			ShakeStrength = Mathf.Max(
				ShakeStrength - ShakeDecay * (float)delta,
				0.0f
			);
			Position = _originalPosition + new Vector2(
				(float)GD.RandRange(-ShakeStrength, ShakeStrength),
				(float)GD.RandRange(-ShakeStrength, ShakeStrength)
			);
		}
		else
		{
			Position = _originalPosition;
		}
	}
	
//Shake amount and function
	public void Shake(float amount)
	{
		ShakeStrength = Mathf.Max(ShakeStrength, amount);
	}
}
