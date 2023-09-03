
using Godot;
using System;

using Sfs2X.Entities;
using Sfs2X.Entities.Variables;
using static System.Net.Mime.MediaTypeNames;

public partial class BuddyListItem : Control
{
    [Export] public TextureRect statusIcon;
    [Export] public LineEdit mainLabel;
    [Export] public LineEdit moodLabel;

    [Export] public TextureButton chatButton;
    [Export] public Label chatButtonText;
    [Export] public TextureButton blockButton;
    [Export] public TextureButton removeButton;
    [Export] public TextureButton addButton;
    [Export] public TextureButton inviteButton;

    [Export] public Texture buttonIconBlock;
    [Export] public Texture buttonIconUnblock;

        [Export] public Texture statusIconAvailable;
        [Export] public Texture statusIconAway;
        [Export] public Texture statusIconOccupied;
        [Export] public Texture statusIconOffline;
        [Export] public Texture statusIconBlocked;

    public bool isBlocked;

    /**
	 * Initialize the prefab instance.
	 */
    public void Init(Buddy buddy)
    {
        SetState(buddy);
    }

    /**
	 * Update prefab instance based on the state of the corresponding buddy.
	 */
    public void SetState(Buddy buddy)
    {

        // Nickname
        mainLabel.Text = ((buddy.NickName != null && buddy.NickName != "") ? buddy.NickName : buddy.Name) ;

        // Age
        DateTime now = DateTime.Now;
        BuddyVariable year = buddy.GetVariable(LobbyManager.BUDDYVAR_YEAR);
        mainLabel.Text += (year != null && !year.IsNull()) ? "(" + (now.Year - year.GetIntValue()) + " yo)" : "";

        // Mood
        BuddyVariable mood = buddy.GetVariable(LobbyManager.BUDDYVAR_MOOD);

        if (mood != null && !mood.IsNull() && mood.GetStringValue() != "")
        {
            moodLabel.Text = mood.GetStringValue();
 
        }
 

        // Save blocked state
        // (see LobbySceneController.UpdateBuddyListItem method)
        isBlocked = buddy.IsBlocked;

        // If buddy is not blocked and is temporary, show add button and hide remove button
        bool showAddButton = !buddy.IsBlocked && buddy.IsTemp;
        if (showAddButton )
        {
            removeButton.Hide();
            addButton.Show();
        } else
        {
            addButton.Hide();
            removeButton.Show();

        }

        // Status icon and buttons
        if (buddy.IsBlocked)
        {
            blockButton.TextureNormal = (Texture2D)buttonIconUnblock;
            EnableInteraction(true);
            SetChatMsgCounter(0);
        }
        else
        {
            blockButton.TextureNormal = (Texture2D)buttonIconBlock;

            if (!buddy.IsOnline)
            {
                statusIcon.Texture = (Texture2D)statusIconOffline;
                EnableInteraction(true);
                SetChatMsgCounter(0);
            }
            else
            {
                string state = buddy.State;

                if (state == "Available")
                    statusIcon.Texture = (Texture2D)statusIconAvailable;
                else if (state == "Away")
                    statusIcon.Texture = (Texture2D)statusIconAway;
                else if (state == "Occupied")
                    statusIcon.Texture = (Texture2D)statusIconOccupied;

                EnableInteraction(false);
            }
        }
    }

    /**
	 * Show unread chat messages counter.
	 */
    public void SetChatMsgCounter(int value)
    {
        if (value > 0)
            chatButtonText.Text = value.ToString();
        else
            chatButtonText.Text = "";
    }

    /**
	 * Enable/disable interaction with buttons.
	 */
    private void EnableInteraction(bool enable)
    {
      chatButton.Disabled = enable;
    }
}
