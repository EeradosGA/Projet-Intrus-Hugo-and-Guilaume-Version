using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("IMAGES")]
    [SerializeField] private UnityEngine.UI.Image _imageMainMenu;
    [SerializeField] private UnityEngine.UI.Image _imageChoicePlayer;

    [Header("BOUTONS")]
    [SerializeField] private Button _ButtonPlayer1;
    [SerializeField] private Button _ButtonPlayer2;
    [SerializeField] private Button _ButtonPlayer3;
    [SerializeField] private Button _ButtonPlayer4;

    [Header("MENU PLAYER")]
    [SerializeField] private GameObject _MenuPlayer1;
    [SerializeField] private GameObject _MenuPlayer2;
    [SerializeField] private GameObject _MenuPlayer3;
    [SerializeField] private GameObject _MenuPlayer4;

    [Header("BOUTON JOUER")]
    [SerializeField] private Button _ButtonPlay;


    public enum MainMenu_State
    {
        MAIN_MENU,
        CHOICE_PLAYER
    }
    private MainMenu_State _mainMenu_State = MainMenu_State.MAIN_MENU;

    private void Start()
    {
        if(_imageMainMenu == null)
        { Debug.LogError(this.name + " Start --> _imageMainMenu est null"); }
        if (_imageChoicePlayer == null)
        { Debug.LogError(this.name + " Start --> _imageChoicePlayer est null"); }


        //Gestion
        _imageMainMenu.gameObject.SetActive(true);
        _imageChoicePlayer.gameObject.SetActive(false);

        _MenuPlayer1.SetActive(false);
        _MenuPlayer2.SetActive(false);
        _MenuPlayer3.SetActive(false);
        _MenuPlayer4.SetActive(false);

        _ButtonPlay.gameObject.SetActive(false);

    }

    private void Update()
    {
        if(_mainMenu_State == MainMenu_State.CHOICE_PLAYER)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                this.TransitionToMainMenu();

            if(_MenuPlayer1.activeSelf
                && _MenuPlayer2.activeSelf
                && _MenuPlayer3.activeSelf
                && _MenuPlayer4.activeSelf)
            {
                _ButtonPlay.gameObject.SetActive(true);
            }
        }
    }

    public void TransitionToChoicePlayer()
    {
        _imageMainMenu.gameObject.SetActive(false);
        _imageChoicePlayer.gameObject.SetActive(true);

        _mainMenu_State = MainMenu_State.CHOICE_PLAYER;
    }

    public void TransitionToMainMenu()
    {
        _imageMainMenu.gameObject.SetActive(true);
        _imageChoicePlayer.gameObject.SetActive(false);

        _mainMenu_State = MainMenu_State.MAIN_MENU;
    }

    public void CallLoadSceneGame()
    {
        GameManager.getInstance.LoadScene(GameManager.SceneName.GAME);
    }

    public void CallApplicationQuit()
    {
        Application.Quit();
    }

    //pour l'instant des fonctions pour chaque bouton
    public void Player1_ActivateMenuPlayer()
    {
        _MenuPlayer1.SetActive(true);
    }

    public void Player2_ActivateMenuPlayer()
    {
        _MenuPlayer2.SetActive(true);
    }

    public void Player3_ActivateMenuPlayer()
    {
        _MenuPlayer3.SetActive(true);
    }

    public void Player4_ActivateMenuPlayer()
    {
        _MenuPlayer4.SetActive(true);
    }
}
