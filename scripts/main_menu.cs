using Godot;
public partial class main_menu : Control
{
	private void updateLabels()
	{
		var playerData = StaticData.Instance.GetPlayerData();
		GetNode<Label>("ChoppedCounter/CounterLabel").Text = playerData.Currencies.WoodChopped.ToString();
		GetNode<Label>("CoinsCounter/CounterLabel").Text = playerData.Currencies.Coins.ToString();
	}
	public override void _Ready()
	{
		StaticData.Instance.GeneratePlayerData();
	}
	public override void _PhysicsProcess(double delta)
	{
		updateLabels();
	}

	private void _on_start_button_pressed()
	{
		if (StaticData.Instance.GetPlayerData().Username == "")
		{
			PackedScene namePopupScene = (PackedScene)ResourceLoader.Load("res://scenes/UI scenes/game UI/Popups/NamePopup.tscn");
			Control accountNamePopup = (Control)namePopupScene.Instantiate();
			AddChild(accountNamePopup);
		}
		else
		{
			GetTree().ChangeSceneToFile("res://scenes/game.tscn");
		}
	}
	private Control currentPopup = null;
	private void ShowPopup(PackedScene scene)
	{
		if (currentPopup != null)
		{
			currentPopup.QueueFree(); // Free the current popup if it exists
		}
		currentPopup = (Control)scene.Instantiate();
		AddChild(currentPopup);
	}

	private void _on_shop_pressed()
	{
		PackedScene shopScene = (PackedScene)ResourceLoader.Load("res://scenes/UI scenes/game UI/Popups/shop.tscn");
		ShowPopup(shopScene);
	}

	private void _on_axes_pressed()
	{
		PackedScene ownedAxesScene = (PackedScene)ResourceLoader.Load("res://scenes/UI scenes/game UI/Lose/OwnedAxesPanel.tscn");
		ShowPopup(ownedAxesScene);
	}

	private void _on_account_pressed()
	{
		PackedScene accountScene = (PackedScene)ResourceLoader.Load("res://scenes/UI scenes/game UI/Popups/Account.tscn");
		ShowPopup(accountScene);
	}

	private void _on_how_to_play_button_pressed()
	{
		PackedScene howToPlayScene = (PackedScene)ResourceLoader.Load("res://scenes/UI scenes/game UI/Popups/HowToPlay.tscn");
		ShowPopup(howToPlayScene);
	}
	private void _on_boxes_button_pressed()
	{
		PackedScene boxesScene = (PackedScene)ResourceLoader.Load("res://scenes/UI scenes/game UI/Boxes/Boxes.tscn");
		ShowPopup(boxesScene);
	}
}
