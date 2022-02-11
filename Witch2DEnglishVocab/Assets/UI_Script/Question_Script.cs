using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [TextArea(3, 10)]
    [SerializeField] private string main_script, highlighted_word;
    [SerializeField] private string speaker;
    [SerializeField] private List<string> clues, type_clue;
    [SerializeField] private List<string> choices, results;
    [TextArea(3, 10)]
    [SerializeField] private List<string> remarks;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int get_clue_count() {
        return clues.Count;
    }
    public string get_speaker() {
        return speaker;
    }
    public string get_main_script() {
        return main_script;
    }
    public string get_clue(int num) {
        return clues[num];
    }
    public string get_choice(int num) {
        return choices[num];
    }
    
    public string get_clue_type(int num) {
        return type_clue[num];
    }
    public int get_choice_count() {
        return choices.Count;
    }
    public string get_remark_diag(int num) {
        return remarks[num];
    }
    public string get_result_diag(int num) {
        return results[num];
    }
    public string get_highlighted_sentence() {
        return highlighted_word;
    }
    public void set_panel_counter(int counter) {
        
    }
}
