using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_dialogue_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private tutorial_dialogue_manager tut_diag_manager ;
    private int counter = 1;
    private int max_count = 0;
    private bool cango = false;
    private bool after_choose = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            if (cango) {
                next_dialogue();
            }
        }
    }
    public void next_dialogue() {
        if (max_count > counter) {
            
            counter++;
            Debug.Log("MAX " + max_count + " COUNTER " + counter);
            int index = counter - 1;
            tut_diag_manager.next_dialogue(index);
        }
    }
    public void initialize_dialogue(List<string> dialogue, List<string> names) {
        cango = true;
        counter = 1;
        max_count = dialogue.Count ;
        tut_diag_manager.start_dialogue(dialogue, names);

    }
    void trigger_dialogue() {

    }
}
