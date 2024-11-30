using Sirensong.UserInterface;
using Wholist.Common;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal static class WindowTab
    {
        internal static void Draw()
        {
            SiGui.Heading(Strings.UserInterface_Settings_Visibility_Heading);
            DrawVisibilityOptions();

            SiGui.Heading(Strings.UserInterface_Settings_Positional_Heading);
            DrawPositionalOptions();
        }

        private static void DrawVisibilityOptions()
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin, Strings.UserInterface_Settings_NearbyPlayers_OpenOnLogin_Description, ref Services.Configuration.NearbyPlayers.OpenOnLogin);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInCombat, Strings.UserInterface_Settings_NearbyPlayers_HideInCombat_Description, ref Services.Configuration.NearbyPlayers.HideInCombat);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_HideInInstance, Strings.UserInterface_Settings_NearbyPlayers_HideInInstance_Description, ref Services.Configuration.NearbyPlayers.HideInInstance);
        }

        private static void DrawPositionalOptions()
        {
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockPosition, Strings.UserInterface_Settings_NearbyPlayers_LockPosition_Description, ref Services.Configuration.NearbyPlayers.LockPosition);
            Checkbox.Draw(Strings.UserInterface_Settings_NearbyPlayers_LockSize, Strings.UserInterface_Settings_NearbyPlayers_LockSize_Description, ref Services.Configuration.NearbyPlayers.LockSize);
        }
    }
}
