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
	private float _rangedCoolDown;
	public int Tier;
	public int Damage;
	public float Health;
	public bool IsHit;
	public bool CanShoot;
	private bool _firstWait;
	private bool _secondWait;
	public Vector2 Velocity;
	public Vector2 ForwardVelocity;
	void Start()
	{
		_firstWait = true;
		_secondWait = true;
		IsHit = false;
		_rangedCoolDown = 0f;
		_pointIndex = 1;
		_patrolPositions = transform.parent.GetComponentsInChildren<Transform>().Where(x => x.name.Contains("Patrol Point")).OrderBy(x => x.name).Select(x => x.gameObject).ToList();
		if (transform.position != _patrolPositions[0].transform.position)
		{
			transform.position = _patrolPositions[0].transform.position;
		}
		Rb = GetComponent<Rigidbody2D>();
		int multiplier = 0;
		if (Game.DifficultyMultiplier == PlayerDifficultyMultiplier.Easy)
		{
			multiplier = 1;
		}
		else if (Game.DifficultyMultiplier == PlayerDifficultyMultiplier.Medium)
		{
			multiplier = 2;
		}
		else
		{
			multiplier = 3;
		}
		Tier = (Game.DistanceDifficulty * multiplier) / Game.DistanceInterval;
		if (Tier < 1)
		{
			Tier = 1;
		}
		Health = Tier * 30;
		Damage = Tier * 3;
		Velocity = transform.up * 2;

	}

	private void FixedUpdate()
	{
		ForwardVelocity = transform.up * 2;
		if (PlayerSeen)
		{
			Patrol = false;
		}
		if (Patrol)
		{
			Rb.velocity = Velocity;
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
			//Rb.velocity = transform.up * 2;
			Velocity = ForwardVelocity;
			Rb.velocity = Velocity;
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
				Movement.Player.GetComponent<Player>().Damage(Damage);
			}
			if (IsHit)
			{
				Rb.velocity = Velocity;
			}
			else
			{
				Rb.velocity = ForwardVelocity;
			}

		}
		else if (PlayerSeen && IsRanged && !IsStrong)
		{
			Vector2 playerPos = Movement.Player.transform.position;
			LookAt2D(playerPos);
			if (_rangedCoolDown > 1f && CanShoot)
			{
				Instantiate(BulletPrefab, Muzzle.transform.position, Muzzle.transform.rotation);
				_rangedCoolDown = 0f;
			}
			if (IsHit)
			{
				Rb.velocity = Velocity;
				CanShoot = false;
			}
			else if (Rb.velocity == Vector2.zero)
			{
				IsHit = false;
				CanShoot = true;
			}
			if (!IsHit)
			{
				Rb.velocity = Vector2.zero;
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
			_rangedCoolDown += Time.fixedDeltaTime;
		}
	}

	void Update()
	{
		if (Health <= 0)
		{
			Destroy(gameObject);
		}
	}

	private IEnumerator Explode()
	{
		if (_firstWait)
		{
			_firstWait = false;
			yield return new WaitForSeconds(0.5f);
		}
		//add animation here and trigger collider logic
		if (_secondWait)
		{
			RaycastHit2D[] rays = Physics2D.CircleCastAll(transform.position, 1.5f, Vector2.zero);
			if (rays != null)
			{
				if (rays.Count() != 0)
				{
					foreach (RaycastHit2D ray in rays)
					{
						if (ray.transform.tag == "Player")
						{
							Movement.Player.GetComponent<Player>().Damage(Damage * 3);
						}
						else if (ray.transform.tag == "Enemy")
						{
							ray.transform.GetComponent<BaseAIBehaviour>().Health -= (Damage * 3);
						}
					}
				}
			}
			_secondWait = false;
			yield return new WaitForSeconds(0.5f); //change this time to allow for the animation to play and for damage to register
		}
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
