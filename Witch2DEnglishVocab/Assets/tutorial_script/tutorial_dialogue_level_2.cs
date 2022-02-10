using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_dialogue_level_2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_trigger tut_dtrigger ;

    [SerializeField] private tutorial_character_transport tut_char_transp ;
    [SerializeField] private tutorial_distance_trigger tut_dist_trigger ;
    [SerializeField] private GameObject tut_dtrigger_empty ;
    [SerializeField] private GameObject tut_level_1_empty ;
    [SerializeField] private GameObject clue_panel ;
    [SerializeField] private GameObject freeze_panel;
    [SerializeField] private GameObject highlighter_panel;
    [SerializeField] private GameObject yes_or_no_panel;
    [SerializeField] private GameObject arrow_pic ;

   
    [SerializeField] private List<int> dialogue_counters, after_counters;
    [SerializeField] private int num_go_back;
    [SerializeField] private int conf_mult;
    [SerializeField] private bool has_confirmation = true;
    [SerializeField] private int tutorial_num ;
    [SerializeField] private string sample_word, vocab_word;
    private List<string> result_dialogue;
    private List<int> special_numbers_list;
    private int counter = 1;
    private int after_counter = 0;
    private int hello_world = 0;
    private int confirm_counter = 0;
    private int after_counter_max = 0;
    private int repeat_index;
    private int max_count = 0;
    private bool cango = false;
    private bool after_choose = false;
    private bool can_g = true, can_f = false, can_z = false ;
    private bool is_correct = false;
    private bool is_marked = false;
    private bool is_diag_hidden = false;
    private bool is_img_on = false, is_passed = false ;
    private bool is_active = false;
    private bool is_on_freeze_demo = false;
    private bool has_confirmed = false;
    private bool is_on_conf_diag = false;
    private bool is_after_conf = false;
    private bool is_antonym_demo = false;
 
    private int special_num_type = 0;
    private int freeze_counter_demo = 0;

    private int conf_start = 0;
    private bool arrow_on = false;

    private int after_confirm_counter = 0;
    private int after_confirm_max = 0;
    private bool can_tab = false;
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
            bool after_choose = tut_dtrigger.get_after_choose();

            if (after_choose) {
                bool is_correct = tut_dtrigger.get_is_correct();
                if (is_correct) {
                    if (after_counter == after_counter_max) {
                        Debug.Log("CORREC");
                        this.set_can_g(false);
                        after_counter = 0;
                        tut_dtrigger.set_after_choose(false);
                        tut_dtrigger.reset_highlighter();
                        tut_dtrigger.panel_mechanic_dialogue_pane_active(false);
                        enable_movement();
                    

                        
                 
                        tut_dist_trigger.set_can_interact(true);
                        
                    }
                    else {
                        tut_dtrigger.after_diag_trigger(after_counter);
                        after_counter++;
                        if (after_counter == after_counter_max) {
                            this.tut_dtrigger.panel_mech_change_vocab_word(vocab_word);
                        }
                    }
                }
                else {
                    
                    Debug.Log("INCORREC");
                        tut_dtrigger.set_panel_mech_can_a(true);
                        tut_dtrigger.set_panel_mech_can_freeze(true);
                        tut_dtrigger.set_panel_mech_can_d(true);
                        tut_dtrigger.set_panel_mech_can_tab(true);
                        this.set_can_tab(true);
                        this.set_can_z(true);
                        this.set_can_g(false);
                        tut_dtrigger.incorrect_return(counter);
                }
            }
            else {
                next_dialogue();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.Z) && can_z) {
            freeze_tutorial_sequence();
        }
        if (Input.GetKeyDown(KeyCode.F) && can_f) {
            check_answer();
            
            set_can_g(true);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && can_tab) {
            this.change_highlighter_trigger();
        }
        
    }
    public void after_choose_effects() {
        for (int i = 0; i < after_counters.Count; i++) {
            int num = after_counters[i];
            if (after_counter == num) {
                after_choose_show_effect(i);
            }
        }
    }
    void after_choose_show_effect(int i) {
        if (i == 0) {
            tut_dtrigger.panel_mech_change_vocab_word("Exacerbate");
        }
    }
    public void change_highlighter_trigger() {
        if (can_tab) {
            Debug.Log("IS IT DEMO TIME");
            if (is_antonym_demo) {
                Debug.Log("DEMO ANTONYM");
                this.demo_change_highlighter();

            }
            else {
                tut_dtrigger.change_context_highlighter();
            }
        }
        
    }
    void set_is_antonym_demo(bool is_on) {
        this.is_antonym_demo = is_on;
    }
    void after_confirm_sequence() {
        Debug.Log("afttttsss " + after_confirm_counter);
        if (tutorial_num == 1) {
            for (int i = 0; i < after_counters.Count; i++) {
            int num = after_counters[i];
            Debug.Log("afttttsssnum " + num);
            if (num == after_confirm_counter) {
                after_confirm_effect(i);
                break;
            }
        }
        }
        
        tut_dtrigger.after_confirm_trigger_dialogue(after_confirm_counter);
    }
    void after_confirm_effect(int num) {
        Debug.Log("IS IT CHANGING?");
        if (num == 0) {
            if (tutorial_num == 1) {
                tut_dtrigger.panel_mech_change_vocab_word("Assuage");
            }
            else {
                tut_dtrigger.panel_mech_change_vocab_word("Exacerbate");
            }
            
        }
    }
    void set_is_on_conf_diag(bool is_on) {
        this.is_on_conf_diag = is_on;
    }
    void set_has_confirm(bool conf) {
        this.has_confirmed = conf;
    }
    void show_yes_or_no_choice_activate(bool active) {
        yes_or_no_panel.SetActive(active);
    }
    public void freeze_tutorial_sequence() {
        Debug.Log("FREEZE HER UP");
        if (can_z) {
            if (is_on_freeze_demo) {
              
                freeze_demo_only();
            }
            else {
                Debug.Log("WOAH FREEZER WOW");
                tut_dtrigger.freeze_or_defreeze();
            }
        }
        
    }
    void next_trigger_dialogue() {
        
    }
    public void change_vocab_panel_active(bool active) {
        tut_dtrigger.set_vocab_panel_active(active);
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
        can_f = activate;
        can_g = activate;
        //for tutorial images

        //tut_panel_mech.key_actives(activate);
    }
    void check_answer() {
        
        tut_dtrigger.set_panel_mech_can_tab(false);
        this.set_can_tab(false);
        tut_dtrigger.set_panel_mech_can_d(false);
        tut_dtrigger.set_panel_mech_can_a(false);
        this.set_can_z(false);
        tut_dtrigger.set_panel_mech_can_freeze(false);
        tut_dtrigger.answer_trigger();
      
    }
    
    
    public void next_dialogue() {
        
        if (max_count > counter) {
            
            //is_marked = tut_img_show.return_is_on_marked_diag(counter);
            counter++;
            if (max_count == counter) {
                check_if_on_diag_counter();
            this.set_can_f(true);
            this.set_can_tab(true);
            this.set_can_z(true);
            tut_dtrigger.set_panel_mech_can_z(true);
            tut_dtrigger.set_panel_mech_can_a(true);
            tut_dtrigger.set_panel_mech_can_d(true);
            tut_dtrigger.panel_mech_change_vocab_word(sample_word);
            tut_dtrigger.panel_mech_vocabulary_panel_switch(true);
            }
            
            
            tut_dtrigger.next_dialogue();
            
        }
       
    }
    void check_if_on_diag_counter() {
        for (int i = 0; i < dialogue_counters.Count; i++) {
            int num_found = dialogue_counters[i];
            if (num_found == counter) {
                
                tutorial_link(i);
                break;
            }
        }
    }
    void tutorial_link(int i) {
        if (tutorial_num == 1) {
            if (i == 0) {
            tut_dtrigger.panel_mech_freeze_button_switch(true);
        }
        else if (i == 1) {
            //if it's on the freeze demo
           
            set_can_g(false);
        
            tut_dtrigger.set_panel_mech_can_z(true);
            set_can_z(true);
            set_is_on_freeze_demo(true);
            tut_dtrigger.set_panel_mech_can_z(true);
            tut_dtrigger.set_panel_mech_can_freeze(true);
            this.set_can_g(false);

        }
        else if (i == 2) {
            tut_dtrigger.panel_mech_highlighter_button_switch(true);
        }
        else if (i == 3) {
            tut_dtrigger.panel_mech_change_vocab_word("Humorous");
            tut_dtrigger.panel_mech_vocabulary_panel_switch(true);
        }
        else {
            //if it's getting synonyms
            set_can_g(false);
        
            tut_dtrigger.set_panel_mech_can_z(true);
            set_can_z(true);
            this.set_can_f(true);
            
            tut_dtrigger.set_panel_mech_can_z(true);
            tut_dtrigger.set_panel_mech_can_freeze(true);
            this.set_can_g(false);
            tut_dtrigger.set_panel_mech_can_a(true);
            tut_dtrigger.set_panel_mech_can_d(true);
            
           
        
        }
        }
        else {
            if (i == 0) {
                Debug.Log("MOSHI MOSHI");
                tut_dtrigger.panel_mech_freeze_button_switch(true);
                tut_dtrigger.panel_mech_highlighter_button_switch(true);
                tut_dtrigger.set_panel_mech_can_freeze(true);
                this.set_can_g(false);
                this.set_can_z(true);
                this.tut_dtrigger.set_panel_mech_can_z(true);
                this.tut_dtrigger.set_panel_mech_can_tab(true);
                this.set_can_tab(true);
                this.set_is_antonym_demo(true);
            }
            else if (i == 1) {
                //show the arrow
                this.set_arrow_active(true);
            }
            else if (i == 2) {
                this.tut_dtrigger.panel_mech_change_vocab_word("Enthusiastic");
                this.tut_dtrigger.panel_mech_vocabulary_panel_switch(true);
            }
            else {
                //actual test
                this.set_can_f(true);
                set_can_g(false);
                set_can_z(true);
                this.set_can_tab(true);
                
                tut_dtrigger.set_panel_mech_can_z(true);
                tut_dtrigger.set_panel_mech_can_freeze(true);
                tut_dtrigger.reset_highlighter();
                tut_dtrigger.set_panel_mech_can_a(true);
                tut_dtrigger.set_panel_mech_can_d(true);
            }
        }
        
    }
    void set_can_tab(bool can) {
        this.can_tab = can;
    }
    public void demo_change_highlighter() {
        bool is_curr_on = tut_dtrigger.get_ison_panel_mech();
        if (is_curr_on) {
            set_is_antonym_demo(false);
            
            
            set_can_g(true);
            
            
            tut_dtrigger.change_context_highlighter();
            tut_dtrigger.freeze_or_defreeze();
            tut_dtrigger.set_panel_mech_can_freeze(false);
            tut_dtrigger.set_panel_mech_can_z(false);
            tut_dtrigger.set_panel_mech_can_tab(false);
            this.set_can_z(false);
            this.set_can_tab(false);
            next_dialogue();
        }
        
    }

    void set_arrow_active(bool active) {
        arrow_pic.SetActive(active);
        arrow_on = active;
    }
    public void yes_or_no_select(int num) {
        this.show_yes_or_no_choice_activate(false);
        conf_start = num * conf_mult;
        this.set_can_g(true);
        this.set_is_on_conf_diag(true);
        if (num == 0) {
            this.set_has_confirm(true);
        }
        else {
            counter = num_go_back;
            tut_dtrigger.set_normal_counter(counter);
        }
        confirm_counter++;
        tut_dtrigger.confirm_trigger_dialogue(conf_start);
    }
    void set_can_g(bool can) {
        can_g = can;
    }
    public void store_result_diag(List<string> result_diag , string speaker) {
        //storing of result dialogues
        tut_dtrigger.store_result_diag(result_diag, speaker);
        //tut_diag_manager.initialize_result_dialogues(result_diag, speaker);
    }
    public void store_after_diag(List<string> aft_diag, List<string> aft_name) {
        //storing of dialogues after the result
        after_counter_max = aft_diag.Count;

        tut_dtrigger.store_after_diag(aft_diag, aft_name);
        /*after_counter = 0;
        after_counter_max = aft_diag.Count;
        tut_diag_manager.initialize_after_dialogues(aft_diag, aft_name);*/
    }
    void set_can_z(bool can) {
        this.can_z = can;
    }
    void freeze_demo_only() {
        freeze_counter_demo++;
        tut_dtrigger.freeze_or_defreeze();
        bool press_z_active = false;
        if (freeze_counter_demo == 2) {
            freeze_counter_demo = 0;
            set_is_on_freeze_demo(false);
            set_can_g(true);
            set_can_z(false);
            tut_dtrigger.set_panel_mech_can_z(false);
            tut_dtrigger.set_panel_mech_can_freeze(false);
            next_dialogue();
        }
        else {
            press_z_active = true;
        }
        tut_dtrigger.freeze_prompt_switch(false);
        Debug.Log("SHOWMAN " + press_z_active);
        tut_dtrigger.press_z_panel_switch(press_z_active);
    }
    void freeze_demo_antonym() {

    }
    public void initialize_dialogue(List<string> dialogue, List<string> names) {
        //tut_img_show.set_is_not_on_dialogue(false);
        
    
        counter = 1;
        max_count = dialogue.Count ;
        tut_dtrigger.set_panel_mech_can_z(false);
        tut_dtrigger.set_after_choose(false);
        tut_dtrigger.set_is_correct(false);
        this.set_can_z(false);
      
       
        //main dialogue codes
        tut_dtrigger.set_can_f(false);
        tut_dtrigger.set_can_g(true);
        tut_dtrigger.initialize_dialogue(dialogue, names);
        tut_dtrigger.panel_mechanic_dialogue_pane_active(true);
        
        /*tut_panel_mech.set_can_tab(false);
        is_active = true;
       is_marked = false;
        cango = true;
        is_correct = false;
        counter = 1;
        
        tut_diag_manager.start_dialogue(dialogue, names);*/

    }
    public void change_active_of_tutorial_diag_trigger(bool active) {
        tut_dtrigger_empty.SetActive(active);
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
    void set_is_on_freeze_demo(bool is_on) {
        this.is_on_freeze_demo = is_on;
    }
    void set_clue_panel_active(bool active) {
        clue_panel.SetActive(active);
    }
    public void set_can_f(bool can) {
        can_f = can;
    }
    public void start_dialogue_on_door() {
        Debug.Log("WOOOAHHHHNALLLL ");
        set_can_g(true);
        is_after_conf = true;
        after_confirm_sequence();
        tut_dtrigger.panel_mechanic_dialogue_pane_active(true);
        after_confirm_counter++;
    }
    public void confirmation_dialogue(List<string> dialogues, List<string> names) {
        
        tut_dtrigger.confirmation_dialogue(dialogues, names);
    }
}
