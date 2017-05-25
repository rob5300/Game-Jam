using UnityEngine;

public class Item : MonoBehaviour {

    public string Name = "Item";
    public Sprite sprite;
    public int Damage = 1;
    public float CostPerUse = 1f;

    public void OnTriggerEnter(Collider other)
    {
        Player.player.PickupItem(this);
    }

}
