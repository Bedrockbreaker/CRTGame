using Godot;
using System;
namespace CRTGame;

public partial class FastCheck : CheckButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	void OnToggled(bool value)
	{
		Player_Script variablename = GetNode<Player_Script>("/root/SMainGame/P_Player_Obj");
		variablename.speed = value? 1000:150;
	}
}
