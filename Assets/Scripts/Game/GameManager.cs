using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    public bool _isCoop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isCoop == false)
        {
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            {
                _isGameOver = false;
                SceneManager.LoadScene(1);
            }
        }
        else if (_isCoop == true)
        {
            if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            {
                _isGameOver = false;
                SceneManager.LoadScene(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameIsOver()
    {
        Debug.Log("GameManager::GameIsOver() called");
        _isGameOver = true;
    }
}
