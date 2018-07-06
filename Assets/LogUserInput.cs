using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class LogUserInput : MonoBehaviour {

    [SerializeField] static GameObject textPrefab;
    static int maxMessageCount;
    static string lastMessage;
    static Transform m_transform;

	// Use this for initialization
	void Start () {
        m_transform = transform;
        textPrefab = m_transform.GetChild(0).gameObject;
        maxMessageCount = 6;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void LogMessage(string message)
    {
        if (LogUserInput.GetLastMessage() == message)
        {
            return;
        }

        GameObject textlog;
        if (m_transform.childCount < maxMessageCount)
        {
            textlog = Instantiate(textPrefab, m_transform);
        }
        else
        {
            textlog = m_transform.GetChild(0).gameObject;
        }
        textlog.GetComponent<Text>().text = message;
        textlog.transform.SetAsLastSibling();

        lastMessage = message;

        using (StreamWriter w = File.AppendText("log.txt"))
        {
            Log(message, w);
        }

    }

    public static string GetLastMessage()
    {
        return lastMessage;
    }

    public static void Log(string logMessage, TextWriter w)
    {
        w.Write("\r\nLog Entry : ");
        w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
            DateTime.Now.ToLongDateString());
        w.WriteLine("  :");
        w.WriteLine("  :{0}", logMessage);
        w.WriteLine("-------------------------------");
    }
}
