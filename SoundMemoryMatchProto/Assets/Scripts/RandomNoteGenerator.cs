using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RandomNoteGenerator : MonoBehaviour
{
    // List of array with music notes
    public string[] notes = { "A", "B", "C", "D", "E", "F", "G" };

    // Creating a list of prefabs which can have a game object within it
    public GameObject[] notesPrefabs;
    private GameObject[] spawnedNotes; // Tracks the spawned notes

    public GameObject[] correctSpawnPoint;

    // Transform variable to get position of the actual spawn point which in this case is the spawning of note variable
    public Transform[] spawnPoints; // Spawns multiple locations
    private int correctNoteIndex; // Variable to be assigned to correct notes

    // Variables for timer
    [SerializeField] TextMeshProUGUI timerText;
    float timeLeft = 90f;
    bool timerStatus = true;

    private int cnt = 0;

    public Transform correctNoteSpawnPoint;
    private GameObject correctNoteInstance;
    void Start()
    {
        SpawnNotes();
    }

    // Update is called once per frame
    void Update()
    {
        // While the timer is true the timer will go on until it reaches 0
        if (timerStatus)
        {
            timeLeft -= Time.deltaTime;
            // Will not allow the timer to go below 0 with Mathf.Max
            timeLeft = Mathf.Max(0f, timeLeft);
            timerText.text = timeLeft.ToString();
        }
        else
        {
            timerStatus = false;
            Debug.Log("Time is over");
            // A function is done to destroy the created notes
            // you should implement a function clearNotes();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; //if in scene view
            #else
                Application.Quit(); //if exe
            #endif
        }
    }

    public void SpawnNotes()
    {
        cnt ++;
        Debug.Log("Spawn notes has been called "+cnt);
        // Destroy old notes if they exist
        if (spawnedNotes != null)
        {
            foreach (GameObject note in spawnedNotes)
            {
                if (note != null)
                {
                    Destroy(note);
                }
            }
        }
        //destroys the correct note gameovject when its correct
        if (correctNoteInstance != null){
            Destroy(correctNoteInstance);
        }




        spawnedNotes = new GameObject[spawnPoints.Length];

        // Chooses the correct note randomly
        correctNoteIndex = UnityEngine.Random.Range(0, notes.Length);

        // Spawning the actual correct note to match with
        int correctSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        spawnedNotes[correctSpawnPoint] = Instantiate(notesPrefabs[correctNoteIndex], spawnPoints[correctSpawnPoint].position, Quaternion.identity);

        //This spawns the correct note on the bottom of the notes and makes it unclickable, it's important that this is called after the correctNoteIndex is initalised
        correctNoteInstance = Instantiate(notesPrefabs[correctNoteIndex], correctNoteSpawnPoint.position, Quaternion.identity);
        correctNoteInstance.GetComponent<Collider2D>().enabled = false; // Make it unclickable

        // Filling in random notes in the rest of the spawn points
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == correctSpawnPoint)
                continue;

            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, notes.Length);
            } while (randomIndex == correctNoteIndex);

            spawnedNotes[i] = Instantiate(notesPrefabs[randomIndex], spawnPoints[i].position, Quaternion.identity);
        }

        // Debugging to check which note is correct and where it is spawned
        Debug.Log($"Correct note is {notes[correctNoteIndex]} at spawn point {correctSpawnPoint + 1}");
    }

    //int index is a parameter to call the actual index of the notes
    public string GetNote(int index)
    {
        return notes[index];
    }

    public int GetCorrectNoteIndex()
    {
        return correctNoteIndex;
    }
}