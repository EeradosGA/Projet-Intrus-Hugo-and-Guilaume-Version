using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public enum SceneName 
    {
        MENU,
        GAME,
    }

    private SceneName _sceneName = SceneName.MENU;

    private const string _sceneGame = "Game";
    private const string _sceneMenu = "Menu";
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        //this.LoadScene(_sceneName);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
            this.LoadScene(SceneName.GAME);
        if (Input.GetKeyDown(KeyCode.M))
            this.LoadScene(SceneName.MENU);
    }

    public void LoadScene(SceneName pSceneName)
    {
        switch (pSceneName)
        {
            case SceneName.MENU:
                Debug.Log(this.name + "LoadScene --> SceneMenu");
                SceneManager.LoadScene(_sceneMenu);
                break;
            case SceneName.GAME:
                Debug.Log(this.name + "LoadScene --> SceneGame");
                SceneManager.LoadScene(_sceneGame);
                break;
            default:
                break;
        }
    }
}
