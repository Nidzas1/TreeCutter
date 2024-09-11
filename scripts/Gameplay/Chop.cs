using Godot;
using System;

public partial class Chop : Node
{
	// Variables
	private bool chopping = false;
	private int chopStrength = 0;
	private const float chopStrengthMax = 50f;
	private int score = 0;
	private int strengthMargin = 0;
	private int strengthNeeded; // BASED ON WOOD STRENGTH
	private bool isReady = true;
	private Timer incrementTimer = new Timer();
	Wood wood;
	// UI Elements
	private Label scoreLabel;
	private Label woodChoppedLabel;
	private Label coinsEarnedLabel;
	private AnimationPlayer axePlayer;
	private AnimationPlayer woodPlayer;
	private Panel strengthColorRect;
	SoundManager soundManager;
	Sprite2D axeSprite;
	GridContainer gridContainer;
	TextureRect axeRect = new TextureRect();

	// JSON DATA
	PlayerData playerData;
	Axe equippedAxe = new Axe();
	public int axeLifePoints = 0;
	int sceneWood;
	int sceneCoins;

	public override void _Ready()
	{
		playerData = StaticData.Instance.GetPlayerData();
		equippedAxe = playerData.EquippedAxe;

		axeLifePoints = equippedAxe.LifePoints;

		axeSprite = (Sprite2D)GetNode("CanvasLayer/Control/axe");
		axeSprite.Texture = (Texture2D)GD.Load($"res://assets/axes/{equippedAxe.Type}.png");
		LoadAnimations();
		SetupUI();
		PlaceNewWood("chopped");
		SetupWood();
		HeartCounterInitialize();
		SetupTimer();
	}
	private void HeartCounterInitialize()
	{
		int axeHealth = playerData.EquippedAxe.LifePoints;
		for (int i = 0; i < axeHealth; i++)
		{
			axeRect = new TextureRect
			{
				Texture = (Texture2D)GD.Load("res://assets/lifePoints/newLifePoints.png")
			};
			gridContainer.AddChild(axeRect);
		}
	}
	public void RemoveLifePoints()
	{
		gridContainer.GetChild<Control>(gridContainer.GetChildCount() - 1).QueueFree();
	}
	private void SetupUI()
	{
		PackedScene gameUIScene = GD.Load<PackedScene>("res://scenes/UI scenes/game_ui.tscn");

		Control gameUI = (Control)gameUIScene.Instantiate();
		AddChild(gameUI);

		gridContainer = (GridContainer)gameUI.GetNode("GameUICanvas/CountersMain/AxeLifePoints/GridContainer");
		scoreLabel = (Label)gameUI.GetNode("GameUICanvas/CountersMain/Score");
		woodChoppedLabel = (Label)gameUI.GetNode("GameUICanvas/CountersMain/ChoppedCounter/CounterLabel");
		coinsEarnedLabel = (Label)gameUI.GetNode("GameUICanvas/CountersMain/CoinsCounter/CounterLabel");

		soundManager = (SoundManager)GetNode("CanvasLayer/Control/SoundManager");
		strengthColorRect = (Panel)gameUI.GetNode("GameUICanvas/CountersMain/StrengthColorRect");
	}
	private void ShowLoseGame()
	{
		GetTree().ChangeSceneToFile("res://scenes/UI scenes/game UI/Lose/Lose.tscn");
	}
	private void SetupWood()
	{
		woodPlayer.Connect("animation_finished", new Callable(this, "PlaceNewWood"));
	}

