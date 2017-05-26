using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BouncyBullet : MonoBehaviour
{

	public int Damage;
	private int _maxBounces;
	private int _currentBounces;

	// Use this for initialization
	void Start()
	{
		_maxBounces = 5;
		_currentBounces = 0;
	}

	void FixedUpdate()
	{
		GetComponent<Rigidbody2D>().velocity = transform.up * 10;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Player")
		{
			Movement.Player.GetComponent<Player>().Damage(Damage);
		}
		else if (collision.transform.tag == "Enemy")
		{
			collision.transform.GetComponent<BaseAIBehaviour>().Health -= (Damage);
		}
        GetComponent<AudioSource>().Play();
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.eulerAngles.z + 90f);
		_currentBounces++;
		if (_currentBounces >= _maxBounces)
		{
			Destroy(gameObject);
		}
	}
}
