using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardBot : MonoBehaviour
{
    public AudioClip[] songs;
    int index = 0;
    Animator animator;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {  
        animator = gameObject.GetComponent<Animator>();
        // source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SwitchSong() {
        if (index == 0)
            animator.SetTrigger("StartPlaying");
        source.Stop();
        source.clip = songs[index];
        source.Play();
        index ++;
    }


}
