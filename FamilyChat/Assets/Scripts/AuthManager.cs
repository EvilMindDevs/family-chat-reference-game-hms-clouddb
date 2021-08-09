using HmsPlugin;
using HuaweiMobileServices.AuthService;
using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    private HMSAuthServiceManager authServiceManager = null;
    public Text errorLine;

    void Start()
    {
        authServiceManager = HMSAuthServiceManager.Instance;
        authServiceManager.OnSignInSuccess = OnAuthSericeSignInSuccess;
        authServiceManager.OnSignInFailed = OnAuthSericeSignInFailed;

        if (authServiceManager.GetCurrentUser() != null)
        {
            SceneManager.LoadScene("MainScene");
        } 
    }

    private void OnAuthSericeSignInFailed(HMSException error)
    {
        errorLine.text = error.WrappedCauseMessage;
    }

    private void OnAuthSericeSignInSuccess(SignInResult signInResult)
    {
        SceneManager.LoadScene("MainScene");
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

    public void SignInAnonymously() => authServiceManager.SignInAnonymously();

}
