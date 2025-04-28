using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RandomNoteGenerator : MonoBehaviour
{
    //empty game object will be needed in the unity editor to allow the note to generate
    public GameObject randomNoteGenerator;
    //list of array with music notes
    //string[] notes = { "A", "B", "C", "D", "E", "F", "G" };
    //creating a list of prefabs which can have a gameobject within it
    public GameObject[] notesPrefabs;
    private GameObject[] spawnedNotes; //tracks the spawned notes

    //transform variable to get position of the actual spawn point which in this case is the spawning of note variable
    public Transform spawnPoint;
    public Transform[] spawnPoints; //spawns multiple locations
    private int correctNoteIndex; // variable to be assigne to correct notes


    //variables for timer 
    [SerializeField] TextMeshProUGUI timerText;
    float timeLeft = 90f;
    bool timerStatus = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnNotes();
    }

    // Update is called once per frame
    void Update()
    {
        //while the timer is true the timer will go on until it reaches 0
        if(timerStatus)
        {
            timeLeft -= Time.deltaTime;
            //will not allow the timer to go below 0 w the mathf.max
            timeLeft = Mathf.Max(0f, timeLeft);
            timerText.text = timeLeft.ToString();
        }
        else
        {
            timerStatus = false;
            Debug.Log("Time is over");
            //a function is done to destroy the created notes
            //clearNotes();
        }

    }
    void SpawnNotes()
    {
        spawnedNotes = new GameObject[spawnPoints.Length];

        //chooses the correct note randomly
        correctNoteIndex = UnityEngine.Random.Range(0, notesPrefabs.Length);

        //spawning the actual correct note to match with
        int correctSpawnPoint = UnityEngine.Random.Range(0, spawnedNotes.Length);
        GameObject correctNote = Instantiate(notesPrefabs[correctNoteIndex], spawnPoints[correctSpawnPoint].position, Quaternion.identity);

        //filling in random notes in the rest of the spawn points
        for(int i = 0; i < spawnedNotes.Length; i++)
        {
            //this condition is set to not allow any repetition of the same note but how will it actually spawn the other note to match with??? confusion is real rn
            if (i == correctSpawnPoint) 
                continue;
            //add a loop for a counter to reach 7 different outputs
            //randomising the index for the rest of the cards
            int randomIndex = UnityEngine.Random.Range(0, notesPrefabs.Length);

        }
           /* //adding conditioner that if the timer is more than zero the notes will spawn
            if (timeLeft > 0)
            {
                //kept showing ambuguity error without unityengine
                int range = UnityEngine.Random.Range(0, notesPrefabs.Length);
                //gameobject for spawn point
                //spawning the actual notes
                GameObject spawningOfNote = Instantiate(notesPrefabs[range], spawnPoint.position, Quaternion.identity);
                Debug.Log(range);

                //destroying and then spawning a new note every second
                Destroy(spawningOfNote, 1f);
                yield return new WaitForSeconds(1f);
            }
            //else condition will turn the boolean variable into false and stopping the timer plus spawning of notes
            else
            {
                timerStatus = false;
                Debug.Log("Notes have stopped spawning");
            } */

    }
}
