using UnityEngine;

// A script of the JellyFish creature the player wants to save

public class JellyFish : MonoBehaviour
{
    private void Update()
    {
        float moveY = (GameManager.Instance.worldSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);

        if (transform.position.y < -11)
        {
            Destroy(gameObject);
        }
    }
}
