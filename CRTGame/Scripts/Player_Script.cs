using Godot;
using System;

public partial class Player_Script : CharacterBody2D
{
	public float speed = 300.0f;

    public override void _EnterTree()
    {
        Player_Data.Instance.player = this;
    }

    public override void _Ready()
    {
        Velocity = Player_Data.Instance.lastVelocity.Normalized() * speed;
		Position = Player_Data.Instance.lastPosition;
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
