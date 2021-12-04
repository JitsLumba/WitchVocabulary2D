using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_transport : MonoBehaviour
{
    // Start is called before the first frame update
 
    bool cantransport = false;
    bool cango = true;
    [SerializeField] float camera_x, camera_y, player_x, player_y, second_char_x_transf, second_char_y_rot, return_camera_x, return_player_x, return_scnd_char_x_transf, return_scnd_char_y_rot;
    [SerializeField] private GameObject camdest, playerdest, second_char;

    [SerializeField] private string samp;
    [SerializeField] private string door_num;
    [SerializeField] private Sub_Level_Script subscrpt ;
    [SerializeField] private interval_door invdsy;
    [SerializeField] private bool canchangediag;
    
    [SerializeField] private level_return lreturn ;

    [SerializeField] private CharacterController2D c2d ;
    [SerializeField] private PlayerMovement pmove ;
    bool cantransp = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        cango = invdsy.getInterv();
        
        //Debug.Log("Ryan " + cantransp + " cango " + cango + " samp " + samp);
        if (Input.GetKeyDown(KeyCode.F) && cantransp && cango) {
         
           invdsy.setFalseInterv();
           lreturn.set_return_door(this.door_num);
           lreturn.set_coordinates(return_camera_x, return_player_x, return_scnd_char_x_transf, return_scnd_char_y_rot);
           camdest.transform.position = new Vector3(camera_x, camdest.transform.position.y, camdest.transform.position.z);
           playerdest.transform.position = new Vector2(player_x, playerdest.transform.position.y);
           var rotateVect = second_char.transform.eulerAngles;
           rotateVect.y = this.second_char_y_rot;
           second_char.transform.rotation = Quaternion.Euler(rotateVect);
           second_char.transform.position = new Vector3(second_char_x_transf ,second_char.transform.position.y, second_char.transform.position.z);
           disable_movement();
           if (canchangediag) {
              
               set_script();
           }
           
        }
    }
    void disable_movement() {
        this.c2d.enabled = false;
        this.pmove.enabled = false;
    }
    void set_script() {
        subscrpt.set_definition();
        subscrpt.set_dialogues();
    }
    public void set_transp_bool(bool can_transport) {
        this.cantransp = can_transport;
    }
    
    public void set_values_innit(float cam_x, float play_x,  float second_chr_x, float second_chr_y, bool transport) {
        this.camera_x = cam_x;
        this.player_x = play_x;
     
        this.second_char_x_transf = second_chr_x;
        this.second_char_y_rot = second_chr_y;
        this.cantransp = transport;
    }
    IEnumerator Door_Open() {
        yield return new WaitForSeconds(1.0f);
       Debug.Log("Up " + samp);
        cango = true;
    }
}
