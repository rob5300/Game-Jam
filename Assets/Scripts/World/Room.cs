using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Room : Piece {

    public static List<Room> Rooms = new List<Room>();

    public static void Load()
    {
        Rooms = Resources.LoadAll<Room>("Rooms").ToList();
    }
	
    public void OnPlace()
    {

    }
}
