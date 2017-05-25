using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public static UI ui;

    public Image Slot0;
    public Image Slot1;
    public Image Slot2;
    public Image Slot3;
    public Image Slot4;

    public void Start()
    {
        if (!ui) ui = this;
    }

}
