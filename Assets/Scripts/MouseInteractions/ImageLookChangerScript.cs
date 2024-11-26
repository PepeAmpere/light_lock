using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageLookChangerScript : ALookChanger
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
        _defaultSprite = _image.sprite;
    }
    public override void LooksToDefault(bool hardSet = false)
    {
        _activationCounter -= 1;
        if (_activationCounter < 0 )
            _activationCounter = 0;
        if ( hardSet )
            _activationCounter = 0;

        if (_activationCounter == 0)
        {
            _image.sprite = _defaultSprite;
            _image.color = _defaultColor;
        }
    }

    public override void LooksToHover()
    {
        _activationCounter += 1;

        if (_activationCounter >= 1)
        {
            _image.color = _onHoverColor;
            if(_onHoverSprite != null)
                _image.sprite = _onHoverSprite;
        }
    }
}
