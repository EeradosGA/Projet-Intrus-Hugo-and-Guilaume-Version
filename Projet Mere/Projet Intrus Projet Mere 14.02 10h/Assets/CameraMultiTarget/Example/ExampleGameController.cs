using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExampleGameController : MonoBehaviour
{
    public CameraMultiTarget cameraMultiTarget;

    public GameObject[] players;
    private IEnumerator Start() {
        var numberOfTargets = 4;
        var targets = new List<GameObject>(numberOfTargets);
        targets.Add(players[0]);
        targets.Add(players[1]);
        targets.Add(players[2]);
        targets.Add(players[3]);
        cameraMultiTarget.SetTargets(targets.ToArray());
        foreach (var _ in Enumerable.Range(0, numberOfTargets - targets.Count)) {
            yield return new WaitForSeconds(5.0f);
            cameraMultiTarget.SetTargets(targets.ToArray());
        }
        yield return null;
    }
}