using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light2D _light;
    [SerializeField] private ButtonsControlScript _buttonsControlScript;

    private void Start()
    {
        SetLightColor(new ColorInput { R = 0, G = 0, B = 0 });
    }

    public ColorInput GetCurrentInput()
    {
        return _buttonsControlScript.ColorInput;
    }

    public void SetLightColor(ColorInput colorInput) // 0 or 1
    {
        _light.color = new Color(colorInput.R, colorInput.G, colorInput.B);
    }
}
