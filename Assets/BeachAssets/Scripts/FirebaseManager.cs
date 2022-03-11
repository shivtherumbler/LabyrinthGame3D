using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private Firebase.FirebaseApp app;
    private string user;
    private string pswd;
    public GameObject wrong;
    public GameObject typeagain;

    // Start is called before the first frame update
    void Awake()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void Start()
    {
        if (app != null)
        {
            // Log an event with no parameters.
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLevelStart);

            // Log an event with a float parameter
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent("progress", "percent", 0.4f);

            // Log an event with an int parameter.
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventPostScore,
                Firebase.Analytics.FirebaseAnalytics.ParameterScore,
                42
              );

            // Log an event with a string parameter.
            Firebase.Analytics.FirebaseAnalytics
              .LogEvent(
                Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
                Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
                "spoon_welders"
              );

            // Log an event with multiple parameters, passed as a struct:
            Firebase.Analytics.Parameter[] LevelUpParameters = {
  new Firebase.Analytics.Parameter(
    Firebase.Analytics.FirebaseAnalytics.ParameterLevel, 5),
  new Firebase.Analytics.Parameter(
    Firebase.Analytics.FirebaseAnalytics.ParameterCharacter, "mrspoon"),
  new Firebase.Analytics.Parameter(
    "hit_accuracy", 3.14f)
};
            Firebase.Analytics.FirebaseAnalytics.LogEvent(
              Firebase.Analytics.FirebaseAnalytics.EventLevelUp,
              LevelUpParameters);
        }

        
    }

    public void RegisterUser()
    {
        Debug.Log("app:" + app);
        if (app == null) return;
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        auth.CreateUserWithEmailAndPasswordAsync(user, pswd).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                typeagain.SetActive(true);
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                typeagain.SetActive(true);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            SceneManager.LoadScene("SampleScene");

        });
    }

    public void Login()
    {
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.SignInWithEmailAndPasswordAsync(user, pswd).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                wrong.SetActive(true);
                return;
                
            }
            if (task.IsFaulted)
            {
                
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                wrong.SetActive(true);
                return;
                
            }

            
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            SceneManager.LoadScene("SampleScene");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(GameObject open)
    {
        open.SetActive(true);
    }

    public void Close(GameObject close)
    {
        close.SetActive(false);
    }

    public void OnUserChanged(string newUser)
    {
        user = newUser;
    }
    public void OnPasswordChanged(string newPswd)
    {
        pswd = newPswd;
    }

}
