using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player player;

    public float Health = 10f;
    public float DreamCatchers = 0f;
    public List<ItemSlot> Inventory = new List<ItemSlot>() { new ItemSlot(0), new ItemSlot(1), new ItemSlot(2), new ItemSlot(3), new ItemSlot(4) };
    public Dictionary<int, GameObject> itemObjects;
    public int SelectedSlot = 0;

	void Start () {
        if (!player) player = this;
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
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel > 0.01)
        {
            ChangeSlot(SelectedSlot + 1);
        }
        else if (mouseWheel < -0.01)
        {
            ChangeSlot(SelectedSlot - 1);
        }
    }

    public void ChangeSlot(int slotnumber)
    {
        if (slotnumber < Inventory.Count && slotnumber > -1)
        {
            SelectedSlot = slotnumber;
        }
    }

    public void PickupItem(Item item)
    {
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
        switch (SelectedSlot)
        {
            case 0:
                UI.ui.Slot0.sprite = item.sprite;
                UI.ui.Slot0.gameObject.SetActive(true);
                break;
            case 1:
                UI.ui.Slot1.sprite = item.sprite;
                UI.ui.Slot1.gameObject.SetActive(true);
                break;
            case 2:
                UI.ui.Slot2.sprite = item.sprite;
                UI.ui.Slot2.gameObject.SetActive(true);
                break;
            case 3:
                UI.ui.Slot3.sprite = item.sprite;
                UI.ui.Slot3.gameObject.SetActive(true);
                break;
            case 4:
                UI.ui.Slot4.sprite = item.sprite;
                UI.ui.Slot4.gameObject.SetActive(true);
                break;
        }
    }
}
