using UnityEngine;

public class ItemPlacer : MonoBehaviour {

	void Start () {
		if(Game.Items.Count == 0)
        {
            Game.LoadItems();
        }

        Instantiate(Game.Items[Random.Range(0, Game.Items.Count)], transform.position, Quaternion.identity);
        Destroy(gameObject);
	}
	
}