	private void SetupTimer()
	{
		incrementTimer.WaitTime = 0.1f;
		incrementTimer.OneShot = false;
		incrementTimer.Connect("timeout", new Callable(this, "IncrementStrength"));
		AddChild(incrementTimer);
	}
	private void LoadAnimations()
	{
		axePlayer = (AnimationPlayer)GetNode("CanvasLayer/Control/axe/ChoppingAnimation");
		woodPlayer = (AnimationPlayer)GetNode("CanvasLayer/Control/wood/ChoppedAnimation");

		axePlayer.Connect("animation_finished", new Callable(this, "ChopCooldown"));
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.IsPressed() && isReady)
			{
				chopping = true;
				axePlayer.Play("steady");
				incrementTimer.Start();
			}
			else if (mouseEvent.IsReleased() && chopping)
			{
				chopping = false;
				isReady = false;
				axePlayer.Play("chop");
				incrementTimer.Stop();
				CheckChopStrength();
				chopStrength = 0;
			}
		}
	}
	public void IncrementStrength()
	{
		if (chopping && chopStrength < chopStrengthMax)
		{
			chopStrength += 1;
			GD.Print("Chop strength: " + chopStrength);
		}
	}
	private void CheckChopStrength()
	{
		// SEE WHAT TO DO ABOUT THIS VARIABLE
		strengthMargin = DifficultyMargin();
		if (chopStrength <= wood.strengthNeeded - strengthMargin)
		{
			axeLifePoints--;
			woodPlayer.Play("shaking");
			axePlayer.Play("weak_swing");
			soundManager.PlayThunkSound();
			RemoveLifePoints();
			Lose();
		}
		else if (chopStrength >= wood.strengthNeeded + strengthMargin)
		{
			axeLifePoints--;
			woodPlayer.Play("shaking");
			axePlayer.Play("weak_swing");
			soundManager.PlayThunkSound();
			RemoveLifePoints();
			Lose();
		}
		else
		{
			woodPlayer.Play("chopped");
			soundManager.PlayChoppedSound();
			UpdateScore();
		}
	}

	private void PlaceNewWood(string anim_name)
	{
		if (anim_name == "chopped")
		{
			wood = new Wood().ReturnWood();
			woodPlayer.Play("place");
			strengthNeeded = wood.strengthNeeded;
			strengthColorRect.SelfModulate = wood.ColorStrength;
			GD.Print("Current strength needed: " + strengthNeeded);
			GD.Print("Current margin: " + DifficultyMargin());
		}
	}

	private void ChopCooldown(string anim_name)
	{
		if (anim_name == "chop" || anim_name == "weak_swing")
		{
			isReady = true;
		}
	}

	private void UpdateScore()
	{
		if (chopStrength == strengthNeeded)
		{
			StaticData.Instance.AddHighScore(10);
			StaticData.Instance.AddCoinsEarned(5);
			soundManager.PlayCoinSound();
		}
		else
		{
			StaticData.Instance.AddHighScore(Math.Abs(strengthNeeded - chopStrength));
		}

		StaticData.Instance.AddWoodChopped();

		if (StaticData.Instance.GetWoodChopped() % 3 == 0)
		{
			StaticData.Instance.AddCoinsEarned(2);
			soundManager.PlayCoinSound();
		}

		scoreLabel.Text = "Score: " + StaticData.Instance.GetHighScore().ToString();
		coinsEarnedLabel.Text = StaticData.Instance.GetCoinsEarned().ToString();
		woodChoppedLabel.Text = StaticData.Instance.GetWoodChopped().ToString();
	}

	private int DifficultyMargin()
	{

		strengthMargin = equippedAxe.Precision;

		// SCORE
		score = StaticData.Instance.GetHighScore();

		if (score >= 30 && score < 50)
		{
			strengthMargin -= 4;
			GD.Print("Your score is high enough to increase difficulty");
		}
		else if (score >= 50 && score < 100)
		{
			strengthMargin -= 5;
		}
		else if (score >= 100)
		{
			strengthMargin -= 8;
		}
		return strengthMargin;
	}
	private void Lose()
	{
		if (axeLifePoints == 0)
		{
			axePlayer.Play("break");
			GetTree().CreateTimer(1.5).Connect("timeout", new Callable(this, "ShowLoseGame"));
		}
	}
}
