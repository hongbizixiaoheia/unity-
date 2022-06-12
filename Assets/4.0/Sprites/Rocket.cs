using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            float rotation = Random.Range(0, 360);
            Instantiate(explosion, transform.position,
                        Quaternion.Euler(0, 0, rotation)); //ÊµÀý»¯±¬Õ¨
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hurt();
        }

        if (collision.tag == "BombPickup")
        {
            // ... find the Bomb script and call the Explode function.
            collision.gameObject.GetComponent<Bomb>().Explode();

            // Destroy the bomb crate.
            Destroy(collision.transform.root.gameObject);

            // Destroy the rocket.
            Destroy(gameObject);
        }
    }
}
