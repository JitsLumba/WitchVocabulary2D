using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_adaptor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_level_1 tutorial_dialogue_level_1 ;

    [SerializeField] private GameObject tut_diag_trigger_empty;
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
    public void store_result_diag (List<string>result_dialogues, string result_name) {
        if (level_type == 1) {
            tutorial_dialogue_level_1.store_result_diag(result_dialogues, result_name);
        }
        else {

        }
    }
    public void store_after_diag(List<string> after_dialogue, List<string> after_names) {
        if (level_type == 1) {
            tutorial_dialogue_level_1.change_vocab_panel_active(false);
            tutorial_dialogue_level_1.store_after_diag(after_dialogue, after_names);
        }
        else {
            
        }
    }
    public void initialize_dialogue(List<string> dialogues, List<string> names) {
        if (level_type == 1) {
            tutorial_dialogue_level_1.initialize_dialogue(dialogues, names);
        }
        else {
            
        }
    }
    public void confirmation_dialogue(List<string> dialogues, List<string> names) {
        if (level_type == 1) {

        }
        else {
            
        }
    }
}
