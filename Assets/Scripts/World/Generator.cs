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
        //We get usable pieces, ones that have join points on the correct side to avoid overlapping to join to opposite sides of a piece.
        List<Corridor> usableCorridors = Corridor.Corridors.Where(x => x.GetSideJoinsForPosition(JoinPosition).Length > 0).ToList();
        Corridor toPlace = usableCorridors[Random.Range(0, Corridor.Corridors.Count - 1)];
        GameObject newPiece = Instantiate(toPlace.gameObject,JoinPosition + toPlace.GetSideJoinsForPosition(JoinPosition)[0], Quaternion.identity);
        newPiece.GetComponent<Corridor>().JoinedTo = ParentPiece;
    }
}
