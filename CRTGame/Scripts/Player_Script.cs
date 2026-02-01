using Godot;

namespace CRTGame;

public partial class Player_Script : CharacterBody2D
{
	[Export]
	public float speed = 300.0f;

	[Export]
	public AudioStreamWav bounce;

	[Export]
	public AudioStreamPlayer2D audioOutput;

	private RandomNumberGenerator Random = new();

	public override void _Ready()
	{
		Velocity = new Vector2(-1, -1).Normalized() * speed;

		ZIndex = 5;
	}

	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(Velocity * (float)delta);
		if (collision != null)
		{
			Velocity = Velocity.Bounce(collision.GetNormal());

			audioOutput.Stream = bounce;
			audioOutput.PitchScale = Random.Randf() * 0.3f + 0.85f;
			audioOutput.Play();
		}
	}
}
