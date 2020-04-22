using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintALine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am alive!");
        Transform transform = GetComponent<Transform>();
        transform.Translate(5, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
