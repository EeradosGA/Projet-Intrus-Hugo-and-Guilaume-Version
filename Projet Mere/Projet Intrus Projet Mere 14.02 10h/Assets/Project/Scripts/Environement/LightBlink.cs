using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    [SerializeField]
    private float totalSeconds = 2.0f;// The total of seconds the flash wil last
    [SerializeField]
    private float maxIntensity = 1.0f;     // The maximum intensity the flash will reach
    [SerializeField]
    private Light myLight;        // Your light

    private void Start()
    {
        myLight = this.GetComponent<Light>();
        StartCoroutine("flashNow");
    }

    public IEnumerator flashNow()
    {
        totalSeconds = Random.Range(0.03f, 0.1f);
        float waitTime = totalSeconds / 2;
        // Get half of the seconds (One half to get brighter and one to get darker)
        while (myLight.intensity < maxIntensity)
        {
            myLight.intensity += Time.deltaTime / waitTime;        // Increase intensity
            yield return null;
        }
        while (myLight.intensity > 0)
        {
            myLight.intensity -= Time.deltaTime / waitTime;        //Decrease intensity
            yield return null;
        }
        yield return null;
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        StartCoroutine("flashNow");
    }
}
