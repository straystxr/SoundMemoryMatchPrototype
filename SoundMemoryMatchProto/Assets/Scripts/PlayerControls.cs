using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Vector2 mousePosition;
    //list of array with music notes
    string[] notes = { "A", "B", "C", "D", "E", "F", "G" };
    private int correctNote;

    //score
    private int score = 0;

    private void Start()
    {
        correctNote = Random.Range(0, 2);// notes.Length);
        Debug.Log($"Correct note is {notes[correctNote]}");
    }

    private void OnPoint(InputValue inputValue)
    {
        mousePosition = inputValue.Get<Vector2>();
    }

    private void OnClick(InputValue inputValue)
    {
        // convert screen position to world position when we click
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        // check if we actually clicked on something
        Collider2D clickedNote = Physics2D.OverlapPoint(worldPos);
        if (clickedNote == null)
        {
            return; // stops the function from continuing on
        }

        //seeing that the clicked notes adds score
        string note = notes[correctNote];
        if (clickedNote.CompareTag(note))
        {
            Debug.Log("Correct note!");
        }

    }
}
