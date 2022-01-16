using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_script : MonoBehaviour
{
    
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_trigger tut_diag_trig ;
    [SerializeField] private tutorial_definition_check tut_def_check ;
    [TextArea(3, 10)]
    [SerializeField] private List<string> dialogues;
    [SerializeField] private List<string> names ;
    [TextArea(3, 10)]
    [SerializeField] private List<string> result_dialogues;

    [SerializeField] private string result_name;
    [TextArea(3, 10)]
    [SerializeField] private List<string> after_dialogue ; 
    [SerializeField] private List<string> after_names ;  
    [SerializeField] private string answer ;
    [SerializeField] private string type_clue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void send_scripts() {
        tut_def_check.set_values(answer, type_clue);
        tut_diag_trig.store_result_diag(result_dialogues, result_name);
        tut_diag_trig.store_after_diag(after_dialogue, after_names);
        tut_diag_trig.initialize_dialogue(dialogues, names);
    }
}
