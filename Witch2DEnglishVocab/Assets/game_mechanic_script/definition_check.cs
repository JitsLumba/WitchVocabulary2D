using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class definition_check : MonoBehaviour
{
    // Start is called before the first frame update
    private string answer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_answer(string def) {
        answer = def;
    }
    public string get_answer() {
        return answer;
    }
}
