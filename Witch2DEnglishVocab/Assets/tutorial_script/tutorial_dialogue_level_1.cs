using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_level_1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_trigger tut_dtrigger ;
    [SerializeField] private tutorial_dialogue_manager tut_diag_manager ;
    [SerializeField] private tutorial_panel_mechanic tut_panel_mech ;
   
    [SerializeField] private tutorial_distance_trigger tut_dist_trigger ;
    [SerializeField] private GameObject tut_dtrigger_empty ;
    [SerializeField] private GameObject tut_level_1_empty ;
    [SerializeField] private GameObject clue_panel ;
    [SerializeField] private GameObject freeze_panel;
    [SerializeField] private GameObject highlighter_panel;

    [SerializeField] private List<int> dialogue_counters;
    private List<string> result_dialogue;
    private List<int> special_numbers_list;
    private int counter = 1;
    private int after_counter = 0;
    private int after_counter_max = 0;
    
    private int max_count = 0;
    private bool cango = false;
    private bool after_choose = false;
    private bool can_z = true, can_l = true;
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

        

        /*if (Input.GetKeyDown(KeyCode.G) && can_z) {
            bool allowed = true;
            bool browse_can = tut_img_show.get_can_browse();
            bool is_showing_img = tut_img_show.return_is_showing_image();
            is_passed = tut_img_show.get_is_passed();
            if (browse_can && is_showing_img) {
                allowed = false;
            }
            
            if (cango && allowed) {
               if (after_choose) {
                   if (is_correct) {
                       //proceed to the next dialogues
                       
                       if (after_counter == after_counter_max) {
                           //end it here
                           after_counter = 0;
                           after_choose = false;
                           is_correct = false;
                           tut_diag_manager.set_active_dialogue_box(false);
                           tut_img_show.set_is_not_on_dialogue(true);
                           cango = false;
                           is_active = false;
                           //tut_img_show.set_can_browse(true);
                           tut_img_show.set_image_counter(0);
                           tut_img_show.set_counter(0);
                           tut_dist_trigger.set_can_interact(true);
                           enable_movement();
                       }
                       else {
                           
                           tut_diag_manager.next_after_dialogue(after_counter);
                           after_counter++;
                       }
                   }
                   else {
                       //last dialogue
                       tut_panel_mech.set_can_freeze(true);
                       
                       tut_panel_mech.set_highlighter_panel_active(true);
                        tut_panel_mech.set_freeze_panel_obj_active(true);
                        
                       tut_diag_manager.next_dialogue(counter - 1);
                       after_choose = false;
                   }
                   
               }
               else {
                   if (is_marked) {
                       is_diag_hidden = true;
                       is_marked = false;
                       tut_panel_mech.set_dialogue_active(false);
                       tut_panel_mech.set_can_browse(false);
                       tut_img_show.set_vocabulary_active(false);
                       tut_img_show.set_can_browse(false);
                       tut_img_show.show_images_within();
                   }
                   else {
                       if (is_diag_hidden) {
                           bool has_passed = tut_img_show.get_is_passed();
                           Debug.Log(has_passed + " US IT");
                           
                           is_diag_hidden = false;
                           
                           tut_img_show.remove_last_image();
                           if (has_passed) {
                               Debug.Log("has_passed");
                               tut_img_show.set_can_browse(true);
                               tut_panel_mech.set_can_browse(true);
                                tut_img_show.set_counter(0);
                           }
                           tut_img_show.set_is_showing_image(false);
                           tut_img_show.set_vocabulary_active(true);
                           tut_panel_mech.set_dialogue_active(true);
                       }
                       next_dialogue();
                   }
                   
               }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && can_l) {
            bool is_on = tut_panel_mech.get_ison();
            if (is_on) {
                check_answer();
            }
        }
        if (Input.GetKeyDown(KeyCode.X) && is_passed && is_active) {
            
           
                key_change_active();
                tut_img_show.show_img_sequence();
                tut_panel_mech.tutorial_dialogue_show();
            
         
            
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && is_passed && is_active) {
            
            bool is_showing = tut_img_show.return_is_showing_image();
            if (is_showing) {
                key_change_active();
                tut_img_show.exit_images();
                tut_panel_mech.tutorial_dialogue_show();
            }
            
        }
        */
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
        can_l = activate;
        can_z = activate;
        tut_panel_mech.key_actives(activate);
    }
    void check_answer() {
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
    
    public void next_dialogue() {
        
        if (max_count > counter) {
            
            //is_marked = tut_img_show.return_is_on_marked_diag(counter);
            
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
    public void store_result_diag(List<string> result_diag , string speaker) {
        //storing of result dialogues
        tut_dtrigger.store_result_diag(result_diag, speaker);
        //tut_diag_manager.initialize_result_dialogues(result_diag, speaker);
    }
    public void store_after_diag(List<string> aft_diag, List<string> aft_name) {
        //storing of dialogues after the result

        tut_dtrigger.store_after_diag(aft_diag, aft_name);
        /*after_counter = 0;
        after_counter_max = aft_diag.Count;
        tut_diag_manager.initialize_after_dialogues(aft_diag, aft_name);*/
    }
    public void initialize_dialogue(List<string> dialogue, List<string> names) {
        //tut_img_show.set_is_not_on_dialogue(false);

        //main dialogue codes
        tut_dtrigger.set_can_z(true);
        tut_dtrigger.initialize_dialogue(dialogue, names);
        /*tut_panel_mech.set_can_tab(false);
        is_active = true;
       is_marked = false;
        cango = true;
        is_correct = false;
        counter = 1;
        max_count = dialogue.Count ;
        tut_diag_manager.start_dialogue(dialogue, names);*/

    }
    void trigger_dialogue() {

    }
    public void confirmation_dialogue(List<string> dialogues, List<string> names) {
        
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
    void set_clue_panel_active(bool active) {
        clue_panel.SetActive(active);
    }
}
