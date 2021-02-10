using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestGraph quest = new QuestGraph();

    [System.Serializable]
    public class ConnectionID
    {
        public string start;
        public string end;
    }

    [System.Serializable]
    public class CompletionItems
    {
        public GameObject obj;
        public string questID;
    }

    public QuestEvent[] quests;
    public ConnectionID[] connections;
    public CompletionItems[] completion;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            quests[i] = quest.AddQuestEvent(quests[i].name, quests[i].description,quests[i].id);
        }

        for (int i = 0; i < connections.Length; i++)
        {
            bool isStartPresent = false;
            bool isEndPresent = false;

            for (int j = 0; j < quests.Length; j++)
            {
                if (quests[j].id == connections[i].start)
                {
                    isStartPresent = true;
                }

                if (quests[j].id == connections[i].end)
                {
                    isEndPresent = true;
                }
            }

            if (isStartPresent && isEndPresent)
            {
                quest.AddPath(connections[i].start, connections[i].end);
            }
        }

        quest.BFS(quests[0].getID());

        for (int i = 0; i < completion.Length; i++)
        {
            completion[i].obj.GetComponent<QuestCompletion>().Setup(this, quest.FindQuestEvent(completion[i].questID));
        }

        quest.printPath();
    }

    public void UpdateQuestOnCompletion(QuestEvent e)
    {
        foreach (QuestEvent n in quest.questEvents)
        {
            if (n.order == (e.order + 1))
            {
                n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
            }
        }
    }

}
