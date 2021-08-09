#  Unity Mobile Reference Game with Cloud DB

This reference project is created for that to show features HMS Cloud DB. In this project was used writing data to db and updating listening in Real Time.

# Project - (Family Chat)
This Project consists of 2 scenes with [HMS Unity Plugin](https://github.com/EvilMindDevs/hms-unity-plugin). In the first scene, it contains authentication modes. In the second scene, after the login, it contains chat panel with Cloud DB.

![image](https://user-images.githubusercontent.com/32878124/128687564-f262a50d-28e3-4384-9c86-a566ebe6ed72.png)

## Cloud DB Features
Cloud DB is a device-cloud synergy database product that enables seamless data synchronization between the device and cloud and between devices, and supports offline application operations, helping you quickly develop device-cloud and multi-device synergy applications.

* Flexible synchronization modes
* Powerful query capability
* Real-time update
* Offline operations
* Scalability
* Security level

## Requirements
* Android SDK min 21
* Net 4.x

## Important
This plugin supports:
* [Unity version 2019, 2020 - Developed in master Branch](https://github.com/EvilMindDevs/hms-unity-plugin/releases)
* [Unity version 2018 - Developed in 2.0-2018 Branch](https://github.com/EvilMindDevs/hms-unity-plugin/releases)

**Make sure to download the corresponding unity package for the Unity version you are using from the release section**

## Troubleshooting 1
Please check our [wiki page](https://github.com/EvilMindDevs/hms-unity-plugin/wiki/Troubleshooting)

## Status
This is an ongoing project. Feel free to contact us if you'd like to collaborate and use Github issues for any problems you might encounter. We'd try to answer in no more than a working day.

## Connect your game Huawei Mobile Services in 5 easy steps

1. Register your app at Huawei Developer
2. Import the Plugin to your Unity project
3. Connect your game with the HMS Kit Managers

### 1 - Register your app at Huawei Developer

#### 1.1-  Register at [Huawei Developer](https://developer.huawei.com/consumer/en/)

#### 1.2 - Create an app in AppGallery Connect.
During this step, you will create an app in AppGallery Connect (AGC) of HUAWEI Developer. When creating the app, you will need to enter the app name, app category, default language, and signing certificate fingerprint. After the app has been created, you will be able to obtain the basic configurations for the app, for example, the app ID and the CPID.

1. Sign in to Huawei Developer and click **Console**.
2. Click the HUAWEI AppGallery card and access AppGallery Connect.
3. On the **AppGallery Connect** page, click **My apps**.
4. On the displayed **My apps** page, click **New**.
5. Enter the App name, select App category (Game), and select Default language as needed.
6. Upon successful app creation, the App information page will automatically display. There you can find the App ID and CPID that are assigned by the system to your app.

#### 1.3 Add Package Name
Set the package name of the created application on the AGC.

1. Open the previously created application in AGC application management and select the **Develop TAB** to pop up an entry to manually enter the package name and select **manually enter the package name**.
2. Fill in the application package name in the input box and click save.

> Your package name should end in .huawei in order to release in App Gallery

#### Generate a keystore.

Create a keystore using Unity or Android Tools. make sure your Unity project uses this keystore under the **Build Settings>PlayerSettings>Publishing settings**

#### Generate a signing certificate fingerprint.

During this step, you will need to export the SHA-256 fingerprint by using keytool provided by the JDK and signature file.

1. Open the command window or terminal and access the bin directory where the JDK is installed.
2. Run the keytool command in the bin directory to view the signature file and run the command.

    ``keytool -list -v -keystore D:\Android\WorkSpcae\HmsDemo\app\HmsDemo.jks``
3. Enter the password of the signature file keystore in the information area. The password is the password used to generate the signature file.
4. Obtain the SHA-256 fingerprint from the result. Save for next step.

#### Add fingerprint certificate to AppGallery Connect
During this step, you will configure the generated SHA-256 fingerprint in AppGallery Connect.

1. In AppGallery Connect, click the app that you have created and go to **Develop> Overview**
2. Go to the App information section and enter the SHA-256 fingerprint that you generated earlier.
3. Click √ to save the fingerprint.

#### Enabling Auth Service
1. Click your project for which you need to enable Auth Service from the project list.
2. Go to Build > Auth Service. If it is the first time that you use Auth Service, click Enable now in the upper right corner.
![image](https://user-images.githubusercontent.com/32878124/128504970-e3be6ab4-344d-4e99-9647-89518db9be6d.png)
3. Click Enable in the row of each authentication mode to be enabled.
![image](https://user-images.githubusercontent.com/32878124/128504971-c90074da-2e26-4d35-b37a-c8dad4d9f496.png)

####  Cloud DB Configuration
1. You need to add ObjectTypes on Huawei Console. 
![image](https://user-images.githubusercontent.com/32878124/128687587-962b3177-b814-4fa7-9816-e8d9cb242ab3.png)
2. And then need to add Cloub DB Zone.
![image](https://user-images.githubusercontent.com/32878124/128687590-138e0760-6b56-4e86-924e-066cfdbc7d5c.png)
3. You can check whole datas on Data Entries.
![image](https://user-images.githubusercontent.com/32878124/128687594-1ed816c0-8f7f-433a-9aad-2871aadfc802.png)
4. Finally you need to export JSON file and Java file. Pass these file to HMS plugin tab bar.
![image](https://user-images.githubusercontent.com/32878124/128687580-0120077f-325b-4e7d-bf12-b30d89da7e09.png)
5. After this operation, HMS Unity plugin generates neceseery files for your project.
![image](https://user-images.githubusercontent.com/32878124/128687584-c0488200-2de6-4039-bec1-0bb23af70224.png)
____

### 2 - Import the plugin to your Unity Project

To import the plugin:

1. Download the [.unitypackage](https://github.com/EvilMindDevs/hms-unity-plugin/releases)
2. Open your game in Unity
3. Choose Assets> Import Package> Custom Package
4. In the file explorer select the downloaded HMS Unity plugin. The Import Unity Package dialog box will appear, with all the items in the package pre-checked, ready to install.
![image](https://user-images.githubusercontent.com/6827857/113576269-e8e2ca00-9627-11eb-9948-e905be1078a4.png)
5. Select Import and Unity will deploy the Unity plugin into your Assets Folder
____

### 3 - Update your agconnect-services.json file.

In order for the plugin to work, some kits are in need of agconnect-json file. Please download your latest config file from AGC and import into Assets/StreamingAssets folder.
![image](https://user-images.githubusercontent.com/6827857/113585485-f488bd80-9634-11eb-8b1e-6d0b5e06ecf0.png)
____

### 4 - Connect your game with any HMS Kit
In order for the plugin to work, you need to select the needed kits Huawei > Kit Settings.
In this project , I selected the Account kit, Auth Service and Cloud DB. 
**For CloudDB, you have to use Auth Service**
![2](https://user-images.githubusercontent.com/32878124/128687586-f03249df-6fa1-4128-a664-83cbc7b9127e.png)
I selected Account kit also, because i use Huawei Account for authentication mode.

Now you need to call HMSAuthServiceManager as below.
```csharp
  public void Start(){
        authServiceManager = HMSAuthServiceManager.Instance;
        authServiceManager.OnSignInSuccess = OnAuthSericeSignInSuccess;
        authServiceManager.OnSignInFailed = OnAuthSericeSignInFailed;
        authServiceManager.OnCreateUserSuccess = OnAuthSericeCreateUserSuccess;
        authServiceManager.OnCreateUserFailed = OnAuthSericeCreateUserFailed;

        if (authServiceManager.GetCurrentUser() != null) { // skip the login scene }
    }
    private void OnAuthSericeSignInSuccess(SignInResult signInResult) { }
    private void OnAuthSericeSignInFailed(HMSException error) { }
    private void OnAuthSericeCreateUserSuccess(SignInResult signInResult) { }
    private void OnAuthSericeCreateUserFailed(HMSException error) { }

```

Now HMS Auth Service is ready to use. Lets look at the authentication modes.

## Huawei Account
First you need to sign in with Huawei Account Kit and take the access token. And then call AuthService SignIn method and pass the credential parameter with this method.
```csharp
private void OnAccountKitLoginSuccess(AuthAccount authHuaweiId)
{
    AGConnectAuthCredential credential = HwIdAuthProvider.CredentialWithToken(authHuaweiId.AccessToken);
    authServiceManager.SignIn(credential);
}
private void OnAccountKitLoginFailed(HMSException error)
{
   // ...
}
public void SignInWithHuaweiAccount()
{
    HMSAccountManager.Instance.OnSignInSuccess = OnAccountKitLoginSuccess;
    HMSAccountManager.Instance.OnSignInFailed = OnAccountKitLoginFailed;
    HMSAccountManager.Instance.SignIn();
}
```

## Anonymous Account
You need to SignInAnonymously method from AuthServiceManager.
```csharp
public void SignInAnonymously() => authServiceManager.SignInAnonymously();
```

## Cloud DB
### Initialize
```csharp
private HMSCloudDBManager cloudDBManager = null;
private List<ChatAppOT> ChatAppList = null;
private CloudDBZone cloudDBZone = null;

private readonly string CloudDBZoneName = "ChatAppCloudZone";
private readonly string ChatAppClass = "com.clouddbdemo.kb.huawei.ChatAppOT";
private readonly string ObjectTypeInfoHelper = "com.clouddbdemo.kb.huawei.ObjectTypeInfoHelper";

void Start()
{
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
```

After CloudDBZone initialized, call SubcribeSnapshot method for real time updating.

```csharp
private void OnOpenCloudDBZone2Success(CloudDBZone obj)
{
    cloudDBZone = obj;
    CloudDBZoneQuery mCloudQuery = CloudDBZoneQuery.Where(new AndroidJavaClass(ChatAppClass)).EqualTo("shadowFlag", true);
    cloudDBManager.SubscribeSnapshot(mCloudQuery, CloudDBZoneQuery.CloudDBZoneQueryPolicy.CLOUDDBZONE_CLOUD_CACHE);
}
```

Now need to listen and parse snapshots.

```csharp
    private void OnExecuteQuerySuccess(CloudDBZoneSnapshot<ChatAppOT> snapshot) => ProcessQueryResult(snapshot);
    
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

```

And when you want to send message, and create new record on Cloud DB, check below code block.

```csharp
void sendMessage(string text)
{
    ChatAppOT chat = new ChatAppOT();
    chat.Id = Guid.NewGuid().ToString();
    chat.UserId = user.IsAnonymous() ? "Guest" : user.DisplayName;
    chat.Message = text;
    chat.Date = DateTime.Now;
    cloudDBManager.ExecuteUpsert(chat);
}
```

## Sign Out
Use SignOut method to log out.
```csharp
authServiceManager.SignOut();
```

## Troubleshooting 2
1. If you received package name error , please check your package name on File->Build Settings -> Player Settings -> Other Settings -> Identification
![image](https://user-images.githubusercontent.com/8115505/128307687-6629559d-d873-4e6f-9b2f-54545360e0c0.png)

2. If you received min sdk error , 
![image](https://user-images.githubusercontent.com/67346749/125592730-940912c8-f9b4-4f8b-8fe4-b13532342613.PNG)
Please set your API level as implied in the **Requirements** section
![image](https://user-images.githubusercontent.com/8115505/128321008-a8fb7d82-dce8-4b1d-bce2-05dcc690e249.png)

## License
MIT