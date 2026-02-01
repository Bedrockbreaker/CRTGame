using Godot;

namespace CRTGame;

public partial class MusicPlayer : Node2D
{
	public static MusicPlayer Instance { get; private set; }

	[Export]
	public AudioStreamPlayer2D PlayerBase;
	[Export]
	public AudioStreamPlayer2D PlayerRed;
	[Export]
	public AudioStreamPlayer2D PlayerGreen;
	[Export]
	public AudioStreamPlayer2D PlayerBlue;

	public override void _EnterTree()
	{
		Instance = this;
	}

	public void PlayRedMusic()
	{
		PlayerRed.VolumeDb = 0.0f;
	}

	public void StopRedMusic()
	{
		PlayerRed.VolumeDb = -80.0f;
	}

	public void PlayGreenMusic()
	{
		PlayerGreen.VolumeDb = 0.0f;
	}

	public void StopGreenMusic()
	{
		PlayerGreen.VolumeDb = -80.0f;
	}

	public void PlayBlueMusic()
	{
		PlayerBlue.VolumeDb = 0.0f;
	}

	public void StopBlueMusic()
	{
		PlayerBlue.VolumeDb = -80.0f;
	}
}