using Godot;
using System;

public partial class Player_Data : Node
{
    public static Player_Data Instance { get; private set; }

    [Export]
    public Vector2 lastPosition { get; set; } = new Vector2(0, 0);

    [Export]
    public Vector2 lastVelocity { get; set; } = new Vector2(-900, -900);

    [Export]
    public CharacterBody2D player { get; set; }

    public override void _EnterTree()
    {
        Instance = this;

    }

    public override void _Ready()
    {
        lastPosition = player.GlobalPosition;
    }
}
