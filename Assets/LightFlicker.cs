using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    private Light2D lightScript;
    public float flickerIntensity = 0.5f;

    private float defaultIntensity;

    private void Awake()
    {
        lightScript = GetComponent<Light2D>();
        defaultIntensity = lightScript.intensity;
        

    }

    private void Update()
    {
        lightScript.intensity = Random.Range(defaultIntensity - flickerIntensity, defaultIntensity + flickerIntensity);
    }

}
