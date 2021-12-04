using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue_Manager : MonoBehaviour
{
    [SerializeField] private Text name_text, dial_text, choice1, choice2, choice3, result;

    [SerializeField] private GameObject panel, choicepanel, result_obj;

    [SerializeField] private level_return lreturn;
   private Queue<string> sentences, nameq;
   private string result_txt = "";
   private int counter = 0;
    private int mode = 1, start = 0, end = 0;
   private Dialogue dial_1;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        nameq = new Queue<string>();
        sentences.Enqueue("Hello");
        nameq.Enqueue("Elaina");
        //DELETE ONCE CLEARED GET BACK TO THIS LATER
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_result(string res) {
        this.result_txt = res;
    }
    public void temp_check(Dialogue dial ) {
        Debug.Log(dial.sentence_list.Count);
    }
    public void StartDialogue(Dialogue dialogue) {
       
            sentences.Clear();
            nameq.Clear();
      
        
        if (mode == 1) {
             for (int i = 0; i < dialogue.sentence_list.Count; i++) {
            
                sentences.Enqueue(dialogue.sentence_list[i]);
                nameq.Enqueue(dialogue.name_list[i]);
          
            
           
            
            }
        }
       else if (mode == 2) {
      
           for (int i = start; i < end; i++) {
            
                sentences.Enqueue(dialogue.result_list[i]);
                nameq.Enqueue(dialogue.name_list_result[i]);

            
           
            
            }
       }
        
        dial_1 = dialogue;
        
        
        Display_Next_Sentence();
    }
    public void set_choices(List<string> choices) {
        choice1.text = choices[0];
        choice2.text = choices[1];
        choice3.text = choices[2];
    }
    public void Display_Next_Sentence() {
        //Debug.Log("Player " + dial_1.name);
        counter++;
        
        if (sentences.Count == 0) {
            //Debug.Log("End of sentence queue");
            if (mode == 2) {
                mode = 1;
            }
            else {
                StartDialogue(dial_1);
            }
            
        }
        else {
            string sentence = sentences.Dequeue(), name = nameq.Dequeue();
            
            //Debug.Log(sentence);
            name_text.text = name;
            dial_text.text = sentence;
        }
        Debug.Log("DISPLAY " + sentences.Count);
    }
    public void set_start_end(int st, int ed) {
        this.start = st;
        this.end = ed;
    }
    
    public void set_active_dialogue(bool dialogue_active, bool choice_active) {
        panel.SetActive(dialogue_active);
        choicepanel.SetActive(choice_active);
    }
    public void show_result(string result) {
        //set_active_dialogue(true);
        mode = 1;
        this.result.text = result;
        result_obj.SetActive(true);
        StartCoroutine(result_pop());
        Debug.Log(result);
    }
    public void set_mode(int num) {
        this.mode = num;
    }
    public void return_level() {
        result_obj.SetActive(false);
        lreturn.start_return();
    }
    IEnumerator result_pop() {
        yield return new WaitForSeconds(1.0f);
        return_level();
    }
    
    
}
