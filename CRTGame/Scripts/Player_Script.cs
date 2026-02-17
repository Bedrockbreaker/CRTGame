using Godot;

namespace CRTGame;

public partial class Player_Script : CharacterBody2D
{
	[Export]
	public float speed = 300.0f;

	[Export]
	public AudioStreamWav bounce;

	[Export]
	public int maxBounceSounds = 1;
	private int bounceTimes = 0;

	[Export]
	public AudioStreamPlayer2D audioOutput;

	[Export]
	public PackedScene bounceParticle;

	[Export]
	public float Shake = 0.0f;

	private RandomNumberGenerator Random = new();

	public override void _EnterTree()
	{
		Player_Data.Instance.player = this;
	}

	public override void _Ready()
	{
		Velocity = Player_Data.Instance.lastVelocity.Normalized() * speed;
		Position = Player_Data.Instance.lastPosition;

		ZIndex = 100;
	}

	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(Velocity * (float)delta);
		if (collision != null)
		{
			Velocity = Velocity.Bounce(collision.GetNormal());

			GpuParticles2D particle = bounceParticle.Instantiate<GpuParticles2D>();
			particle.Position = Position;
			particle.Restart();
			GetParent().AddChild(particle);

			PlayBounceSound();

			var screenShake = GetTree().Root.GetNode<ScreenShake>("SMainGame");
			screenShake.Shake(Shake);

		}
	}

	public async void PlayBounceSound()
	{
		if (bounceTimes <= maxBounceSounds)
		{
			audioOutput.Stream = bounce;
			audioOutput.PitchScale = Random.Randf() * 0.3f + 0.85f;
			audioOutput.Play();
			bounceTimes++;
			await ToSignal(audioOutput, "finished");
			bounceTimes--;
		}
	}
}
