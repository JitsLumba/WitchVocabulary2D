using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class finished_level_check : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player, door, result_panel, f_button, lock_door, open_door;
    [SerializeField] private Text result_text;
    [SerializeField] private Witch_Scene_Manager wscenemanager ;

    [SerializeField] private int needed_complete;
    
    private bool can_press = true, is_not_complete = true;
    private int counter = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerdist = player.transform.position.x, door_dist = door.transform.position.x;
        float dist = playerdist - door_dist;
        if (dist >= -0.5 && dist <= 0.5) {
                f_button.SetActive(true);
            }
        else {
            f_button.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && can_press && is_not_complete) {
            
            if (dist >= -0.5 && dist <= 0.5) {
                complete_check();
            }
        }
    }
    public void completed_count() {
        this.counter++;

        if (counter == needed_complete) {
            lock_door.SetActive(false);
            open_door.SetActive(true);
        }
    }
    public void complete_check() {
        string result_n = "";
        int diff = needed_complete - counter;
        can_press = false;
        if (diff == 0) {
           
            is_not_complete = false;
            result_n = "Completed";
            StartCoroutine(next_level());
        }
        else {
          
            
            result_n = diff + " tasks unfinished";
        }
        
        StartCoroutine(interval(result_n));
    }
    void remove_panel() {
        result_panel.SetActive(false);
        can_press = true;
    }
    IEnumerator interval(string result) {
        result_text.text = result;
        result_panel.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);
        remove_panel();
    }
    IEnumerator next_level() {
        yield return new WaitForSeconds(2.0f);
        wscenemanager.change_scene();
    }
}
