using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorSwitchScript : MonoBehaviour
{
    public int ColorIndex = 0; // the index in the RGB array
    public bool State;

    public UnityEvent OnToggle;

    [SerializeField] private SpriteRenderer _childRenderer;
    private Color _ogColor;
    private Color _transparentColor;

    private void Start()
    {
        _ogColor = _childRenderer.color;
        _transparentColor = new Color(_ogColor.r, _ogColor.g, _ogColor.b, 0);
    }

    public void Toggle()
    {
        State = !State;

        OnToggle?.Invoke();
        SetChildVisuals();
    } 

    private void SetChildVisuals()
    {
        if (State)
        {
            _childRenderer.color = _transparentColor;
        }
        else
        {
            _childRenderer.color = _ogColor;
        }
    }
}
