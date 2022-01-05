using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_Level_Script : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private List<string> sentences, choices, results, remarks, clues, clue_type;
    [SerializeField] private List<string> names, names_result;
    [SerializeField] private Dialogue_Trigger dtrigger ;
    [SerializeField] private Panel_Mechanic pmech ;
 
    [SerializeField] private string answer;
    [SerializeField] private int context_no;
    [SerializeField] private definition_check dcheck ;
    [SerializeField] private int mult = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_definition() {
       dcheck.set_answer(clues, clue_type);
    }
    public void set_dialogues() {
        dtrigger.set_freeze(true);
        dtrigger.set_stop_at(sentences.Count);
        dtrigger.clues_add(clues, clue_type);
        dtrigger.change_dial_vals(sentences, names, choices, results, remarks, names_result, mult);
        
    }
    
}
