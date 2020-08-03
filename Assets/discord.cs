using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discord : MonoBehaviour
{
    public Discord.Discord discordClient;
    // Start is called before the first frame update
    void Start()
    {
        const long CLIENT_ID = 739944939894472811;
        discordClient = new Discord.Discord(CLIENT_ID, (System.UInt64)Discord.CreateFlags.Default);

        var activityManager = discordClient.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = "Still Testing",
            Details = "Bigger Test"
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.LogError("Everything is fine!");
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        discordClient.RunCallbacks();
    }
}