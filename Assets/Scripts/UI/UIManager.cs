using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bestText;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Image _liveImage2;
    [SerializeField]
    private Sprite[] _livesDisplay;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Button _mainMenu;
    [SerializeField]
    private Button _resume;
    public int score, bestScore;


    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        _bestText.text = "Best: " + bestScore;

        if(_gm == null)
        {
            Debug.LogError("Game Manager NULL");
        }
        _mainMenu.onClick.AddListener(MainMenu);
        _resume.onClick.AddListener(ResumeGame);
    }

    public void UpdateScore()
    {
        score += 10;
        _scoreText.text = "Score: " + score.ToString();
    }
    public void BestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            _bestText.text = "Best: " + bestScore.ToString();
        }
    }
    public void CurrentLive(int currentLives)
    {
        _liveImage.sprite = _livesDisplay[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        } 
    }

    public void GameOverSequence()
    {
        _gm.GameIsOver();
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _restartText.gameObject.SetActive(true);
    }
    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
    }
    public void ResumeGame()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
