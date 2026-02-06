using DinaCSharp.Services;
using DinaCSharp.Services.Fonts;

namespace Dungeon100Steps.Core.Keys
{
    public class FontKeys
    {
        public readonly static Key<FontTag> Default = Key<FontTag>.FromString("Default");

        #region Menu principal
        public readonly static Key<FontTag> MainMenu_Title = Key<FontTag>.FromString("MainMenu_Title");
        public readonly static Key<FontTag> MainMenu_MenuItems = Key<FontTag>.FromString("MainMenu_MenuItems");
        #endregion

        #region Écran des options
        public readonly static Key<FontTag> Options_Title = Key<FontTag>.FromString("Options_Title");
        public readonly static Key<FontTag> Options_Category = Key<FontTag>.FromString("Options_Category");
        public readonly static Key<FontTag> Options_Label = Key<FontTag>.FromString("Options_Label");
        public readonly static Key<FontTag> Options_Texts = Key<FontTag>.FromString("Options_Texts");
        #endregion


        #region Écran du jeu
        public static readonly Key<FontTag> Progression_Label = Key<FontTag>.FromString("Progression_Label");
        public static readonly Key<FontTag> SelectPlayer_Label = Key<FontTag>.FromString("SelectPlayer_Label");

        // Écran de chargement
        public static readonly Key<FontTag> Loading_Title = Key<FontTag>.FromString("Loading_Title");
        public static readonly Key<FontTag> Loading_Message = Key<FontTag>.FromString("Loading_Message");

        // Écran d'attente entre 2 événements
        public static readonly Key<FontTag> Player_Label = Key<FontTag>.FromString("Player_Label");
        public static readonly Key<FontTag> Player_Value = Key<FontTag>.FromString("Player_Value");

        // Équipement
        public static readonly Key<FontTag> Equipment_Bonus_Label = Key<FontTag>.FromString("Equipment_Bonus_Label");
        public static readonly Key<FontTag> Equipment_Bonus_Text = Key<FontTag>.FromString("Equipment_Bonus_Text");
        public static readonly Key<FontTag> Equipment_Name = Key<FontTag>.FromString("Equipment_Name");

        #endregion

        #region Écran pour passer le tutoriel
        public static readonly Key<FontTag> TutorialSkip_Message = Key<FontTag>.FromString("TutorialSkip_Message");
        public static readonly Key<FontTag> TutorialSkip_Button_Label = Key<FontTag>.FromString("TutorialSkip_Button_Label");
        #endregion

        #region Écran du combat
        public static readonly Key<FontTag> PlayerMenu_Items = Key<FontTag>.FromString("PlayerMenu_Items");
        #endregion

        #region Écran de l'inventaire
        public static readonly Key<FontTag> Inventory_Button_Text = Key<FontTag>.FromString("Inventory_Button_Text");
        public static readonly Key<FontTag> Inventory_Item_Menu = Key<FontTag>.FromString("Inventory_Item_Menu");
        #endregion
    }
}
