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
        DONE
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
        status = es;
    }

    public string getID()
    {
        return id;
    }
 
}
