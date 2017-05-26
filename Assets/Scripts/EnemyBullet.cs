using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private Rigidbody2D _rb;
	private int _damage;
	private int Tier;

	// Use this for initialization
	void Start () {
		_rb = GetComponent<Rigidbody2D>();
		Tier = Game.DistanceDifficulty / Game.DistanceInterval;
		if (Tier < 1)
		{
			Tier = 1;
		}
		_damage = Tier * 3;
	}

	void FixedUpdate()
	{
		_rb.velocity = transform.up * 3;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Movement.Player.GetComponent<Player>().Damage(_damage);
		}
		Destroy(gameObject);
	}
}
