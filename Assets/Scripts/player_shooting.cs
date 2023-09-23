using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_shooting : MonoBehaviour
    
{
    public GameObject playerBullet;
    public Transform[] gunPoint;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.Play();  
            for (int i = 0; i < gunPoint.Length; i++)
            {
                Instantiate(playerBullet, gunPoint[i].position, Quaternion.identity);
            }
        }
    }
}
