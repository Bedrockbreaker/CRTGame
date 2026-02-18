using Godot;
using System;
namespace CRTGame;

public partial class GlowSlider : HSlider
{
	
	
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ValueChanged += OnValueChanged;
	}
	
	private void OnValueChanged(double value)
	{
		ColorRect variablename = GetNode<ColorRect>("/root/SMainGame/CanvasLayer/ColorRect");
		((ShaderMaterial)variablename.GetMaterial()).SetShaderParameter("crt_color", value);
	}

}
