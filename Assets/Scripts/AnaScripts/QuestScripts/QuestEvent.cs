using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestEvent 
{
    public enum EventStatus
    {
        WAITING,
        CURRENT,
        DONE,
        FAIL
    }

    public string name;
    public string description;
    public string id;
    [HideInInspector]
    public EventStatus status;
    [HideInInspector]
    public int order=-1;
    [HideInInspector]
    public List<QuestPath> pathList = new List<QuestPath>();

    public QuestEvent(string _name, string _description, string _id)
    {
        id = _id;
        name = _name;
        description = _description;
        status = EventStatus.WAITING;
    }

    public void UpdateQuestEvent(EventStatus es)
    {

        Debug.Log("Quest " + name);
        Debug.Log("Chnage state from "+status);
        status = es;
        Debug.Log("Chnage state to " + status);
    }

    public string getID()
    {
        return id;
    }
 
}
