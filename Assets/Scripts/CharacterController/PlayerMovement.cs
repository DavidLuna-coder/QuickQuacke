using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float speed = 50f;
    public Animator animator;
    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //if (camera == null)camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
        animator.SetFloat("X", horizontalMove);
        animator.SetFloat("Y", verticalMove);
        
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false, 0f);
        controller.Move(0f, false, false, verticalMove * Time.fixedDeltaTime);

        if(camera != null)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, camera.position.z);
            camera.position = Vector3.Lerp(camera.position, targetPosition, Time.deltaTime * 8f);
        }
    }
}
