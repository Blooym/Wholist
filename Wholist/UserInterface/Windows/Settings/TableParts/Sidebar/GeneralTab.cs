using Sirensong.UserInterface;
using Wholist.Resources.Localization;
using Wholist.UserInterface.Windows.Settings.Components;

namespace Wholist.UserInterface.Windows.Settings.TableParts.Sidebar
{
    internal sealed class GeneralTab
    {
        public static void Draw(SettingsLogic _)
        {
            SiGui.Heading(Strings.UserInterface_Settings_General_Heading);

            Checkbox.Draw(Strings.UserInterface_Settings_General_OpenOnLogin, Strings.UserInterface_Settings_General_OpenOnLogin_Description, ref SettingsLogic.Configuration.NearbyPlayers.OpenOnLogin);
            Checkbox.Draw(Strings.UserInterface_Settings_General_LockPosition, Strings.UserInterface_Settings_General_LockPosition_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockPosition);
            Checkbox.Draw(Strings.UserInterface_Settings_General_LockSize, Strings.UserInterface_Settings_General_LockSize_Description, ref SettingsLogic.Configuration.NearbyPlayers.LockSize);

            Checkbox.Draw(Strings.UserInterface_Settings_General_HideInCombat, Strings.UserInterface_Settings_General_HideInCombat_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInCombat);
            Checkbox.Draw(Strings.UserInterface_Settings_General_HideInInstance, Strings.UserInterface_Settings_General_HideInInstance_Description, ref SettingsLogic.Configuration.NearbyPlayers.HideInInstance);

            Checkbox.Draw(Strings.UserInterface_Settings_General_FilterAFKPlayers, Strings.UserInterface_Settings_General_FilterAFKPlayers_Description, ref SettingsLogic.Configuration.NearbyPlayers.FilterAfk);
        }
    }
}