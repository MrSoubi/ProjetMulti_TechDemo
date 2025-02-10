using TMPro;
using UnityEngine;

public class UI_Resources : MonoBehaviour
{
    public RSO_ResourceCount resourceCount;
    public TextMeshProUGUI text;

    private void OnEnable()
    {
        resourceCount.onValueChanged += UpdateText;
    }

    private void OnDisable()
    {
        resourceCount.onValueChanged -= UpdateText;
    }

    private void UpdateText(int value)
    {
        text.text = "Resources: " + value;
    }

    void Start()
    {
        UpdateText(resourceCount.Value);
    }

}
