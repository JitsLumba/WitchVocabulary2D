using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_dialogue_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_manager tut_diag_manager ;
    [SerializeField] private tutorial_panel_mechanic tut_panel_mech ;
    [SerializeField] private tutorial_image_show tut_img_show ;
    [SerializeField] private tutorial_distance_trigger tut_dist_trigger ;
    private List<string> result_dialogue;
    private List<int> special_numbers_list;
    private int counter = 1;
    private int after_counter = 0;
    private int confirm_counter = 0;
    private int after_counter_max = 0;
    
    private int max_count = 0;
    private bool cango = false;
    private bool after_choose = false;
    private bool can_g = false, can_f = true;
    private bool is_correct = false;
    private bool is_marked = false;
    private bool is_diag_hidden = false;
    private bool is_img_on = false, is_passed = false ;
    private bool is_active = false;
 
    private int special_num_type = 0;
    void Start()
    {

        //pink
        //FF0FAB
        result_dialogue = new List<string>();
        special_numbers_list = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && can_g) {

        }
       
    }
    public void panel_mechanic_dialogue_pane_active(bool active) {
        tut_panel_mech.set_dialogue_active(active);
    }
    public void after_diag_trigger(int after_num) {
        tut_diag_manager.next_after_dialogue(after_num);
    }
    public void incorrect_return(int counter_num) {
        tut_panel_mech.set_can_freeze(true);
                       
        tut_panel_mech.set_highlighter_panel_active(true);
        tut_panel_mech.set_freeze_panel_obj_active(true);
                        
        tut_diag_manager.next_dialogue(counter_num - 1);
        after_choose = false;
    }
    public void reset_highlighter() {
        tut_panel_mech.reset_highlighter();
        string curr = tut_panel_mech.get_current_type();
        Debug.Log(curr + " JAKES");
    }
    public void freeze_prompt_switch(bool active) {
        tut_panel_mech.freeze_prompt_panel_switch(active);
    }
    public void press_z_panel_switch(bool active) {
        Debug.Log("WALLOWS " + active);
        tut_panel_mech.press_z_panel_switch(active);
    }
    void key_change_active() {
        bool activate = true;
        if (is_img_on) {
            is_img_on = false;
           
        }
        else {
            is_img_on = true;
            activate = false;
        }
        set_can_f(activate);
        set_can_g(activate);
        tut_panel_mech.key_actives(activate);
    }
    public void answer_trigger() {
        string curr = tut_panel_mech.get_current_type();
        Debug.Log("IS_ON YEAH " + curr);
        bool is_on = tut_panel_mech.get_ison();
            if (is_on) {
                check_answer();
            }
    }
    public void set_can_f(bool can) {
        can_f = can;
    }
    public void set_can_g(bool can) {
        can_g = can;
    }
    public void panel_mech_freeze_button_switch(bool active) {
        tut_panel_mech.change_freeze_panel_active(active);
    }
    public void panel_mech_highlighter_button_switch(bool active) {
        tut_panel_mech.change_highlighter_panel_active(active);
    }
    public void panel_mech_vocabulary_panel_switch(bool active) {
        tut_panel_mech.change_vocab_panel_active(active);
    }
    public void panel_mech_change_vocab_word(string word) {
        tut_panel_mech.change_vocab_text(word);
    }
    public void check_answer() {
        tut_panel_mech.change_freeze_panel_color("#FFFFFF");
        tut_panel_mech.freeze_prompt_show_or_hide(false);
        tut_panel_mech.change_vocab_color(false);
        tut_panel_mech.set_highlighter_panel_active(false);
        tut_panel_mech.set_freeze_panel_obj_active(false);
        tut_panel_mech.change_panel_sprite(false);
        tut_panel_mech.compare_answers();
        int cor_counter = tut_panel_mech.get_correct_counter();
        if (cor_counter == 0) {
            is_correct = true;
        }
        else {
            is_correct = false;
        }
        after_choose = true;
        tut_panel_mech.set_can_freeze(false);
        tut_diag_manager.next_result(cor_counter);
    }
    public void set_after_choose(bool choose) {
        after_choose = choose;
    }
    public void set_is_correct(bool correct) {

    }
    public bool get_after_choose() {
        return after_choose;
    }
    public bool get_is_correct() {
        return is_correct;
    }
    public void set_panel_mech_can_z(bool can) {
        tut_panel_mech.set_can_z(can);
    }
    public void freeze_or_defreeze() {
        tut_panel_mech.freeze_or_defreeze();
    }
    public void set_panel_mech_can_a(bool can) {
        tut_panel_mech.set_can_a(can);
    }
    public void set_panel_mech_can_d(bool can) {
        tut_panel_mech.set_can_d(can);
    }
    
    public void next_dialogue() {
        
        if (max_count > counter) {
            
            is_marked = tut_img_show.return_is_on_marked_diag(counter);
            
            counter++;
            //check if it is past the tutorial max
            //special number case

            //last dialogue
            if (max_count == counter) {
              
                tut_panel_mech.set_can_freeze(true);
                tut_panel_mech.set_freeze_panel_obj_active(true);
                tut_panel_mech.set_clue_number(1);
                tut_panel_mech.set_highlighter_panel_active(true);
                tut_panel_mech.set_clue_panel_active(true);
                tut_panel_mech.set_can_tab(true);
            }
            int index = counter - 1;
            tut_diag_manager.next_dialogue(index);
            
            
        }
       
    }
    public void set_panel_mech_can_freeze(bool active) {
        tut_panel_mech.set_can_freeze(active);
    }
    public void store_result_diag(List<string> result_diag , string speaker) {
        tut_diag_manager.initialize_result_dialogues(result_diag, speaker);
    }
    public void store_after_diag(List<string> aft_diag, List<string> aft_name) {
        after_counter = 0;
        after_counter_max = aft_diag.Count;

        tut_diag_manager.initialize_after_dialogues(aft_diag, aft_name);
    }
    public void confirmation_dialogue(List<string> conf_diag, List<string> conf_name) {
        confirm_counter = 0;
        after_counter_max = conf_diag.Count;
        tut_diag_manager.confirmation_dialogue(conf_diag, conf_name);
    }
   
    public void initialize_dialogue(List<string> dialogue, List<string> names) {
        
        tut_img_show.set_is_not_on_dialogue(false);
        tut_panel_mech.set_can_tab(false);
        tut_panel_mech.set_can_f(false);
        tut_panel_mech.set_can_a(false);
        tut_panel_mech.set_can_d(false);
        tut_panel_mech.set_counter(0);
        is_active = true;
       is_marked = false;
        cango = true;
        is_correct = false;
        counter = 1;
        max_count = dialogue.Count ;
        tut_diag_manager.start_dialogue(dialogue, names);

    }
    public void change_context_highlighter() {
        tut_panel_mech.change_context_highlighter();
    }
    public bool get_ison_panel_mech() {
        return tut_panel_mech.get_ison();
    }
    public void set_panel_mech_can_tab(bool can) {
        tut_panel_mech.set_can_tab(can);
    }
    public void set_normal_counter(int count) {
        counter = count;
    }
    public void change_highlight_counter(int count) {
        tut_panel_mech.set_counter(count);
    }
    public void set_vocab_panel_active(bool active) {
        tut_panel_mech.change_vocab_panel_active(active);
    }
    public void confirm_trigger_dialogue(int counter) {
        tut_diag_manager.next_confirm(counter);
    }
    public void after_confirm_trigger_dialogue(int counter) {
        tut_diag_manager.next_after_confirm(counter);
    }
    public void initialize_after_confirm_diag(List<string> dialogues, List<string> names) {
        tut_diag_manager.initialize_after_confirm_diag(dialogues, names);
    }
    void enable_movement() {
        CharacterController2D charsub2d;
        PlayerMovement pmovement ;
        GameObject player_search = GameObject.Find("Player");
        if (player_search != null) {
            charsub2d = player_search.GetComponent<CharacterController2D>();
            pmovement = player_search.GetComponent<PlayerMovement>();
            charsub2d.enabled = true;
            pmovement.enabled = true;
        }
    }
}
