using System.Collections.Generic;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class ADController : IInterstitialAdListener, IAppodealInitializationListener
{
    private const string AppKey = "1c1e57cb3cdae0b3afde949eafc56230ea3132a142e0b6cb";
    
    public ADController()
    {
        int adTypes = Appodeal.INTERSTITIAL;

        Appodeal.initialize(AppKey, adTypes, this);

        Appodeal.setInterstitialCallbacks(this);

    }

    public void ShowInterstitial()
    {
        if(Appodeal.isLoaded(Appodeal.INTERSTITIAL)) {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        Debug.Log("Show Interstitial");
    }
    public void onInterstitialLoaded(bool isPrecache)
    {
        Debug.Log("InterstitialLoaded");
    }

    public void onInterstitialFailedToLoad()
    {
        Debug.Log("InterstitialFailedToLoad");
    }

    public void onInterstitialShowFailed()
    {
        Debug.Log("InterstitialShowFailed");
    }

    public void onInterstitialShown()
    {
        Debug.Log("InterstitialShown");
    }

    public void onInterstitialClosed()
    {
        Debug.Log("InterstitialClosed");
    }

    public void onInterstitialClicked()
    {
        Debug.Log("InterstitialClicked");
    }

    public void onInterstitialExpired()
    {
        Debug.Log("InterstitialExpired");
    }

    public void onInitializationFinished(List<string> errors)
    {
        Debug.Log("InitializationFinished");

        if (errors.Count != 0)
        {
            int index = 1;
            foreach (var error in errors)
            {
                Debug.Log($"{index}_error: {error}");
            }
        }
    }
}
