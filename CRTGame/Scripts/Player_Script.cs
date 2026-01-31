using Godot;
using System;

public partial class Player_Script : CharacterBody2D
{
	public const float speed = 300.0f;

    public override void _Ready()
    {
        Velocity = new Vector2(-200, -200).Normalized() * speed;
    }

    public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(Velocity * (float)delta);
		if (collision != null)
		{
			Velocity = Velocity.Bounce(collision.GetNormal());
		}
	}
}
