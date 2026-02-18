using Godot;
using System;

public partial class CurvatureCheck : CheckButton
{
[Export]
	public CanvasLayer menuCanvas;

void OnToggled(bool value)
	{
		ColorRect variablename = menuCanvas.GetNode<ColorRect>("../CanvasLayer/ColorRect");
		((ShaderMaterial)variablename.GetMaterial()).SetShaderParameter("crt_curve", value? .06:0);
	}
}
