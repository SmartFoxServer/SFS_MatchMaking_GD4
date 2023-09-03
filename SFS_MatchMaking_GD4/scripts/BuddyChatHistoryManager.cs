using System.Collections.Generic;
using Godot;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public static class BuddyChatHistoryManager
{
    private static Dictionary<string, List<BuddyChatMessage>> buddyChatLists;
    private static Dictionary<string, int> unreadMsgCounters;

    public static void Init()
    {
        buddyChatLists = new Dictionary<string, List<BuddyChatMessage>>();
        unreadMsgCounters = new Dictionary<string, int>();
    }

    public static BuddyChatMessage AddMessage(IDictionary<string, object> msgParams)
    {
        bool isItMe = (bool)msgParams["isItMe"];
        Buddy sender = (Buddy)msgParams["buddy"];
        string message = (string)msgParams["message"];

        // If the sender of the message is the current user, we have to extract the recipient buddy name from the "data" object
        string buddyName = isItMe ? (msgParams["data"] as ISFSObject).GetUtfString("recipient") : sender.Name;

        GD.Print(buddyName);
        // Initialize queue
        if (!buddyChatLists.ContainsKey(buddyName))
        {
            buddyChatLists.Add(buddyName, new List<BuddyChatMessage>());
            unreadMsgCounters.Add(buddyName, 0);
        }

        // Add message to queue
        BuddyChatMessage chatMsg = new BuddyChatMessage(buddyName, message, isItMe);
        buddyChatLists[buddyName].Add(chatMsg);

        // Return chat message
        return chatMsg;
    }

    public static int IncreaseUnreadMessagesCount(string buddyName)
    {
        // Increase unread messages counter
        unreadMsgCounters[buddyName]++;

        // Return total number of unread messages
        return unreadMsgCounters[buddyName];
    }

    public static List<BuddyChatMessage> GetBuddyChatMessages(string buddyName)
    {
        buddyChatLists.TryGetValue(buddyName, out List<BuddyChatMessage> buddyChatMessages);

        return buddyChatMessages;
    }

    public static int GetUnreadMessagesCount(string buddyName)
    {
        if (unreadMsgCounters.ContainsKey(buddyName))
            return unreadMsgCounters[buddyName];

        return 0;
    }

    public static void ResetUnreadMessagesCount(string buddyName)
    {
        if (unreadMsgCounters.ContainsKey(buddyName))
            unreadMsgCounters[buddyName] = 0;
    }
}
