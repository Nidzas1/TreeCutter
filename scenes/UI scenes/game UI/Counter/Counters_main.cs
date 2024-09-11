using Godot;
using System;

public partial class Counters_main : Control
{
	CanvasLayer canvasLayer;
	public override void _Ready()
	{
		canvasLayer = GetNode<CanvasLayer>("CountersMainCanvas");
	}
	private void _on_pause_button_pressed()
	{
		PackedScene PausedPanelScene = (PackedScene)ResourceLoader.Load("res://scenes/UI scenes/game UI/Popups/PausedPanel.tscn");
		Control PausedPanel = (Control)PausedPanelScene.Instantiate();
		canvasLayer.AddChild(PausedPanel);
	}
}
