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
    
    public List<string> name_list, name_list_result, clues, clue_type;
    private List<int> result_stoppers;
    public string result = "";
    public string result_name = "";
  public string name;
  public void samplerun() {
      sentence_list.RemoveAt(0);

      Debug.Log(sentence_list[0]);
  }
  public void clear_result_stoppers() {
      result_stoppers.Clear();
  }
  public int get_clue_num() {
      return clues.Count;
  }
  public void start_list() {
      sentence_list = new List<string>();
      name_list = new List<string>();
      result_list = new List<string>();
      name_list_result = new List<string>();
      remark_list = new List<string>();
      clues = new List<string>();
      clue_type = new List<string>();
      result_stoppers = new List<int>();
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
  public void clear_clue_lists() {
      clues.Clear();
      clue_type.Clear();
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
  public int get_num_remarks() {
      return remark_list.Count;
  }
  public int get_num_results() {
      return result_list.Count;
  }
  public int get_stopper(int ind) {
      return result_stoppers[ind];
  }
  public void add_result_stoppers(List<int> stoppers) {
      for (int i = 0; i < stoppers.Count; i++) {
          result_stoppers.Add(stoppers[i]);
      }
  }
  public string get_clues(int num) {
      return clues[num];
  }
  public string get_clue_type(int num) {
      return clue_type[num];
  }
  public void add_clues(List<string> cluewords, List<string> type_clue) {

      for (int i = 0; i < cluewords.Count; i++) {
      
          clues.Add(cluewords[i]);
          clue_type.Add(type_clue[i]);
      }
      Debug.Log("JOG");
  }
  public string get_remark(int num) {
      return remark_list[num];
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
