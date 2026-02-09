using UnityEngine;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("General Puzzle Settings")]
    public int totalPuzzles = 5;
    public int completedPuzzles = 0;
    public TMP_Text progressText;
    public TMP_Text timerText; // UI element for countdown display

    [Header("General Timer")]
    public float puzzleTimeLimit = 600f; // 10 minutes
    private float currentTime;
    private bool timerRunning = true;

    [Header("Puzzle 1: Button Sequence")]
    private int[] correctButtonSequence = { 1, 2, 3 };
    private int currentButtonStep = 0;

    [Header("Puzzle 2: Sound Pads")]
    private int[] correctSoundSequence = { 1, 2, 3 };
    private int currentSoundStep = 0;
    private bool puzzle2Completed = false;

    [Header("Puzzle 5: Timed Tile Activation")]
    public float puzzle5TimeLimit = 30f; // 30 seconds for puzzle 5
    private float puzzle5CurrentTime;
    private bool puzzle5Active = false;
    private int[] correctTileSequence = { 3, 1, 5, 2, 4 }; // Example correct order
    private int currentTileStep = 0;

    void Start()
    {
        currentTime = puzzleTimeLimit;
        UpdateProgressUI();
    }

    void Update()
    {
        // General countdown timer
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI(currentTime, "Time Left");

            if (currentTime <= 0f)
            {
                timerRunning = false;
                Debug.Log("Time's up! All puzzles reset.");
                ResetAllPuzzles();
            }
        }

        // Puzzle 5 specific timer
        if (puzzle5Active)
        {
            puzzle5CurrentTime -= Time.deltaTime;
            UpdateTimerUI(puzzle5CurrentTime, "Puzzle 5 Time Left");

            if (puzzle5CurrentTime <= 0f)
            {
                puzzle5Active = false;
                Debug.Log("Puzzle 5 failed! Resetting...");
                ResetPuzzle5();
            }
        }
    }

    void UpdateTimerUI(float time, string label)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timerText.text = $"{label}: {minutes:00}:{seconds:00}";
        }
    }

    void ResetAllPuzzles()
    {
        completedPuzzles = 0;
        UpdateProgressUI();

        // Reset sequences
        currentButtonStep = 0;
        currentSoundStep = 0;
        puzzle2Completed = false;

        // Reset Puzzle 5
        ResetPuzzle5();

        // Reset general timer
        currentTime = puzzleTimeLimit;
        timerRunning = true;

        Debug.Log("All puzzles have been reset. Try again!");
    }

    // ------------------- Puzzle 1: Button Sequence -------------------
    public void CheckButtonSequence(int buttonNumber)
    {
        if (buttonNumber == correctButtonSequence[currentButtonStep])
        {
            currentButtonStep++;
            Debug.Log("Correct button! Step: " + currentButtonStep);

            if (currentButtonStep >= correctButtonSequence.Length)
            {
                CompletePuzzle();
                currentButtonStep = 0;
            }
        }
        else
        {
            Debug.Log("Wrong button sequence! Resetting.");
            currentButtonStep = 0;
        }
    }

    // ------------------- Puzzle 2: Sound Pads -------------------
    public void CheckSoundSequence(int padNumber)
    {
        Debug.Log("CheckSoundSequence called with padNumber: " + padNumber);
        Debug.Log("puzzle2Completed: " + puzzle2Completed + ", currentSoundStep: " + currentSoundStep);
        Debug.Log("Expected padNumber: " + correctSoundSequence[currentSoundStep]);

        if (puzzle2Completed)
        {
            Debug.Log("Puzzle 2 already completed, ignoring.");
            return;
        }

        if (padNumber == correctSoundSequence[currentSoundStep])
        {
            currentSoundStep++;
            Debug.Log("Correct sound! Step: " + currentSoundStep + " / " + correctSoundSequence.Length);

            if (currentSoundStep >= correctSoundSequence.Length)
            {
                Debug.Log("Puzzle 2 completed!");
                puzzle2Completed = true;
                CompletePuzzle();
                currentSoundStep = 0;
            }
        }
        else
        {
            Debug.Log("Wrong sound sequence! Expected " + correctSoundSequence[currentSoundStep] + " but got " + padNumber + ". Resetting...");
            currentSoundStep = 0;
        }
    }

    // ------------------- Puzzle 5: Timed Tile Activation -------------------
    public void StartPuzzle5()
    {
        currentTileStep = 0;
        puzzle5CurrentTime = puzzle5TimeLimit;
        puzzle5Active = true;

        // Reset all tiles
        TimedTile[] tiles = FindObjectsOfType<TimedTile>();
        foreach (TimedTile t in tiles)
            t.ResetTile();

        Debug.Log("Puzzle 5 started! Step on tiles in the correct order.");
    }

    public void CheckTimedTile(int tileNumber)
    {
        if (tileNumber == correctTileSequence[currentTileStep])
        {
            currentTileStep++;
            Debug.Log("Correct tile! Step: " + currentTileStep);

            if (currentTileStep >= correctTileSequence.Length)
            {
                Debug.Log("Puzzle 5 complete!");
                CompletePuzzle();
                puzzle5Active = false;
            }
        }
        else
        {
            Debug.Log("Wrong tile! Resetting Puzzle 5.");
            ResetPuzzle5();
        }
    }

    void ResetPuzzle5()
    {
        currentTileStep = 0;
        puzzle5CurrentTime = puzzle5TimeLimit;
        puzzle5Active = true;

        // Reset all tiles
        TimedTile[] tiles = FindObjectsOfType<TimedTile>();
        foreach (TimedTile t in tiles)
            t.ResetTile();
    }

    // ------------------- General Puzzle Completion -------------------
    public void CompletePuzzle()
    {
        completedPuzzles++;
        if (completedPuzzles > totalPuzzles) completedPuzzles = totalPuzzles;

        UpdateProgressUI();

        if (completedPuzzles == totalPuzzles)
        {
            Debug.Log("All puzzles complete! Door unlocked!");
            timerRunning = false;
            puzzle5Active = false;
            // Add your door unlock logic here
        }
    }

    void UpdateProgressUI()
    {
        if (progressText != null)
            progressText.text = "Puzzle Progress: " + completedPuzzles + " / " + totalPuzzles;
    }
}
