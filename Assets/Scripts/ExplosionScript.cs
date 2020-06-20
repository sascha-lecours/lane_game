using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float shakeMagnitude = 0.5f;
    public bool underwaterSound = false;
    public float explosionVolume = 1f;

    void Start()
    {
        // Limited time to live
        Destroy(gameObject, 3); // 3sec

        // Explosion Sound
        if (underwaterSound)
        {
            SoundEffectsHelper.Instance.MakeUnderwaterExplosionSound(explosionVolume);
        }
        else
        {
            SoundEffectsHelper.Instance.MakeExplosionSound(explosionVolume);
        }
        

        // Add screenshake
        Camera.main.GetComponent<CameraShakeScript>().addShake(shakeMagnitude); 
    }
}
