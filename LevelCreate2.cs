using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreate2 : MonoBehaviour
{
    public GameObject[] t0, t90;
    public int size;
    private float rand;
    private int counterror,radius;
    private GameObject[] activemesh;
    // Start is called before the first frame update
    void Start()
    {
        radius = 4;
        counterror = 0;
        activemesh = new GameObject[size];
        activemesh[0] = Instantiate(t0[(int)Random.value*t0.Length], Vector3.zero, Quaternion.Euler(0, 0, 0));
        for(int i = 1; i < size; i++)
        {
            rand = Random.value;
            if (rand < 0.2f)
            {
                activemesh[i] = Instantiate(t0[(int)(Random.value * t0.Length)], activemesh[i - 1].transform.position, activemesh[i - 1].transform.rotation);
            }
            else
            {
                activemesh[i] = Instantiate(t90[(int)(Random.value * t90.Length)], activemesh[i - 1].transform.position, activemesh[i - 1].transform.rotation);
            }
            if (activemesh[i-1].gameObject.tag == "t90")
            {
                activemesh[i].transform.Rotate(0, -90, Random.value*360, Space.Self);
            }
            activemesh[i].transform.Translate(0, 0, 4.5f, Space.Self);
            activemesh[i].transform.Rotate(0, 0, Random.value * 360, Space.Self);
            if (IsOnCol(activemesh, i))
            {
                counterror += 1;
                Destroy(activemesh[i]);
                activemesh[i] = null;
                Destroy(activemesh[i - 1]);
                activemesh[i - 1] = null;
                i -= 2;
            }
            if (counterror > size)
            {
                break;
            }
        }
    }

    private bool IsOnCol(GameObject[] current, int indice)
    {
        bool test = false;
        for (int k = 0; k < indice; k++)
        {
            if ((current[indice].transform.position - current[k].transform.position).magnitude < radius)
            {
                test = true;
            }
        }
        return test;
    }

}
