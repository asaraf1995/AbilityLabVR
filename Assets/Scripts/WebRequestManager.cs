using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using WebSocketSharp;


public class WebRequestManager : MonoBehaviour {


	public string serverIPAddress = "127.0.0.1";
    public string port = "3000";
    public int sessionId = 0;

	private WebSocket ws;

	[System.Serializable]
    public class Step
    {
        public float xPos;
        public float zPos;
		public float yRot;
        public bool isIn;
        public int foot;
            
        public Step(float xPos, float zPos, float yRot, bool isIn, int foot)
        {
            this.xPos = xPos;
            this.zPos = zPos;
			this.yRot = yRot;
            this.isIn = isIn;
            this.foot = foot;
        }
    }



	[System.Serializable]
    public class Message
    {
        public String message;

    }

	void Start()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        string input = "";
        for (int i = 0; i < args.Length; i++)
        {
            //Debug.Log("ARG " + i + ": " + args[i]);
            if (args[i] == "-folderInput")
            {
                input = args[i + 1];
            }
        }
        if (args.Length > 1)
            //Debug.Log("user: " + args[1]);

        startNewSession();

        ws = new WebSocket("ws://" + serverIPAddress + ":" + port);

        Message message;

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Server says: " + e.Data);
            message = JsonUtility.FromJson<Message>(e.Data);
            if (message.message == "quit")
            {
                Debug.Log("quitting");
                QuitAppMainThreadCall();            
            }
        };

        ws.OnOpen += (sender, e) => {
            Debug.Log(sender);
            Debug.Log(e);
        };

        ws.Connect();
        //Debug.Log("trying to open socket");
        //ws.Send("message from unity");
    }



	public void startNewSession()
    {
        String PatientId = "fm1";
		string json = "{\"PatientId\" : \""+ PatientId + "\"}";
        StartCoroutine(newSessionRequest(json));
    }

	IEnumerator newSessionRequest(string json)
    {
        string url = "http://" + serverIPAddress + ":" + port;

        var uwr = new UnityWebRequest(url, "GET");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            sessionId = int.Parse(uwr.downloadHandler.text);
        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}

    public void sendStep(float xPos, float yPos, float yRot, bool isIn, int foot)
    {
        Step step = new Step(xPos, yPos, yRot, isIn, foot);
        StartCoroutine(postRequest("api/bridge/showStep", JsonUtility.ToJson(step)));
    }

    
	IEnumerator postRequest(string path, string json)
    {
        string url = "http://" + serverIPAddress + ":" + port + "/" + path;

        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }


	public IEnumerator QuitAppOnTheMainThread()
    {
        Debug.Log("Quitting app from the main thread");
        Application.Quit();

        yield return null;
    }
    public void QuitAppMainThreadCall()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(QuitAppOnTheMainThread());
    }

}