using Godot;
using System;

public partial class ScanlineCheck : CheckButton
{
	[Export]
	public CanvasLayer menuCanvas;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	void OnToggled(bool value)
	{
		ColorRect variablename = menuCanvas.GetNode<ColorRect>("../CanvasLayer/ColorRect");
		((ShaderMaterial)variablename.GetMaterial()).SetShaderParameter("crt_grid", value? 0:1);
		
	}
}
