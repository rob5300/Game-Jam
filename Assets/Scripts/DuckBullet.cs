using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DuckBullet : MonoBehaviour
{

    public int Damage;
    private bool _collided;


    // Use this for initialization
    void Start()
    {
        _collided = false;
    }

    void FixedUpdate()
    {
        if (_collided)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = transform.up * 10;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (!_collided) StartCoroutine(DuckExplode());
        if (collision.transform.tag != "Player") _collided = true;
    }

    private IEnumerator DuckExplode()
    {
        GetComponent<AudioSource>().Play();
        RaycastHit2D[] rays = Physics2D.CircleCastAll(transform.position, 1.5f, Vector2.zero);
        if (rays != null)
        {
            if (rays.Count() != 0)
            {
                foreach (RaycastHit2D ray in rays)
                {
                    if (ray.transform.tag == "Player")
                    {
                        Movement.Player.GetComponent<Player>().Damage(Damage * 3);
                    }
                    else if (ray.transform.tag == "Enemy")
                    {
                        ray.transform.GetComponent<BaseAIBehaviour>().Health -= (Damage * 3);
                    }
                }
            }
        }
        GetComponent<Animator>().SetTrigger("Explode");
        yield return new WaitForSeconds(3f); //change this for the animation
        Destroy(gameObject);
    }
}
