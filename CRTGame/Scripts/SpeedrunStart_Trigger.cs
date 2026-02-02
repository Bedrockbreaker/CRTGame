using Godot;

namespace CRTGame;

public partial class SpeedrunStart_Trigger : NextLevel_Trigger
{
	protected override void OnBodyEntered(PhysicsBody2D body)
	{
		base.OnBodyEntered(body);

		SpeedrunTimer.Instance.StartTime();
	}
}
