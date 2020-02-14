using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunGameStudio.Tools;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> objectToSpawn = new List<GameObject>();
    [SerializeField] float spawnFrequencyBetweenEachEntity = 0.2f;

    [SerializeField] GameObject gameObjectToSpawn = null;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnObject()
    {
        for (int i = 0; i < objectToSpawn.Count; i++)
        {
            Debug.Log("Spawn Entities");
            gameObjectToSpawn = Instantiate(objectToSpawn[i]);
            yield return new WaitForSeconds(spawnFrequencyBetweenEachEntity);
        }
    }
}
