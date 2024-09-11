using Godot;
using System;

public partial class AccountInfo : Control
{
	Label Username;
	Label HighScore;
	Label OwnedAxes;
	Label GamesPlayed;
	public void loadUI()
	{
		Username = GetNode<Label>("Panel/GridContainer/UsernameColor/Username");
		HighScore = GetNode<Label>("Panel/GridContainer/HighScoreColor/HighScore");
		OwnedAxes = GetNode<Label>("Panel/GridContainer/OwnedAxesColor/OwnedAxes");
		GamesPlayed = GetNode<Label>("Panel/GridContainer/GamesPlayedColor/GamesPlayed");
	}
	public override void _Ready()
	{
		loadUI();
		Username.Text = StaticData.Instance.GetPlayerData().Username;
		HighScore.Text = StaticData.Instance.GetPlayerData().HighScore.ToString();
		OwnedAxes.Text = StaticData.Instance.GetPlayerData().OwnedAxes.Length.ToString() + "/7";
		GamesPlayed.Text = StaticData.Instance.GetPlayerData().GamesPlayed.ToString();
	}
}
