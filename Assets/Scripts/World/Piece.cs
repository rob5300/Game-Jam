using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Piece : MonoBehaviour {

    public Transform[] JoinPoints;
    public List<Piece> ConnectedPieces = new List<Piece>();
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

    public static double GetAngle(Vector3 from, Vector3 to)
    {
        Vector2 delta = new Vector2(to.y - from.y, to.x - from.x).normalized;
        return (Math.Atan2(delta.y, delta.x) * 180) / Math.PI;
    }
}
