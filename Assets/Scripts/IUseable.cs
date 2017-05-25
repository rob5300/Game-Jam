using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public interface IUseable
{
	void Use(RaycastHit2D[] collisionInfo);
	void IncrementValues();
}