using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {
    public Room StartingRoom;

    Piece CurrentPiece;
    Piece BranchPiece;
    List<Piece> EndPieces = new List<Piece>();
    List<Piece> PlacedPieces = new List<Piece>();
    int CurrentRoutes = 1;

    public int FramesPerCheck = 5;
    int elapsedFrames = 0;

    void Start()
    {
        //Set current piece to the starting piece,
        //Load both corridor and room prefabs into their respectful lists.
        CurrentPiece = StartingRoom;
        Corridor.Load();
        Room.Load();

        //Join and place a Corridor on the first join position.
        JoinCorridor(CurrentPiece.JoinPoints[0].position, CurrentPiece);
    }

    public void Update()
    {
        elapsedFrames++;
        if(elapsedFrames >= FramesPerCheck)
        {
            elapsedFrames = 0;
            GenerationUpdate();
        }
    }

    public void GenerationUpdate()
    {
        for(int i=0; i < PlacedPieces.Count; i++)
        {
            Piece piece = PlacedPieces[i];
            if((piece.transform.position.y + 20) < Camera.main.transform.position.y)
            {
                PlacedPieces.Remove(piece);
                Destroy(piece.gameObject);
                i--;
            }
        }
        ProcessNext();
    }

    public void JoinRoom(Vector3 JoinPosition, Piece ParentPiece)
    {
        //We get usable pieces, ones that have join points on the correct side to avoid overlapping to join to opposite sides of a piece.
        //We first get what side this join lives on. Then we get a list of usable corridors with joins on opposing sides.
        Piece.Side side = ParentPiece.GetSideForJoin(JoinPosition);
        Piece.Side chosenSide = Piece.Side.Top;
        List<Room> usableRooms = new List<Room>();
        if (side == Piece.Side.Top)
        {
            usableRooms = Room.Rooms.Where(x => x.GetBottomJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Bottom;
        }
        else if (side == Piece.Side.Right)
        {
            usableRooms = Room.Rooms.Where(x => x.GetLeftJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Left;
        }
        else if (side == Piece.Side.Bottom)
        {
            usableRooms = Room.Rooms.Where(x => x.GetTopJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Top;
        }
        else if (side == Piece.Side.Left)
        {
            usableRooms = Room.Rooms.Where(x => x.GetRightJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Right;
        }

        Room toPlace = usableRooms[Random.Range(0, usableRooms.Count)];
        GameObject newPiece = Instantiate(toPlace.gameObject, JoinPosition, Quaternion.identity);
        Room newRoom = newPiece.GetComponent<Room>();
        PlacedPieces.Add(newRoom);

        //Move new piece position to fit next avaliable join point on the correct side.
        //Remove the join strait away.
        Vector3 newPieceJoinPos = newRoom.GetJoinsForSide(chosenSide)[0];
        newRoom.JoinPoints.Remove(newRoom.JoinPoints.Where(x => x.position == newPieceJoinPos).First());
        newPiece.transform.position += newPiece.transform.position - newPieceJoinPos;
        newRoom.JoinedTo = ParentPiece;
        newRoom.OnPlace();

        //We now need to remove these joins from both the placed piece and current piece to ensure we know what joins are populated.
        //We have to use linq to find the correct transform to remove. Transforms are used as it makes creating the joins easier in editor.
        //We have to regain the side position vector3 as it has moved and will not be the same anymore and will not remove properly.
        ParentPiece.JoinPoints.Remove(ParentPiece.JoinPoints.Where(x => x.position == JoinPosition).First());
        CurrentPiece = newRoom;
        ProcessNext();
    }

    public void JoinCorridor(Vector3 JoinPosition, Piece ParentPiece)
    {
        //We have to decide if we will allow a second route to appear, which will go into a loot room.
        List<Corridor> usableCorridors = new List<Corridor>();
        if (AllowSecondRoute())
        {
            usableCorridors = Corridor.Corridors.Where(x => x.JoinPoints.Count > 2).ToList();
            CurrentRoutes = 2;
        }
        else
        {
            usableCorridors = Corridor.Corridors.Where(x => x.JoinPoints.Count < 3).ToList();
        }
        //We get usable pieces, ones that have join points on the correct side to avoid overlapping to join to opposite sides of a piece.
        //We first get what side this join lives on. Then we get a list of usable corridors with joins on opposing sides.
        Piece.Side side = ParentPiece.GetSideForJoin(JoinPosition);
        Piece.Side chosenSide = Piece.Side.Top;

        if (side == Piece.Side.Top)
        {
            usableCorridors = usableCorridors.Where(x => x.GetBottomJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Bottom;
        }
        else if(side == Piece.Side.Right)
        {
            usableCorridors = usableCorridors.Where(x => x.GetLeftJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Left;
        }
        else if (side == Piece.Side.Bottom)
        {
            usableCorridors = usableCorridors.Where(x => x.GetTopJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Top;
        }
        else if (side == Piece.Side.Left)
        {
            usableCorridors = usableCorridors.Where(x => x.GetRightJoins().Length > 0).ToList();
            chosenSide = Piece.Side.Right;
        }

        Corridor toPlace = usableCorridors[Random.Range(0, usableCorridors.Count)];
        GameObject newPiece = Instantiate(toPlace.gameObject, JoinPosition, Quaternion.identity);
        Corridor newCorridor = newPiece.GetComponent<Corridor>();
        PlacedPieces.Add(newCorridor);

        //Move new piece position to fit next avaliable join point on the correct side.
        //Remove join strait away.
        Vector3 newPieceJoinPos = newCorridor.GetJoinsForSide(chosenSide)[0];
        newCorridor.JoinPoints.Remove(newCorridor.JoinPoints.Where(x => x.position == newPieceJoinPos).First());
        newPiece.transform.position += newPiece.transform.position - newPieceJoinPos;
        newCorridor.JoinedTo = ParentPiece;

        //We now need to remove these joins from both the placed piece and current piece to ensure we know what joins are populated.
        //We have to use linq to find the correct transform to remove. Transforms are used as it makes creating the joins easier in editor.
        //We have to regain the side position vector3 as it has moved and will not be the same anymore and will not remove properly.
        ParentPiece.JoinPoints.Remove(ParentPiece.JoinPoints.Where(x => x.position == JoinPosition).First());
        if(CurrentPiece.JoinPoints.Count == 0)
        {
            CurrentPiece = newCorridor;
        }
        ProcessNext();
    }

    public void ProcessNext()
    {
        if(PlacedPieces.Count > 10) { return; }
        if(CurrentPiece is Corridor)
        {
            JoinRoom(CurrentPiece.JoinPoints[0].position, CurrentPiece);
        }
        else if(CurrentPiece is Room)
        {
            JoinCorridor(CurrentPiece.JoinPoints[0].position, CurrentPiece);
        }
    }

    public bool AllowSecondRoute()
    {
        //Currently only allow one route.
        return false;
    }
}
