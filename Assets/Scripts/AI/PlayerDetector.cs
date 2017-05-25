using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerDetector : MonoBehaviour {

	public int AmountOfMobs;
	public bool MeleeMobs;
	public bool RangedMobs;
	public bool StrongerMobs;
	private List<BaseAIBehaviour> _mobs;
	private List<KeyValuePair<string, bool>> _switchesList;
	private int _partition;

	void Start()
	{
		StartCoroutine(SpawnMobs());
		_mobs = transform.parent.GetComponentsInChildren<BaseAIBehaviour>().ToList();
		Dictionary<string, bool> switches = new Dictionary<string, bool>();
		switches.Add("Melee", MeleeMobs);
		switches.Add("Ranged", RangedMobs);
		switches.Add("Stronger", StrongerMobs);
		_switchesList = switches.Where(x => x.Value == true).ToList();
		_partition = (int)Mathf.Floor(AmountOfMobs / _switchesList.Count);
	}

	private IEnumerator SpawnMobs()
	{
		foreach (KeyValuePair<string, bool> pair in _switchesList)
		{
			for (int i = 0; i < _partition; i++)
			{
				if (pair.Key == "Melee")
				{
					//generate melee here and scale with the difficulty of the area
				}
				else if (pair.Key == "Ranged")
				{
					//same as melee but for ranged
				}
				else if (pair.Key == "Stronger")
				{
					//matt pls if you don't know what this is for by now you need your MTA revoked
				}
				yield return new WaitForSeconds(1f);
			}
		}
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
