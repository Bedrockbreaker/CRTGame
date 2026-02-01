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
		CollisionLayer = 0b1111111100000000;
		CollisionMask = CollisionLayer;

		// The StaticBody2D (child node) is responsible for physics
		Collider.CollisionLayer = 0b0000000011111111;
		Collider.CollisionMask = Collider.CollisionLayer;

		// "panel" (in lowercase) is an id reference, apparently. I hate godot.
		Panel.AddThemeStyleboxOverride("panel", new StyleBoxFlat
		{
			BgColor = Color
		});

		ZIndex = -1;
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

				ColorMask ^= sheet.ColorLayer;
			}

			if (ColorMask == 0) bDisabledThisTick = true;
		}

		if (bDisabledThisTick == bDisabled) return;

		// HACK: This assumes stylebox will never be null. In a game jam, who cares?
		// StyleBoxFlat stylebox = Panel.GetThemeStylebox("panel").Duplicate() as StyleBoxFlat;

		bDisabled = bDisabledThisTick;
		if (bDisabled)
		{
			Collider.CollisionLayer &= ~0b0000000011111111U;
			Collider.CollisionMask &= ~0b0000000011111111U;
		}
		else
		{
			Collider.CollisionLayer |= 0b0000000011111111U;
			Collider.CollisionMask |= 0b0000000011111111U;
		}

		// Panel.AddThemeStyleboxOverride("panel", stylebox);
		GD.Print(this + " disabled: " + bDisabled);
	}

	private void OnAreaEntered(Area2D area)
	{

		if (area is Sheet sheet)
		{
			GD.Print(this + " entered: " + area);
			CandidateSheets.Add(sheet);
		}
	}

	private void OnAreaExited(Area2D area)
	{

		if (area is Sheet sheet)
		{
			GD.Print(this + " exited: " + area);
			CandidateSheets.Remove(sheet);
		}
	}
}
