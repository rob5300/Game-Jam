using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public static UI ui;

    public Image[] Slots;
    public Image[] Backgrounds;

    public void Start()
    {
        if (!ui) ui = this;
    }

}
