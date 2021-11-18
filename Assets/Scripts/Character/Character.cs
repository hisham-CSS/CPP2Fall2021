using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    CharacterController controller;
    float health = 100;

    public GameObject characterObject;
    Animator anim;



    [Header("Player Settings")]
    [Space(10)]
    [Tooltip("Speed value between 1 and 6")]
    [Range(1.0f, 6.0f)]
    public float speed = 6;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;


    enum ControllerType { SimpleMove, Move }
    [SerializeField] ControllerType type;

    Vector3 moveDirection;
    public bool canMove = true;

    public bool godModeActive = false;
    public float godModeTimer = 3.0f;

    public float jumpMultipler = 2.0f;
    public float jumpModeTimer = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();

            controller.minMoveDistance = 0.0f;

            if (speed <= 0)
            {
                speed = 6.0f;
                throw new UnassignedReferenceException("Speed not set on " + name + " defaulting to " + speed);
            }

            if (jumpSpeed <= 0)
            {
                jumpSpeed = 6.0f;

                Debug.Log("JumpSpeed not set on " + name + " defaulting to " + jumpSpeed);
            }

            if (gravity <= 0)
            {
                gravity = 9.81f;

                Debug.Log("Gravity not set on " + name + " defaulting to " + gravity);
            }

            moveDirection = Vector3.zero;


            if (!characterObject)
                Debug.LogWarning("Missing character object on " + name);
            else
            {
                anim = characterObject.GetComponent<Animator>();
                if (!anim)
                    Debug.LogWarning("Missing animator on " + characterObject.name);
            }


        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        finally
        {
            Debug.LogWarning("Always Get Called");
        }


    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case ControllerType.SimpleMove:
                controller.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

                break;
            case ControllerType.Move:

                if (controller.isGrounded && canMove)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDirection *= speed;

                    moveDirection = transform.TransformDirection(moveDirection);
                    if (Input.GetButtonDown("Jump"))
                        moveDirection.y = jumpSpeed;
                }

                moveDirection.y -= gravity * Time.deltaTime;
                controller.Move(moveDirection * Time.deltaTime);

                break;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
            canMove = false;
            moveDirection = Vector3.zero;
        }
        //Fire();

        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("Die");
            canMove = false;
            moveDirection = Vector3.zero;
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", transform.InverseTransformDirection(controller.velocity).z);
    }

    [ContextMenu("Reset Stats")]
    void ResetStats()
    {
        speed = 6.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndLevel")
            GameManager.Instance.GoToEndScene();

        if (other.gameObject.CompareTag("Pickup_GodMode"))
        {
            Destroy(other.gameObject);
            godModeActive = true;
            StartCoroutine(StopGodMode());
        }

        if (other.gameObject.CompareTag("Pickup_SuperJump"))
        {
            Destroy(other.gameObject);
            jumpSpeed *= jumpMultipler;
            StartCoroutine(StopJumpMode());
        }

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "EndLevel")
            GameManager.Instance.GoToEndScene();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HealingPond"))
        {
            //starting healing every tick
            Debug.Log("Staying in healing pond");
        }
    }


    public float Health
    {
        get { return health; }
        set
        {
            if (godModeActive)
                return;

            health = value;
            //if (health <= 0)
            //Call gameover here
        }
    }

    public void ChangeHealth(float value)
    {

        Health += value;

        Debug.Log("Health changed to " + Health);
    }

    IEnumerator StopGodMode()
    {
        yield return new WaitForSeconds(godModeTimer);

        godModeActive = false;
    }

    IEnumerator StopJumpMode()
    {
        yield return new WaitForSeconds(godModeTimer);

        jumpSpeed /= jumpMultipler;
    }

}
