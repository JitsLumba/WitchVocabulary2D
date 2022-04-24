using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class to_text_file : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeStart = 0.0f;
    bool isPaused = false;
    void Start()
    {
        Directory.CreateDirectory(Application.streamingAssetsPath + "/sample/");
        create_text_file();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Built-in Function
	// When app is in focus
	void OnApplicationFocus (bool hasFocus)
	{
		isPaused = !hasFocus;
		timeStart += Time.deltaTime;
	}

	// When app is Paused
	void OnApplicationPause (bool pauseStatus)
	{
        Debug.Log("PAUSE");
		isPaused = pauseStatus;
	}

	// When App Ends
	void OnApplicationQuit()
	{
		// Passing the variable to prevent from updating
        Debug.Log("QUIT");
		CreateTimeFile(timeStart);
	}
    public void create_text_file() {
        string dir = Application.streamingAssetsPath + "/sample/chat.txt"; 

        if (!(File.Exists(dir))) {
            File.WriteAllText(dir, "TITLE OF MY CHAT LOG");
        }
        File.AppendAllText(dir, "Elsa is so beautiful");
    }
    public void CreateTimeFile(float time) {
        string dir = Application.streamingAssetsPath + "/sample/time.txt";
         
            File.WriteAllText(dir, "" + time);
        
    }

}
