using Godot;

namespace CRTGame;

public partial class Win_Screen : ColorRect
{
	[Export]
	public Label timeLabel;

	private bool bAlreadyShown = false;

	public override void _Ready()
	{
		Hide();
	}

	public void Showtime()
	{
		if (bAlreadyShown) return;
		bAlreadyShown = true;

		Show();

		// time in microseconds
		ulong time = SpeedrunTimer.Instance.GetTime();

		ulong minutes = time / 60_000_000;
		ulong seconds = (time - minutes * 60_000_000) / 1_000_000;
		ulong milliseconds = (time - minutes * 60_000_000 - seconds * 1_000_000) / 1000;

		timeLabel.Text = $"Time: {minutes:00}:{seconds:00}.{milliseconds:000}";
	}
}
