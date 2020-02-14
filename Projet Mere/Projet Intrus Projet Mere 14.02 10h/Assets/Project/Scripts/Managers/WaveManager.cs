using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunGameStudio.Tools;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Spawner> Level_1_Waves;
    [SerializeField] List<Spawner> Level_2_Waves;
    [SerializeField] List<Spawner> Level_3_Waves;

    List<List<Spawner>> allWaves = new List<List<Spawner>>();

    [SerializeField] float timeBetweenWaves;

    Timer timer;
    int currentWave = 0;
    int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        allWaves.Add(Level_1_Waves);
        allWaves.Add(Level_2_Waves);
        allWaves.Add(Level_3_Waves);
        timer = new Timer(timeBetweenWaves);
        timer.StartTimer();
        StartCoroutine(Level_1_Waves[currentWave].SpawnObject());
        currentWave++;
    }

    // Update is called once per frame
    void Update()
    {
        if(allWaves.Count > currentLevel)
        {
            if (allWaves[currentLevel].Count > currentWave)
            {
                if (timer.hasReachTime())
                {
                    Debug.Log("Start Wave");
                    StartCoroutine(allWaves[currentLevel][currentWave].SpawnObject());
                    currentWave++;
                }
            }
            else
            {
                Debug.Log("Next level");
                currentLevel++;
                currentWave = 0;
            }
        }
    }
}
