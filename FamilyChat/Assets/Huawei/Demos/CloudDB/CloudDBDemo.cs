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
using UnityEngine.UI;
using Text = UnityEngine.UI.Text;

public class CloudDBDemo : MonoBehaviour
{
    private string TAG = "CloudDBDemo";
    private HMSAuthServiceManager authServiceManager = null;
    private AGConnectUser user = null;
    private Text loggedInUser;

    private const string NOT_LOGGED_IN = "No user logged in";
    private const string LOGGED_IN = "{0} is logged in";
    private const string LOGGED_IN_ANONYMOUSLY = "Anonymously Logged In";
    private const string LOGIN_ERROR = "Error or cancelled login";

    private HMSCloudDBManager cloudDBManager = null;
    private readonly string cloudDBZoneName = "ChatAppCloudZone";
    private readonly string ChatAppClass = "com.clouddbdemo.kb.huawei.ChatAppOT";
    private readonly string ObjectTypeInfoHelper = "com.clouddbdemo.kb.huawei.ObjectTypeInfoHelper";

    List<ChatAppOT> bookInfoList = null;

    public void Start()
    {
        loggedInUser = GameObject.Find("LoggedUserText").GetComponent<Text>();
        loggedInUser.text = NOT_LOGGED_IN;

        authServiceManager = HMSAuthServiceManager.Instance;
        authServiceManager.OnSignInSuccess = OnAuthSericeSignInSuccess;
        authServiceManager.OnSignInFailed = OnAuthSericeSignInFailed;

        if (authServiceManager.GetCurrentUser() != null)
        {
            user = authServiceManager.GetCurrentUser();
            loggedInUser.text = user.IsAnonymous() ? LOGGED_IN_ANONYMOUSLY : string.Format(LOGGED_IN, user.DisplayName);
        }
        else
        {
            SignInWithHuaweiAccount();
        }

        cloudDBManager = HMSCloudDBManager.Instance;
        cloudDBManager.Initialize();
        cloudDBManager.GetInstance();
        cloudDBManager.OnExecuteQuerySuccess = OnExecuteQuerySuccess;
        cloudDBManager.OnExecuteQueryFailed = OnExecuteQueryFailed;
    }

    private void OnAccountKitLoginSuccess(AuthAccount authHuaweiId)
    {
        AGConnectAuthCredential credential = HwIdAuthProvider.CredentialWithToken(authHuaweiId.AccessToken);
        authServiceManager.SignIn(credential);
    }

    public void SignInWithHuaweiAccount()
    {
        HMSAccountManager.Instance.OnSignInSuccess = OnAccountKitLoginSuccess;
        HMSAccountManager.Instance.OnSignInFailed = OnAuthSericeSignInFailed;
        HMSAccountManager.Instance.SignIn();
    }

    private void OnAuthSericeSignInFailed(HMSException error)
    {
        loggedInUser.text = LOGIN_ERROR;
    }

    private void OnAuthSericeSignInSuccess(SignInResult signInResult)
    {
        user = signInResult.GetUser();
        loggedInUser.text = user.IsAnonymous() ? LOGGED_IN_ANONYMOUSLY : string.Format(LOGGED_IN, user.DisplayName);
    }

    public void CreateObjectType()
    {
        cloudDBManager.CreateObjectType(ObjectTypeInfoHelper);
    }

    public void GetCloudDBZoneConfigs()
    {
        IList<CloudDBZoneConfig> CloudDBZoneConfigs = cloudDBManager.GetCloudDBZoneConfigs();
        Debug.Log($"{TAG} " + CloudDBZoneConfigs.Count);
    }

    public void OpenCloudDBZone()
    {
        cloudDBManager.OpenCloudDBZone(cloudDBZoneName, CloudDBZoneConfig.CloudDBZoneSyncProperty.CLOUDDBZONE_CLOUD_CACHE, CloudDBZoneConfig.CloudDBZoneAccessProperty.CLOUDDBZONE_PUBLIC);
    }

    public void OpenCloudDBZone2()
    {
        cloudDBManager.OpenCloudDBZone2(cloudDBZoneName, CloudDBZoneConfig.CloudDBZoneSyncProperty.CLOUDDBZONE_CLOUD_CACHE, CloudDBZoneConfig.CloudDBZoneAccessProperty.CLOUDDBZONE_PUBLIC);
    }

