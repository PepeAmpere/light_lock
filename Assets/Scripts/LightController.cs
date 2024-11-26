using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light2D _light;

    private void Start()
    {
        SetLightColor(new int[] { 0, 0, 0 });
    }

    public void SetLightColor(int[] colorInput) // 0 or 1
    {
        _light.color = new Color(colorInput[0], colorInput[1], colorInput[2]);
    }
}
