using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPlayer : MonoBehaviour
{
    bool canSeePlayer = false;

    public float sightDistance = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 offsetOnY = transform.position;
        offsetOnY.y += 2f;
        if (Physics.Raycast(offsetOnY, Vector3.forward, out hit, sightDistance))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                gameObject.SetActive(false);
            }
        }

        Vector3 dir = transform.TransformDirection(Vector3.forward) * sightDistance;

        Debug.DrawRay(offsetOnY, dir, Color.red);

    }
}
