using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsControlScript : MonoBehaviour
{
    public LightController LightController;
    public List<ColorSwitchScript> SwitchScriptList;

    private int[] _colorInput = new int[3];

    private void Start()
    {
        foreach (ColorSwitchScript switchScript in SwitchScriptList)
        {
            switchScript.OnToggle.AddListener(OnSwitchToggle);
        }
    }

    private void OnSwitchToggle()
    {
        _colorInput = new int[3];
        foreach (ColorSwitchScript switchScript in SwitchScriptList)
        {
            if (switchScript.State)
            {
                _colorInput[switchScript.ColorIndex] = 1;
            }
        }

        LightController.SetLightColor(_colorInput);
    }
}
