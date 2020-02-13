using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunGameStudio.Tools;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> objectToSpawn;
    [SerializeField] float spawnFrequencyBetweenEachEntity = 0.2f;

    GameObject gameObjectToSpawn = null;
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
            gameObjectToSpawn = Instantiate(objectToSpawn[i], this.transform.position, this.transform.rotation, this.transform);
            yield return new WaitForSeconds(spawnFrequencyBetweenEachEntity);
        }
    }
}
