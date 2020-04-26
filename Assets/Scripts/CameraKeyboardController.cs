using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey( KeyCode.S ) )
        {
            transform.Rotate( 20 * Time.deltaTime, 0, 0 );
        }else if( Input.GetKey( KeyCode.W ) )
        {
            transform.Rotate( -20 * Time.deltaTime, 0, 0 );
        }else if( Input.GetKey( KeyCode.D ) )
        {
            transform.Rotate( 0, 20 * Time.deltaTime, 0 );
        } else if( Input.GetKey( KeyCode.A ) )
        {
        	transform.Rotate( 0, -20 * Time.deltaTime, 0 );
        }
    }
}
