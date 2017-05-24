using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Corridor : Piece {

    public static List<Corridor> Corridors = new List<Corridor>();

    public static void Load()
    {
        Corridors = Resources.LoadAll<Corridor>("Corridors").ToList();
    }

}
