using Godot;
using System;
namespace CRTGame;

public partial class MusicSlider : HSlider
{
	[Export]
	public string BusName { get; set; }
	
	private int BusIndex;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BusIndex = AudioServer.GetBusIndex(BusName);
		ValueChanged += OnValueChanged;
	}
	
	private void OnValueChanged(double value)
	{
		AudioServer.SetBusVolumeDb(BusIndex, Mathf.LinearToDb((float)value));
	}

}
