using Godot;
using System;
namespace CRTGame;

public partial class MusicSlider : HSlider
{
	[Export]
	public string busName { get; set; }
	
	private int busIndex;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		busIndex = AudioServer.GetBusIndex(busName);
		ValueChanged += OnValueChanged;
	}
	
	private void OnValueChanged(double value)
	{
		AudioServer.SetBusVolumeDb(busIndex, Mathf.LinearToDb((float)value));
	}

}
