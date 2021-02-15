using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletion : MonoBehaviour
{
    [HideInInspector]
    public QuestManager _manager;
    [HideInInspector]
    public QuestEvent _event;

    public enum TypeOfQuest
    {
        Item,
        Kill,
        Escort,
        Location
    }

    public TypeOfQuest type;

    public void Compltion()
    {
        if (_event.status != QuestEvent.EventStatus.CURRENT)
        {
            if (_event.status != QuestEvent.EventStatus.DONE && _event.status != QuestEvent.EventStatus.FAIL&&type!=TypeOfQuest.Location)
                _event.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
            return;
        }

        _event.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        _manager.UpdateQuestOnCompletion(_event);
        Debug.Log(_event.name + "  done");
    }

    public void Fail()
    {
        if (_event.status != QuestEvent.EventStatus.CURRENT)
        {
            if (_event.status != QuestEvent.EventStatus.DONE && _event.status != QuestEvent.EventStatus.FAIL)
                _event.UpdateQuestEvent(QuestEvent.EventStatus.FAIL);
            return;
        }

        _event.UpdateQuestEvent(QuestEvent.EventStatus.FAIL);
        _manager.UpdateQuestOnCompletion(_event);
        Debug.Log(_event.name + "  fail");
    }

    public void Setup(QuestManager m, QuestEvent e)
    {
        _manager = m;
        _event = e;
    }
}
