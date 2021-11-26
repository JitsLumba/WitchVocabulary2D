using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transporter_leap 
{
    float camerax, cameray, playerx, playery;
    bool cantransport = false;
   public void set_values(float cam_x, float cam_y, float play_x, float play_y, bool transport) {
       this.camerax = cam_x;
       this.cameray = cam_y;
       this.playerx = play_x;
       this.playery = play_y;
       this.cantransport = transport;

   }
   public void set_false_transport() {
       this.cantransport = false;
   }
   public float get_camerax() {
       return camerax;
   }
   public float get_cameray() {
       return cameray;
   }
   public float get_playerx() {
       return playerx;
   }
   public float get_playery() {
       return playery;
   }
   public bool get_cantransport() {
       return cantransport;
   }
}
