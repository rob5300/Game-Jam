using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{

	public string Name = "Item";
	public Sprite sprite;
	public int Damage = 1;
	public float CostPerUse = 1f;
	public ItemType SelectedItemType;
	public int Tier;
	public float AttackSpeed;
	public int MaxDamage;
	public int Cost;
	public int MaxCost;

	protected virtual void Start()
	{
		if (SelectedItemType != ItemType.Consumable)
		{
			float result = Game.DistanceDifficulty / Game.DistanceInterval;
			if (Tier < 1)
			{
				Tier = 1;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") Player.player.PickupItem(this);
	}

	private void Reset()
	{
		GetComponent<BoxCollider2D>().isTrigger = true;
		sprite = GetComponent<SpriteRenderer>().sprite;
	}
}

public enum ItemType
{
	BasicSword, PinballSword, BouncyGun, DuckBazooka, Consumable
}
