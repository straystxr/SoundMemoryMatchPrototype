using System;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //kept showing ambuguity error without unityengine
        int range = UnityEngine.Random.Range(1, notesPrefabs.Length);
        //gameobject for spawn point
        GameObject spawningOfNote = notesPrefabs[range];
        Debug.Log(range);

        //spawning the actual notes
        Instantiate(spawningOfNote, spawnPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
