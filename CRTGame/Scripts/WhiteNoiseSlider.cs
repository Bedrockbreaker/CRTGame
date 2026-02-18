using Godot;
using System;
namespace CRTGame;

public partial class WhiteNoiseSlider : HSlider
{
	[Export]
	public CanvasLayer menuCanvas;
	
	public override void _Ready()
	{
		ValueChanged += OnValueChanged;
	}
	
	private void OnValueChanged(double value)
	{
		ColorRect variablename = menuCanvas.GetNode<ColorRect>("../CanvasLayer/ColorRect");
		((ShaderMaterial)variablename.GetMaterial()).SetShaderParameter("crt_white_noise", value);

		
	}
}
