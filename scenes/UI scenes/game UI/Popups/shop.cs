using Godot;
using System;
using System.Collections.Generic;

public partial class shop : Control
{

	private Label Precision;
	private Label LifePoints;
	private Label Price;
	private Panel panel;
	private VBoxContainer shopContainer;
	public override void _Ready()
	{
		shopContainer = GetNode<VBoxContainer>("Panel/ScrollContainer/MarginContainer/ShopContainer");

		List<Axe> axes = StaticData.Instance.GetAxes(); // GET AXES FROM JSON

		foreach (Axe axe in axes)
		{
			PackedScene shopComponentScene = GD.Load<PackedScene>("res://scenes/UI scenes/game UI/ShopUI/Components/shop_component.tscn");

			if (shopComponentScene != null)
			{
				shop_component shopComponentInstance = (shop_component)shopComponentScene.Instantiate();

				shopComponentInstance.Initialize(
					axe.ID,
					axe.Type,
					axe.Precision,
					axe.LifePoints,
					axe.Price
				);

				shopContainer.AddChild(shopComponentInstance);
			}
		}
	}

}