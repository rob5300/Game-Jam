using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public static UI ui;

    public Image[] Slots;
    public Image[] Backgrounds;
    public Slider HealthSlider;
    public Text HealthText;

    public void Awake()
    {
        if (!ui) ui = this;
    }

}
