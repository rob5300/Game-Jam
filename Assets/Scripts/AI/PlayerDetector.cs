using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerDetector : MonoBehaviour {

	private List<BaseAIBehaviour> _mobs;
	void Start()
	{
		//place spawning logic here
		_mobs = transform.parent.GetComponentsInChildren<BaseAIBehaviour>().ToList();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			foreach (BaseAIBehaviour mob in _mobs)
			{
				mob.PlayerSeen = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			foreach (BaseAIBehaviour mob in _mobs)
			{
				mob.PlayerSeen = false;
			}
		}
	}
}
