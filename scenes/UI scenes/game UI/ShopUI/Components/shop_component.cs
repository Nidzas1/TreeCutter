using Godot;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

public partial class shop_component : PanelContainer
{
	private Label Precision;
	private Label LifePoints;
	private Label Price;
	private Button EquipButton;
	TextureRect AxeTexture;
	Texture2D axeTexture;
	string axeType;
	int axeID;
	int precision;
	int lifePoints;
	int price;
	public void Initialize(int ID, string Type, int precisionText, int lifePointsText, int priceText)
	{
		AxeTexture = GetNode<TextureRect>("HBoxContainer/AxeTextureRect");
		axeTexture = (Texture2D)GD.Load($"res://assets/axes/{Type}.png");
		AxeTexture.Texture = axeTexture;
		axeID = ID;
		axeType = Type;

		precision = precisionText;
		lifePoints = lifePointsText;
		price = priceText;

		Precision = GetNode<Label>("HBoxContainer/VBoxContainer/Percision");
		LifePoints = GetNode<Label>("HBoxContainer/VBoxContainer/LifePoints");
		Price = GetNode<Label>("HBoxContainer/VBoxContainer/Price");
		EquipButton = GetNode<Button>("HBoxContainer/CenterContainer/Buy");

		Precision.Text = "Precision: " + precisionText + "🎯";
		LifePoints.Text = "Life Points: " + lifePointsText + "❤️";
		Price.Text = "Price: " + priceText + "🏷️";
	}
	private void _on_buy_pressed()
	{
		BuyAxe(axeID);
	}
	public void BuyAxe(int axeID)
	{
		List<Axe> axes = StaticData.Instance.GetAxes();
		Axe boughtAxe = axes.Find(x => x.ID == axeID);
		PlayerData playerData = StaticData.Instance.GetPlayerData();


		if (playerData.Currencies.Coins < boughtAxe.Price)
		{
			
		}
		else
		{
			if (playerData.OwnedAxes.Any(x => x.ID == boughtAxe.ID))
			{
				GD.Print("You already own this axe");
			}
			else
			{
				playerData.OwnedAxes = playerData.OwnedAxes.Append(new Axe
				{
					ID = boughtAxe.ID,
					Type = boughtAxe.Type,
					Precision = boughtAxe.Precision,
					LifePoints = boughtAxe.LifePoints,
					Price = boughtAxe.Price
				}).ToArray();
				playerData.Currencies = new Currencies()
				{
					WoodChopped = playerData.Currencies.WoodChopped,
					Coins = playerData.Currencies.Coins - boughtAxe.Price
				};
				GD.Print("Bought axe!");
			}
		}

		string updatedPlayerData = JsonConvert.SerializeObject(playerData);

		var playerDataFile = FileAccess.Open("user://playerdata.json", FileAccess.ModeFlags.Write);
		playerDataFile.StoreString(updatedPlayerData);
		playerDataFile.Close();
	}
}
