using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ProjectIntrus.Tools;
using XInputDotNetPure; // Required in C#



public class GameManager : MonoSingleton<GameManager>
{
    public struct joystickPlayer
    {
        public bool playerIndexSet;
        public PlayerIndex playerIndex;
        public GamePadState state;
        public GamePadState prevState;
        public bool canBeUse;
    }
    private joystickPlayer[] _joystickPlayerTab = new joystickPlayer[4];
    public joystickPlayer[] joystickPlayerTab { get { return _joystickPlayerTab; } }

    [SerializeField] private List<GameObject> playerList;

    private int _nbrJoysitckControllerConnect = 0;
    public int nbrJoysitckControllerConnect { get { return _nbrJoysitckControllerConnect; } }

    public enum SceneName 
    {
        MENU,
        GAME,
    }

    private SceneName _currentSceneName = SceneName.MENU;

    private const string _sceneGame = "Game";
    private const string _sceneMenu = "Menu";
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        //this.LoadScene(_sceneName);

        this.ManageJoystickConnect();
        Debug.Log(this.name + " Start --> joystick controller connect : " + _nbrJoysitckControllerConnect);

    }

    private void Update()
    {

        #region MANAGE JOYSTICK

        this.ManageJoystickConnect();
        Debug.Log(this.name + " Update --> joystick controller connect : " + _nbrJoysitckControllerConnect);
        this.ManageJoystickButton();

        #endregion MANAGE JOYSTICK


        //test
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
                _currentSceneName = SceneName.MENU;
                SceneManager.LoadScene(_sceneMenu);
                break;
            case SceneName.GAME:
                Debug.Log(this.name + "LoadScene --> SceneGame");
                _currentSceneName = SceneName.GAME;
                SceneManager.LoadScene(_sceneGame);
                break;
            default:
                break;
        }
    }

    #region MANAGE JOYSTICK

    private void ManageJoystickConnect()
    {
        _nbrJoysitckControllerConnect = 0;
        for (int i = 0; i < _joystickPlayerTab.Length; ++i)
        {
            if (!_joystickPlayerTab[i].playerIndexSet || !_joystickPlayerTab[i].prevState.IsConnected)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(this.name + " ManageJoystickConnect --> Joystick " + i + " IsConnected");
                    _joystickPlayerTab[i].playerIndex = testPlayerIndex;
                    _joystickPlayerTab[i].playerIndexSet = true;
                }
            }

            _joystickPlayerTab[i].prevState = _joystickPlayerTab[i].state;
            _joystickPlayerTab[i].state = GamePad.GetState(_joystickPlayerTab[i].playerIndex);


            if (_joystickPlayerTab[i].playerIndexSet && _joystickPlayerTab[i].prevState.IsConnected)
            {
                _joystickPlayerTab[i].canBeUse = true;
                _nbrJoysitckControllerConnect += 1;
            }

        }
    }

    /// <summary>
    /// example
    /// </summary>
    private void ManageJoystickButton()
    {
        //if (_currentSceneName == SceneName.MENU)
        //{
        for (int i = 0; i < _joystickPlayerTab.Length; ++i)
        {

            if (_joystickPlayerTab[i].canBeUse)
            {
                Debug.Log(this.name + " ManageJoystickButton --> Joystick " + i + " playerIndexSet the playerindexvalue is :  " + _joystickPlayerTab[i].playerIndex);

                //Detect if a button was pressed this frame
                //if (_joystickPlayerTab[i].prevState.Buttons.A == ButtonState.Released && _joystickPlayerTab[i].state.Buttons.A == ButtonState.Pressed)
                //{
                //    Debug.Log(this.name + " Update --> Player : " + i + "press button a");
                //}
            }
        }

    }



    #endregion MANAGE JOYSTICK

    public List<GameObject> getListPlayer()
    {
        return playerList;
    }

    public void SetIntrus()
    {
        int randID = Random.Range(0, playerList.Count);
        //playerList[randID].isIntrus = true;
        Debug.Log("Intrus set, id : " + randID + " but for now there isnt an effect on the player");
    }

    public joystickPlayer getJoystickPlayer1()
    {
        return _joystickPlayerTab[0];
    }

}
