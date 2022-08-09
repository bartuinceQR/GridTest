using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEdit : MonoBehaviour
{
    private Button _button;
    [SerializeField]
    private Slider tileSlider;
    [SerializeField]
    private Slider paddingSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TriggerGeneration);
    }

    // Update is called once per frame
    void TriggerGeneration()
    {
        MainController.Instance.GenerateGrid((int)tileSlider.value, (int)paddingSlider.value);
    }
}
