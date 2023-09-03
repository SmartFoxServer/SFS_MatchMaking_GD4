using Godot;
using Sfs2X.Entities.Invitation;
using System;

public partial class InvitationPanel : Control
{
    [Export] public Control LobbyManagerNode;

    [Export] public Label invMessageText1;
    [Export] public Label invMessageText2;
    [Export] public Label expirationText;
    private InvitationWrapper invitationWrapper;
    public Invitation invitation;
    private float timer;
    private bool timerEnabled;

    /**
	 * Update invitation expiration timer.
	 */
    public override void _Process(double delta)
    {
        if (timerEnabled)
        {
            timer -= (float)delta;
            ShowCountdown();

            if (Math.Floor(timer) <= 0)
                TriggerInvitationReply(false);
        }

    }

    /**
	 * Show panel instance with the invitation details.
	 */
    public void ShowPanel(InvitationWrapper iw)
    {
        this.invitationWrapper = iw;

        // Display invitation message
        string message1 = "";
        string message2 = "";

        if (iw.invitation.Params.GetUtfString("message") != "")
            message1 += iw.invitation.Params.GetUtfString("message");

        message2 += "You have been invited by " + iw.invitation.Inviter.Name + " to play " + iw.invitation.Params.GetUtfString("room");

        invMessageText1.Text = message1;
        invMessageText2.Text = message2;

        // Set expiration time
        timer = iw.expiresInSeconds;
        timerEnabled = true;

        // Display remaining time for replying
        ShowCountdown();

        // Show panel
        this.Show();
    }


    /**
	 * On Accept button click, send invitation reply.
	 */
    public void OnInvitationAcceptClick()
    {
        // Accept invitation
        TriggerInvitationReply(true);
    }

    /**
	 * On Refuse button click, send invitation reply.
	 */
    public void OnInvitationRefuseClick()
    {
        // Refuse invitation
        TriggerInvitationReply(false);
    }

    /**
	 * Dispatch a custom event to reply to the invitation.
	 */
    private void TriggerInvitationReply(bool accept)
    {
        timerEnabled = false;

        invitation = invitationWrapper.invitation;

        LobbyManagerNode.Call("OnInvitationReplyClick", accept);

        // Delete reference to invitation wrapper
        this.invitationWrapper = null;


        // Hide panel
        this.Hide();


    }

    /**
	 * Display countdown to expiration.
	 */
    private void ShowCountdown()
    {
        int secs = (int)Math.Floor(timer);
        expirationText.Text = "This invitation will expire in " + secs + " seconds";
    }
}
