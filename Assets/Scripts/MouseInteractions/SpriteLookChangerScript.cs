using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLookChangerScript : ALookChanger
{
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
        _defaultSprite = _spriteRenderer.sprite;
    }
    public override void LooksToDefault(bool hardSet = false)
    {
        _activationCounter -= 1;
        if (_activationCounter < 0 )
            _activationCounter = 0;
        if (hardSet)
            _activationCounter = 0;

        if (_activationCounter == 0)
        {
            _spriteRenderer.sprite = _defaultSprite;
            _spriteRenderer.color = _defaultColor;
        }
    }
    public override void LooksToHover()
    {
        _activationCounter += 1;
        if (_activationCounter >= 1)
        {
            _spriteRenderer.color = _onHoverColor;
            if (_onHoverSprite != null)
                _spriteRenderer.sprite = _onHoverSprite;
        }
    }
}
