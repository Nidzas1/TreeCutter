using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Godot;

public partial class StaticData : Node
{
    private static StaticData _instance;
    public static StaticData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new StaticData();
            }
            return _instance;
        }
    }
    public List<Axe> GetAxes()
    {
        var axeDataFile = FileAccess.Open("res://data/AxeData.json", FileAccess.ModeFlags.Read);
        var axeDataString = axeDataFile.GetAsText();

        var axeData = JsonConvert.DeserializeObject<List<Axe>>(axeDataString);

        return axeData.ToList();
    }
    public PlayerData GetPlayerData()
    {
        var playerDataFile = FileAccess.Open("res://data/PlayerData.json", FileAccess.ModeFlags.Read);
        var playerDataString = playerDataFile.GetAsText();
        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataString);

        playerDataFile.Close();

        return new PlayerData(
            playerData.Username,
            playerData.EquippedAxe,
            playerData.HighScore,
            playerData.Currencies,
            playerData.OwnedAxes,
            playerData.GamesPlayed
        );
    }
    public void GeneratePlayerData()
    {
        if (!FileAccess.FileExists("res://data/PlayerData.json"))
        {
            var WritePlayerData = FileAccess.Open("res://data/PlayerData.json", FileAccess.ModeFlags.Write);
            PlayerData playerData = new PlayerData()
            {
                Username = "",
                EquippedAxe = new()
                {
                    ID = 0,
                    Type = "oldAxe",
                    Precision = 8,
                    LifePoints = 3,
                    Price = 0,
                },
                HighScore = 0,
                Currencies = new()
                {
                    WoodChopped = 0,
                    Coins = 0
                },
                OwnedAxes = new Axe[]
                {
                    new Axe(0, "oldAxe", 8, 3, 0)
                },
                GamesPlayed = 0,
            };
            string updatedPlayerData = JsonConvert.SerializeObject(playerData);
            WritePlayerData.StoreString(updatedPlayerData);
            WritePlayerData.Close();
        }
    }

    int woodChopped = 0;
    int coins = 0;
    int highScore = 0;
    public void AddWoodChopped()
    {
        woodChopped += 1;
    }
    public void AddCoinsEarned(int amount)
    {
        coins += amount;
    }
    public void AddHighScore(int amount)
    {
        highScore += amount;
    }
    public int GetWoodChopped()
    {
        return woodChopped;
    }
    public int GetCoinsEarned()
    {
        return coins;
    }
    public int GetHighScore()
    {
        return highScore;
    }

    public void LoseGame()
    {
        woodChopped = 0;
        coins = 0;
        highScore = 0;
    }
    public void SaveData()
    {
        PlayerData playerData = Instance.GetPlayerData();

        playerData.Currencies = new Currencies
        {
            WoodChopped = playerData.Currencies.WoodChopped + woodChopped,
            Coins = playerData.Currencies.Coins + coins
        };
        if (playerData.HighScore < highScore)
        {
            playerData.HighScore = highScore;
        }
        playerData.GamesPlayed += 1;
        string updatedPlayerData = JsonConvert.SerializeObject(playerData);

        var playerDataFile = FileAccess.Open("res://data/PlayerData.json", FileAccess.ModeFlags.Write);
        playerDataFile.StoreString(updatedPlayerData);
        playerDataFile.Close();
    }

    public Axe GetEquippedAxe()
    {
        Axe equippedAxe = Instance.GetPlayerData().EquippedAxe;
        return equippedAxe;
    }
}

// MAKE SOUND EFFECTS
// REMAKE UI

// ADD SCOREBOARD
// SECURE PLAYERDATA, SO IT'S UNEDITABLE
// 100 WOOD GIVE PLAYER 50 COINS


// DO THIS AFTER RELEASE

// LOOTBOXES THAT GIVE YOU CERTAIN AMOUNT OF COIN
// LOOTBOXES THAT CHANGE APPEARANCE OF THE AXE
// LOOTBOXES = WOOD CURRENCY
// DIFFERENT RANK LOOTBOXES


//IMPORTANT:

// IN CASE YOU'RE USING ANDROID DEVICE, CHANGE res://data/PlayerData.json TO user://playerdata.json , CHANGE res://data/AxeData.json TO user://axedata.json.
// ALSO CHANGE NAME OF FILES IN FILE SYSTEM FROM playerdata.json to PlayeData.json, do equivalent to AxeData.json.
// IN CASE YOU'RE USING WINDOWS MACHINE, REVERT CHANGES.
