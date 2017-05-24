using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour {

	void Update () {
        Debug.Log(Piece.GetAngle(Vector3.zero, transform.position));
	}
}
