using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct ColorInput
{
    public float R;
    public float G;
    public float B;

    // Constructor that takes an int[3] as a parameter
    public ColorInput(int[] rgb)
    {
        if (rgb == null || rgb.Length != 3)
        {
            throw new System.ArgumentException("Input array must be of length 3 and not null.");
        }
        R = Mathf.Clamp01(rgb[0]);
        G = Mathf.Clamp01(rgb[1]);
        B = Mathf.Clamp01(rgb[2]);
    }

    // Explicit conversion from ColorInput to Unity's Color structure
    public static explicit operator Color(ColorInput input)
    {
        // Clamp the values between 0 and 1 to ensure valid Color values
        return new Color(Mathf.Clamp01(input.R), Mathf.Clamp01(input.G), Mathf.Clamp01(input.B));
    }

    // Explicit implementation of the Equals method
    public override bool Equals(object obj)
    {
        if (obj is ColorInput other)
        {
            // Use Mathf.Approximately for floating-point comparison
            return Mathf.Approximately(R, other.R) &&
                   Mathf.Approximately(G, other.G) &&
                   Mathf.Approximately(B, other.B);
        }
        return false;
    }

    // Override GetHashCode as well to maintain consistency with Equals
    public override int GetHashCode()
    {
        return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
    }

    // Define equality and inequality operators
    public static bool operator ==(ColorInput lhs, ColorInput rhs) => lhs.Equals(rhs);
    public static bool operator !=(ColorInput lhs, ColorInput rhs) => !lhs.Equals(rhs);
}
public struct Used
{
    public bool Up;
    public bool Right;
    public bool Down;
    public bool Left;
}
    public struct SolveAttemptRecord
{
    public Color Up;
    public Color Right;
    public Color Down;
    public Color Left;

    public int Perfect;
    public int MissPos;
}

public struct PuzzleSolution
{
    public ColorInput Up;
    public ColorInput Right;
    public ColorInput Down;
    public ColorInput Left;

    public List<string> Hints;
}

public class PuzzleController : MonoBehaviour
{
    public LightController Up;
    public LightController Right;
    public LightController Down;
    public LightController Left;

    private List<PuzzleSolution> Solutions = new List<PuzzleSolution>
    {
        new PuzzleSolution
        {
            Down = new ColorInput { B = 0, R = 0, G = 0, },
            Left = new ColorInput { B = 0, R = 1, G = 0, },
            Up = new ColorInput { B = 1, R = 0, G = 1, },
            Right = new ColorInput { B = 0, R = 0, G = 0, },
            Hints = new List<string> {
                "This lock has 1 components of color Red  active.",
                "Down part of lock has 0 components enabled.",
                "This lock has 1 occurences of color Red.",
                "This lock has 1 components of color Green  active.",
                "This lock has 0 occurences of color White.",
                "This lock has 1 components of color Blue  active.",
                "This lock has 0 occurences of color Green.",
                "Left part of lock has 1 components enabled.",
                "This lock has 1 occurences of color Cyan.",
                "This lock has 0 occurences of color Blue.",
                "Right part of lock has 0 components enabled.",
                "Up part of lock has 2 components enabled.",
            }
        }
    };

    private PuzzleSolution _currentSolution;
    private List<SolveAttemptRecord> _solveAttemptRecords = new List<SolveAttemptRecord>();

    private void Start()
    {
        // set solution randlomly from Solutions
        _currentSolution = Solutions[Random.Range(0, Solutions.Count)];
    }

    private int _hintCount = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (IsSoved())
            {
                Debug.Log("Puzzle Solved");
            }
            else
            {
                Debug.Log("Puzzle Not Solved");
            }

            if (_hintCount < 4)
            {
                AddRandomHint();
                _hintCount++;
            }
        }

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    AddRandomHint();
        //}
    }

    public bool IsSoved()
    {
        int perfect = 0;
        int missPos = 0;

        bool[] notUsed = new bool[4] { true, true, true, true };

        // Check if each input matches the corresponding solution
        if (_currentSolution.Up == Up.GetCurrentInput())
        {
            perfect++;
            notUsed[0] = false;
        }
        else if(ColorExistsInCurrentSolution(Up.GetCurrentInput(), ref notUsed))
        {
            missPos++;
        }

        if (_currentSolution.Right == Right.GetCurrentInput())
        {
            perfect++;
            notUsed[1] = false;
        }
        else if (ColorExistsInCurrentSolution(Right.GetCurrentInput(), ref notUsed))
        {
            missPos++;
        }

        if (_currentSolution.Down == Down.GetCurrentInput())
        {
            perfect++;
            notUsed[2] = false;
        }
        else if (ColorExistsInCurrentSolution(Down.GetCurrentInput(), ref notUsed))
        {
            missPos++;
        }

        if (_currentSolution.Left == Left.GetCurrentInput())
        {
            perfect++;
            notUsed[3] = false;
        }
        else if (ColorExistsInCurrentSolution(Left.GetCurrentInput(), ref notUsed))
        {
            missPos++;
        }

        SolveAttemptRecord record = new SolveAttemptRecord {
            Up = (Color)Up.GetCurrentInput(),
            Right = (Color)Right.GetCurrentInput(),
            Down = (Color)Down.GetCurrentInput(),
            Left = (Color)Left.GetCurrentInput(),

            Perfect = perfect,
            MissPos = missPos
        };

        LogCanvasManager.Instance.AddLogRow(record);
        _solveAttemptRecords.Add(record);

        return record.Perfect == 4;
    }

    private bool ColorExistsInCurrentSolution(ColorInput color, ref bool[] notUsed)
    {
        if (color == _currentSolution.Up && notUsed[0])
        {
            notUsed[0] = false;
            return true;
        }
        else if (color == _currentSolution.Right && notUsed[1])
        {
            notUsed[1] = false;
            return true;
        }
        else if (color == _currentSolution.Down && notUsed[2])
        {
            notUsed[2] = false;
            return true;
        }
        else if (color == _currentSolution.Left && notUsed[3])
        {
            notUsed[3] = false;
            return true;
        }

        return false;
    }
    private void AddRandomHint()
    {
        if (_currentSolution.Hints.Count > 0)
        {
            HintCanvasManager.Instance.AddHintRow(_currentSolution.Hints[Random.Range(0, _currentSolution.Hints.Count)]);
        }
    }
    private void LogRandomHint()
    {
        if (_currentSolution.Hints.Count > 0)
        {
            Debug.Log(_currentSolution.Hints[Random.Range(0, _currentSolution.Hints.Count)]);
        }
    }
    private void DebugLogRecords()
    {
        Debug.Log("...");
        Debug.Log("Vizualizing All Your Attempts");
        foreach (var record in _solveAttemptRecords)
        {
            Debug.Log($"Up: {record.Up}, Right: {record.Right}, Down: {record.Down}, Left: {record.Left}, Perfect: {record.Perfect}, MissPos: {record.MissPos}");
        }
    }
}
