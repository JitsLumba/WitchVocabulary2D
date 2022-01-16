using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_definition_check : MonoBehaviour
{
    // Start is called before the first frame update
    private string answer = "", type_clue = "";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_values(string answer_give, string type_give) {
        answer = answer_give;
        type_clue = type_give;
        Debug.Log(answer_give);
        Debug.Log(answer + " 1");
        Debug.Log(type_clue + " 2");
    }
    public string get_answer() {
        return answer;
    }
    public string get_type() {
        return type_clue;
    }
}
