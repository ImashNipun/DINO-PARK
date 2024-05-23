using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneheadSoundEffects : MonoBehaviour
{
    //Variables
    AudioSource audioSource;

    //Sound Variants
    public AudioClip[] growlClips;
    public AudioClip[] yelpClips;
    public AudioClip[] barkClips;
    public AudioClip[] roarClips;
    public AudioClip[] deathClips;

    // Delegate array to store references to the sound functions
    private delegate void SoundEffectDelegate();
    private SoundEffectDelegate[] soundEffectFunctions;

    //Gather variables
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Initialize the delegate array with the sound functions
        soundEffectFunctions = new SoundEffectDelegate[]
        {
           
            Bark,
            Roar,
            
        };

        // Start invoking the random sound function at random intervals
        InvokeRepeating("InvokeRandomSoundEffect", 0f, Random.Range(1f, 5f));
    }

    // Function to invoke a random sound effect function
    void InvokeRandomSoundEffect()
    {
        // Select a random sound effect function from the array
        int index = Random.Range(0, soundEffectFunctions.Length);
        soundEffectFunctions[index]();
        
        // Schedule the next invocation at a random interval
        CancelInvoke("InvokeRandomSoundEffect");
        Invoke("InvokeRandomSoundEffect", Random.Range(1f, 5f));
    }

    // Growl Sounds (Random)
    public void Growl()
    {
        int index = Random.Range(0, growlClips.Length);
        AudioClip clip = growlClips[index];
        audioSource.PlayOneShot(clip);
    }

    // Yelp Sounds (Random)
    public void Yelp()
    {
        int index = Random.Range(0, yelpClips.Length);
        AudioClip clip = yelpClips[index];
        audioSource.PlayOneShot(clip);
    }

    // Bark Sounds (Random)
    public void Bark()
    {
        int index = Random.Range(0, barkClips.Length);
        AudioClip clip = barkClips[index];
        audioSource.PlayOneShot(clip);
    }

    // Roar Sounds (Random)
    public void Roar()
    {
        int index = Random.Range(0, roarClips.Length);
        AudioClip clip = roarClips[index];
        audioSource.PlayOneShot(clip);
    }

    // Death Sounds (Random)
    public void Death()
    {
        int index = Random.Range(0, deathClips.Length);
        AudioClip clip = deathClips[index];
        audioSource.PlayOneShot(clip);
    }
}
