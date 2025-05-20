using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class RandomNoteGenerator : MonoBehaviour
{

    public GameObject NegativeFeedback;
    public GameObject PositiveFeedback;

    public void SpawnBadParticles(Vector3 position)
    {
        Instantiate(NegativeFeedback, position, Quaternion.identity);
    }

    public void SpawnGoodParticles(Vector3 position)
    {
        Instantiate(PositiveFeedback, position, Quaternion.identity);
    }

    // List of array with music notes
    public string[] notes = { "A", "B", "C", "D", "E", "F", "G" };

    // Creating a list of prefabs which can have a game object within it
    public GameObject[] notesPrefabs;
    private GameObject[] spawnedNotes; // Tracks the spawned notes

    //gameobject variable to spawn the blank ccard
    public GameObject unknownCard;
    //array of audioclips for the blank card
    public AudioClip[] noteClips;

    // Transform variable to get position of the actual spawn point which in this case is the spawning of note variable
    public Transform[] spawnPoints; // Spawns multiple locations
    private int correctNoteIndex; // Variable to be assigned to correct notes

    // Variables for timer
    [SerializeField] TextMeshProUGUI timerText;
    float timeLeft = 90f;
    bool timerStatus = true;

    //variable for rounds
    private int cnt = 0;

    //variables for correct note spawn point and of prefab
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
        //Timer to reduce over time every second
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
            //if timer ends the GameOverScreen will start
            sceneLoadingStarted = true;
            Debug.Log("Time is over — loading scene...");
            StartCoroutine(LoadGameOverSceneAfterDelay(1f));
        }
    }

    //function to spawn the assets into the scene using a random generator, rounds to add more cards with every couple of rounds
    public void SpawnNotes()
    {
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
        if (correctNoteInstance != null)
        {
            Destroy(correctNoteInstance);
        }

        //round variable will be add by 1 with every correct guess to track at what round the card will no longer be visible
        cnt++;
        Debug.Log("Spawn notes has been called " + cnt);

        //clamping
        int totalNotes = Mathf.Min(3 + (cnt / 5), spawnPoints.Length); // Clamp between 3–7
        spawnedNotes = new GameObject[totalNotes];


        //setting the correct note index
        correctNoteIndex = Random.Range(0, notes.Length);
        int correctSpawnPoint = Random.Range(0, totalNotes); // Choose where to place the correct note

        // Instantiate correct note
        spawnedNotes[correctSpawnPoint] = Instantiate(notesPrefabs[correctNoteIndex], spawnPoints[correctSpawnPoint].position, Quaternion.identity);

        //removed cause of overriding
        //spawnedNotes = new GameObject[spawnPoints.Length];

        if (correctNoteInstance != null)
        {
            Destroy(correctNoteInstance);
        }
        //need to set a condition so that after 7 rounds the card asset for the correct note will be the blank card
        if (cnt >= 7)
        {
            //This spawns the correct note on the bottom of the notes and makes it clickable only for the sound, it's important that this is called after the correctNoteIndex is initalised
            correctNoteInstance = Instantiate(unknownCard, correctNoteSpawnPoint.position, Quaternion.identity);
            NotePlayer notePlayer = correctNoteInstance.AddComponent<NotePlayer>();
            notePlayer.noteClip = noteClips[correctNoteIndex];
        }
        else
        {
            //If it is less than 7 rounds using the cnt variable the cards will spawn showing the matching sound of the correct card
            correctNoteInstance = Instantiate(notesPrefabs[correctNoteIndex], correctNoteSpawnPoint.position, Quaternion.identity);
            correctNoteInstance.AddComponent<NotePlayer>();
        }

        // Filling in random notes in the rest of the spawn points
        List<GameObject> avilableNotes = new List<GameObject>(notesPrefabs); // copy all the notes in a modifiable list
        avilableNotes.RemoveAt(correctNoteIndex); // remove the correct note so there are no doubles

        //loop that will spawn all the other random notes that aren't correct to fill in the other spawn points
        for (int i = 0; i < totalNotes; i++)
        {
            if (i == correctSpawnPoint)
                continue;

            int randomIndex = Random.Range(0, avilableNotes.Count);
            spawnedNotes[i] = Instantiate(avilableNotes[randomIndex], spawnPoints[i].position, Quaternion.identity);
            avilableNotes.RemoveAt(randomIndex);
        }

        // Debugging to check which note is correct and where it is spawned
        Debug.Log($"Correct note is {notes[correctNoteIndex]} at spawn point {correctSpawnPoint + 1}");
    }

    //coroutine to delay the game over screen by only a few seconds
    IEnumerator LoadGameOverSceneAfterDelay(float delay)
    {
        //delay of 3 seconds to avoid the score not being updated as it is a constant issue
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOverScene");
    }

    //int index is a parameter to call the actual index of the notes
    public string GetNote(int index)
    {
        return notes[index];
    }

    //Function to call the correct note index
    public int GetCorrectNoteIndex()
    {
        return correctNoteIndex;
    }
}