using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BaseAIBehaviour : MonoBehaviour
{
	private List<GameObject> _patrolPositions;
	private int _pointIndex;
	public bool IsRanged;
	public GameObject BulletPrefab;
	private Rigidbody2D _rb;
	public bool Patrol;
	public bool PlayerSeen;
	private float _shootingCooldown;

	void Start()
	{
		_pointIndex = 1;
		_patrolPositions = transform.parent.GetComponentsInChildren<Transform>().Where(x => x.name.Contains("Patrol Point")).OrderBy(x => x.name).Select(x => x.gameObject).ToList();
		if (transform.position != _patrolPositions[0].transform.position)
		{
			transform.position = _patrolPositions[0].transform.position;
		}
		_rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (PlayerSeen)
		{
			Patrol = false;
		}
		_rb.velocity = transform.up * 2;
		if (Patrol)
		{
			float result = Vector2.Distance(transform.position, _patrolPositions[_pointIndex].transform.position);
			if (result < 0)
			{
				result = -result;
			}
			if (result < 0.1f)
			{
				_pointIndex++;
				if (_pointIndex == _patrolPositions.Count)
				{
					_pointIndex = 0;
				}
				LookAt2D(_patrolPositions[_pointIndex].transform.position);
			}
		}
		else if (PlayerSeen && !IsRanged)
		{
			Vector2 playerPos = Movement.Player.transform.position;
			LookAt2D(playerPos);
			//handle melee behaviour here
		}
		else if (PlayerSeen && IsRanged)
		{
			_rb.isKinematic = true;
			Vector2 playerPos = Movement.Player.transform.position;
			LookAt2D(playerPos);
			//shoot here
		}
		else
		{
			_rb.isKinematic = false;
			Patrol = true;
			LookAt2D(_patrolPositions[_pointIndex].transform.position);
		}

	}

	private void LookAt2D(Vector3 target)
	{
		Vector3 diff = target - transform.position;
		diff.Normalize();
		float angle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg) - 90;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}


}
