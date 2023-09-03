using Godot;
using System;

using Sfs2X.Entities;
using static System.Net.Mime.MediaTypeNames;

public partial class GameListItem : Control
{
    [Export] public TextureButton playButton;
    [Export] public TextureButton watchButton;
    [Export] public LineEdit nameText;
    [Export] public RichTextLabel detailsText;

    [Export] public int roomId;

    [Export] public Texture statusPlay;
    [Export] public Texture statusPlayGreyed;
    [Export] public Texture statusWatch;
    [Export] public Texture statusWatchGreyed;

    // Called when the node enters the scene tree for the first time.
    public void Init(Room room)
    {
        nameText.Text = room.Name;
        roomId = room.Id;
        SetState(room);
    }

    /**
 * Update prefab instance based on the corresponding Room state.
 */
    public void SetState(Room room)
    {
        int playerSlots = room.MaxUsers - room.UserCount;
        int spectatorSlots = room.MaxSpectators - room.SpectatorCount;

        // Set player count and spectator count in game list item
        detailsText.Text = String.Format("Player slots: {0} \nSpectator slots: {1}", playerSlots, spectatorSlots);
  
        // Enable/disable game play button
        if (playerSlots > 0) 
        {
            playButton.Disabled = false;
            playButton.TextureNormal = (Texture2D)statusPlay;
        }
        else
        {
            playButton.Disabled = true;
            playButton.TextureNormal = (Texture2D)statusPlayGreyed;

        }

        // Enable/disable game watch button
        if (spectatorSlots > 0)
        {
            watchButton.Disabled = false;
            watchButton.TextureNormal = (Texture2D)statusWatch;
        }
        else
        {
            watchButton.Disabled = true;
            watchButton.TextureNormal = (Texture2D)statusWatchGreyed;

        }
        
    }
}
