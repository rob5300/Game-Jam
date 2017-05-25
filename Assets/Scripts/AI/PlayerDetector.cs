using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class PlayerDetector : MonoBehaviour
{

	public int AmountOfMobs;
	public bool MeleeMobs;
	public bool RangedMobs;
	public bool KamikazeMobs;
	private List<BaseAIBehaviour> _mobs;
	private List<KeyValuePair<string, bool>> _switchesList;
	private int _partition;
	public GameObject MeleePrefab;
	public GameObject RangedPrefab;
	public GameObject KamikazePrefab;

	void Start()
	{
		_mobs = transform.parent.GetComponentsInChildren<BaseAIBehaviour>().ToList();
		Dictionary<string, bool> switches = new Dictionary<string, bool>();
		switches.Add("Melee", MeleeMobs);
		switches.Add("Ranged", RangedMobs);
		switches.Add("Kamikaze", KamikazeMobs);
		_switchesList = switches.Where(x => x.Value == true).ToList();
		_partition = (int)Mathf.Floor(AmountOfMobs / _switchesList.Count);
		StartCoroutine(SpawnMobs());
	}

	private IEnumerator SpawnMobs()
	{
		//if (_switchesList != null)
		//{
		foreach (KeyValuePair<string, bool> pair in _switchesList)
		{
			for (int i = 0; i < _partition; i++)
			{
				if (pair.Key == "Melee")
				{
					Instantiate(MeleePrefab, transform.parent);
				}
				else if (pair.Key == "Ranged")
				{
					Instantiate(RangedPrefab, transform.parent);
				}
				else if (pair.Key == "Kamikaze")
				{
					Instantiate(KamikazePrefab, transform.parent);
				}
				yield return new WaitForSeconds(1f);
			}
		}
		//}
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
