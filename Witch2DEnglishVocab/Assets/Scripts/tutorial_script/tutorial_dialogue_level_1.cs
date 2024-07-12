using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class tutorial_dialogue_level_1 : MonoBehaviour
{
    //DOES CONNECT TO THE TRANSPORT DOOR
    //THIS ONE SHOULD BE THE START OF THE CONNECTION
    //Description of the script: This tutorial is meant for demos with pause at certain counters before proceeding to the test demo
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_trigger tut_dtrigger ;
    [SerializeField] private tutorial_dialogue_manager tut_dmanager ;
    [SerializeField] private tutorial_panel_mechanic tut_panel_mech;

    [SerializeField] private tutorial_character_transport tut_char_transp ;
    [SerializeField] private tutorial_distance_trigger tut_dist_trigger ;
    
    [SerializeField] private tutorial_definition_check tut_def_check;

    [SerializeField] private tutorial_script tut_script;
    [SerializeField] private GameObject tut_diag_lvl_1_empty;
    [SerializeField] private GameObject tut_dtrigger_empty ;
    
    [SerializeField] private GameObject clue_panel ;
    [SerializeField] private GameObject freeze_panel;
    [SerializeField] private GameObject highlighter_panel;
    [SerializeField] private GameObject yes_or_no_panel;
    [SerializeField] private GameObject arrow_pic ;

    [TextArea(3, 10)]
    [SerializeField] private List<string> confirm_dialogues;

    [SerializeField] private List<string> confirm_names;
    //MOVE THIS TO THE TUTORIAL MODEL
    [TextArea(3, 10)]
    [SerializeField] private List<string> after_confirm_dialogues;
    [SerializeField] private List<string> after_confirm_names;
    [SerializeField] private List<int> dialogue_counters, after_counters;
    
    [SerializeField] private int num_go_back;
    [SerializeField] private int conf_mult;
    [SerializeField] private bool has_confirmation = true;
    [SerializeField] private int tutorial_num ;
    
  
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

    private bool is_clue_panel_active = false;
    void Start()
    {

        //pink
        //FF0FAB
        initialize_tutorial_sequence();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.G) && can_g) {
            if (is_clue_panel_active) {
                is_clue_panel_active = false;
                clue_panel.SetActive(false);
            }
            //for level 2
            if (arrow_on) {
                this.set_arrow_active(false);
            }
            if (is_on_conf_diag) {
                Debug.Log("LOOK CAREFULLY CONFIRM");
                if (conf_mult == confirm_counter) {
                    Debug.Log("confirmed can move");
                  
                    this.set_can_g(false);
                    enable_movement();
                        
                    confirm_counter = 0;
                    tut_panel_mech.set_dialogue_active(false);
                    tut_dist_trigger.set_can_interact(true);

                  
                    
                    set_is_on_conf_diag(false);

                    confirm_counter = 0;
                }
                else {
                    //this is the 2nd dialogue on the confirm
                    Debug.Log("WHAT IS THIS CONFIRM PERHAPS?");
                    confirm_counter++;
                    int conf_num = conf_start + confirm_counter;
                    if (has_confirmation) {
                        on_confirm_effects(conf_num);
                    }
                    
                    tut_dtrigger.confirm_trigger_dialogue(conf_num);
                    
                }
                Debug.Log("COUNTERS " + conf_mult + " vs confirm_counter " + confirm_counter);
            }
            else if (is_after_conf) {
                if (after_confirm_counter == after_confirm_max) {
                    //end it here
                    Debug.Log("END CONFIRM AFTER");
                    is_after_conf = false;
                    enable_movement();
                    
                    after_confirm_counter = 0;
                    tut_panel_mech.set_dialogue_active(false);
                    tut_char_transp.transport();
                    tut_dtrigger_empty.SetActive(false);
                    tut_diag_lvl_1_empty.SetActive(false);
                    //tut_dist_trigger.set_can_interact(true);
                }
                else {
                    Debug.Log("Not yet end after");
                    tut_dtrigger.after_confirm_trigger_dialogue(after_confirm_counter);
                    after_confirm_counter++;
                }
            }
            else {
                bool after_choose = tut_dtrigger.get_after_choose();
                Debug.Log("CONFIRM_COUNTER RECHECK " + confirm_counter);
                if (after_choose) {
                    //this pops up the yes or no buttons
                    Debug.Log("AFTER_CHOOSE TRUE ");
                    bool is_correct = tut_dtrigger.get_is_correct();
                    if (is_correct) {
                        
                        Debug.Log("AFTER CHOOSE IS_CORRECT IS TRUE");
                        if (after_counter == after_counter_max) {
                            //before the yes or no selection
                            this.set_can_g(false);
                            after_counter = 0;
                            tut_dtrigger.set_after_choose(false);
                            if (has_confirmation) {
                                Debug.Log("THIS MAY POP UP THE CHOICES");
                                this.show_yes_or_no_choice_activate(true);
                            
                            }
                            else {
                                Debug.Log("THIS MAY NOT POP UP THE CHOICES");
                                tut_dtrigger.reset_highlighter();
                                tut_panel_mech.set_dialogue_active(false);
                                enable_movement();
                    
                  
                 
                 
                                tut_dist_trigger.set_can_interact(true);

                            }
                        
                        }
                    else {
                        //THIS IS THE START OF THE DIALOGUE BEFORE THE CONFIRM POPUP
                        Debug.Log("THIS ONE IS BEFORE THE CHOOSE EFFECTS");
                        tut_dtrigger.after_diag_trigger(after_counter);
                        if (tutorial_num == 2) {
                            this.after_choose_effects();
                        }
                        after_counter++;
                    }
                }
                else {
                    Debug.Log("THIS ONE IS THE LAST DIALOGUE");
                    //last dialogue
                    this.set_can_f(false);
                    tut_dtrigger.set_panel_mech_can_a(true);
                    tut_panel_mech.set_can_freeze(true);
                    tut_dtrigger.set_panel_mech_can_d(true);
                    tut_dtrigger.set_panel_mech_can_tab(true);
                    this.set_can_tab(true);
                    this.set_can_z(true);
                    tut_dtrigger.incorrect_return(counter);
                }

            }
            else {
                Debug.Log("TUTORIAL NORMAL NEXT");
                next_dialogue();
                
            }
        }
            
        }
        if (Input.GetKeyDown(KeyCode.Z) && can_z) {
            
            freeze_tutorial_sequence();
        }
        if (Input.GetKeyDown(KeyCode.F) && can_f) {
            Debug.Log("MONSERSs ");
            tut_dtrigger.show_me_now();
            check_answer();
            
            set_can_g(true);
            

            
        }

        if (Input.GetKeyDown(KeyCode.Tab) && can_tab) {
            //if it's on freeze and doing the demo
            change_highlighter_trigger();
        }

        

        
    }
    void disable_movement() {
        //disables character movement
        CharacterController2D charsub2d;
        PlayerMovement pmovement ;
        GameObject player_search = GameObject.Find("Player");
        if (player_search != null) {
            
            charsub2d = player_search.GetComponent<CharacterController2D>();
            pmovement = player_search.GetComponent<PlayerMovement>();
            charsub2d.enabled = false;
            pmovement.enabled = false;
        }
        /*play_move.enabled = false;
        controller_character.enabled = false;*/
    }
    void initialize_tutorial_sequence() {
        
        
        //THIS HERE IS THE START OF THE TUTORIAL SEQUENCE
        tut_dmanager.initialize_all_lists();
        disable_movement();
        tut_def_check.set_values(tut_script.get_answer(), tut_script.get_type_clue());
        change_active_of_tutorial_diag_trigger(true);
        List<string> result_diags = new List<string>();
       
        for (int i = 0; i < tut_script.get_size_of_result_dialogue(); i++) {
            result_diags.Add(tut_script.get_result_dialogue(i));
            
        }
        store_result_diag(result_diags, tut_script.get_result_name());
        List<string> after_diags = new List<string>();
        List<string> after_names = new List<string>();
        for (int i = 0; i < tut_script.get_after_dialogue_size(); i++) {
            after_diags.Add(tut_script.get_after_dialogue_sentence(i));
            after_names.Add(tut_script.get_after_name(i));
        }
        
        store_after_diag(after_diags, after_names);
        List<string> initial_dialogue = new List<string>();
        List<string> initial_names = new List<string>();

        for (int i = 0; i < tut_script.get_initial_dialogue_size(); i++) {
            initial_dialogue.Add(tut_script.get_initial_dialogue_sentence(i));
            initial_names.Add(tut_script.get_initial_name(i));
        }
        initialize_dialogue(initial_dialogue, initial_names);
        /*
        
        tu_adapt.set_tut_diag_trigger_empty_active(true);
        tu_adapt.activate_tutorial_empty(true);
        tu_adapt.store_result_diag(result_dialogues, result_name);
        tu_adapt.store_after_diag(after_dialogue, after_names);
        tu_adapt.initialize_dialogue(dialogues, names);
        */
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
            tut_panel_mech.change_vocab_text("Exacerbate");
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
                Debug.Log("NOW CHANGING TO THIS");
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
                tut_panel_mech.change_vocab_text("Assuage");
            }
            else {
                tut_panel_mech.change_vocab_text("Exacerbate");
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
                Debug.Log("CAN I CHECK F DEMO" + can_f);
            }
            else {
                
                this.set_can_f(!can_f);
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
        
        Debug.Log("WONSERSs ");
        tut_dtrigger.show_me_now();
        set_can_f(false);
        this.set_can_tab(false);
        tut_dtrigger.set_panel_mech_can_d(false);
        tut_dtrigger.set_panel_mech_can_a(false);
        this.set_can_z(false);
        tut_panel_mech.set_can_freeze(false);
        tut_dtrigger.answer_trigger();
        tut_dtrigger.set_panel_mech_can_tab(false);
      
    }
    
    
    public void next_dialogue() {
        //goes to the next dialogue
        if (max_count > counter) {
            
            //is_marked = tut_img_show.return_is_on_marked_diag(counter);
            check_if_on_diag_counter();
            Debug.Log("NEXT DIALOGUE COUNTER IN TUTORIAL IS " + counter);
            counter++;
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
            //for level 1 only with synonym only
            //this shows the freeze button in level 1
            if (i == 0) {
                tut_panel_mech.change_freeze_panel_active(true);
            }
            else if (i == 1) {
                //if it's on the freeze demo
                //stops the player from pressing g so that they will do demo
                Debug.Log("CAN I CHECK F 1" + can_f);
                set_can_g(false);
        
                tut_dtrigger.set_panel_mech_can_z(true);
                set_can_z(true);
                set_is_on_freeze_demo(true);
                tut_dtrigger.set_panel_mech_can_z(true);
                tut_panel_mech.set_can_freeze(true);
                this.set_can_g(false);

            }
            else if (i == 2) {
                Debug.Log("CAN I CHECK F 2" + can_f);
                tut_panel_mech.change_highlighter_panel_active(true);
            }
            else if (i == 3) {
                Debug.Log("CAN I CHECK F 3 " + can_f);
                this.clue_panel.SetActive(true);
                this.is_clue_panel_active = true;
            }
            else if (i == 4) {
            
                tut_panel_mech.change_vocab_text("Humorous");
                tut_panel_mech.change_vocab_panel_active(true);
                Debug.Log("CAN I CHECK F 4 " + can_f);
            }
            else {
                //if it's getting synonyms
                
                set_can_g(false);
                
                tut_dtrigger.reset_highlighter();
                
                tut_dtrigger.set_panel_mech_can_z(true);
                set_can_z(true);
                
                
                tut_dtrigger.set_panel_mech_can_z(true);
                tut_panel_mech.set_can_freeze(true);
                this.set_can_g(false);
                tut_dtrigger.set_panel_mech_can_a(true);
                tut_dtrigger.set_panel_mech_can_d(true);
                Debug.Log("WHY IS THIS F " + can_f);
           
        
            }
        }
        else {
            //for antonym level 2
            if (i == 0) {
                Debug.Log("MOSHI MOSHI");
                
                tut_panel_mech.change_highlighter_panel_active(true);
                
                this.set_can_g(false);
              
               
                this.tut_dtrigger.set_panel_mech_can_tab(true);
                this.set_can_tab(true);
                this.set_is_antonym_demo(true);
            }
            else if (i == 1) {
                //show the arrow
                this.set_arrow_active(true);
            }
            else if (i == 2) {
                tut_panel_mech.change_vocab_text("Enthusiastic");
                tut_panel_mech.change_vocab_panel_active(true);
            }
            else {
                //actual test
                
                tut_dtrigger.reset_highlighter();
                tut_panel_mech.change_freeze_panel_active(true);
                
                set_can_g(false);
                set_can_z(true);
                this.set_can_tab(true);
                tut_dtrigger.set_panel_mech_can_tab(true);
                tut_dtrigger.set_panel_mech_can_z(true);
                tut_panel_mech.set_can_freeze(true);
                
                tut_dtrigger.set_panel_mech_can_a(true);
                tut_dtrigger.set_panel_mech_can_d(true);
            }
        }
        
    }
    void set_can_tab(bool can) {
        
        this.can_tab = can;
    }
    public void demo_change_highlighter() {
       
            set_is_antonym_demo(false);
            
            
            set_can_g(true);
            
            
            tut_dtrigger.change_context_highlighter();
            
            tut_panel_mech.set_can_freeze(false);
            tut_dtrigger.set_panel_mech_can_z(false);
            tut_dtrigger.set_panel_mech_can_tab(false);
            this.set_can_z(false);
            this.set_can_tab(false);
            next_dialogue();
        
        
    }

    void set_arrow_active(bool active) {
        arrow_pic.SetActive(active);
        arrow_on = active;
    }
    public void yes_or_no_select(int num) {
        this.show_yes_or_no_choice_activate(false);
        conf_start = 0;
        this.set_can_g(true);
        this.set_is_on_conf_diag(true);
        if (num == 0) {
            this.set_has_confirm(true);
            if (has_confirmation) {
                on_confirm_effects(num);
            }
            tut_dtrigger.confirm_trigger_dialogue(conf_start);
            Debug.Log("CONFIRMED " + is_on_conf_diag);
        }
        else {
            counter = num_go_back;
            tut_dtrigger.set_normal_counter(counter);
            tut_panel_mech.change_vocab_panel_active(false);
            is_on_conf_diag = false;
            next_dialogue();
        }
        
        
    }
    void set_can_g(bool can) {
        can_g = can;
    }
    public void tutorial_diag_initialize_list_call() {
        
    }
    public void store_result_diag(List<string> result_diag , string speaker) {
        //storing of result dialogues
        tut_dmanager.initialize_result_dialogues(result_diag, speaker);
        //tut_diag_manager.initialize_result_dialogues(result_diag, speaker);
    }
    public void store_after_diag(List<string> aft_diag, List<string> aft_name) {
        //storing of dialogues after the result
        after_counter_max = aft_diag.Count;
        tut_dtrigger.initialize_after_diag_numbers(after_counter_max);
        tut_dmanager.initialize_after_dialogues(aft_diag, aft_name);
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
            tut_panel_mech.set_can_freeze(false);
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
        after_confirm_max = after_confirm_dialogues.Count;
    
        counter = 1;
        max_count = dialogue.Count ;
        tut_panel_mech.set_can_z(false);
        tut_dtrigger.set_after_choose(false);
     
        this.set_can_z(false);
        tut_dtrigger.initialize_conirm_dialogue_numbers(confirm_dialogues.Count);
        tut_dmanager.confirmation_dialogue(confirm_dialogues, confirm_names);
        tut_dmanager.initialize_after_confirm_diag(after_confirm_dialogues, after_confirm_names);
        //main dialogue codes
        tut_dtrigger.set_can_f(false);
        tut_dtrigger.set_can_g(true);

        tut_panel_mech.set_can_tab(false);
        tut_panel_mech.set_can_f(false);
        tut_panel_mech.set_can_a(false);
        tut_panel_mech.set_can_d(false);
        tut_panel_mech.set_counter(0);

        tut_dtrigger.initialize_dialogue_values(dialogue.Count);
        tut_dmanager.start_dialogue(dialogue, names);
        tut_panel_mech.set_dialogue_active(true);
        
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
    public void confirmation_dialogue(List<string> dialogues, List<string> names) {
        //ERASE
        tut_dtrigger.confirmation_dialogue(dialogues, names);
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
    void on_confirm_effects(int num) {
        for (int i = 0; i < after_counters.Count; i++) {
            int num_show = after_counters[i];

            if (num_show == num) {
                on_confirm_show_effect(i);
                break;
            }
        }
    }
    void on_confirm_show_effect(int i) {
        if (i == 0) {
            tut_panel_mech.change_vocab_text("Assuage");
        }
    }
    public void set_can_f(bool can) {
        can_f = can;
    }
    public void start_dialogue_on_door() {
        //before player is transported dialogue
        Debug.Log("WOOOAHHHHNALLLL ");
        set_can_g(true);
        is_after_conf = true;
        tut_dtrigger.after_confirm_trigger_dialogue(after_confirm_counter);
        tut_panel_mech.set_dialogue_active(true);
        after_confirm_counter++;
    }
}
