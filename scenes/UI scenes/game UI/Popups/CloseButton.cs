using Godot;
using System;

public partial class CloseButton : Button
{

	public override void _Ready()
	{
		Connect("pressed", new Callable(this, "OnCloseButtonPressed"));
	}
	private void OnCloseButtonPressed()
	{
		Node popup = GetParent();
		popup.QueueFree();
	}
}
