using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Panel_Mechanic : MonoBehaviour
{
    [SerializeField] private Image panel;
    
    [SerializeField] private Text text_dialogue;
    [SerializeField] private definition_check dcheck ;
    [SerializeField] private level_return lreturn ;
    private bool ison = false, canfreeze = true, hashighlight = false;
    private string original = "";
    int counter = 0;
    private string highlighted_word = "";
    Color panelcolor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string[] words;
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (canfreeze)
            {
                if (ison)
                {
                    ColorUtility.TryParseHtmlString("#FFFFFF", out panelcolor);
             
                    ison = false;
                    panel.color = panelcolor;
                    text_dialogue.text = original;
                }
                else
                {
                    ColorUtility.TryParseHtmlString("#00F8FA", out panelcolor);
                    original = text_dialogue.text;
                    panel.color = panelcolor;
                    ison = true;
                    words = original.Trim().Split(' ');
                    set_dialogue_box(words, counter);
                    
                   
                }
                canfreeze = false;
                StartCoroutine(Freeze_Interv());
            }

        }
       
       if (Input.GetKeyDown(KeyCode.P) && ison) {
       
            words = original.Trim().Split(' ');
           int num = words.Length - 1;
           
           
           if (counter >= 0) {
               
                   if (counter < num) {
                   counter++;
                }
              
              
              
               
           }
           this.set_dialogue_box(words, counter);
           int beforecounter = counter - 1;
           
            
        }
        else if (Input.GetKey(KeyCode.O) && ison) {
             
            words = original.Trim().Split(' ');
            int num = words.Length;
            if (counter > 0) {
               
                    counter--;
                
               
               
               
               
           }
           
           
           
           this.set_dialogue_box(words, counter);
       
        }

        if (Input.GetKeyDown(KeyCode.L) && ison) {
            compare_answers();
        }
    }
    void compare_answers() {
        string answer = dcheck.get_answer();

        if (answer.Equals(highlighted_word)) {
            Debug.Log("Correct");
            lreturn.start_return();
        }
        else {
            Debug.Log("Incorrect");
        }

    }
    public void set_dialogue_box(string[] words, int beforecounter) {
            highlighted_word = words[beforecounter];
           string highlight = "<color=#09FF00>" + highlighted_word + "</color>";
           
           string new_word = "";
           int reduce = words.Length - 1;
            for (int i = 0; i < words.Length; i++) {
                if (i != beforecounter) {
                    new_word = new_word + words[i];
                }
                else {
                    new_word = new_word + highlight;
                }

                if (i != reduce) {
                    new_word = new_word + " ";
                } 
                
            }
            
            text_dialogue.text = new_word;
    }
    IEnumerator Freeze_Interv()
    {
        yield return new WaitForSeconds(1.0f);
        canfreeze = true;
        Debug.Log("CAN GO");
    }

}
