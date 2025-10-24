using System.Collections;
using UnityEngine;

// This script handles all the asteroid image and animations 

public class Asteroid : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    [SerializeField] private Sprite[] sprites;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private int lives;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        rb = GetComponent<Rigidbody2D>();
        float pushY = Random.Range(0, -1f);
        float pushX = Random.Range(1, -1f);
        rb.linearVelocity = new Vector2(pushX, pushY);

        
        float asteroidScale = Random.Range(0.3f, 1.6f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.transform.localScale = new Vector2(asteroidScale, asteroidScale);

    }

    
    private void Update()
    {
        float moveY = (GameManager.Instance.worldSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);

        if (transform.position.y < -11)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        spriteRenderer.material = whiteMaterial;
        StartCoroutine("ResetMaterial");
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
        lives -= damage;
        if (lives <= 0)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            Destroy(gameObject);
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }


}
