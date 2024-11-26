using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsControlScript : MonoBehaviour
{
    public LightController LightController;
    public List<ColorSwitchScript> SwitchScriptList;

    private ColorInput _colorInput = new ColorInput { };
    public ColorInput ColorInput { get { return _colorInput; } }

    private void Start()
    {
        foreach (ColorSwitchScript switchScript in SwitchScriptList)
        {
            switchScript.OnToggle.AddListener(OnSwitchToggle);
        }
    }
    private void OnSwitchToggle()
    {
        var colorInput = new int[3];
        foreach (ColorSwitchScript switchScript in SwitchScriptList)
        {
            if (switchScript.State)
            {
                colorInput[switchScript.ColorIndex] = 1;
            }
        }
        _colorInput = new ColorInput(colorInput);
        LightController.SetLightColor(_colorInput);
    }
}
