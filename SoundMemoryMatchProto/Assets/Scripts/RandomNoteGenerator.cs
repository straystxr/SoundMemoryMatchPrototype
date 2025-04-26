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
    public GameObject[] notesPrefabs;

    //transform variable to get position of the actual spawn point which in this case is the spawning of note variable
    public Transform spawnPoint;

    //variables for timer 
    [SerializeField] TextMeshProUGUI timerText;
    float timeLeft = 90f;
    bool timerStatus = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawnNotes());
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStatus)
        {
            timeLeft -= Time.deltaTime;
            //will not allow the timer to go below 0 w the mathf.max
            timeLeft = Mathf.Max(0f, timeLeft);
            timerText.text = timeLeft.ToString();
        }

    }

    IEnumerator spawnNotes()
    {
        while (timerStatus) {
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
            else
            {
                timerStatus = false;
                Debug.Log("Notes have stopped spawning");
            }
        }

    }
}
