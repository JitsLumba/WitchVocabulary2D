using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    [TextArea(3, 10)]
   public string[] sentences;
   [TextArea(3, 10)]
    public List<string> sentence_list;
    public List<string> name_list;
  public string name;
  public void samplerun() {
      sentence_list.RemoveAt(0);

      Debug.Log(sentence_list[0]);
  }
  public void clear_sentences() {
      sentence_list.Clear();
      name_list.Clear();
  }
  public void add_dialogues(List<string> senten, List<string> names) {
      for (int i = 0; i < senten.Count; i++) {
          sentence_list.Add(senten[i]);
          name_list.Add(names[i]);
      }
   
  }
}
