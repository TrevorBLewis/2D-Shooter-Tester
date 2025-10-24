using UnityEngine;

// This script functions as the main audio controller for the game music and sound FX.
// I declared each audio source to access an instance of the sounds that will attach to each instance. 
// I wanted the sounds to vary, so added the PlayModifiedSound method to make repeating sounds with different pitches
// making the experience more interesting.

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource fire;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource MainMusic;
    public AudioSource boom2;
    public AudioSource hitRock;
    public AudioSource shoot;
    public AudioSource zapped;
    public AudioSource burned;
    public AudioSource hitArmor;
    public AudioSource bossCharge;


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

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }

    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.7f, 0.3f);
        sound.Stop();
        sound.Play();
    }
}
