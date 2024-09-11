using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public partial class OwnedAxesComponent : PanelContainer
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

	public override void _PhysicsProcess(double delta)
	{
		if (StaticData.Instance.GetPlayerData().EquippedAxe.ID == axeID)
		{
			EquipButton.Text = "Equipped";
		}
		else
		{
			EquipButton.Text = "Equip";
		}
	}

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
	private void _on_equip_pressed()
	{
		EquipAxe(axeID);
	}

	public void EquipAxe(int axeID)
	{
		List<Axe> axes = StaticData.Instance.GetAxes();
		Axe equippedAxe = axes.Find(x => x.ID == axeID);

		PlayerData playerData = StaticData.Instance.GetPlayerData();

		playerData.EquippedAxe = new Axe
		{
			ID = equippedAxe.ID,
			Type = equippedAxe.Type,
			Precision = equippedAxe.Precision,
			LifePoints = equippedAxe.LifePoints,
			Price = equippedAxe.Price
		};

		string updatedPlayerData = JsonConvert.SerializeObject(playerData);

		var playerDataFile = FileAccess.Open("user://playerdata.json", FileAccess.ModeFlags.Write);
		playerDataFile.StoreString(updatedPlayerData);
		playerDataFile.Close();
	}
}
