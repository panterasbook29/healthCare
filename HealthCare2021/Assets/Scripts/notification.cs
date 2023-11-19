using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using Unity.Notifications.Android;


public class notification : MonoBehaviour
{
    [SerializeField]
    public int timer = 5;
    public void RequestAuth()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }
    void Start()
    {
        RequestAuth();
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Notification Channel",
            Importance = Importance.Default,
            Description = "Reminder notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);


    }
    public void sendNotif1()
    {
        var notification = new AndroidNotification();
        notification.Title = "Time for your pills";
        notification.Text = "Metformin";
        notification.FireTime = System.DateTime.Now;

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

    }
    public void sendNotif2()
    {
        var notification = new AndroidNotification();
        notification.Title = "Time for your pills";
        notification.Text = "Estrovit Hydro Out";
        notification.FireTime = System.DateTime.Now;

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

    }
    public void sendNotif3()
    {
        var notification = new AndroidNotification();
        notification.Title = "Time for your pills";
        notification.Text = "Evogen Super Dry";
        notification.FireTime = System.DateTime.Now;

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

    }
    public void sendNotifFumat()
    {
        var notification = new AndroidNotification();
        notification.Title = "Smoke";
        notification.Text = "Did you smoke yersterday?";
        notification.FireTime = System.DateTime.Now;

        var id = AndroidNotificationCenter.SendNotification(notification, "channel_id");

    }
}