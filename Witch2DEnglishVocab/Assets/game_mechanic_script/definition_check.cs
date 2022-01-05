using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class definition_check : MonoBehaviour
{
    // Start is called before the first frame update
    private List<string> answers, type_clue;
    private int list_num = 0;
    void Start()
    {
        answers = new List<string>();
        type_clue = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int get_count() {
        return list_num;
    }
    public void set_answer(List<string> clues, List<string> type) {
        list_num = clues.Count;
        
        for (int i = 0; i < clues.Count; i++) {
            answers.Add(clues[i]);
            type_clue.Add(type[i]);
        }
    }
    public string get_answer(int ind) {
        return answers[ind];
    }
}
