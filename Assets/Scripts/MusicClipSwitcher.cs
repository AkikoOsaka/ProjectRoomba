using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicClipSwitcher : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.name == "Player")
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        
        }
    }
}
