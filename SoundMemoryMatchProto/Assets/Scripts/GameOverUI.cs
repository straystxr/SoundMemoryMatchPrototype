using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        //calling the score from the PlayerControls and carrying it over to the GameOverScene
        scoreText.text = PlayerControls.score.ToString();
    }
}