using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question_Dialogue_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Question_Script> question_list ;
    [SerializeField] private definition_check dcheck;
    [SerializeField] private Question_Dialogue_Manager question_manager ;
    private List<string> choices, clues, clue_typing;
    private bool canproc = false;
    private int button_select = 0;
    private bool after_result = false;
    private string play_name = "Elaina";
    int counter = -1;
    
    void Start()
    {
        choices = new List<string>();
        clues = new List<string>();
        clue_typing = new List<string>();
        initialize_script();
    }
    void initialize_script() {
        counter++;
        add_definitions();
        choice_button_edit();
        trigger_dialogue();
    }
    void add_definitions() {
        dcheck.clear_lists();
        int clue_count = question_list[counter].get_clue_count();
        for (int i = 0; i < clue_count; i++) {
            string clue_word = question_list[counter].get_clue(i);
            string clue_type = question_list[counter].get_clue_type(i);
            clues.Add(clue_word);
            clue_typing.Add(clue_type);
        }
        dcheck.set_answer(clues, clue_typing);
    }
    void choice_button_edit() {
        clear_choice_list();
        int choice_num = question_list[counter].get_choice_count();
        for (int i = 0; i < choice_num; i++) {
            choices.Add(question_list[counter].get_choice(i));
        }
        question_manager.set_choices(choices);
    }
    void clear_choice_list() {
        choices.Clear();
    }
    public void set_can_proc(bool proc) {
        canproc = proc;
    }
    public int get_clue_num() {
        return dcheck.get_count();
    }
    public string get_clue(int num) {
        return dcheck.get_answer(num);
    }
    public string get_clue_type(int num) {
        return dcheck.get_clue(num);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && canproc) {
            if (after_result) {
                canproc = false;
                after_result = false;
                question_manager.show_result(question_list[counter].get_result_diag(button_select));
            }
            else {
               question_manager.set_active_dialogue(false, true);
               canproc = false;
            }
        }
    }
  
    public void choice_trigger(List<string> clues) {
        //trigger dialogue
        canproc = true;
        
        question_manager.indicate_context(clues, play_name);
        
    }
    public int get_counter() {
        return counter;
    }
    
    public void trigger_dialogue() {
        
        string question = question_list[counter].get_main_script();
        
        string speaker = question_list[counter].get_speaker();
        question_manager.new_dialogue(question, speaker);
    }
    public void choice_click_event(int num) {
        button_select = num;
        after_result = true;
        canproc = true;
        string result_diag = question_list[counter].get_remark_diag(num);
        Debug.Log(result_diag);
        string talker = question_list[counter].get_speaker();
        question_manager.set_active_dialogue(true, false);
        question_manager.new_dialogue(result_diag, talker);
    }
}
