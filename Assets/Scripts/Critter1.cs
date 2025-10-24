using UnityEngine;


// This script programs the little critters that get destoyed when in contact with the player
// There are different animations based on how the critters are destroyed


public class Critter1 : MonoBehaviour
{
    
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] sprites;

    private float moveSpeed;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float moveTimer;
    private float moveInterval;
    [SerializeField] private GameObject zappedEffect;
    [SerializeField] private GameObject burnEffect;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)];
        moveSpeed = Random.Range(0.5f, 3f);
        GenerateRandomPosition();
        moveInterval = Random.Range(0.5f, 3f);
        moveTimer = moveInterval;
    }

    
    public void Update()
    {
        
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
        }
        else
        {
            GenerateRandomPosition();
            moveInterval = Random.Range(0.1f, 2f);
            moveTimer = moveInterval;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 relativePos = targetPosition - transform.position;
        if (relativePos != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 * Time.deltaTime);
        }

        float moveY = (GameManager.Instance.worldSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);

        if (transform.position.y < -11)
        {
            Destroy(gameObject);
        }
    }


    private void GenerateRandomPosition()
    {
        float randomX = Random.Range(5f, -5f);
        float randomY = Random.Range(5f, -5f);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Instantiate(zappedEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.zapped);
            Destroy(gameObject);
            GameManager.Instance.critterCounter++;

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(burnEffect, transform.position, transform.rotation);
            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.burned);
            Destroy(gameObject);
            GameManager.Instance.critterCounter++;
        }
        
    }
    // May use for future. Make only random critters move in a zigzag motion.
    // This code makes them all zigzag

    /*public void zigZagMove()
    {
        float sine = Mathf.Sin(transform.position.y);
        transform.position = new Vector3(sine, transform.position.y);
    }*/

}
