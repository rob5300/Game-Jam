using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    public float MovementMultiplier = 5f;

    Rigidbody2D rb;
    float Horizontal = 0f;
    float Vertical = 0f;
    Vector2 newVelocity;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        newVelocity = new Vector2(Horizontal, Vertical).normalized;
        newVelocity *= MovementMultiplier;

        rb.velocity = newVelocity;
	}
}
