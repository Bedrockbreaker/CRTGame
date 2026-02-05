using Godot;
using System;

public partial class ScreenShake : Node2D
{
	[Export]
	
	public float ShakeDecay = 15.0f;
	public float _shakeStrength = 0.0f;
	
	private Vector2 _originalPosition;

//reset position
	public override void _Ready()
	{
		_originalPosition = Position;
	}
	
	//shake strength process
	public override void _Process(double delta)
	{
		if (_shakeStrength > 0.0f)
		{
			_shakeStrength = Mathf.Max(
				_shakeStrength - ShakeDecay * (float)delta,
				0.0f
			);
			Position = _originalPosition + new Vector2(
				(float)GD.RandRange(-_shakeStrength, _shakeStrength),
				(float)GD.RandRange(-_shakeStrength, _shakeStrength)
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
		_shakeStrength = Mathf.Max(_shakeStrength, amount);
	}
}
