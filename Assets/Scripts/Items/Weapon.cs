using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody projectile;            //used to create projectile.
    public int ammo;                        //used to keep track of ammo
    public Transform projectileSpawnPoint;  //position to spawn bullets
    public float projectileForce;           //used to apply force to the bullet being fired.

    // Start is called before the first frame update
    void Start()
    {
        if (ammo <= 0)
            ammo = 20;

        if (projectileForce <= 0)
            projectileForce = 3.0f;
    }   

    // Update is called once per frame
    public int Shoot()
    {
        if (projectile && ammo > 0)
        {
            Rigidbody temp = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            temp.AddForce(transform.forward * projectileForce, ForceMode.Impulse);

            Destroy(temp.gameObject, 2.0f);

            ammo--;
        }
        else
        {
            //reload animation, and add extra bullets to the ammo
            Debug.Log("Reload");
        }    
        return ammo;
    }
}
