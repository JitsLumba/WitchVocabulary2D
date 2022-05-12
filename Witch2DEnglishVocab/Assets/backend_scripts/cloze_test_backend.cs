using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class cloze_test_backend : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string file_name;
    private int time_num = 0;
    private int question_time = 0;
    private bool is_on_question = false;
    private bool has_loaded = false;
    private string dir = "";
    private string question_stop;

    private System.DateTime timeStart, questionStart;
    void Start()
    {
        write_test_log(file_name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*void OnApplicationFocus (bool hasFocus)
	{
		
      
            if (hasFocus) {
            Debug.Log("FOCUSED");
            timeStart = System.DateTime.UtcNow;
            string message = "Player switched to the game tab\n\n";

            append_file_log(message);
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
*/
	// When app is Paused
	void OnApplicationPause (bool pauseStatus)
	{
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
                    
                    if (is_on_question) {
                        int quest_temp;
                        System.TimeSpan qs = System.DateTime.UtcNow - questionStart;
                        if (int.TryParse(qs.Seconds.ToString(),out quest_temp)) {
                            question_time = question_time + quest_temp;
                        }
                    }
                    
                    append_file_log(message);
                    append_file_log(ended_seconds); 
                }

        }
        else {
            Debug.Log("Continued");
            if (has_loaded) {
                string message = "Player unpaused game\n\n";

                append_file_log(message);
                timeStart = System.DateTime.UtcNow;
                if (is_on_question) {
                    questionStart = System.DateTime.UtcNow;
                }
                
            }
            
        }
	}

	// When App Ends
	void OnApplicationQuit()
	{
		// Passing the variable to prevent from updating
       
            Debug.Log("QUIT");
            System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
            int temp;
            if (int.TryParse(ts.Seconds.ToString(),out temp))
            {
                Debug.Log("Application Quitting");
                time_num = time_num + temp;
                string message = "Player has quitted\n\n";
                string ended_seconds = "Time: " + time_num + " seconds";
                append_file_log(message);
                append_file_log(ended_seconds); 
            }
        
        
        /*System.TimeSpan ts = System.DateTime.UtcNow - startTime;
        string seconds = ts.Seconds.ToString();
        float minutes = float.Parse(seconds) / 60 ;
        Debug.Log (minutes + " minutes");
		CreateTimeFile(timeStart);*/
        
	}
    public void start_question_time() {
        is_on_question = true;
        question_time = 0;
        questionStart = System.DateTime.UtcNow;
    }
    public void end_test() {
        System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
        int temp;
        if (int.TryParse(ts.Seconds.ToString(),out temp))
            {
                time_num = time_num + temp;
                string ended_seconds = "Total Time: " + time_num + " seconds";

                append_file_log(ended_seconds);
            }
        
    }
    public void write_test_log(string file_name) {
        Debug.Log("RITTEN");
        dir = Application.streamingAssetsPath + "/sample/" + file_name + ".txt";
         File.WriteAllText(dir, "Backend for cloze test: " + file_name + "\n\n");
         time_num = 0;
        timeStart = System.DateTime.UtcNow;
        has_loaded = true;
    }
    public void append_file_log(string text) {
        File.AppendAllText(dir, text);
    }
    public void end_of_question() {
        is_on_question = false;
        System.TimeSpan qs = System.DateTime.UtcNow - questionStart;
        int quest_temp;
        if (int.TryParse(qs.Seconds.ToString(),out quest_temp)) {
            question_time = question_time + quest_temp;
            string ended_seconds = "Question time taken: " + question_time + " seconds\n\n";

            question_stop = ended_seconds;
        }
        
    }
    public void write_end_time() {
        append_file_log(question_stop);
    }
}
