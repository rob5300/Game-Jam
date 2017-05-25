using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Piece : MonoBehaviour {
    public enum Side { Top, Bottom, Left, Right};

    public List<Transform> JoinPoints;
    public Dictionary<Vector3, Piece> ConnectedPieces = new Dictionary<Vector3, Piece>();
    public Piece JoinedTo;

    public Vector3[] GetTopJoins()
    {
        return JoinPoints.Where(x => (GetAngle(transform.position, x.position) >= 0 && GetAngle(transform.position, x.position) < 45) || 
        (GetAngle(transform.position, x.position) <= 0 && GetAngle(transform.position, x.position) > -45)).Select(x => x.position).ToArray();
    }
    public Vector3[] GetBottomJoins()
    {
        return JoinPoints.Where(x => (GetAngle(transform.position, x.position) > 135 && GetAngle(transform.position, x.position) <= 180) ||
        (GetAngle(transform.position, x.position) < -135 && GetAngle(transform.position, x.position) >= -180)).Select(x => x.position).ToArray();
    }
    public Vector3[] GetLeftJoins()
    {
        return JoinPoints.Where(x => GetAngle(transform.position, x.position) > -135 && GetAngle(transform.position, x.position) < -45).Select(x => x.position).ToArray();
    }
    public Vector3[] GetRightJoins()
    {
        return JoinPoints.Where(x => GetAngle(transform.position, x.position) > 45 && GetAngle(transform.position, x.position) < 135).Select(x => x.position).ToArray();
    }

    public Vector3[] GetSideJoinsForPosition(Vector3 position)
    {
        int angle = (int)GetAngle(transform.position, position);
        if((angle >= 0 && angle < 45) || (angle <= 0 && angle > -45))
        {
            return GetTopJoins();
        }
        else if(angle > 45 && angle <= 135)
        {
            return GetRightJoins();
        }
        else if((angle > 135 && angle <= 180) || (angle < -135 && angle >= -180))
        {
            return GetBottomJoins();
        }
        else if(angle > -135 && angle < -45)
        {
            return GetLeftJoins();
        }
        else
        {
            Debug.LogError("Angle given was out of bounds! Angle: " + angle);
            return null;
        }
    }

    public Side GetSideForJoin(Vector3 join)
    {
        double angle = GetAngle(transform.position, join);
        if ((angle >= 0 && angle < 45) || (angle <= 0 && angle > -45))
        {
            return Side.Top;
        }
        else if (angle > 45 && angle <= 135)
        {
            return Side.Right;
        }
        else if ((angle > 135 && angle <= 180) || (angle < -135 && angle >= -180))
        {
            return Side.Bottom;
        }
        else if (angle > -135 && angle < -45)
        {
            return Side.Left;
        }
        else
        {
            Debug.LogError("GetSideForJoin angle was out of range.");
            return Side.Top;
        }
    }

    public static double GetAngle(Vector3 from, Vector3 to)
    {
        Vector2 delta = new Vector2(to.y - from.y, to.x - from.x).normalized;
        return (Math.Atan2(delta.y, delta.x) * 180) / Math.PI;
    }

    public Vector3[] GetJoinsForSide(Side side)
    {
        if (side == Side.Top)
        {
            return GetTopJoins();
        }
        else if(side == Side.Right)
        {
            return GetRightJoins();
        }
        else if(side == Side.Bottom)
        {
            return GetBottomJoins();
        }
        else if(side == Side.Left)
        {
            return GetLeftJoins();
        }
        else
        {
            return null;
        }
    }
}
