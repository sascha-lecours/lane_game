using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeScript : MonoBehaviour
{
    public float shakeMultiplier = 1f; // for quick fine-tuning
    public float shakeDecayRate = 10f;

    private Vector3 originalPosition = new Vector3();
    private float shakeMagnitude = 0f;
    private float shakeMin = 0.001f;

    public void addShake(float shake)
    {
        shakeMagnitude += shake;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    void moveCamera()
    {
        float x = originalPosition.x + Random.Range(-1f, 1f) * shakeMagnitude;
        float y = originalPosition.y + Random.Range(-1f, 1f) * shakeMagnitude;
        float z = originalPosition.z;
        transform.position = new Vector3(x, y, z);
    }

    void reduceShake()
    {
        shakeMagnitude = Mathf.Lerp(shakeMagnitude, 0, shakeDecayRate * Time.deltaTime);
        if (shakeMagnitude < shakeMin) shakeMagnitude = 0;
    }

    // Update is called once per frame
    void Update()
    {

        moveCamera();
        reduceShake();

        if (Input.GetKeyDown("q")) // For debugging
        {
            addShake(0.5f);
        }
    }
}
