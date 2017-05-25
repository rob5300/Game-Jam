using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Rigidbody2D _rb;
	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		_rb.velocity = transform.up;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			//do health stuff here
		}
		Destroy(gameObject);
	}
}
