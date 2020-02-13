using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectIntrus.Tools;

public class CheckDestinationDefault : MonoSingleton<CheckDestinationDefault>
{
    List<GameObject> listGameObject;

    private float maxDistance = -1;

    public float _maxDistance
    {
        get { return maxDistance; }
    }

    int indexMax1, indexMax2;

    private Vector3 pos;
    public Vector3 _pos
    {
        get { return pos; }
    }


    bool maxOk = false;

    [SerializeField]
    GameObject prefabCircle;

    GameObject circle;
    GameObject circle_2;

    private void Start()
    {
        listGameObject = new List<GameObject>();
        if(GameManager.Instance != null)
        {
            if (GameManager.Instance.getListPlayer() != null)
            {
                for (int i = 0; i < GameManager.Instance.getListPlayer().Count; i++)
                {
                    listGameObject.Add(GameManager.Instance.getListPlayer()[i]);
                }
            }
        }

        if(listGameObject.Count == 0)
        {
            Debug.Log("List 0");
        }
    }

    void Update()
    {
        updateMaxDistance();

        if (maxOk)
        {
            Vector3 direction = listGameObject[indexMax1].transform.position - listGameObject[indexMax2].transform.position;
            pos = new Vector3(0, 0, 0);

            pos = listGameObject[indexMax1].transform.position + -(direction / 2);

            if (circle == null)
            {
                circle = Instantiate(prefabCircle, pos, new Quaternion());
                circle_2 = Instantiate(prefabCircle, pos, new Quaternion());
            }

            circle.transform.position = pos;
            circle.transform.localScale = new Vector3(maxDistance, maxDistance, maxDistance);

            circle_2.transform.position = pos;
            circle_2.transform.localScale = new Vector3(maxDistance + 30f, maxDistance + 30f, maxDistance + 30f);

            Debug.DrawLine(listGameObject[indexMax1].transform.position, listGameObject[indexMax2].transform.position, Color.green);
        }
    }

    void updateMaxDistance()
    {
        float distance = 0;
        maxDistance = -1;

        for (int i = 0; i < listGameObject.Count; i++)
        {
            for (int j = 0; j < listGameObject.Count; j++)
            {
                distance = (listGameObject[i].transform.position - listGameObject[j].transform.position).magnitude;

                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    indexMax1 = i;
                    indexMax2 = j;
                }
            }
        }

        if (maxDistance != -1)
        {
            maxOk = true;
        }
        else
        {
            maxOk = false;
        }
    }

}
