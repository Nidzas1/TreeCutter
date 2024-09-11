using Godot;

public partial class OwnedAxesPanel : Control
{
	// Called when the node enters the scene tree for the first time.
	VBoxContainer ownedAxesContainer;
	public override void _Ready()
	{
		ownedAxesContainer = GetNode<VBoxContainer>("Panel/ScrollContainer/MarginContainer/OwnedAxesContainer");
		Axe[] axes = StaticData.Instance.GetPlayerData().OwnedAxes;
		foreach (Axe axe in axes)
		{
			PackedScene ownedAxesScene = GD.Load<PackedScene>("res://scenes/UI scenes/game UI/Lose/OwnedAxesComponent.tscn");
			if (ownedAxesScene != null)
			{
				OwnedAxesComponent ownedAxesComponent = (OwnedAxesComponent)ownedAxesScene.Instantiate();

				ownedAxesComponent.Initialize(
					axe.ID,
					axe.Type,
					axe.Precision,
					axe.LifePoints,
					axe.Price
				);

				ownedAxesContainer.AddChild(ownedAxesComponent);
			}
		}
	}
}
