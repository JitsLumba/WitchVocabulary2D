using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class cloze_test_backend : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string file_name;
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
    public void start_question_time() {
        questionStart = System.DateTime.UtcNow;
    }
    public void end_test() {
        System.TimeSpan ts = System.DateTime.UtcNow - timeStart;
        string ended_seconds = "Total Time: " + ts.Seconds.ToString() + " seconds";

        append_file_log(ended_seconds);
    }
    public void write_test_log(string file_name) {
        dir = Application.streamingAssetsPath + "/sample/" + file_name + ".txt";
         File.WriteAllText(dir, "Backend for cloze test: " + file_name + "\n\n");
        timeStart = System.DateTime.UtcNow;
    }
    public void append_file_log(string text) {
        File.AppendAllText(dir, text);
    }
    public void end_of_question() {
        System.TimeSpan ts = System.DateTime.UtcNow - questionStart;
        string ended_seconds = "Time: " + ts.Seconds.ToString() + " seconds\n\n";

        question_stop = ended_seconds;
    }
    public void write_end_time() {
        append_file_log(question_stop);
    }
}
