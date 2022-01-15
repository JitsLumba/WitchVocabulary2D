using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorial_dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private GameObject dialogue_box;
    [SerializeField] private Text dialogue_text, name_text;

    int counter = 0;
    private List<string> dialogue_list, names_list ;
    void Start()
    {
        dialogue_list = new List<string>();
        names_list = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void clear_dialogue_lists() {
        dialogue_list.Clear();
        names_list.Clear();
    }
    void initialize_dialogue_lists(List<string> dialogue, List<string> names) {
     
        for (int i = 0; i < dialogue.Count; i++) {
            dialogue_list.Add(dialogue[i]);
            names_list.Add(names[i]);
        }
    }
    void set_active_dialogue_box(bool active) {
        dialogue_box.SetActive(active);
    }
    public void start_dialogue(List<string> dialogue, List<string> names) {
        clear_dialogue_lists();
       
        initialize_dialogue_lists(dialogue, names);
        
        next_dialogue(0);
        set_active_dialogue_box(true);
    }
    public void next_dialogue(int count) {
        name_text.text = names_list[count];
        dialogue_text.text = dialogue_list[count];
    }
}
