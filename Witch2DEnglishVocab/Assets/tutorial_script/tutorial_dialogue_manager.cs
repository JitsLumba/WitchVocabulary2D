using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial_dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private GameObject dialogue_box;
    [SerializeField] private Text dialogue_text, name_text;
    

    int counter = 0;
    private string resultspeaker = "";
    private List<string> dialogue_list, names_list, result_dialogues , after_dialogues , after_names ;
    void Start()
    {
        dialogue_list = new List<string>();
        names_list = new List<string>();
        result_dialogues = new List<string>();
        after_dialogues = new List<string>();
        after_names = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void result_list_clear() {
        result_dialogues.Clear();
    }
    void clear_after_lists() {
        after_names.Clear();
        after_dialogues.Clear();
    }
    
    void clear_dialogue_lists() {
        dialogue_list.Clear();
        names_list.Clear();
    }
    void add_result_dialogues(List<string> res_diag) {
        for (int i = 0; i < res_diag.Count; i++) {
            result_dialogues.Add(res_diag[i]);
        }
    }
    void add_after_dialogues(List<string> aft_diag, List<string> aft_name) {
        for (int i = 0; i < aft_diag.Count; i++) {
            after_names.Add(aft_name[i]);
            after_dialogues.Add(aft_diag[i]);
        }
    }
    
    public void initialize_result_dialogues(List<string> res_diag , string speak) {
        resultspeaker = speak;
        result_list_clear();
        add_result_dialogues(res_diag);

    }
    public void initialize_after_dialogues(List<string> aft_diag, List<string> aft_name) {
        clear_after_lists();
        add_after_dialogues(aft_diag, aft_name);
    }
    void initialize_dialogue_lists(List<string> dialogue, List<string> names) {
     
        for (int i = 0; i < dialogue.Count; i++) {
            dialogue_list.Add(dialogue[i]);
            names_list.Add(names[i]);
        }
    }
    public void set_active_dialogue_box(bool active) {
        dialogue_box.SetActive(active);
    }
    public void start_dialogue(List<string> dialogue, List<string> names) {
        clear_dialogue_lists();
       
        initialize_dialogue_lists(dialogue, names);
        
        next_dialogue(0);
        set_active_dialogue_box(true);
    }
    public void next_dialogue(int count) {
        string name = names_list[count];
        string dialogue = dialogue_list[count];
        set_dialogue_boxes(dialogue, name);
    }
    public void next_result(int count) {
        string dialogue = result_dialogues[count];
        set_dialogue_boxes(dialogue, resultspeaker);
    }
    public void next_after_dialogue(int count) {
        string name = after_names[count];
        string dialogue = after_dialogues[count];
        set_dialogue_boxes(dialogue, name);
    }
    void set_dialogue_boxes(string dialogue, string name) {
        name_text.text =name;
        dialogue_text.text = dialogue;
    }
}
