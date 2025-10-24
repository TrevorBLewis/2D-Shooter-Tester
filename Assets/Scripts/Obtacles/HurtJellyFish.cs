using UnityEngine;
using UnityEngine.SceneManagement;

// A script of a hurt jelly fish object.
// If the player touches the hurt jelly fish, the scene ends and the level is complete.

public class HurtJellyFish : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level 1 Complete");
        }
    }

}
