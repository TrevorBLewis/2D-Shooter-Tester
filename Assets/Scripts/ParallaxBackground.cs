using UnityEngine;

// This script handles the move speed of the background elements helping with the parallax effect

public class ParallaxBackground : MonoBehaviour
{

    [SerializeField] private float moveSpeed;


    float backgroundImageHeight;

    private void Start()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        backgroundImageHeight = sprite.texture.height / sprite.pixelsPerUnit;

    }

    private void Update()
    {
        float moveY = (moveSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, moveY);
        if (Mathf.Abs(transform.position.y) - backgroundImageHeight > 0) {
            transform.position = new Vector3(transform.position.x, 0f);
        }
    }



}
