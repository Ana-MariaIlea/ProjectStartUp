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

    public QuestEvent[] quests;
    public ConnectionID[] connections;
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

        quest.printPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
