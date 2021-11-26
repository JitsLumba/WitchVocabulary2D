using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sub_Level_Script : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private List<string> sentences;
    [SerializeField] private List<string> names;
    [SerializeField] private Dialogue_Trigger dtrigger ;
  
    [SerializeField] private string answer;
    [SerializeField] private definition_check dcheck ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_definition() {
        dcheck.set_answer(answer);
    }
    public void set_dialogues() {
        dtrigger.set_freeze(true);
        dtrigger.change_dial_vals(sentences, names);
    }
    
}
