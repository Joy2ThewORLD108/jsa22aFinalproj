using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    private AudioSource audioSource;
    private string microphoneName;

    void Start()
    {
        // 1. Get the AudioSource attached to this object
        audioSource = GetComponent<AudioSource>();

        // 2. Check if the user has a microphone plugged in
        if (Microphone.devices.Length > 0)
        {
            // Grab the default microphone (the first one in the list)
            microphoneName = Microphone.devices[0];

            // 3. Start recording from the microphone
            // Parameters: Device Name, Loop recording, Buffer length in seconds, Sample rate
            audioSource.clip = Microphone.Start(microphoneName, true, 10, 44100);

            // Set the AudioSource to loop so it plays continuously
            audioSource.loop = true;

            // 4. Wait a tiny fraction of a second for the mic to start capturing
            // This prevents the AudioSource from trying to play an empty clip
            while (!(Microphone.GetPosition(microphoneName) > 0)) { }

            // 5. Play the captured audio
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No microphone detected! Please plug one in.");
        }
    }

    void OnDisable()
    {
        // Clean up: Stop the microphone when this object is turned off or destroyed
        if (microphoneName != null)
        {
            Microphone.End(microphoneName);
        }
    }
}