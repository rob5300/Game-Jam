using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : MonoBehaviour
{
    public Transform KillZone;
    public float MoveRate = 1f;

    void Update()
    {
        transform.Translate(new Vector2(0, MoveRate * Time.deltaTime));
        float result = Vector2.Distance(transform.position, Player.player.transform.position);
        if (result < 0)
        {
            result = -result;
        }
        if (result > 17.5f)
        {
            transform.position = new Vector2(transform.position.x, Player.player.transform.position.y - 17.5f);
        }
        transform.position = new Vector2(Player.player.transform.position.x, transform.position.y);

        if(Player.player.transform.position.y < KillZone.transform.position.y)
        {
            Player.player.Damage(99999);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.player.Damage(99999);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.player.Damage(99999);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
