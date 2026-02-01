using Godot;
using System;

public static partial class Player_Data
{
    [Export]
    public static Vector2 lastPosition = new Vector2(0, 0);

    [Export]
    public static Vector2 lastVelocity = new Vector2(0, 0);
}
