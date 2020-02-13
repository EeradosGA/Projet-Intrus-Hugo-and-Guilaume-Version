using ProjectIntrus.Tools;
using ProjetIntrus.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private List<GameObject> playerList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public List<GameObject> getListPlayer()
    {
        return playerList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
