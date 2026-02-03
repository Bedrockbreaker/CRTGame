using Godot;

namespace CRTGame;

public partial class NextLevel_Trigger : Area2D
{
	[Export]
	public PackedScene nextLevel;

	[Export]
	public AudioStreamWav levelWinSound;

	[Export]
	public AudioStreamPlayer2D audioOutput;

	private RandomNumberGenerator Random = new();
	private float hue = 0f;

	public override void _Process(double delta)
	{
		hue = (hue + 0.5f * (float)delta) % 1f;
		Modulate = Color.FromHsv(hue, 1f, 1f);
	}

	protected virtual void OnBodyEntered(PhysicsBody2D body)
	{
		if (body.IsInGroup("Player"))
		{
			CharacterBody2D player = (CharacterBody2D)body;
			if (nextLevel == null)
			{
				GD.PushWarning("No Level Set in nextLevel variable");
				return;
			}

			audioOutput.Stream = levelWinSound;
			audioOutput.PitchScale = Random.Randf() * 0.3f + 0.85f;
			audioOutput.Play();

			// Player_Data.Instance.lastPosition = Position;
			Player_Data.Instance.lastVelocity = player.Velocity;

			CallDeferred(nameof(ChangeSceneDeferred), nextLevel);
		}
	}

	private void ChangeSceneDeferred(PackedScene scene)
	{
		GetTree().ChangeSceneToPacked(scene);
	}
}
