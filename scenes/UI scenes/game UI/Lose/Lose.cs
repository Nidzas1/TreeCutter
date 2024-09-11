using Godot;
using System;

public partial class Lose : Control
{
	public override void _Ready()
	{
		Label woodChoppedLabel = GetNode<Label>("Panel/VBoxContainer/woodChoppedLabel");
		Label coinsEarnedLabel = GetNode<Label>("Panel/VBoxContainer/coinsEarnedLabel");
		Label highScoreLabel = GetNode<Label>("Panel/VBoxContainer/highScoreLabel");
		woodChoppedLabel.Text = "Wood Chopped: " + StaticData.Instance.GetWoodChopped();
		coinsEarnedLabel.Text = "Coins Earned: " + StaticData.Instance.GetCoinsEarned();
		highScoreLabel.Text = "High Score: " + StaticData.Instance.GetHighScore();
		StaticData.Instance.SaveData();
		StaticData.Instance.LoseGame();
	}

	private void SaveData()
	{
		StaticData.Instance.SaveData();
	}

	private void _on_tryagain_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
	private void _on_mainmenu_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}
	private void _on_close_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/main.tscn");
	}
}
