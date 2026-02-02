using Godot;

namespace CRTGame;

public partial class SpeedrunTimer : Node
{
	public static SpeedrunTimer Instance { get; private set; }

	private ulong initialTime;

	public override void _Ready()
	{
		Instance = this;
		StartTime(); // For dev-purposes only. The first level should re-call StartTime()
	}

	public void StartTime()
	{
		initialTime = Time.GetTicksUsec(); // Don't need microsecond accuracy, but who cares?
	}

	public ulong GetTime()
	{
		return Time.GetTicksUsec() - initialTime;
	}
}
