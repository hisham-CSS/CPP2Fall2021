using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    Character player;

    [Header("Weapon Settings")]
    [Space(10)]
    public float projectileForce;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Character>();
        if (!player)
            Debug.LogError("Parent character script component was not set. Parent gameobject is:" + transform.parent.name);

        if (projectileForce <= 0)
        {
            projectileForce = 10.0f;

            Debug.Log("ProjectileForce not set on " + name + " defaulting to " + projectileForce);
        }

        if (!projectilePrefab)
            Debug.LogWarning("Missing projectilePrefab on " + name);

        if (!projectileSpawnPoint)
            Debug.LogWarning("Missing projectileSpawnPoint on " + name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        if (projectilePrefab && projectileSpawnPoint)
        {
            Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            temp.AddForce(-projectileSpawnPoint.forward * projectileForce, ForceMode.Impulse);

            Destroy(temp.gameObject, 2.0f);

        }
    }

    public void End()
    {
        player.canMove = true;
    }
}
