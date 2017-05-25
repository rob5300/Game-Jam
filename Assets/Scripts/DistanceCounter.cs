using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCounter : MonoBehaviour {

    public double MaxCovered = 0f;

	void Update () {
        double y = Movement.Player.transform.position.y;
        y = Math.Round(y, 2);
        if (y > MaxCovered)
        {
            MaxCovered = y;
        }
	}
}
