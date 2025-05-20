using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
public class PlayerControls : MonoBehaviour
{
    //Vector2 variable for mouse position
    private Vector2 mousePosition;

    //public statis int is used for the score so it remains the same without resetting for the score
    public static int score = 0;
    //Canvas variable to dynamically update the score
    [SerializeField] TextMeshProUGUI scoreText;

    //set as false so it can only be triggrred when Cooldown is needed
    private bool isCooldown = false; // Cooldown variable
    RandomNoteGenerator noteGenerator; // Reference to the note generator

    //Variables to change animation from idle to sound
    public Animator robotAnimator;

    private void Start()
    {
        score = 0;
        noteGenerator = FindAnyObjectByType<RandomNoteGenerator>(); // Get the note generator component
        if (noteGenerator == null)
        {
            Debug.Log("RandomNoteGenerator not found!");
        }
    }

    private void OnPoint(InputValue inputValue)
    {
        //will get the cursor's position on point
        mousePosition = inputValue.Get<Vector2>();
    }

    private void OnClick(InputValue inputValue)
    {
        //on click
        if(isCooldown){
            return;
        }
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePosition);

        //check if something was clicked
        Collider2D clickedNote = Physics2D.OverlapPoint(worldPos);

        //condition to check if the clicked notes are null
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

        //condition that will spawn the bad particles if the isNote is not true
        if (isNote == false)
        {
            noteGenerator.SpawnBadParticles(clickedNote.transform.position);
            return;
        }

        Debug.Log("Correct note!");
        //adding score with every correct guess
        score += 10;
        //turning the score to String to update the score on the canvas
        scoreText.text = score.ToString();

        Debug.Log("Score: " + score);
        
        //the positive feedback particles will spawn if correct
        noteGenerator.SpawnGoodParticles(clickedNote.transform.position);
        robotAnimator.SetTrigger("IsPlaying");
        StartCoroutine(CooldownCouritine(clickedNote));
    }

    //cooldown coroutine that activates with every correct match to respawn new cards on click
    private IEnumerator CooldownCouritine(Collider2D clickedNote){
        isCooldown = true;
        yield return new WaitForSeconds(0.5f);
        robotAnimator.SetTrigger("IsNotPlaying");
        Destroy(clickedNote.gameObject);
        noteGenerator.SpawnNotes();
        isCooldown = false;

    }

    //function to call the score to the RandomNoteGenerator script
    public int GetScore()
    {
        Debug.Log("Returning Score" + score);
        return score;
    }
}