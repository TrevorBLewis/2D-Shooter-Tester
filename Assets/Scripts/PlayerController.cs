using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

// A script with all the players actions, inputs, collision effects and some sounds.
// Could have put the sounds here in the AudioManager...keeping this in mind as I should probably do that.


public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }

    private const int SOUND_VOLUME_MAX = 10;
    private static int soundVolume = 6;

    private SpriteRenderer spriteRenderer;

    private Material defaultMaterial;
    [SerializeField] private Material whiteMaterial;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 playerDirection;
    [SerializeField] private float moveSpeed;

    public float boost = 1f;
    private float boostPower = 5f;
    private bool boosting = false;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject vFx;

    [SerializeField] private AudioClip crashAudioClip;
    [SerializeField] private ParticleSystem engineEffect;
    [SerializeField] private CinemachineImpulseSource crashImpulse;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
            
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;

        energy = maxEnergy;
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);

        health = maxHealth;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);

        AudioManager.Instance.PlaySound(AudioManager.Instance.MainMusic);

    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            float directionY = Input.GetAxisRaw("Vertical");
            float directionX = Input.GetAxisRaw("Horizontal");


            animator.SetFloat("moveY", directionY);
            animator.SetFloat("moveX", directionX);

            playerDirection = new Vector2(directionX, directionY).normalized;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                EnterBoost();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                ExitBoost();
            }

            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("Fire1"))
            {
                PhaserWeapon.Instance.Shoot(); 
            }
        }
                      
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x * moveSpeed, playerDirection.y * moveSpeed);

        if (boosting)
        {
            if (energy > 0.2f) energy -= 0.2f;

            else
            {
                ExitBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }

        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
               
    }


    private void EnterBoost()
    {
        if (energy > 6)
        {
            AudioManager.Instance.PlaySound(AudioManager.Instance.fire);
            animator.SetBool("boosting", true);
            boost = boostPower;
            boosting = true;
            engineEffect.Play();
        }
    }

    public void ExitBoost()
    {
        animator.SetBool("boosting", false);
        boost = 1f;
        boosting = false;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage(1);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(10);
        }

    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        AudioManager.Instance.PlaySound(AudioManager.Instance.hit);
        spriteRenderer.material = whiteMaterial;
        StartCoroutine("ResetMaterial");
        if (health <= 0)
        {
            boost = 0f;
            Boom();
            AudioManager.Instance.fire.Stop();
            AudioManager.Instance.MainMusic.Stop();
        }
    }

    IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultMaterial;
    }

    private void Boom()
    {
        Instantiate(vFx, transform.position, transform.localRotation);
        gameObject.SetActive(false);
        crashImpulse.GenerateImpulse(5f);
        AudioSource.PlayClipAtPoint(crashAudioClip, Camera.main.transform.position, GetSoundVolumeNormalized());
        GameManager.Instance.GameOver();

    }

    public int GetSoundVolume()
    {
        return soundVolume;
    }

    public float GetSoundVolumeNormalized()
    {
        return ((float)soundVolume) / SOUND_VOLUME_MAX;
    }

}
