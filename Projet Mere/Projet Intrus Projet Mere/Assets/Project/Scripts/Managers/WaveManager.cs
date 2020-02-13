using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunGameStudio.Tools;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Spawner> allWaves;
    [SerializeField] float timeBetweenWaves;

    Timer timer;
    int currentWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer(timeBetweenWaves);
        timer.StartTimer();
        StartCoroutine(allWaves[currentWave].SpawnObject());
        currentWave++;
    }

    // Update is called once per frame
    void Update()
    {
        if (allWaves.Count > currentWave)
        {
            if (timer.hasReachTime())
            {
                Debug.Log("Start Wave");
                StartCoroutine(allWaves[currentWave].SpawnObject());
                currentWave++;
            }
        }
    }
}
