using UnityEngine;


// A script that controllers the Boss and its visuals
// Used Lean Tween along with the animation tool to create unique effects with the boss character

public class Boss1 : MonoBehaviour
{

    [SerializeField] private float duration;
    [SerializeField] LeanTweenType loopType;
    [SerializeField] private float alphaDuration;
    [SerializeField] LeanTweenType alphaLoopType;


    private Animator animator;
    private float speedX;
    private float speedY;
    private bool charging;

    private float switchInterval;
    private float switchTimer;

    private int lives;
    
    private void Start()
    {
        lives = 100;
        animator = GetComponent<Animator>();
        EnterChargeState();
    }

    
    private void Update()
    {
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (charging)
            {
                
                EnterPatrolState();
                
            }
            else
            {
                EnterChargeState();
            }
        }

        if (transform.position.x > 3 || transform.position.x < -3)
        {
            speedX *= -1;
        }

        float moveY =  speedY * PlayerController.Instance.boost * Time.deltaTime;
        float moveX = speedX * Time.deltaTime;

        transform.position += new Vector3(moveX, moveY);

        if (transform.position.y < -11)
        {
            Destroy(gameObject);
        }
    }

    private void EnterPatrolState()
    {
        LeanTween.reset();
        LeanTween.scale(gameObject, new Vector3(1.1f, 1.1f), duration).setLoopType(loopType);
        LeanTween.alpha(gameObject, 1.5f, alphaDuration).setLoopType(alphaLoopType);
        speedY = 0;
        speedX = Random.Range(2f, -2f);
        switchInterval = Random.Range(3f, 8f);
        switchTimer = switchInterval;
        charging = false;
        animator.SetBool("charging", false);
    }

    private void EnterChargeState()
    {
        LeanTween.reset();
        LeanTween.alpha(gameObject, 1f, alphaDuration);
        speedY = -3f;
        speedX = 0;
        switchInterval = Random.Range(0.9f, 2f);
        switchTimer = switchInterval;
        charging = true;
        animator.SetBool("charging", true);
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.bossCharge);
    }

    public void TakeDamage(int damage)
    {
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitArmor);
        lives -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(0);
        }
    }

}
