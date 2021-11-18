using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPond : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (rotationSpeed <= 0)
            rotationSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
