using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyGun : Item, IUseable
{

	public int DamageIncreaserValue;
	public int CostIncreaserValue;
	public float TimeSinceLastAttack = 0f;
	public GameObject BulletPrefab;
	public GameObject Muzzle;

	public void IncrementValues()
	{
		if (Damage >= MaxDamage)
		{
			Damage = MaxDamage;
		}
		if (Cost >= MaxCost)
		{
			Cost = MaxCost;
		}
		if (Damage != MaxDamage)
		{
			Damage += DamageIncreaserValue;
		}
		if (Cost != MaxCost)
		{
			Cost += CostIncreaserValue;
		}
	}

	public void Use(RaycastHit2D[] collisionInfo)
	{
		return;
	}

	public void Use()
	{
		GameObject bullet = Instantiate(BulletPrefab, Muzzle.transform.position + Muzzle.transform.up * 1, Muzzle.transform.rotation) as GameObject;
		bullet.GetComponent<BouncyBullet>().Damage = Damage;
		//play sound here
		IncrementValues();
	}

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		Damage = Tier * 5;
		AttackSpeed = 0.5f / Tier;
		MaxDamage = ((Tier + 1) * 10) + 10;
		Cost = Tier * 20;
		MaxCost = (Tier + 1) * 40;
		Muzzle = Movement.Muzzle;
	}

	// Update is called once per frame
	void Update()
	{
		if (Muzzle == null)
		{
			Muzzle = Movement.Muzzle;
		}
	}
}