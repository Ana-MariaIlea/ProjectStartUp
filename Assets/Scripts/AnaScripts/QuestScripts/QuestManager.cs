using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    [SerializeField]
    private AudioClip missionComplete;
    [SerializeField]
    private AudioClip missionFail;
    [SerializeField]
    private AudioClip missionDone;

    private AudioSource audio;

    private int maxOrder = 0;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();

        for (int i = 0; i < quests.Length; i++)
        {
            quests[i] = quest.AddQuestEvent(quests[i].name, quests[i].description, quests[i].id);
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

        for (int i = 0; i < quests.Length; i++)
        {

            if (quests[i].order > maxOrder) maxOrder = quests[i].order;
        }

        StartFirstQuest();

        //quest.printPath();
    }

    public void StartFirstQuest()
    {
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i].order == 1)
            {
                quests[i].UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                nameText.text = quests[i].name;
                descriptionText.text = quests[i].description;
                break;
            }

        }
    }

    public void UpdateQuestOnCompletion(QuestEvent e, string status)
    {
        // Debug.Log("Quest change from  "+e.name + "   " + e.order);

        if (e.order == maxOrder)
        {
            nameText.text = "Quests completed";
            audio.PlayOneShot(missionDone);
            descriptionText.text = "";
        }
        else
        {
            foreach (QuestEvent n in quest.questEvents)
            {
                //Debug.Log(n.name+"   "+n.order);
                if (n.order == (e.order + 1))
                {
                    if (n.status == QuestEvent.EventStatus.DONE)
                    {
                        UpdateQuestOnCompletion(n,"c");
                    }
                    else if (n.status == QuestEvent.EventStatus.FAIL)
                    {
                        UpdateQuestOnCompletion(n, "f");
                    }
                    else
                    {
                        if (status == "c")
                        {
                            audio.PlayOneShot(missionComplete);

                        }
                        else if (status == "f")
                        {
                            audio.PlayOneShot(missionFail);
                        }
                        n.UpdateQuestEvent(QuestEvent.EventStatus.CURRENT);
                        nameText.text = n.name;
                        descriptionText.text = n.description;
                        //Debug.Log(n.name+"  curent mission");
                    }
                    // Debug.Log(n.name+"  added");
                }
            }
        }
    }


}
