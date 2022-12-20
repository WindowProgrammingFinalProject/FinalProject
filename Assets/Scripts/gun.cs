using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class gun : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

}

internal class MouseEventArgs
{
}