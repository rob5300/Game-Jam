using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Game {

    public static int DistanceDifficulty = 1;
	public static int DistanceInterval = 50;
	public static PlayerDifficultyMultiplier DifficultyMultiplier;
    public static List<Item> Items = new List<Item>();

    public static void LoadItems()
    {
        Items = Resources.LoadAll<Item>("Items").ToList();
    }
}

public enum PlayerDifficultyMultiplier
{
	Easy, Medium, Hard
}