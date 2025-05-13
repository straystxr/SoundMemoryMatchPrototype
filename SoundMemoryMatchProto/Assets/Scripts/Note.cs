using UnityEngine;

public class Note : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnMouseDown()
    {
        // good to check the object, but doesn't link very well to other scripts.
        Debug.Log(tag);
        Debug.Log(audioSource);
        audioSource.Play();
    }
}
