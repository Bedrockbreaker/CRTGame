using Godot;
using Godot.Collections;

namespace CRTGame;

public partial class Sheet : Area2D
{
	[Export]
	public uint ColorLayer = 1;

	[Export]
	public Color Color = Colors.White;

	[Export]
	public CollisionShape2D Collider;

	private Vector2 DragOffset;
	private bool bDragging;

	public override void _Ready()
	{
		CollisionLayer = ColorLayer << 8;
		CollisionMask = CollisionLayer;

		Modulate = Color;

		ZIndex = (int)(ColorLayer + 8);
	}

	public override void _Process(double delta)
	{
		Vector2 mousePos = GetGlobalMousePosition();

		PhysicsPointQueryParameters2D raycast = new()
		{
			Position = mousePos,
			CollisionMask = ~0U << 8, // Ignore layers 0-7
			CollideWithAreas = true,
			CollideWithBodies = false
		};

		Array<Dictionary> result = GetWorld2D().DirectSpaceState.IntersectPoint(raycast);

		if (result.Count > 0)
		{
			Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
		}
		else
		{
			Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
		}

		if (Input.IsActionJustPressed("Click", true))
		{
			// Test mouse overlap with sheet (or other sheets on top of this one)
			foreach (Dictionary hit in result)
			{
				// Ignore overlaps with non-sheet objects
				if (hit["collider"].As<GodotObject>() is not Sheet other) continue;

				// Ignore sheets on top of this one
				if (other.ColorLayer > ColorLayer)
				{
					bDragging = false;
					return;
				}

				// Pick the arbitrary first sheet if they share the same color layer
				if (other.ColorLayer == ColorLayer && other != this)
				{
					bDragging = false;
					return;
				}

				// Ignore sheets below, but continue to check the remaining sheets
				if (other.ColorLayer < ColorLayer) continue;

				bDragging = true;
				DragOffset = raycast.Position - Position;
			}
		}

		bDragging &= Input.IsActionPressed("Click");
		if (!bDragging) return;

		Position = GetGlobalMousePosition() - DragOffset;
	}
}
