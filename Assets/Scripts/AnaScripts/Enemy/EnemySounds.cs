using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] footSteps;
    [SerializeField]
    private AudioClip[] dyingSounds;
    [SerializeField]
    private AudioClip[] scream;

    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponentInParent<AudioSource>();
    }

    public void Step()
    {
        if (footSteps.Length > 0)
        {
            int index = Random.Range(0, footSteps.Length);
            if (source != null)
            {
                source.PlayOneShot(footSteps[index]);
            }
            else
            {
                Debug.Log("source is null");
            }
        }
    }

    public void Die()
    {
        if (dyingSounds.Length > 0)
        {
            int index = Random.Range(0, dyingSounds.Length);
            if (source != null)
            {
                source.PlayOneShot(dyingSounds[index]);
            }
            else
            {
                Debug.Log("source is null");
            }

        }
    }

    public void Scream()
    {
        if (scream.Length > 0)
        {
            int index = Random.Range(0, scream.Length);
            if (source != null)
            {
                source.PlayOneShot(scream[index]);
            }
            else
            {
                Debug.Log("source is null");
            }
            
            
        }
    }

}
