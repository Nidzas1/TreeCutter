using Godot;
using System;

public partial class PausedPanel : Control
{
	private void _on_retry_button_pressed()
	{
		StaticData.Instance.LoseGame();
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
	private void _on_main_menu_button_pressed()
	{
		StaticData.Instance.LoseGame();
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}
}
