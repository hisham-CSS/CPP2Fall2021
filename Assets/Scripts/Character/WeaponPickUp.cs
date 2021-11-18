using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickUp : MonoBehaviour
{
    public Weapon weapon;   //store the weapon that gets picked up in this variable. must have Weapon.CS attached because we need the weapon.cs in order to call shoot.

    public GameObject weaponAttach; //Used to place the weapon on the player.

    public float weaponDropForce;  //used to add force to the "weapon" when it gets dropped.

    public TMP_Text ammoText;   //used to show ammo left when a weapon is active.

    // Start is called before the first frame update
    void Start()
    {
        weapon = null;

        ammoText.text = string.Empty;

        if (!weaponAttach)
        {
            weaponAttach = GameObject.Find("WeaponPlacement");
        }

        if (weaponDropForce <= 0)
            weaponDropForce = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (weapon)
            {
                weaponAttach.transform.DetachChildren();

                StartCoroutine(EnableCollisions(1.0f));

                weapon.GetComponent<Rigidbody>().isKinematic = false;
                weapon.GetComponent<Rigidbody>().AddForce(weapon.transform.forward * weaponDropForce, ForceMode.Impulse);

                ammoText.text = string.Empty;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (weapon)
            {
                ammoText.text = weapon.Shoot().ToString();
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!weapon && hit.collider.CompareTag("Weapon"))
        {
            weapon = hit.gameObject.GetComponent<Weapon>();
            if (weapon)
            {
                weapon.GetComponent<Rigidbody>().isKinematic = true;
                weapon.transform.position = weaponAttach.transform.position;
                weapon.transform.SetParent(weaponAttach.transform);
                weapon.transform.localRotation = weaponAttach.transform.localRotation;
                Physics.IgnoreCollision(weapon.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
            }
        }
    }

    IEnumerator EnableCollisions(float timeToDisable)
    {
        yield return new WaitForSeconds(timeToDisable);
        Physics.IgnoreCollision(weapon.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), false);

        weapon = null;

    }
}
