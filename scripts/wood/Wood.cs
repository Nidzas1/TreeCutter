using System;
using Godot;

public partial class Wood : Node
{
	public string Type { get; set; }
	public int strengthNeeded { get; set; }
	public Color ColorStrength { get; set; }

	public Wood() { }

	public Wood(string Type, int strengthNeeded, Color ColorStrength)
	{
		this.Type = Type;
		this.strengthNeeded = strengthNeeded;
		this.ColorStrength = ColorStrength;
	}

	// SNIPPETS
	Random rand = new Random();

	// GAME BALANCING BELOW
	public Wood ReturnWood()
	{
		Wood[] woodArr = new Wood[] {
			new ("Weak", rand.Next(5, 10), new Color(0, 1, 0, 1)), // green
			new ("Middle", rand.Next(15,20),  new Color(0, 0, 1, 1)), // blue
			new ("Strong", rand.Next(25,30), new Color(1, (float)0.54902, 0, 1)), // orange
			new ("Tough", rand.Next(35,40), new Color(1, 0, 0, 1)), // red
			new ("Durable", rand.Next(45,50), new Color(1, (float)0.843137, 0, 1)) // gold 
		};
		Wood randomWood = woodArr[rand.Next(0, woodArr.Length)];
		return new Wood(randomWood.Type, randomWood.strengthNeeded, randomWood.ColorStrength);
	}
}
