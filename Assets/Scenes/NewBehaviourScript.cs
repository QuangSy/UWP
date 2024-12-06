using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] string appID;
    [SerializeField] string interstitial;
    [SerializeField] string reward;

    [SerializeField] TextMeshProUGUI txtNoti;
    string mess = "";

    private void Start()
    {
        Vungle.adPlayableEvent += OnAdPlayableEvent;
        Vungle.onLogEvent += OnLogEvent;
        Vungle.onErrorEvent += OnErrorEvent;
        Vungle.onWarningEvent += OnWarningEvent;
        Vungle.onAppTrackingEvent += OnTrackingEvent;

        Vungle.onInitializeEvent += () =>
        {
            Log($"[VUNGLE LOG] Initialized Vungle");
            Vungle.loadAd(interstitial);
            Vungle.loadAd(reward);
        };

        try
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                Log("[VUNGLE Network] Network is available, proceeding with Vungle SDK initialization.");
            }
            else
            {
                Log("[VUNGLE Network] Network is unavailable, Vungle SDK might not initialize properly.");
            }

            Vungle.init(appID);
            Log($"[VUNGLE LOG] Used Vungle - AppId: {appID}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Vungle initialization failed: " + ex.Message);
        }
    }

    private void OnAdPlayableEvent(string placementID, bool adPlayable)
    {
        Log($"[VUNGLE LOG] Ad's playable state has been changed! placementID {placementID}. Now: {adPlayable}");
    }

    private void OnLogEvent(string message)
    {
        Log($"[VUNGLE LOG] LogEvent: {message}");
    }

    private void OnErrorEvent(string message)
    {
        Log($"[VUNGLE LOG] ErrorEvent: {message}");
    }

    private void OnWarningEvent(string message)
    {
        Log($"[VUNGLE LOG] WarningEvent: {message}");
    }

    private void OnTrackingEvent(Vungle.AppTrackingStatus appTrackingStatus)
    {
        Log($"[VUNGLE LOG] TrackingEvent: {appTrackingStatus}");
    }

    private void Log(string mess)
    {
        mess += "\n";
        this.mess += mess;

        txtNoti.text = this.mess;
    }    
}
