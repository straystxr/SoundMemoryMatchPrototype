using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerControls : MonoBehaviour
{
    private Vector2 mousePosition;
    private int score = 0;
    private bool isCooldown = false; // Cooldown variable

    RandomNoteGenerator noteGenerator; // Reference to the note generator

    private void Start()
    {
        noteGenerator = FindObjectOfType<RandomNoteGenerator>(); // Get the note generator component
        if (noteGenerator == null)
        {
            Debug.LogError("RandomNoteGenerator not found!");
        }
    }

    private void OnPoint(InputValue inputValue)
    {
        mousePosition = inputValue.Get<Vector2>();
    }

    private void OnClick(InputValue inputValue)
    {
        if(isCooldown){
            return;
        }
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        //check if something was clicked
        Collider2D clickedNote = Physics2D.OverlapPoint(worldPos);

        if (clickedNote == null)
        {
            return; //STOOOOOOOOOOOOOOOPS the function
        }

        //If the clicked note add it to the score and then go to the spawn notes method in the random note generator scripts
        if (noteGenerator != null && clickedNote.CompareTag(noteGenerator.GetNote(noteGenerator.GetCorrectNoteIndex())))
        {
            Debug.Log("Correct note!");
            score += 10;
            Debug.Log("Score: " + score);
            Destroy(clickedNote.gameObject);

            StartCoroutine(CooldownCouritine());
            noteGenerator.SpawnNotes();
        }
    }

    private IEnumerator CooldownCouritine(){
        isCooldown = true;
        yield return new WaitForSeconds(0.5f);
        isCooldown = false;

    }
}