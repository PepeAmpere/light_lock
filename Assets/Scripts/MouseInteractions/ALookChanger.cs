using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALookChanger : MonoBehaviour
{
    protected Color _defaultColor;
    [SerializeField] protected Color _onHoverColor = Color.white;
    protected Sprite _defaultSprite;
    [SerializeField] protected Sprite _onHoverSprite;
    protected int _activationCounter;
    public abstract void LooksToHover();
    public abstract void LooksToDefault(bool hardSet = false);
}
