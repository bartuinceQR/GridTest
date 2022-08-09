using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderEdit : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeText);
    }

    // Update is called once per frame
    void ChangeText(float value)
    {
        countText.SetText(value.ToString());
    }
}
