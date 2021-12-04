using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    [TextArea(3, 10)]
   public string[] sentences;
   [TextArea(3, 10)]
    public List<string> sentence_list, result_list, remark_list;
    
    public List<string> name_list, name_list_result;
    public string result = "";
    public string result_name = "";
  public string name;
  public void samplerun() {
      sentence_list.RemoveAt(0);

      Debug.Log(sentence_list[0]);
  }
  public void start_list() {
      sentence_list = new List<string>();
      name_list = new List<string>();
      result_list = new List<string>();
      name_list_result = new List<string>();
      remark_list = new List<string>();
      sentence_list.Add("SAMPLE");
      name_list.Add("Player");
  }
  public void clear_sentences() {
      sentence_list.Clear();
      name_list.Clear();
      result_list.Clear();
      name_list_result.Clear();
      remark_list.Clear();
  }
  public void set_result(int num) {
      this.result = result_list[num];
      this.result_name = name_list_result[num];
  }
  public void add_remarks(List<string> remarks) {
      for (int i = 0; i < remarks.Count; i++) {
          remark_list.Add(remarks[i]);
      }
  }
  public void add_result_dialogues(List<string> senten, List<string> names) {
      for (int i = 0; i < senten.Count; i++) {
          result_list.Add(senten[i]);
          name_list_result.Add(names[i]);
      }
  }
  public void add_dialogues(List<string> senten, List<string> names) {
      for (int i = 0; i < senten.Count; i++) {
          sentence_list.Add(senten[i]);
          name_list.Add(names[i]);
      }
   
  }
}
