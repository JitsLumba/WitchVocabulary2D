using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue_Manager : MonoBehaviour
{
    [SerializeField] private Text name_text, dial_text;
    
   private Queue<string> sentences, nameq;
   private Dialogue dial_1;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        nameq = new Queue<string>();

        //DELETE ONCE CLEARED GET BACK TO THIS LATER
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartDialogue(Dialogue dialogue) {
        sentences.Clear();
 
        for (int i = 0; i < dialogue.sentence_list.Count; i++) {
            sentences.Enqueue(dialogue.sentence_list[i]);
            nameq.Enqueue(dialogue.name_list[i]);
        }
        
        dial_1 = dialogue;
        
        
        Display_Next_Sentence();
    }
    public void Display_Next_Sentence() {
        //Debug.Log("Player " + dial_1.name);

        if (sentences.Count == 0) {
            //Debug.Log("End of sentence queue");
            StartDialogue(dial_1);
        }
        else {
            string sentence = sentences.Dequeue(), name = nameq.Dequeue();
            
            //Debug.Log(sentence);
            name_text.text = name;
            dial_text.text = sentence;
        }
    }
}
