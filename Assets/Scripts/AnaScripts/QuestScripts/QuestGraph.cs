using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGraph 
{
    public List<QuestEvent> questEvents = new List<QuestEvent>();

    public QuestGraph() { }

    public QuestEvent AddQuestEvent(string _name, string _description, string _id)
    {
        QuestEvent questEvent = new QuestEvent(_name, _description, _id);
        questEvents.Add(questEvent);
        return questEvent;
    }

    public void AddPath(string startQuestID, string endQuestID)
    {
        QuestEvent start = FindQuestEvent(startQuestID);
        QuestEvent end = FindQuestEvent(endQuestID);

        if(start!=null && end != null)
        {
            QuestPath path = new QuestPath(start, end);
            start.pathList.Add(path);
        }
    }

    public QuestEvent FindQuestEvent(string id)
    {
        foreach(QuestEvent e in questEvents)
        {
            if (e.getID() == id)
                return e;
        }
        return null;
    }

    public void BFS(string id, int ordernumber = 1)
    {
        QuestEvent thisEvent = FindQuestEvent(id);
        thisEvent.order = ordernumber;
        foreach(QuestPath e in thisEvent.pathList)
        {
            if (e.endEvent.order == -1)
                BFS(e.endEvent.getID(), ordernumber + 1);
        }
    }

    public void printPath()
    {
        foreach(QuestEvent n in questEvents)
        {
            Debug.Log(n.name+"  "+n.order);
        }
    }
  
}
