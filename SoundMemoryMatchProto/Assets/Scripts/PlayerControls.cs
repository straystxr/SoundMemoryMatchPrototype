using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
public class PlayerControls : MonoBehaviour
{
    private Vector2 mousePosition;
    public int score = 0;
    private bool isCooldown = false; // Cooldown variable

    RandomNoteGenerator noteGenerator; // Reference to the note generator
    [SerializeField] TextMeshProUGUI scoreText;

    private void Start()
    {
        noteGenerator = FindAnyObjectByType<RandomNoteGenerator>(); // Get the note generator component
        if (noteGenerator == null)
        {
            Debug.Log("RandomNoteGenerator not found!");
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

        if (clickedNote == null || clickedNote.GetComponent<NotePlayer>() != null)
        {
            return; //STOOOOOOOOOOOOOOOPS the function
        }

        if (noteGenerator == null)
        {
            return;
        }

        //If the clicked note add it to the score and then go to the spawn notes method in the random note generator scripts
        var isNote = clickedNote.CompareTag(noteGenerator.GetNote(noteGenerator.GetCorrectNoteIndex()));

        if (isNote == false)
        {
            noteGenerator.SpawnBadParticles(clickedNote.transform.position);
            return;
        }

        Debug.Log("Correct note!");
        score += 10;
        scoreText.text = score.ToString();

        Debug.Log("Score: " + score);
            
        noteGenerator.SpawnGoodParticles(clickedNote.transform.position);
        StartCoroutine(CooldownCouritine(clickedNote));
    }

    private IEnumerator CooldownCouritine(Collider2D clickedNote){
        isCooldown = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(clickedNote.gameObject);
        noteGenerator.SpawnNotes();
        isCooldown = false;

    }

    public int GetScore()
    {
        Debug.Log("Returning Score" + score);
        return score;
    }
}