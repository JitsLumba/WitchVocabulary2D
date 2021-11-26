using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_return : MonoBehaviour
{
    [SerializeField] private GameObject camera, player, secondperson;
    private List<string> door_lists;
    private float camx, player_x, rotate_y_second_person, second_person_x;
    private bool can_return = true;
    private string door_return_name;
    // Start is called before the first frame update
    void Start()
    {
        door_lists = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < door_lists.Count; i++) {
            int num = i + 1;
            Debug.Log("Door " + num + ": " + door_lists[i]);
        }
    }
    public void set_return_door(string ret_door) {
        this.door_return_name = ret_door;
    }
    public void set_coordinates(float camera_x, float play_x, float second_ps_x, float second_ps_y_rot) {
        camx = camera_x;
        player_x = play_x;
        second_person_x = second_ps_x;
        this.rotate_y_second_person = second_ps_y_rot;
        
    }
    public void add_door(string door_n) {
        door_lists.Add(door_n);
    }
    public void start_return() {
        add_door(this.door_return_name);
        
        StartCoroutine(interval());
    }
    public bool get_can_return(string door_type) {
        this.can_return = true;
        for (int i = 0; i < door_lists.Count; i++) {
            string door_name = door_lists[i];
            if (door_name.Equals(door_type)) {
                this.can_return = false;
                break;
            }
        }
        return this.can_return;
    }
    public void return_objects() {

        this.camera.transform.position = new Vector3(camx, this.camera.transform.position.y, this.camera.transform.position.z);

        var rotater_second = secondperson.transform.eulerAngles;
        rotater_second.y = rotate_y_second_person;
        this.secondperson.transform.rotation = Quaternion.Euler(rotater_second);

        this.secondperson.transform.position = new Vector2(second_person_x, this.secondperson.transform.position.y);
        
        this.player.transform.position = new Vector2(player_x, this.player.transform.position.y);
    }
    IEnumerator interval() {
        yield return new WaitForSeconds(3.0f);
        return_objects();
    }
    
}
