using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitAttemptLog : MonoBehaviour
{
    public Image UpRenderer;
    public Image RightRenderer;
    public Image DownRenderer;
    public Image LeftRenderer;

    public TextMeshProUGUI PerfectText;
    public TextMeshProUGUI MissPosText;

    public void InitFromAttempt(SolveAttemptRecord record)
    {
        UpRenderer.color = record.Up;
        RightRenderer.color = record.Right;
        DownRenderer.color = record.Down;
        LeftRenderer.color = record.Left;

        PerfectText.text = record.Perfect.ToString();
        MissPosText.text = record.MissPos.ToString();
    }
}
