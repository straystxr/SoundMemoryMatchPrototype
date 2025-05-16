using UnityEngine;

public class NotePlayer : MonoBehaviour
{
    public AudioClip noteClip; // The correct note's sound to play
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (noteClip != null)
        {
            audioSource.PlayOneShot(noteClip);
        }
    }
}