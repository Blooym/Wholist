<div align="center">

<img src="./.assets/Icons/icon.png" alt="Wholist Logo" width="15%">
  
### Wholist

Show who's nearby you, inspired by the `/who` command from World of Warcraft 

[![Latest Version](https://img.shields.io/github/v/release/BitsOfAByte/Wholist?color=blue&label=Latest%20Version "Latest Version")](https://github.com/BitsOfAByte/Wholist/releases/latest)
[![Licence](https://img.shields.io/github/license/BitsOfAByte/Wholist?color=blue "Licence")](https://github.com/BitsOfAByte/Wholist/blob/main/LICENSE)

**[Issues](https://github.com/BitsOfAByte/Wholist/issues) · [Pull Requests](https://github.com/BitsOfAByte/Wholist/pulls) · [Releases](https://github.com/BitsOfAByte/Wholist/releases/latest)**

</div>

---

## About

Wholist is a plugin for [Dalamud](https://github.com/goatcorp/Dalamud) that allows players to easy see and interact with nearby players via a GUI list. Wholist was inspired by the `/who` command from World of Warcraft that provides similar functionality. Some features of Wholist include:

- Minimalistic interface that can be scaled down to become part of your HUD.
- Full localization support, using both game and Dalamud language settings when possible.
- Highlighting of "known players" such as friends and party members in the nearby player list.
- Player context menu items, like sending tells, examining, searching on the Lodestone and more.
- Fully customizable colours & behaviour.
- Integration support with other plugins via IPC ([developer guide](https://github.com/BitsOfAByte/Wholist/blob/main/Wholist/IPC.md))

## Installation

Wholist is installed by using the Dalamud Plugin Installer. Do not manually download the plugin from this repository or other 3rd party sources. No support will be provided for any modified versions of the plugin.

## Restrictions

Due to how Wholist fetches information about nearby players from the game it is impossible to increase how many players show on the "nearby players" list past what is available to the client.

It will also automatically hide the UI if you are partaking in Player vs Player content to prevent any competitive advantages that could come from using this plugin. This behaviour cannot be disabled to ensure competitive integrity.

## Screenshots

<details>
<summary>Nearby Players Window</summary>
<img src="./.assets/Screenshots/screenshot1.png" alt="Screenshot 1" width="50%">
</details>

<details>
<summary>Settings Window</summary>
<img src="./.assets/Screenshots/screenshot2.png" alt="Screenshot 2" width="65%">
</details>

## Contributing

Contributions are welcome as long as they follow Dalamud's rules for plugin creation and remain in-scope. Please use the [Commit Convention](https://github.com/BitsOfAByte/Wholist/blob/main/COMMIT_CONVENTION.md) when making commits to the repository. It is recommended to open an issue before making a pull request for a new feature to make sure it is in-scope.

If you want to help translate Wholist into your language you can do so through the [Crowdin project](https://crwd.in/wholist). If the language you want to translate to is not available please create an issue and it will be added.

## Licence

Wholist is licenced under the [AGPL-3.0 License](https://github.com/BitsOfAByte/Wholist/blob/main/LICENSE) and is maintained by [BitsOfAByte](https://github.com/BitsOfAByte)
