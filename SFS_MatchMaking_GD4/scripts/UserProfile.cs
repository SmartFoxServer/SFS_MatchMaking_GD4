using Godot;
using System;

using Sfs2X.Entities.Managers;
using Sfs2X.Entities.Variables;


public partial class UserProfile : Control
{
  
    [Export] public Control LobbyManagerNode;
    public String callbackName;

    [Export] public LineEdit usernameLabel;
    [Export] public CheckBox onlineToggle;
    [Export] public LineEdit nickInput;
    [Export] public LineEdit yearInput;
    [Export] public TextEdit moodInput;
    [Export] public OptionButton stateDropdown;
    [Export] public OptionButton expDropdown;
    public string selectedExp;

    private CheckBox[] expCheckboxes;
    [Export]
    public CheckBox expCheckbox1;
    [Export]
    public CheckBox expCheckbox2;
    [Export]
    public CheckBox expCheckbox3;
    [Export]
    public CheckBox expCheckbox4;
    [Export]
    public CheckBox expCheckbox5;
    public int selectedRank;


    public override void _Ready()
    {
        expCheckboxes = new CheckBox[] { expCheckbox1, expCheckbox2, expCheckbox3, expCheckbox4, expCheckbox5 };

        // Current user experience
        expDropdown.FocusExited += () => { OnExperienceDropdownChange(); };

        // Current user ranking
        //     
        expCheckbox1.Pressed += () => { OnRankToggleChange(1); };
        expCheckbox2.Pressed += () => { OnRankToggleChange(2); };
        expCheckbox3.Pressed += () => { OnRankToggleChange(3); };
        expCheckbox4.Pressed += () => { OnRankToggleChange(4); };
        expCheckbox5.Pressed += () => { OnRankToggleChange(5); };
    }
        /**
         * Show the generic user profile details.
         */
       public void InitUserProfile(string username)
    {
        // Username
        usernameLabel.Text = "Username: " + username;
    }

    /**
	 * Show the profile details related to the buddy list system.
	 */
    public void InitBuddyProfile(IBuddyManager buddyManager)
    {
        // User online/offline state
        onlineToggle.ButtonPressed = buddyManager.MyOnlineState;
        onlineToggle.FocusExited += () => { OnOnlineToggleChange(); };


        // User nickname
        nickInput.Text = (buddyManager.MyNickName != null ? buddyManager.MyNickName : "");
        nickInput.FocusExited += () => { OnNickInputEnd(); };

        // Available states and current user state
        stateDropdown.Select(buddyManager.BuddyStates.IndexOf(buddyManager.MyState));
        stateDropdown.FocusExited += () => { OnStateDropdownChange(); };

        // Buddy variable: user birth year
        BuddyVariable year = buddyManager.GetMyVariable(LobbyManager.BUDDYVAR_YEAR);
        yearInput.Text = ((year != null && !year.IsNull()) ? year.GetIntValue().ToString() : "");
        yearInput.FocusExited += () => { OnBirthYearInputEnd(); };

        // Buddy variable: user mood
        BuddyVariable mood = buddyManager.GetMyVariable(LobbyManager.BUDDYVAR_MOOD);
        moodInput.Text = ((mood != null && !mood.IsNull()) ? mood.GetStringValue() : "");
        moodInput.FocusExited += () => { OnMoodInputEnd(); };

        OnExpDropdownSet();
        OnRankToggleSet();


    }


    /**
	 * Dispatch a custom event to set the user's online/offline state in the buddy list system.
	 */
    public void OnOnlineToggleChange()
    {
        if (onlineToggle.ButtonPressed)
            LobbyManagerNode.Call("OnOnlineToggleChange", true);
       else
            LobbyManagerNode.Call("OnOnlineToggleChange", false);

    }

    /**
	 * Dispatch a custom event to set the user's nickname in the buddy list system.
	 */
    public void OnNickInputEnd()
    {
        LobbyManagerNode.Call("OnBuddyNickChange", nickInput.Text);
    }

    /**
	 * Dispatch a custom event to set the user's birth year in the buddy list system.
	 */
    public void OnBirthYearInputEnd()
    {

        bool isNumeric = int.TryParse(yearInput.Text, out _);
        if (isNumeric)
            LobbyManagerNode.Call("OnBuddyYearChange", yearInput.Text);
  
    }

    /**
	 * Dispatch a custom event to set the user's mood in the buddy list system.
	 */
    public void OnMoodInputEnd()
    {
        LobbyManagerNode.Call("OnBuddyMoodChange", moodInput.Text);
    }

    /**
	 * Dispatch a custom event to set the user's state in the buddy list system.--
	 */
    public void OnStateDropdownChange()
    {

        string selectedText = stateDropdown.GetItemText(stateDropdown.Selected);
        LobbyManagerNode.Call("OnBuddyStateChange", selectedText);
    }

    public void OnExpDropdownSet()
    {
        // Set Optionbutton
        int index = -1;
        for (int i = 0; i < expDropdown.ItemCount; i++)
        {
            if (expDropdown.GetItemText(i) == selectedExp)
            {
                index = i;
                break;
            }
        }
            expDropdown.Select(index);
    }
    public void OnExperienceDropdownChange()
    {
        // Dispatch event
        string selectedText = expDropdown.GetItemText(expDropdown.Selected);
        LobbyManagerNode.Call("OnPlayerExpChange", selectedText);
    }



    /**
  * Dispatch a custom event to set the user's ranking in User Variables. 
  */

    public void OnRankToggleSet()
    {
  
        for (int i = 0; i < selectedRank; i++)
        {
            expCheckboxes[i].ButtonPressed = true;
        }

        for (int i = selectedRank; i < expCheckboxes.Length; i++)
        {
            expCheckboxes[i].ButtonPressed = false;
        }

    }
    public void OnRankToggleChange(int k)
    {
        selectedRank = k;

        for (int i = 0; i < k; i++)
        {
            expCheckboxes[i].ButtonPressed = true;
        }

        for (int i = k; i < expCheckboxes.Length; i++)
        {
            expCheckboxes[i].ButtonPressed = false;
        }

        LobbyManagerNode.Call("OnPlayerRankChange", selectedRank);
    }
}
