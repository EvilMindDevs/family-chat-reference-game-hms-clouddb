using HmsPlugin;
using HuaweiMobileServices.AuthService;
using HuaweiMobileServices.Base;
using HuaweiMobileServices.CloudDB;
using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;


public class MessageManager : MonoBehaviour
{
    private readonly string TAG = "FamilyChat";

    [SerializeField]
    List<Message> messages = new List<Message>();

    public GameObject chatPanel, textObject;
    public InputField chatInput;

    // Auth Service
    private HMSAuthServiceManager authServiceManager = null;
    private AGConnectUser user = null;


    // Cloud DB
    private HMSCloudDBManager cloudDBManager = null;
    private List<ChatAppOT> ChatAppList = null;
    private CloudDBZone cloudDBZone = null;

    private readonly string CloudDBZoneName = "ChatAppCloudZone";
    private readonly string ChatAppClass = "com.clouddbdemo.kb.huawei.ChatAppOT";
    private readonly string ObjectTypeInfoHelper = "com.clouddbdemo.kb.huawei.ObjectTypeInfoHelper";

    void Start()
    {
        authServiceManager = HMSAuthServiceManager.Instance;

        if (authServiceManager.GetCurrentUser() != null)
        {
            user = authServiceManager.GetCurrentUser();
        }

        cloudDBManager = HMSCloudDBManager.Instance;
        cloudDBManager.Initialize();
        cloudDBManager.GetInstance();
        cloudDBManager.OnExecuteQuerySuccess = OnExecuteQuerySuccess;
        cloudDBManager.OnExecuteQueryFailed = OnExecuteQueryFailed;
        cloudDBManager.OnCloudDBZoneSnapshot = OnExecuteQuerySuccess;
        cloudDBManager.OnCloudDBZoneSnapshotException = OnCloudDBZoneSnapshotException;
        cloudDBManager.OnOpenCloudDBZone2Success = OnOpenCloudDBZone2Success;
        cloudDBManager.OnOpenCloudDBZone2Failed = OnOpenCloudDBZone2Failed;
        cloudDBManager.OnExecuteUpsertFailed = OnExecuteUpsertFailed;
        cloudDBManager.CreateObjectType(ObjectTypeInfoHelper);
        IList<CloudDBZoneConfig> CloudDBZoneConfigs = cloudDBManager.GetCloudDBZoneConfigs();
        cloudDBManager.OpenCloudDBZone2(CloudDBZoneName, 
            CloudDBZoneConfig.CloudDBZoneSyncProperty.CLOUDDBZONE_CLOUD_CACHE, 
            CloudDBZoneConfig.CloudDBZoneAccessProperty.CLOUDDBZONE_PUBLIC);
    }

    private void OnExecuteUpsertFailed(HMSException error) => Debug.Log($"{TAG} OnExecuteQueryFailed(HMSException error) => {error.WrappedExceptionMessage}");

    private void OnOpenCloudDBZone2Failed(HMSException error) => Debug.Log($"{TAG} OnExecuteQueryFailed(HMSException error) => {error.WrappedExceptionMessage}");

    private void OnOpenCloudDBZone2Success(CloudDBZone obj)
    {
        cloudDBZone = obj;
        CloudDBZoneQuery mCloudQuery = CloudDBZoneQuery.Where(new AndroidJavaClass(ChatAppClass)).EqualTo("shadowFlag", true);
        cloudDBManager.SubscribeSnapshot(mCloudQuery, CloudDBZoneQuery.CloudDBZoneQueryPolicy.CLOUDDBZONE_CLOUD_CACHE);
    }

    private void OnExecuteQuerySuccess(CloudDBZoneSnapshot<ChatAppOT> snapshot) => ProcessQueryResult(snapshot);

    private void OnExecuteQueryFailed(HMSException error) => Debug.Log($"{TAG} OnExecuteQueryFailed(HMSException error) => {error.WrappedExceptionMessage}");

    private void OnCloudDBZoneSnapshotException(AGConnectCloudDBException error) => Debug.Log($"{TAG} OnExecuteQueryFailed(HMSException error) => {error.ErrMsg}");

    private void ProcessQueryResult(CloudDBZoneSnapshot<ChatAppOT> snapshot)
    {
        CloudDBZoneObjectList<ChatAppOT> chatAppOTCursor = snapshot.GetSnapshotObjects();
        ChatAppList = new List<ChatAppOT>();
        try
        {
            while (chatAppOTCursor.HasNext())
            {
                ChatAppOT chat = chatAppOTCursor.Next();
                ChatAppList.Add(chat);
            }
            generateMessage();
        }
        catch (Exception e)
        {
            Debug.Log($"{TAG}  processQueryResult:  Exception => " + e.Message);
        }
        finally
        {
            snapshot.Release();
        }
    }

    public void clickButton()
    {
        if (chatInput.text == "") return;
        sendMessage(chatInput.text);
        chatInput.text = "";
    }

    void sendMessage(string text)
    {
        ChatAppOT chat = new ChatAppOT();
        chat.Id = Guid.NewGuid().ToString();
        chat.UserId = user.IsAnonymous() ? "Guest" : user.DisplayName;
        chat.Message = text;
        chat.Date = DateTime.Now;
        cloudDBManager.ExecuteUpsert(chat);
    }

    void generateMessage()
    {
        foreach (ChatAppOT chat in ChatAppList)
        {
            bool x = false;
            foreach (Message _message in messages)
            {
                if (_message.id == chat.Id) {
                    x = true;
                    continue; }
            }
            if (x) continue;

            Message message = new Message();
            message.id = chat.Id;
            message.text = $"{chat.UserId} ({chat.Date.ToString("h:mm tt")}): {chat.Message} ";
            GameObject gameObject = Instantiate(textObject, chatPanel.transform);
            message.textObject = gameObject.GetComponent<Text>();
            message.textObject.text = $"<b>{chat.UserId} ({chat.Date.ToString("h:mm tt")}):</b> {chat.Message}";
            messages.Add(message);
        }
    }

    public void signOut()
    {
        authServiceManager.SignOut();
        cloudDBManager.CloseSubscribeSnapshot();
        cloudDBManager.CloseCloudDBZone(cloudDBZone);
        SceneManager.LoadScene("LoginScene");
    }
}

[System.Serializable]
public class Message
{
    public string id;
    public string text;
    public string name;
    public DateTime dateTime;
    public Text textObject;
}