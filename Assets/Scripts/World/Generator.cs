using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public Room StartingRoom;

    Piece CurrentPiece;
    List<Piece> EndPieces = new List<Piece>();
    List<Piece> PlacedPieces = new List<Piece>();


    void Start()
    {
        CurrentPiece = StartingRoom;
        Corridor.Load();
        Room.Load();

        JoinCorridor(CurrentPiece.JoinPoints[0].position, CurrentPiece);
    }


    public void JoinRoom(Vector3 JoinPosition, Piece ParentPiece)
    {

    }

    public void JoinCorridor(Vector3 JoinPosition, Piece ParentPiece)
    {
        List<Corridor> usableCorridors = Corridor.Corridors.Select(x => x.)
        Corridor toPlace = usableCorridors[Random.Range(0, Corridor.Corridors.Count - 1)];

    }
}
