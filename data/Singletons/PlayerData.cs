using System.Collections.Generic;
using System.IO;
using Godot;
using Newtonsoft.Json;

public class PlayerData
{
    public string Username { get; set; }
    public Axe EquippedAxe { get; set; }
    public int HighScore { get; set; }
    public Currencies Currencies { get; set; }
    public Axe[] OwnedAxes { get; set; }
    public int GamesPlayed { get; set; }
    public PlayerData() { }

    public PlayerData(string Username, Axe EquippedAxe, int HighScore, Currencies Currencies, Axe[] OwnedAxes, int GamesPlayed)
    {
        this.Username = Username;
        this.EquippedAxe = EquippedAxe;
        this.HighScore = HighScore;
        this.Currencies = Currencies;
        this.OwnedAxes = OwnedAxes;
        this.GamesPlayed = GamesPlayed;
    }
}

