using UnityEngine;

public class PhoneAudioScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int[] soundChance = new int[] { // For each sound the phone makes at random, this array sets how likely it is for it to play each frame. This can be manually edited or changed over time.
                3, // Alarm 1
                3, // Alarm 2
                20, // Buzz
                10, // Notification 1
                10, // Notification 3
                10, // Notification 4
                10, // Notification 5
                1, // Ringtone

        };

        AudioSource[] phoneSounds = gameObject.GetComponents<AudioSource>();

        for (global::System.Int32 i = 0; i < phoneSounds.Length; i++)
        {
            if (Random.Range(0, 100000) < soundChance[i])
            {
                phoneSounds[i].Play();
            }
        }
        
        
    }
}
