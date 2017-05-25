using UnityEngine;

public class ItemPlacer : MonoBehaviour {

	void Start () {
		if(Game.Items.Count == 0)
        {
            Game.LoadItems();
        }
	}
	
}
