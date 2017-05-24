using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    public Transform[] JoinPoints;
    public List<Piece> ConnectedPieces = new List<Piece>();
    public Piece JoinedTo;

    public Transform[] GetTopJoins()
    {
        return JoinPoints.Where(x => x.position.y > transform.position.y).ToArray();
    }
    public Transform[] GetBottomJoins()
    {
        return JoinPoints.Where(x => x.position.y < transform.position.y).ToArray();
    }
    public Transform[] GetLeftJoins()
    {
        return JoinPoints.Where(x => x.position.x < transform.position.x).ToArray();
    }
    public Transform[] GetRightJoins()
    {
        return JoinPoints.Where(x => x.position.x > transform.position.x).ToArray();
    }

}
