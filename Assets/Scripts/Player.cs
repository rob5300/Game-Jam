using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Player : MonoBehaviour
{

	public static Player player;

	public float Health = 100f;
	public float DreamCatchers = 0f;
	public List<ItemSlot> Inventory = new List<ItemSlot>() { new ItemSlot(0), new ItemSlot(1), new ItemSlot(2), new ItemSlot(3), new ItemSlot(4) };
	public Dictionary<int, GameObject> itemObjects;
	public int SelectedSlot = 0;
	public IUseable SelectedItem;
	public Vector2 PreviousLocation;
	public bool IsHit;
	public float InvincibilityWindow;

    public AudioClip basicSwordSound;
    public AudioClip pinballSwordSound;
    public AudioClip duckGunSound;

	void Start()
	{
		Health = 100f;
		IsHit = false;
		if (!player) player = this;
        UI.ui.Backgrounds[SelectedSlot].color = Color.yellow;
	}

	public void Damage(float amount)
	{
		if (!IsHit)
		{
			IsHit = true;
			if (Health - amount < 0)
			{
				Health = 0;
                UI.ui.HealthSlider.value = Health;
                UI.ui.HealthText.text = "Dead";
                Die();
			}
			else
			{
				Health -= amount;
                UI.ui.HealthSlider.value = Health;
                UI.ui.HealthText.text = Health + "";
			}
		}
	}

	void Die()
	{
        gameObject.SetActive(false);
	}

	public void Update()
	{
		Vector3 mouse = Input.mousePosition;
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
		Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		float angle = (Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg) - 90;
		Movement.Muzzle.transform.rotation = Quaternion.Euler(0, 0, angle);
		if (IsHit)
		{
			if (InvincibilityWindow > 3f)
			{
				IsHit = false;
				InvincibilityWindow = 0f;
			}
			else
			{
				InvincibilityWindow += Time.deltaTime;
			}
		}

		//Scrolling
		float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
		if (mouseWheel > 0.01)
		{
			ChangeSlot(SelectedSlot - 1);
		}
		else if (mouseWheel < -0.01)
		{
			ChangeSlot(SelectedSlot + 1);
		}
		//Left click to attack.
		if (Input.GetMouseButtonDown(0))
		{
			Attack();
		}
		if (transform.position.y - PreviousLocation.y >= Game.DistanceInterval)
		{
			Game.DistanceDifficulty += Game.DistanceInterval;
			PreviousLocation = transform.position;
		}
	}

	public void ChangeSlot(int slotnumber)
	{
		if (slotnumber < Inventory.Count && slotnumber > -1)
		{
			SelectedSlot = slotnumber;
			foreach (Image image in UI.ui.Backgrounds)
			{
				image.color = Color.white;
			}
			UI.ui.Backgrounds[SelectedSlot].color = Color.yellow;
			if (Inventory[SelectedSlot].item != null)
			{
				SelectedItem = Inventory[SelectedSlot].item.GetComponent<IUseable>();
			}
		}
	}

	public void PickupItem(Item item)
	{
		Debug.Log("Picked up item: " + item.name);
		if (Inventory[SelectedSlot].item != null)
		{
			Inventory[SelectedSlot].item.transform.position = item.transform.position;
            Inventory[SelectedSlot].item.transform.parent = null;
            Inventory[SelectedSlot].item.CanPickup = false;
            Inventory[SelectedSlot].item.gameObject.SetActive(true);
			Inventory[SelectedSlot].item = null;
		}

		Inventory[SelectedSlot].item = item;
		item.gameObject.SetActive(false);
		item.transform.parent = transform;

        SelectedItem = Inventory[SelectedSlot].item.GetComponent<IUseable>();

        //Update UI
        UI.ui.Slots[SelectedSlot].sprite = item.sprite;
		UI.ui.Slots[SelectedSlot].gameObject.SetActive(true);
	}

	public void Attack()
	{
		if (Inventory[SelectedSlot].item == null)
		{
			return;
		}
		else if (SelectedItem == null && Inventory[SelectedSlot].item != null)
		{
			SelectedItem = Inventory[SelectedSlot].item.GetComponent<IUseable>();
		}
		if (Inventory[SelectedSlot].item.SelectedItemType == ItemType.BouncyGun || Inventory[SelectedSlot].item.SelectedItemType == ItemType.DuckBazooka)
		{
            AudioSource.PlayClipAtPoint(duckGunSound, Camera.main.transform.position);
			SelectedItem.Use();
		}
        else
        {
            if (Inventory[SelectedSlot].item.SelectedItemType == ItemType.BasicSword)
            {
                AudioSource.PlayClipAtPoint(basicSwordSound, Camera.main.transform.position, 100);
            }
            else
            {
                AudioSource.PlayClipAtPoint(pinballSwordSound, Camera.main.transform.position);
            }
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + GetComponent<Movement>().Aim.up * 1, 0.5f, Vector2.zero);
            if (hits != null && SelectedItem != null)
            {
                SelectedItem.Use(hits);
            }
        }


	}
}
