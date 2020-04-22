using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONData : MonoBehaviour
{
	string filename = "data.json";
	string path;

	GameData gamedata = new GameData();

    // Start is called before the first frame update
    void Start()
    {
    	path = Application.persistentDataPath + "/" + filename;
    	Debug.Log( path );
    }

    // Update is called once per frame
    void Update()
    {
    	if( Input.GetKey( "up" ) )
    	{
    		ReadData();
    	}   
    }

    void ReadData()
    {
    	string contents = System.IO.File.ReadAllText( path );
    	gamedata = JsonUtility.FromJson<GameData>( contents );
    	// gamedata.firstname = "bla";
    	// gamedata.lastname = "bulla";
    	Debug.Log(gamedata.firstname);
    	Debug.Log(gamedata.lastname);
    }
}
