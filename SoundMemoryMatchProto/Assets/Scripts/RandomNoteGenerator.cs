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
        spawnNotes();
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

    }
    void SpawnNotes()
    {
        spawnedNotes = new GameObject[spawnPoints.Length];

        //chooses the correct note randomly
        correctNoteIndex = UnityEngine.Random.Range(0, notesPrefabs.Length);

        int correctSpawnPoint = UnityEngine.Random.Range(0, spawnedNotes.Length);
        GameObject correctNote = Instantiate(notesPrefabs[correctNoteIndex], spawnPoint[correctSpawnPoint].position, Quaternion.identity)
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
