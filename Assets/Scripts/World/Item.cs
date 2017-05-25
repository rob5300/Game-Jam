using UnityEngine;

public class Item : MonoBehaviour {

    public string Name = "Item";
    public Sprite sprite;
    public int Damage = 1;
    public float CostPerUse = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") Player.player.PickupItem(this);
    }

}
