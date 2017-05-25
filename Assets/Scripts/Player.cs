using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public static Player player;

    public float Health = 10f;
    public float DreamCatchers = 0f;
    public List<ItemSlot> Inventory = new List<ItemSlot>() { new ItemSlot(0), new ItemSlot(1), new ItemSlot(2), new ItemSlot(3), new ItemSlot(4) };
    public Dictionary<int, GameObject> itemObjects;
    public int SelectedSlot = 0;
	public IUseable SelectedItem;

	void Start () {
        if (!player) player = this;

        UI.ui.Backgrounds[SelectedSlot].color = Color.yellow;
    }
	
    void Damage(float amount)
    {
        if(Health - amount < 0)
        {
            Health = 0;
            Die();
        }
        else
        {
            Health -= amount;
        }
    }

    void Die()
    {
        
    }

    public void Update()
    {
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
			if (SelectedItem != null)
			{
				SelectedItem = Inventory[SelectedSlot].item.GetComponent<IUseable>();
			}
		}
    }

    public void PickupItem(Item item)
    {
        Debug.Log("Picked up item: " + item.name);
        if(Inventory[SelectedSlot].item != null)
        {
            Inventory[SelectedSlot].item.transform.position = item.transform.position;
            Inventory[SelectedSlot].item.gameObject.SetActive(true);
            Inventory[SelectedSlot].item = null;
        }

        Inventory[SelectedSlot].item = item;
        item.gameObject.SetActive(false);
        item.transform.parent = transform;

        //Update UI
        UI.ui.Slots[SelectedSlot].sprite = item.sprite;
        UI.ui.Slots[SelectedSlot].gameObject.SetActive(true);
    }

    public void Attack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + GetComponent<Movement>().Aim.up * 1, 0.5f, Vector2.zero);
		if (hits != null && SelectedItem != null)
		{
			SelectedItem.Use(hits);
		}

    }
}
