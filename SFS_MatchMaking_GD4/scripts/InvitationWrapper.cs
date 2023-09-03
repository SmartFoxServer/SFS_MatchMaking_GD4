using System;
using Sfs2X.Entities.Invitation;

/**
 * Utility class to store a queued invitation.
 */
public class InvitationWrapper
{
    public Invitation invitation;
    public int expiresInSeconds;
    public DateTime date;

    public InvitationWrapper(Invitation invitation)
    {
        this.invitation = invitation;

        this.expiresInSeconds = invitation.SecondsForAnswer;
        this.date = DateTime.Now;
    }
}
