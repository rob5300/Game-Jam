﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class BasicSword : Item, IUseable
{
	public int DamageIncreaserValue;
	public int CostIncreaserValue;
	public float TimeSinceLastAttack = 0f;
	protected override void Start()
	{
		base.Start();
		Damage = Tier * 10;
		AttackSpeed = 0.5f / Tier;
		MaxDamage = ((Tier + 1) * 10) + 10;
		Cost = Tier * 20;
		MaxCost = (Tier + 1) * 40;
	}

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

	void Update()
	{
		TimeSinceLastAttack += Time.deltaTime;
	}

	public void Use(RaycastHit2D[] collisionInfo)
	{
		//if (TimeSinceLastAttack > AttackSpeed)
		//{
		if (collisionInfo != null)
		{
			if (collisionInfo.Count() != 0)
			{
				IncrementValues();
				foreach (RaycastHit2D hit in collisionInfo)
				{
					if (hit.transform.tag == "Enemy")
					{
						BaseAIBehaviour enemy = hit.transform.GetComponent<BaseAIBehaviour>();
						enemy.IsHit = true;
						enemy.Health -= Damage;
						enemy.Velocity = -enemy.Velocity;
					}
				}
			}
		}
		TimeSinceLastAttack = 0f;
		//}

	}

	public void Use()
	{
		return;
	}
}

