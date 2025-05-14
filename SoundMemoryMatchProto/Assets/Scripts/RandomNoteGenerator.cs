using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

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

    //bool variable to prevent scene from loading in the Update()
    private bool sceneLoadingStarted = false; 
    void Start()
    {
        SpawnNotes();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStatus)
        {
            timeLeft -= Time.deltaTime;
            timeLeft = Mathf.Max(0f, timeLeft);
            timerText.text = timeLeft.ToString("F0");

            if (timeLeft <= 0f)
            {
                timerStatus = false;
            }
        }
        else if (!sceneLoadingStarted)
        {
            sceneLoadingStarted = true;
            Debug.Log("Time is over — loading scene...");
            StartCoroutine(LoadGameOverSceneAfterDelay(1f));
        }
    }

    public void SpawnNotes()
    {
        //count variable adds by 1
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
        correctNoteIndex = Random.Range(0, notes.Length);

        //add 2 variables a round variables and a list length variable
        // Spawning the actual correct note to match with
        int correctSpawnPoint = Random.Range(0, 3);
        spawnedNotes[correctSpawnPoint] = Instantiate(notesPrefabs[correctNoteIndex], spawnPoints[correctSpawnPoint].position, Quaternion.identity);

        //This spawns the correct note on the bottom of the notes and makes it unclickable, it's important that this is called after the correctNoteIndex is initalised
        correctNoteInstance = Instantiate(notesPrefabs[correctNoteIndex], correctNoteSpawnPoint.position, Quaternion.identity);
        correctNoteInstance.AddComponent<NotePlayer>();

        // correctNoteInstance.GetComponent<Collider2D>().enabled = false; // Make it unclickable

        // Filling in random notes in the rest of the spawn points

        //will find a minimum between 3 and 7
        int totalNotes = Mathf.Min(3 + (cnt / 5), 7);
        for (int i = 0; i < totalNotes; i++)
        {
            //need to set a condition so that after 10 rounds the card asset for the correct note will be the blank card
            if (i == correctSpawnPoint)
                continue;

            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, notes.Length);
            } while (randomIndex == correctNoteIndex);

            spawnedNotes[i] = Instantiate(notesPrefabs[randomIndex], spawnPoints[i].position, Quaternion.identity);
        }

        // Debugging to check which note is correct and where it is spawned
        Debug.Log($"Correct note is {notes[correctNoteIndex]} at spawn point {correctSpawnPoint + 1}");
    }

    /*
    public void clearNotes()
    {
        // Destroy all spawned notes
        if (spawnedNotes != null)
        {
            //loop to check that each spawn point gets destroyed 
            foreach (GameObject note in spawnedNotes)
            {
                if (note != null)
                {
                    Destroy(note);
                }
            }
        }

        // Destroy the correct note instance
        if (correctNoteInstance != null)
        {
            Destroy(correctNoteInstance);
        }
    } */

    IEnumerator LoadGameOverSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        //finding the playerControls script to get access to the score
        PlayerControls playerControls = FindAnyObjectByType<PlayerControls>();
        if (playerControls != null)
        {
            int finalScore = playerControls.GetScore();
            PlayerPrefs.SetInt("FinalScore", finalScore);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Player Controls not found!");
        }
        SceneManager.LoadScene("GameOverScene");
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