using Godot;
using Godot.Collections;
using System.Diagnostics;

namespace CRTGame;

public partial class Sheet : Area2D
{
	[Export]
	public uint ColorLayer = 1;

	[Export]
	public Color Color = Colors.White;

	[Export]
	public ColorRect ColorRect;

	[Export]
	public CollisionShape2D Collider;

	[Export]
	public float[] screenBoundaryPercentage = new float[2] { 1.4f, 1.6f };

	public Vector2 initialLocation { get; private set; }

    private Vector2 DragOffset;
	private bool bDragging;

	public override void _Ready()
	{
        CollisionLayer = ColorLayer << 8;
		CollisionMask = CollisionLayer;

		Modulate = Color;

		ZIndex = (int)(ColorLayer + 8);

		if ((ColorLayer & 1) > 0)
		{
			MusicPlayer.Instance.PlayRedMusic();
		}

		if ((ColorLayer & 2) > 0)
		{
			MusicPlayer.Instance.PlayGreenMusic();
		}

		if ((ColorLayer & 4) > 0)
		{
			MusicPlayer.Instance.PlayBlueMusic();
		}

        //Input.MouseMode = Input.MouseModeEnum.Confined;
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

        ClampMouseBoundary(mousePos);
    }

	public override void _ExitTree()
	{
		if ((ColorLayer & 1) > 0)
		{
			MusicPlayer.Instance.StopRedMusic();
		}

		if ((ColorLayer & 2) > 0)
		{
			MusicPlayer.Instance.StopGreenMusic();
		}

		if ((ColorLayer & 4) > 0)
		{
			MusicPlayer.Instance.StopBlueMusic();
		}
	}

	private void OnMouseEnter()
	{
		GD.Print(this + " entered");
		Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
		ColorRect.MouseDefaultCursorShape = Control.CursorShape.PointingHand;
	}

	private void OnMouseExit()
	{
		GD.Print(this + " exited");
		Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
		ColorRect.MouseDefaultCursorShape = Control.CursorShape.Arrow;
	}

	public void ResetPosition()
	{
		GlobalPosition = initialLocation;
        GD.Print("Resetting sheet to: " + initialLocation);
    }

	public void ClampMouseBoundary(Vector2 mousePos)
	{

		// NEW CODE BUT DOESN'T WORK

		// Get a boundary based on screen size and center it to the screen
        Vector2 screenSize = GetViewportRect().Size;
        Vector2 screenBoundary = new Vector2
        (
            screenSize.X * screenBoundaryPercentage[0],
            screenSize.Y * screenBoundaryPercentage[1]
        );
        Vector2 centeredBoundsMin = (screenSize - screenBoundary) / 2f;
		Vector2 centeredBoundsMax = centeredBoundsMin + screenBoundary;

        // Get the size of this rectangle (the sheet)
        Rect2 sheetRect = Collider.Shape.GetRect();

		// Potential new mouse position?
        Vector2 potentialPos = mousePos - DragOffset;

		potentialPos.X = Mathf.Clamp(potentialPos.X, centeredBoundsMin.X, centeredBoundsMax.X - sheetRect.Size.X);
		potentialPos.Y = Mathf.Clamp(potentialPos.Y, centeredBoundsMin.Y, centeredBoundsMax.Y - sheetRect.Size.Y);

		Position = potentialPos;
    }
}
