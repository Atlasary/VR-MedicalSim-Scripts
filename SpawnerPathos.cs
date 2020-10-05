using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPathos : MonoBehaviour
{
    public GameObject patho;
    // Start is called before the first frame update
    void Start()
    {
        float random = Random.Range(0,5);
        if (random ==0)
        {
            Instantiate(patho, transform.position, Quaternion.identity);
        }
    }

}
