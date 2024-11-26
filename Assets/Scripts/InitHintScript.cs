using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitHintScript : MonoBehaviour
{
    public Image BoxImage;
    public TextMeshProUGUI BoxText;

    public Sprite DefaultBoxSprite;
    public Sprite OpenBoxSprite;

    private string _hint;

    private void Start()
    {
        BoxImage.enabled = true;
        BoxText.text = "";

        BoxImage.sprite = DefaultBoxSprite;
    }

    public void InitHint(string hint)
    {
        _hint = hint;
    }

    public void ActivateHing()
    {
        BoxText.text = _hint;

        BoxImage.sprite = OpenBoxSprite;
        BoxImage.color = new Color(1, 1, 1, 1);
        Destroy(BoxImage.GetComponent<ClickableScript>());
        Destroy(BoxImage.GetComponent<ImageLookChangerScript>());
    }
}
