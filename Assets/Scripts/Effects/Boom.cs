using UnityEngine;

// A script for the explosion animation of the asteroids
// Made sure this explosion animation would not be static and move relative to the vertical movement on screen

public class Boom : MonoBehaviour
{
    private Animator animator;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
      
    }

    private void Update()
    {
        float moveY = (GameManager.Instance.worldSpeed * PlayerController.Instance.boost) * Time.deltaTime;
        transform.position += new Vector3(0, -moveY);
    }

}
