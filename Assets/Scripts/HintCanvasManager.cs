using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCanvasManager : MonoBehaviour
{
    public static HintCanvasManager Instance;

    public GameObject HintRowPrefab;
    public Transform HintColumn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple HintCanvasManager instances detected. Destroying this one.");
            Destroy(this);
        }
    }

    public void AddHintRow(string text)
    {
        // TODO: make sure it is the last child
        GameObject logRow = Instantiate(HintRowPrefab, HintColumn);
        logRow.GetComponent<InitHintScript>().InitHint(text);
    }
}
