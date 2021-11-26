using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dialogue_panel, invisi_button;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Dialogue_Manager dialogue_Manager;
   
    [SerializeField] private Panel_Mechanic pmech ;
    bool canproc = true;
    bool cantrigger = true;
    bool canfreeze = false;

    int counter = 0;
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
    public void set_freeze(bool freeze) {
        this.canfreeze = freeze;
        pmech.set_freeze(freeze);
    }
    public void Trigger_Dialogue()
    {


        if (canproc) {
            this.dialogue_Manager.Display_Next_Sentence();
        }
        
            
       
        //StartCoroutine(Dialogue_Interv());

    }
    public void change_dial_vals(List<string> sentences, List<string> names)
    {
        this.canproc = true;
        dialogue.clear_sentences();

        dialogue.add_dialogues(sentences, names);
        dialogue_panel.SetActive(true);
        invisi_button.SetActive(true);
        this.dialogue_Manager.StartDialogue(dialogue);

    }
    public void set_trigger()
    {
        this.cantrigger = true;
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
