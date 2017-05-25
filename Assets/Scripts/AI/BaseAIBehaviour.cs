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
	public bool IsStrong;
	public GameObject Muzzle;
	public GameObject BulletPrefab;
	public Rigidbody2D Rb;
	public bool Patrol;
	public bool PlayerSeen;
	private float _shootingCooldown;
	private float _rangedCoolDown;
	public float Health;
	public bool IsHit;
	public bool CanShoot;

	void Start()
	{
		IsHit = false; 
		_rangedCoolDown = 0f;
		_pointIndex = 1;
		_patrolPositions = transform.parent.GetComponentsInChildren<Transform>().Where(x => x.name.Contains("Patrol Point")).OrderBy(x => x.name).Select(x => x.gameObject).ToList();
		if (transform.position != _patrolPositions[0].transform.position)
		{
			transform.position = _patrolPositions[0].transform.position;
		}
		Rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		if (PlayerSeen)
		{
			Patrol = false;
		}
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
			Rb.velocity = transform.up * 2;
		}
		else if (PlayerSeen && !IsRanged && !IsStrong)
		{
			Vector2 playerPos = Movement.Player.transform.position;
			LookAt2D(playerPos);
			float distance = Vector2.Distance(transform.position, playerPos);
			if (distance < 0)
			{
				distance = -distance;
			}
			if (distance < 1)
			{
				Rb.velocity = Vector2.zero;
				//ATTACK BITCHES
			}
			else
			{
				if (!IsHit)
				{
					Rb.velocity = transform.up * 2;
				}
				else if (Rb.velocity == Vector2.zero)
				{
					IsHit = false;
				}
			}
		}
		else if (PlayerSeen && IsRanged && !IsStrong)
		{
			Rb.velocity = Vector2.zero;
			Vector2 playerPos = Movement.Player.transform.position;
			LookAt2D(playerPos);
			if (_rangedCoolDown > 1f && CanShoot)
			{
				Instantiate(BulletPrefab, Muzzle.transform.position, Muzzle.transform.rotation);
				_rangedCoolDown = 0f;
			}
			if (IsHit)
			{
				CanShoot = false;
			}
			else if (Rb.velocity == Vector2.zero)
			{
				IsHit = false;
				CanShoot = true;
			}
		}
		else if (PlayerSeen && IsStrong)
		{
			Vector2 playerPos = Movement.Player.transform.position;
			LookAt2D(playerPos);
			float distance = Vector2.Distance(transform.position, playerPos);
			if (distance < 0)
			{
				distance = -distance;
			}
			if (distance < 1)
			{
				Rb.constraints = RigidbodyConstraints2D.FreezeAll;
				StartCoroutine(Explode());
			}
			else
			{
				Rb.velocity = transform.up * 4;
			}
		}
		else
		{
			Patrol = true;
			LookAt2D(_patrolPositions[_pointIndex].transform.position);
		}
		if (IsRanged)
		{
			_rangedCoolDown += Time.deltaTime;
		}
	}

	private IEnumerator Explode()
	{
		yield return new WaitForSeconds(0.5f);
		//add animation here and trigger collider logic
		Destroy(gameObject);
	}

	private void LookAt2D(Vector3 target)
	{
		Vector3 diff = target - transform.position;
		diff.Normalize();
		float angle = (Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg) - 90;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
