using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial_dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    //CHANGE THIS TO GET FROM TUTORIAL SCRIPT AS MODEL
    [SerializeField] private GameObject dialogue_box;
    [SerializeField] private Text dialogue_text, name_text;
    

    int counter = 0;
    private string resultspeaker = "";
    private List<string> dialogue_list, names_list, result_dialogues , after_dialogues , after_names, confirm_diag, confirm_name, after_confirm_diag, after_confirm_name ;
    void Start()
    {
        /*dialogue_list = new List<string>();
        names_list = new List<string>();
        result_dialogues = new List<string>();
        after_dialogues = new List<string>();
        after_names = new List<string>();
        confirm_diag = new List<string>();
        confirm_name = new List<string>();
        after_confirm_diag = new List<string>();
        after_confirm_name = new List<string>();*/
    }
    public void initialize_all_lists() {
        dialogue_list = new List<string>();
        names_list = new List<string>();
        result_dialogues = new List<string>();
        after_dialogues = new List<string>();
        after_names = new List<string>();
        confirm_diag = new List<string>();
        confirm_name = new List<string>();
        after_confirm_diag = new List<string>();
        after_confirm_name = new List<string>();
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
    void clear_confirm_lists() {
        confirm_diag.Clear();
        confirm_name.Clear();
    }
    void clear_after_confirm_lists() {
        after_confirm_diag.Clear();
        after_confirm_name.Clear();
    }
    public void add_result_dialogues(List<string> res_diag) {
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
    void add_confirm_dialogues(List<string> conf_diag, List<string> conf_names) {
        for (int i = 0; i < conf_diag.Count; i++) {
            confirm_name.Add(conf_names[i]);
            confirm_diag.Add(conf_diag[i]);
        }
    }

    void add_after_confirm_dialogues(List<string> after_conf_diag, List<string> after_conf_names) {
        for (int i = 0; i < after_conf_diag.Count; i++) {
            after_confirm_name.Add(after_conf_names[i]);
            after_confirm_diag.Add(after_conf_diag[i]);
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
        Debug.Log("WHY WOULD IT " + count);
        string name = names_list[count];
        string dialogue = dialogue_list[count];
        set_dialogue_boxes(dialogue, name);
    }
    public void next_confirm(int count) {
       
        string name = confirm_name[count];
        string dialogue = confirm_diag[count];
        set_dialogue_boxes(dialogue, name);
    }
    public void next_after_confirm(int count) {
        //this prints out the after confirm dialogues
        string name = after_confirm_name[count];
        string dialogue = after_confirm_diag[count];
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
    public void confirmation_dialogue(List<string> conf_diag, List<string> conf_names) {
        clear_confirm_lists();
        add_confirm_dialogues(conf_diag, conf_names);
    }
    void set_dialogue_boxes(string dialogue, string name) {
        name_text.text =name;
        dialogue_text.text = dialogue;
    }
    
    public void initialize_after_confirm_diag(List<string> dialogues, List<string> names) {
        clear_after_confirm_lists();
        add_after_confirm_dialogues(dialogues, names);
    }
}
