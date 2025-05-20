using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class AmbientParticles : MonoBehaviour
{
    //variables for prefab and for spawn points
    public GameObject FlashinglightPrefab;
    public List<Transform> lightsSpawnPoints;

    //variables to create a delay between one light to another
    public float minimumDelay = 0.75f;
    public float maximumDelay = 3f;
    //variable of lights duration
    public float lightDuration = 0.6f;

    //to use the alpha of the light to make it more smoothly
    public float lightIntensity = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(flashCoroutine());
    }

    //coroutine used to randomly spawn and instantiate the lights between a set of different spawn points
    IEnumerator flashCoroutine()
    {
        //while loop is used so that the lights will keep flashing while player is on main menu screen
        while (true)
        {
            //creating a float where we will store the delay of each light spawning
            ////in by randomly generating between 2 variables
            float delay = Random.Range(minimumDelay, maximumDelay);
            yield return new WaitForSeconds(delay);

            //randomly spawning in 1 - 3 lights to spawn in
            int numberOfLights = Random.Range(1, 3);

            //tracking instantiated lights
            List<GameObject> lightList = new List<GameObject>();


            //for loop to spawn in multiple at a time
            for(int i = 0; i < numberOfLights; i++)
            {
                //creating another spawnPoint variable where the light will actually spawn in using the same logic as before
                Transform spawnPoint = lightsSpawnPoints[Random.Range(0, lightsSpawnPoints.Count)];
                GameObject lightInstance = Instantiate(FlashinglightPrefab, spawnPoint.position, Quaternion.identity);
                lightList.Add(lightInstance);

                Light2D light2D = lightInstance.GetComponent<Light2D>();
                if (light2D != null)
                {
                    light2D.intensity = 0f;
                    //starting animation
                    StartCoroutine(fadingLight(light2D, lightIntensity, lightDuration));
                }

                //using another coroutine to smoothen the animation of the lights
                yield return StartCoroutine(fadingLight(light2D, lightIntensity, lightDuration));

                //another loop to destroying all the lights respectivly
                foreach (GameObject lightObject in lightList)
                {
                    if (lightObject != null)
                    //destroying each light prefab after duration is finished
                    Destroy(lightInstance);

                }
            }
        }
    }

    IEnumerator fadingLight(Light2D light, float targetIntensity, float duration)
    {
        //variable that is half of the light's duration to ensure the time of animation doesnt take longer than duration
        float halfDuration = duration / 2;

        //animation fade in
        for(float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            light.intensity = Mathf.Lerp(0f, targetIntensity, t / halfDuration);
            yield return null;
        }
        //setting the intensity of the light to its max
        light.intensity = targetIntensity;

        //animation fade out
        for (float t = 0; t < halfDuration; t += Time.deltaTime)
        {
            light.intensity = Mathf.Lerp(targetIntensity, 0f, t / halfDuration);
            yield return null;
        }
        //removing the intensity slowly over time 
        light.intensity = 0f;
    }
}
