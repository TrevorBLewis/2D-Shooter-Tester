using UnityEngine;

// Created a script for how the players shot disappears on impact against certain enemies and objects

public class PhaserBullet : MonoBehaviour
{
    
    private void Update()
    {
        transform.position += new Vector3(0f, PhaserWeapon.Instance.speed * Time.deltaTime);

        if (transform.position.y > 7)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Critter") || collision.gameObject.CompareTag("Boss"))
        {
            gameObject.SetActive(false);
        }
    }


}
