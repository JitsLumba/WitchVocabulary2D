using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogue_panel, invisi_button;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Dialogue_Manager dialogue_Manager;

    [SerializeField] private Panel_Mechanic pmech;
    private int stop_at = 0;
    bool canproc = true;
    bool cantrigger = true;
    bool canfreeze = false;
    bool is_on_choice = false;
    bool after_result = false;
    int counter = 0;
    int multiple = 0, button_select = 0;
    void Start()
    {
        dialogue.start_list();
    }

    // Update is called once per frame
    void Update()
    {

        canfreeze = pmech.get_can_freeze();

        if (Input.GetKeyDown(KeyCode.G) && cantrigger)
        {

            
            Trigger_Dialogue();



        }
        else if (Input.GetKeyDown(KeyCode.Z) && cantrigger)
        {

            if (canfreeze)
            {

                if (canproc)
                {


                    canproc = false;
                }
                else
                {
                    canproc = true;
                }
                cantrigger = false;
                StartCoroutine(Freeze_Interv());
            }

        }
    }
    public void set_freeze(bool freeze)
    {
        this.canfreeze = freeze;
        pmech.set_freeze(freeze);
    }
    public void Trigger_Dialogue()
    {

     
        if (canproc)
        {
            if (is_on_choice)
            {

            }
            else
            {
                counter++;
                if (after_result)
                {
                    if (counter == multiple) {
                        //show result
                        after_result = false;
                        canproc = false;
                        counter = 0;
                        dialogue_panel.SetActive(false);
                        dialogue_Manager.show_result(dialogue.remark_list[button_select]);

                    }
                    else {
                        Debug.Log("AFTER RESULT");
                        this.dialogue_Manager.Display_Next_Sentence();
                    }
                }
                else 
                {
                    if (counter != stop_at)
                    {
                        this.dialogue_Manager.Display_Next_Sentence();
                    }
                    else
                    {
                      
                        is_on_choice = true;
                        counter = 0;
                        dialogue_Manager.set_active_dialogue(false, true);
                    }
                }

            }

        }



        //StartCoroutine(Dialogue_Interv());

    }

    public void change_dial_vals(List<string> sentences, List<string> names, List<string> choices, List<string> results, List<string> remarks, List<string> name_res, int mult)
    {
        this.canproc = true;
        this.multiple = mult;
        dialogue.clear_sentences();
        dialogue.add_remarks(remarks);
        dialogue.add_dialogues(sentences, names);
        dialogue.add_result_dialogues(results, name_res);
        dialogue_panel.SetActive(true);
        invisi_button.SetActive(true);
        dialogue_Manager.set_choices(choices);
        dialogue_Manager.set_mode(1);
        Debug.Log("CLEAR");
        this.dialogue_Manager.StartDialogue(dialogue);

    }
    public void set_trigger()
    {
        this.cantrigger = true;
    }
    public void set_stop_at(int stopper)
    {
        this.stop_at = stopper;
    }

    public void can_trigger_again(int num_button)
    {
        is_on_choice = false;
      
        after_result = true;
        button_select = num_button;
        dialogue_Manager.set_start_end(multiple * num_button, multiple * num_button + multiple);
        dialogue_Manager.set_mode(2);
        dialogue_Manager.set_active_dialogue(true, false);
        dialogue_Manager.StartDialogue(dialogue);
        
        
    }
    IEnumerator Dialogue_Interv()
    {
        yield return new WaitForSeconds(1.50f);

        canproc = true;
    }
    IEnumerator Freeze_Interv()
    {
        yield return new WaitForSeconds(1.0f);
        cantrigger = true;
    }
}
