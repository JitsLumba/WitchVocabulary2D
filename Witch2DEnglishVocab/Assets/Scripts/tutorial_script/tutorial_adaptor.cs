using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_adaptor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_level_1 tut_dialogue_level_1 ;
    [SerializeField] private tutorial_dialogue_level_2 tut_dialogue_level_2 ;

    [SerializeField] private GameObject tut_diag_trigger_empty;
    [SerializeField] private GameObject tut_diag_lvl_1, tut_diag_lvl_2;
    
    [SerializeField] private int level_type ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_tut_diag_trigger_empty_active(bool active) {
        tut_diag_trigger_empty.SetActive(active);
    }
    public void activate_tutorial_empty(bool active) {
        if (level_type == 1) {
            tut_diag_lvl_1.SetActive(active);
        }
        else {
            tut_diag_lvl_2.SetActive(active);
        }
    }
    public void switch_empty_tutorials(bool activate) {
        set_tut_diag_trigger_empty_active(activate);
        activate_tutorial_empty(activate);
    }
    public void store_result_diag (List<string>result_dialogues, string result_name) {
        if (level_type == 1) {
            tut_dialogue_level_1.store_result_diag(result_dialogues, result_name);
        }
        else {
            tut_dialogue_level_2.store_result_diag(result_dialogues, result_name);
        }
    }
    public void store_after_diag(List<string> after_dialogue, List<string> after_names) {
        if (level_type == 1) {
      
            tut_dialogue_level_1.change_vocab_panel_active(false);
            tut_dialogue_level_1.store_after_diag(after_dialogue, after_names);
        }
        else {
            tut_dialogue_level_2.change_vocab_panel_active(false);
            tut_dialogue_level_2.store_after_diag(after_dialogue, after_names);
        }
    }
    public void initialize_dialogue(List<string> dialogues, List<string> names) {
        if (level_type == 1) {
            tut_dialogue_level_1.initialize_dialogue(dialogues, names);
        }
        else {
            tut_dialogue_level_2.initialize_dialogue(dialogues, names);
        }
    }
    public void confirmation_dialogue(List<string> dialogues, List<string> names) {
        if (level_type == 1) {
            tut_dialogue_level_1.confirmation_dialogue(dialogues, names);
        }
        else {
            tut_dialogue_level_2.confirmation_dialogue(dialogues, names);
        }
    }
    public void play_dialogues_on_door() {
        tut_dialogue_level_1.start_dialogue_on_door();
    }
}
