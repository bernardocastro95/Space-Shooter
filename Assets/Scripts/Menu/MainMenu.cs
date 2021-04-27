using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _singleplayer, _coop;
   public void LoadGame()
    {
        SceneManager.LoadScene(1);
        _singleplayer.onClick.AddListener(AccessSinglePlayer);
        _coop.onClick.AddListener(AccessCoop);
    }
    public void AccessSinglePlayer()
    {
        SceneManager.LoadScene(1);
    }
    public void AccessCoop()
    {
        SceneManager.LoadScene(2);
    }
}
