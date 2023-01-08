using UnityEngine;
using UnityEngine.UI;

// simple notification system... 
public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;
    public GameObject notificationPrefab;
    void Awake()
    {
        if (Instance != null)
        { Destroy(this); }
        else
            Instance = this;
    }

    public void ShowNotification(string message, float notificationDuration = 5f)
    {
        GameObject notification = Instantiate(notificationPrefab, Vector3.zero, Quaternion.identity);

        notification.GetComponent<Notification>().msg.text = message;

        Destroy(notification, notificationDuration);
    }


}
