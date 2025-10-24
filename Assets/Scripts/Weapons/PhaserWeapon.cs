using UnityEngine;

// This script handles the the players visual shot speed and direction

public class PhaserWeapon : MonoBehaviour
{
    public static PhaserWeapon Instance {  get; private set; }

    //[SerializeField] private GameObject prefab;
    [SerializeField] private ObjectPooler bulletPool;

    public float speed;
    public int damage;

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

        bulletPool.transform.Rotate(0, 0, 90); 
    }

    public void Shoot()
    {

        //Instantiate(prefab, transform.position, transform.rotation);
        GameObject bullet = bulletPool.GetPooledObject();
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.shoot);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }

}