    public void EnableNetwork() => cloudDBManager.EnableNetwork(cloudDBZoneName);
    public void DisableNetwork() => cloudDBManager.DisableNetwork(cloudDBZoneName);

    public void AddBookInfo()
    {
        ChatAppOT bookInfo = new ChatAppOT();
        bookInfo.Id = "1";
        bookInfo.Message = "bookName";
        bookInfo.UserId= "Author 1";
        cloudDBManager.ExecuteUpsert(bookInfo);
    }

    public void AddBookInfoList()
    {
        IList<AndroidJavaObject> bookInfoList = new List<AndroidJavaObject>();

        ChatAppOT bookInfo1 = new ChatAppOT();
        bookInfo1.Id = "1";
        bookInfo1.Message = "Author 2";
        bookInfoList.Add(bookInfo1.GetObj());

        ChatAppOT bookInfo2 = new ChatAppOT();
        bookInfo2.Id = "2";
        bookInfo2.Message = "Author 3";
        bookInfoList.Add(bookInfo2.GetObj());

        cloudDBManager.ExecuteUpsert(bookInfoList);
    }

    public void UpdateBookInfo()
    {
        ChatAppOT bookInfo = new ChatAppOT();
        bookInfo.Id = "1";
        bookInfo.Message = "bookName";
        cloudDBManager.ExecuteUpsert(bookInfo);
    }

    public void DeleteBookInfo()
    {
        ChatAppOT bookInfo = new ChatAppOT();
        bookInfo.Id ="1";
        cloudDBManager.ExecuteDelete(bookInfo);
    }

    public void DeleteBookInfoList()
    {
        IList<AndroidJavaObject> bookInfoList = new List<AndroidJavaObject>();

        ChatAppOT bookInfo1 = new ChatAppOT();
        bookInfo1.Id = "2";
        bookInfo1.Message = "Author 2";
        bookInfoList.Add(bookInfo1.GetObj());

        ChatAppOT bookInfo2 = new ChatAppOT();
        bookInfo2.Id = "3";
        bookInfo2.Message = "Author 3";
        bookInfoList.Add(bookInfo2.GetObj());

        cloudDBManager.ExecuteDelete(bookInfoList);
    }

    public void GetBookInfo()
    {
        CloudDBZoneQuery mCloudQuery = CloudDBZoneQuery.Where(new AndroidJavaClass(ChatAppClass));
        cloudDBManager.ExecuteQuery(mCloudQuery, CloudDBZoneQuery.CloudDBZoneQueryPolicy.CLOUDDBZONE_LOCAL_ONLY);
    }

    private void OnExecuteQuerySuccess(CloudDBZoneSnapshot<ChatAppOT> snapshot) => ProcessQueryResult(snapshot);

    private void OnExecuteQueryFailed(HMSException error) => Debug.Log($"{TAG} OnExecuteQueryFailed(HMSException error) => {error.WrappedExceptionMessage}");

    private void ProcessQueryResult(CloudDBZoneSnapshot<ChatAppOT> snapshot)
    {
        CloudDBZoneObjectList<ChatAppOT> bookInfoCursor = snapshot.GetSnapshotObjects();
        bookInfoList = new List<ChatAppOT>();
        try
        {
            while (bookInfoCursor.HasNext())
            {
                ChatAppOT bookInfo = bookInfoCursor.Next();
                bookInfoList.Add(bookInfo);
                Debug.Log($"{TAG} bookInfoCursor.HasNext() {bookInfo.Id}  {bookInfo.Message}");
            }
        }
        catch (Exception e)
        {
            Debug.Log($"{TAG} processQueryResult:  Exception => " + e.Message);
        }
        finally
        {
            snapshot.Release();
        }
    }

    public void ExecuteSumQuery()
    {
        CloudDBZoneQuery mCloudQuery = CloudDBZoneQuery.Where(new AndroidJavaClass(ChatAppClass));
        cloudDBManager.ExecuteSumQuery(mCloudQuery, "price", CloudDBZoneQuery.CloudDBZoneQueryPolicy.CLOUDDBZONE_LOCAL_ONLY);
    }

    public void ExecuteCountQuery()
    {
        CloudDBZoneQuery mCloudQuery = CloudDBZoneQuery.Where(new AndroidJavaClass(ChatAppClass));
        cloudDBManager.ExecuteCountQuery(mCloudQuery, "price", CloudDBZoneQuery.CloudDBZoneQueryPolicy.CLOUDDBZONE_LOCAL_ONLY);
    }

}
