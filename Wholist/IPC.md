<div align="center">

<img src="../.assets/Icons/icon.png" alt="Wholist Logo" width="15%">
  
### Wholist

</div>

---

## What can I do with Wholist IPC?

Utilising Wholist's IPC, you can create custom context menu entries on players in the "Nearby Players" list. The player information will be passed to your plugin alongside your own `ImGuiNET` draw action, allowing you to use the information to create custom actions that interact with the player.

## How do I use Wholist IPC?

For sake of brevity it is assumed that you already understand how to use Dalamud's IPC system. If you do not, have a look at some other plugins that use IPC to get a better understanding of how it works.

When Wholist's IPC is available it will broadcast `Wholist.Available` which can be listened for in your own plugin. When this is received, you can then register your own context menu entries by invoking `Wholist.RegisterPlayerContextMenu` and then saving the returned GUID to a variable. This GUID is used to identify your context menu entries when they are invoked and also to unregister them when you no longer need them.

> **Information**   
> It is recommended to check `Wholist.ApiVersion` to ensure that the version of Wholist that is installed is compatible with your plugin IPC implementation. If it is not, you should not register your context menu entries and display a warning or similar.

You will then need to register an action to `Wholist.InvokePlayerContextMenu` which will be invoked when your context menu is being hovered over or is active in the User Interface. This action will be passed the GUID of the context menu entry that is being invoked as well as the `PlayerCharacter` object that the context menu entry is being invoked on. You should then use this information to draw your own custom ImGuiNet elements (although please keep in mind that these elements are drawn inside of `ImGui.BeginMenu` already.

When you no longer need your context menu entries, you can unregister them by invoking `Wholist.UnregisterPlayerContextMenu` and passing the GUID that was returned when you registered them, which will remove them from the list.

If you need to update your context menu entries, you will need to unregister them and then re-register them again.

If you need more information, please have a look at the source code for the [Wholist Inbound Ipc Manager](./IntegrationHandling/InboundIpcManager.cs) which handles all of the IPC for Wholist. Feel free to reach out if you have any questions.
