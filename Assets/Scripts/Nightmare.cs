using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : MonoBehaviour {

    public float MoveRate = 1f;

	void Update () {
        transform.Translate(new Vector2(0, MoveRate * Time.deltaTime));
	}

}
