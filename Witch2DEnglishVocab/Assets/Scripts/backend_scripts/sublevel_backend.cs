using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class sublevel_backend : MonoBehaviour
{
    // Start is called before the first frame update
    
    private System.DateTime timeStart;
    private bool is_on_door = false;
    private int time_num = 0;
    private string dir = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*void OnApplicationFocus (bool hasFocus)
	{
		
        if (is_on_door) {
            if (hasFocus) {
            Debug.Log("FOCUSED");
            timeStart = System.DateTime.UtcNow;
            }
            else {
                Debug.Log("Not focused");
            
                System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
                int temp;
                if (int.TryParse(ts.Seconds.ToString(),out temp))
                {
                    Debug.Log("Application not focused");
                    time_num = time_num + temp;
                    string message = "Player switched application tab\n\n";
                    string ended_seconds = "Time: " + ts.Seconds.ToString() + " seconds\n\n";
                    append_file_log(message);
                    append_file_log(ended_seconds); 
                }
            }
        }
        
	}
*/
	// When app is Paused
	void OnApplicationPause (bool pauseStatus)
	{
        if (is_on_door) {
            Debug.Log("PAUSE");
		//isPaused = pauseStatus;
        if (pauseStatus) {
            Debug.Log("STOPPED");
            System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
            int temp;
            if (int.TryParse(ts.Seconds.ToString(),out temp))
                {
                    Debug.Log("Application paused");
                    time_num = time_num + temp;
                    string message = "Player paused the game\n\n";
                    string ended_seconds = "Time: " + ts.Seconds.ToString() + " seconds\n\n";
                    append_file_log(message);
                    append_file_log(ended_seconds); 
                }

        }
        else {
            Debug.Log("Continued");
            string message = "Player unpaused game\n\n";

            append_file_log(message);
            timeStart = System.DateTime.UtcNow;
        }
        }
        
	}

	// When App Ends
	void OnApplicationQuit()
	{
		// Passing the variable to prevent from updating
        if (is_on_door) {
            Debug.Log("QUIT");
            System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
            int temp;
            if (int.TryParse(ts.Seconds.ToString(),out temp) && is_on_door)
            {
                Debug.Log("Application Quitting");
                time_num = time_num + temp;
                string message = "Player has quitted\n\n";
                string ended_seconds = "Time: " + time_num + " seconds";
                append_file_log(message);
                append_file_log(ended_seconds); 
            }
        }
        
        /*System.TimeSpan ts = System.DateTime.UtcNow - startTime;
        string seconds = ts.Seconds.ToString();
        float minutes = float.Parse(seconds) / 60 ;
        Debug.Log (minutes + " minutes");
		CreateTimeFile(timeStart);*/
        
	}
    public void write_file_log(string file_name) {
        dir = Application.streamingAssetsPath + "/sample/" + file_name + ".txt";
         File.WriteAllText(dir, "Backend for sub-level: " + file_name + "\n\n");
         time_num = 0;
        is_on_door = true;
        timeStart = System.DateTime.UtcNow;
        Debug.Log("WROTE " + dir);

    }
    public string get_dir() {
        return dir;
    }
    public void append_file_log(string text) {
        
        File.AppendAllText(dir, text);
    }
    public void end_level() {
        is_on_door = false;
        System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
        int temp;
        if (int.TryParse(ts.Seconds.ToString(),out temp))
        {
            Debug.Log("Recording end");
            time_num = time_num + temp;
            string ended_seconds = "Time: " + time_num + " seconds";

            append_file_log(ended_seconds); 
        }
       

    }
}
