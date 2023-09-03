using System;

/**
 * Utility class to store buddy chat message details.
 */
public class BuddyChatMessage
{
	public string buddyName;
	public string message;
	public bool sentByMe;
	public DateTime date;

	public BuddyChatMessage(string buddyName, string message, bool sentByMe)
	{
		this.buddyName = buddyName;
		this.message = message;
		this.sentByMe = sentByMe;
		this.date = DateTime.Now;
	}
}
