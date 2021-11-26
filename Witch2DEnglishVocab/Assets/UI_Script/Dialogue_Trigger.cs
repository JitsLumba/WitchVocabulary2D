using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Dialogue_Manager dialogue_Manager;
    bool canproc = true;
    bool cantrigger = true;
    bool canfreeze = true;

    int counter = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.G) && cantrigger)
        {
            /*if (canproc) {
                Debug.Log("JAMMER");
                Trigger_Dialogue();
            }*/
            if (canproc)
            {
                Trigger_Dialogue();
            }


        }
        else if (Input.GetKeyDown(KeyCode.Z) && cantrigger)
        {
            if (canfreeze)
            {
                if (canproc)
                {
                    Debug.Log("JASM");
                    canproc = false;
                }
                else
                {
                    canproc = true;
                }
                canfreeze = false;
                StartCoroutine(Freeze_Interv());
            }

        }
    }
    public void Trigger_Dialogue()
    {



        //canproc = false;
        if (counter == 0)
        {

            this.dialogue_Manager.StartDialogue(dialogue);
        }
        else
        {
            this.dialogue_Manager.Display_Next_Sentence();
        }
        counter++;
        //StartCoroutine(Dialogue_Interv());

    }
    public void change_dial_vals(List<string> sentences, List<string> names)
    {
        counter = 0;
        dialogue.clear_sentences();

        dialogue.add_dialogues(sentences, names);

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
        canfreeze = true;
    }
}
