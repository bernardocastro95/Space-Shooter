using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Sprite[] _livesDisplay;
    [SerializeField]
    private Text _gameOverText;
    // Start is called before the first frame update
    void Start()
    {
       // _livesDisplay[3];
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }
    public void CurrentLive(int currentLives)
    {
        _liveImage.sprite = _livesDisplay[currentLives];
    }
    public void DisplayGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }
}
