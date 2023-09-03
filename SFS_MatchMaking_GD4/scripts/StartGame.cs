using Godot;
using System;
using System.Collections.Generic;

using Sfs2X.Entities;


public partial class StartGame : Control
{
    // Called when the node enters the scene tree for the first time.

    public static string DEFAULT_INVITATION_MESSAGE = "Do you want to join my game?";

    [Export] public Control LobbyManagerNode;

    [Export] public LineEdit expText;
    [Export] public LineEdit rankText;
    [Export] public CheckBox publicToggle;
    [Export] public LineEdit invitationMessageInput;
    [Export] public VBoxContainer InviteList;
    [Export]
    public PackedScene inviteListScene;

    private Dictionary<string, bool> inviteListItems;
    private Dictionary<string, Node> itemInstances = new Dictionary<string, Node>();
    private InviteListItem inviteListItem;
    private string invitedBuddyName;
    /**
     * On start, set default invitation message.
     */
    public override void _Ready()
{
        invitationMessageInput.Text = DEFAULT_INVITATION_MESSAGE;
        publicToggle.Pressed += () => { OnPublicToggleChange(); };
    }

    /**
	 * Reset panel.
	 */
    public void Reset(bool isUserOnline, string exp, int rank)
    {
        publicToggle.ButtonPressed = isUserOnline;
    
        InviteList.Hide();

        expText.Text = "= " + exp;
        rankText.Text = ">= " + rank.ToString();

    }

    /**
	 * Add/update/remove toggles in invite list.
	 */
    public void UpdateInviteList(Buddy buddy, bool doRemove = false)
    {
        var instance = inviteListScene.Instantiate<Control>();
        inviteListItem = instance.GetNode<InviteListItem>("InviteListItem");

        InviteList.Hide();
        // Init collection
        if (inviteListItems == null)
            inviteListItems = new Dictionary<string, bool>();

        // Get reference to invite list toggle corresponding to Buddy
         string buddyName = buddy.Name;
        if (inviteListItems.ContainsKey(buddyName))
        {
            if (doRemove)
            {
                InviteList.RemoveChild(itemInstances[buddy.Name.ToString()]);
                itemInstances.Remove(buddyName.ToString());

                // Remove invite list item from dictionary
                inviteListItems.Remove(buddy.Name);

                // Destroy game object
                inviteListItem.QueueFree();

            }
            else
            {
                // Update existing item
                 SetInviteToggleState(buddy);
            }
        }
        else
        {
  
            // Create invite list item
            inviteListItem.BuddyCheckbox.Text = buddy.Name;
            inviteListItem.BuddyCheckbox.ButtonPressed = false;
  
            itemInstances.Add(buddyName.ToString(), instance);
            inviteListItems.Add(buddy.Name, false);
  
            InviteList.AddChild(instance);

            SetInviteToggleState(buddy);

        }
    }

    /**
	 * Enable/disable interaction on invitation section of the panel.
	 */
    public void OnPublicToggleChange()
    {

        if (publicToggle.ButtonPressed == false)
        {
            InviteList.Show();
        }
        else
        {
            InviteList.Hide();
        }
    }

  
        /**
         * Dispatch a custom event to create and join a game Room.
         */
        public void OnStartGameConfirmClick()
    {

        string invitedBuddyName = null;
        bool isPublic = true;
        foreach (Node child in InviteList.GetChildren())
        {
                foreach (Node grandChild in child.GetChildren())
                {
                        foreach (Node greatGrandChild in grandChild.GetChildren())
                        {
                            if (greatGrandChild.Name == "BuddyCheckBox")
                            {
                                CheckBox checkbox = (CheckBox)greatGrandChild;
                                if (checkbox.ButtonPressed)
                                {
                                     invitedBuddyName = checkbox.Text;
                                        isPublic = false;
                                        break;
                                }
                            }
                        }
                    }
                }
        LobbyManagerNode.Call("OnStartGameConfirm", isPublic, invitedBuddyName);
       GetTree().Paused = false;
       this.Hide();
    }

    /**
	 * Return the invitation massage.
	 */
    public string GetInvitationMessage()
    {
        return invitationMessageInput.Text;
    }

    private void SetInviteToggleState(Buddy buddy)
    {
        // Status
       
       bool show = !buddy.IsBlocked && buddy.IsOnline && (buddy.State == "Available" || buddy.State == "Occupied");

        if (show == false)
        {
            InviteList.RemoveChild(itemInstances[buddy.Name.ToString()]);
            itemInstances.Remove(buddy.Name.ToString());
            inviteListItems.Remove(buddy.Name);

        }
        }
}
