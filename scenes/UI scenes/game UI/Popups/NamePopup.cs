using Godot;
using Newtonsoft.Json;

public partial class NamePopup : Control
{
	private void _on_accept_pressed()
	{
		PlayerData playerData = StaticData.Instance.GetPlayerData();
		playerData.Username = GetNode<LineEdit>("Panel/NameLineEdit").Text;
		string updatedPlayerData = JsonConvert.SerializeObject(playerData);

		var playerDataFile = FileAccess.Open("user://playerdata.json", FileAccess.ModeFlags.Write);
		playerDataFile.StoreString(updatedPlayerData);
		playerDataFile.Close();
		GetTree().ChangeSceneToFile("res://scenes/game.tscn");
	}
}
