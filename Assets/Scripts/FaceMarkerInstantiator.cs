using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FaceMarkerInstantiator : MonoBehaviour
{
	public GameObject topLeftFaceMarkerPrefab;
    public GameObject topRightFaceMarkerPrefab;
    public GameObject bottomLeftFaceMarkerPrefab;
    public GameObject bottomRightFaceMarkerPrefab;
	
    string filename = "data.json";
    string path;

    GameData gamedata = new GameData();

    GameObject videosphere;

    VideoPlayer videoplayer;

    long k = 0;
    int num_frames;

    // Start is called before the first frame update
    
    void ReadData()
    {
        string contents = System.IO.File.ReadAllText( path );
        gamedata = JsonUtility.FromJson<GameData>( contents );
        // Debug.Log(gamedata.frames[0].bboxes[0].top_left[0]);
    }

    void Start()
    {
        //Initialize videoplayer
        videosphere = GameObject.Find("Video_display_sphere");
        videoplayer = videosphere.GetComponent<VideoPlayer>();
        
        // Read JSON
        path = Application.persistentDataPath + "/" + filename;
        Debug.Log( path );
        ReadData();
        num_frames = gamedata.numframes;
    }

    // Update is called once per frame
    void Update()
    {
        k = videoplayer.frame;
        if( k < num_frames ){
        FrameData currentFrame = gamedata.frames[k];

        foreach( BboxData bbox in currentFrame.bboxes )
        {
            GameObject topLeftFaceMarkerPrefabClone = Instantiate( topLeftFaceMarkerPrefab,
                                                                     new Vector3( bbox.top_left[0], bbox.top_left[1], bbox.top_left[2] ),
                                                                     Quaternion.identity ) as GameObject;
            
            GameObject topRightFaceMarkerPrefabClone = Instantiate( topRightFaceMarkerPrefab,
                                                                     new Vector3( bbox.top_right[0], bbox.top_right[1], bbox.top_right[2] ),
                                                                     Quaternion.identity ) as GameObject;
            
            GameObject bottomLeftFaceMarkerPrefabClone = Instantiate( bottomLeftFaceMarkerPrefab,
                                                                     new Vector3( bbox.bottom_left[0], bbox.bottom_left[1], bbox.bottom_left[2] ),
                                                                     Quaternion.identity ) as GameObject;
            
            GameObject bottomRightFaceMarkerPrefabClone = Instantiate( bottomRightFaceMarkerPrefab,
                                                                     new Vector3( bbox.bottom_right[0], bbox.bottom_right[1], bbox.bottom_right[2] ),
                                                                     Quaternion.identity ) as GameObject;
            
            Destroy( topLeftFaceMarkerPrefabClone, Time.deltaTime );
            Destroy( topRightFaceMarkerPrefabClone, Time.deltaTime );
            Destroy( bottomLeftFaceMarkerPrefabClone, Time.deltaTime );
            Destroy( bottomRightFaceMarkerPrefabClone, Time.deltaTime );
        }
        }
        // k = k + 1;
        // k = (k + 1) % num_frames;
    }
}
