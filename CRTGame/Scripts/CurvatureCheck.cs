using Godot;
using System;

public partial class CurvatureCheck : CheckButton
{


void OnToggled(bool value)
	{
		ColorRect variablename = GetNode<ColorRect>("/root/SMainGame/CanvasLayer/ColorRect");
		((ShaderMaterial)variablename.GetMaterial()).SetShaderParameter("crt_curve", value? .06:0);
	}
}
