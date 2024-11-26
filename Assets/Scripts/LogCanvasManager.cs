using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCanvasManager : MonoBehaviour
{
    public static LogCanvasManager Instance;

    public GameObject LogRowPrefab;
    public Transform LogColumn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple LogCanvasManager instances detected. Destroying this one.");
            Destroy(this);
        }
    }

    public void AddLogRow(SolveAttemptRecord record)
    {
        // TODO: make sure it is the last child
        GameObject logRow = Instantiate(LogRowPrefab, LogColumn);
        logRow.GetComponent<InitAttemptLog>().InitFromAttempt(record);
    }
}
