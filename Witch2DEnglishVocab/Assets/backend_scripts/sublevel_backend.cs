using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class sublevel_backend : MonoBehaviour
{
    // Start is called before the first frame update
    
    private System.DateTime timeStart;
    private string dir;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void write_file_log(string file_name) {
        dir = Application.streamingAssetsPath + "/sample/" + file_name + ".txt";
         File.WriteAllText(dir, "Backend for sub-level: " + file_name + "\n\n");
        timeStart = System.DateTime.UtcNow;

    }
    public void append_file_log(string text) {
        File.AppendAllText(dir, text);
    }
    public void end_level() {
        System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
        string ended_seconds = "Time: " + ts.Seconds.ToString() + " seconds";

        append_file_log(ended_seconds);

    }
}
