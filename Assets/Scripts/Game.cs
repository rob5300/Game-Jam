using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Game {

    public static int Difficulty = 1;
    public static List<Item> Items = new List<Item>();

    public static void LoadItems()
    {
        Items = Resources.LoadAll<Item>("Items").ToList();
    }
}
