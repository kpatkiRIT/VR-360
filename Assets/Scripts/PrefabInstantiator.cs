using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabInstantiator : MonoBehaviour
{
	public GameObject myFaceMarkerPrefab;
	int k = 0;
	Vector3[] positionlist = {new Vector3(0f,0f,0f),
	                          new Vector3(0f,0f,1f),
	                          new Vector3(0f,1f,1f),
	                          new Vector3(0f,1f,0f) };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject myFaceMarkerPrefabClone = Instantiate( myFaceMarkerPrefab, positionlist[k], Quaternion.identity ) as GameObject;
        Destroy(myFaceMarkerPrefabClone, Time.deltaTime );
        k = (k + 1) % 4;
    }
}
