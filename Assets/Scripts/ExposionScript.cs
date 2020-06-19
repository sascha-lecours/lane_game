using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposionScript : MonoBehaviour
{
    public float shakeMagnitude = 0.5f;

    void Start()
    {
        // Limited time to live
        Destroy(gameObject, 3); // 3sec

        // Explosion Sound
        SoundEffectsHelper.Instance.MakeExplosionSound();

        // Add screenshake
        Camera.main.GetComponent<CameraShakeScript>().addShake(shakeMagnitude); 
    }
}
