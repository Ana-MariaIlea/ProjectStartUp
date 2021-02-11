using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompletion : MonoBehaviour
{
    [HideInInspector]
    public QuestManager _manager;
    [HideInInspector]
    public QuestEvent _event;

    public void Compltion()
    {
        if (_event.status != QuestEvent.EventStatus.CURRENT) return;

        _event.UpdateQuestEvent(QuestEvent.EventStatus.DONE);
        _manager.UpdateQuestOnCompletion(_event);
    }

    public void Fail()
    {
        if (_event.status != QuestEvent.EventStatus.CURRENT) return;

        _event.UpdateQuestEvent(QuestEvent.EventStatus.FAIL);
        _manager.UpdateQuestOnCompletion(_event);
    }

    public void Setup(QuestManager m, QuestEvent e)
    {
        _manager = m;
        _event = e;
    }
}
