using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPath 
{
    public QuestEvent startEvent;
    public QuestEvent endEvent;

    public QuestPath(QuestEvent start, QuestEvent end)
    {
        startEvent = start;
        endEvent = end;
    }
}
