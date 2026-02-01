using System.Collections.Generic;
using Godot;

namespace CRTGame;

public partial class RemovableWall : Area2D
{
	[Export]
	public uint ColorLayer = 1;

	[Export]
	public Color Color = Colors.White;

	[Export]
	public Color BorderColor = Colors.Gray;

	[Export]
	public CollisionShape2D SheetDetector;

	[Export]
	public StaticBody2D Collider;

	[Export]
	public Panel Panel;

	private readonly HashSet<Sheet> CandidateSheets = [];
	private bool bDisabled;

	public override void _Ready()
	{
		ColorLayer &= 255; // Clamp to 8 bits

		// The Area2D (this object) is reponsible for detecting overlaps with color sheets
		CollisionLayer = ColorLayer << 8;
		CollisionMask = CollisionLayer;

		// The StaticBody2D (child node) is responsible for physics
		Collider.CollisionLayer = ColorLayer;
		Collider.CollisionMask = Collider.CollisionLayer;

		// "panel" (in lowercase) is an id reference, apparently. I hate godot.
		Panel.AddThemeStyleboxOverride("panel", new StyleBoxFlat
		{
			BgColor = Color,
			BorderColor = BorderColor,
			BorderWidthLeft = 2,
			BorderWidthTop = 2,
			BorderWidthRight = 2,
			BorderWidthBottom = 2
		});
	}

	public override void _PhysicsProcess(double delta)
	{
		bool bDisabledThisTick = false;
		uint ColorMask = ColorLayer;

		if (CandidateSheets.Count != 0)
		{
			Rect2 wallRect = SheetDetector.Shape.GetRect();

			foreach (Sheet sheet in CandidateSheets)
			{
				Rect2 sheetRect = sheet.Collider.Shape.GetRect();
				if (!(
					Position.X >= sheet.Position.X && Position.Y >= sheet.Position.Y
					&& Position.X + wallRect.Size.X <= sheet.Position.X + sheetRect.Size.X
					&& Position.Y + wallRect.Size.Y <= sheet.Position.Y + sheetRect.Size.Y
				)) continue; // Sheet must enclose this wall

				ColorMask &= ~sheet.ColorLayer; // All sheets covering this wall must combine to this wall's Color Layer
				if (ColorMask != 0) continue;

				bDisabledThisTick = true;
				break;
			}
		}

		if (bDisabledThisTick == bDisabled) return;

		// HACK: This assumes stylebox will never be null. In a game jam, who cares?
		StyleBoxFlat stylebox = Panel.GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;

		bDisabled = bDisabledThisTick;
		if (bDisabled)
		{
			Collider.CollisionLayer &= ~ColorLayer;
			Collider.CollisionMask &= ~ColorLayer;
			stylebox.SetBorderWidthAll(0);
		}
		else
		{
			Collider.CollisionLayer |= ColorLayer;
			Collider.CollisionMask |= ColorLayer;
			stylebox.SetBorderWidthAll(2);
		}

		Panel.AddThemeStyleboxOverride("panel", stylebox);
		GD.Print(this + " disabled: " + bDisabled);
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print(this + " entered: " + area);

		if (area is Sheet sheet && ((sheet.ColorLayer & ColorLayer) != 0))
		{
			CandidateSheets.Add(sheet);
		}
	}

	private void OnAreaExited(Area2D area)
	{
		GD.Print(this + " exited: " + area);

		if (area is Sheet sheet && ((sheet.ColorLayer & ColorLayer) != 0))
		{
			CandidateSheets.Remove(sheet);
		}
	}
}
