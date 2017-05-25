using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {

    public float MovementMultiplier = 5f;
	public static GameObject Player;
    public Transform Aim;

    Rigidbody2D rb;
    Animator animator;
    float Horizontal = 0f;
    float Vertical = 0f;
    Vector2 newVelocity;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		Player = gameObject;
	}

    private void Update()
    {
        Animate();
    }

    void FixedUpdate ()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        newVelocity = new Vector2(Horizontal, Vertical).normalized;
        newVelocity *= MovementMultiplier;

        if (!(Horizontal == 0 && Vertical == 0))
        {
            rb.velocity = newVelocity; 
        }
	}

    void Animate()
    {
        float angle = Aim.transform.rotation.eulerAngles.z;
        animator.SetFloat("Angle", angle);

        if(rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }
    }
}
