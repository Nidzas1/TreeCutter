using System;
using Godot;

public class Axe
{
    public int ID { get; set; }
    public string Type { get; set; }
    public int Precision { get; set; }
    public int LifePoints { get; set; }
    public int Price { get; set; }
    // ADD DURABILITY SO AXES CAN BREAK, AND PLAYER NEEDS TO REPAIR THEM.

    // public Texture AxeTexture { get; set; } ADD AFTER CHANGE TEXTURES BASED ON AXE TYPE
    public Axe() { }

    public Axe(int ID, string Type, int Precision, int LifePoints, int Price)
    {
        this.ID = ID;
        this.Type = Type;
        this.Precision = Precision;
        this.LifePoints = LifePoints;
        this.Price = Price;
    }
}