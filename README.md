<div align="center">

<img src="./.assets/Icons/icon.png" alt="Wholist Logo" width="15%">
  
### Wholist

Show who's nearby you, inspired by the `/who` command from World of Warcraft 

[![Latest Version](https://img.shields.io/github/v/release/BitsOfAByte/Wholist?color=blue&label=Latest%20Version)](https://github.com/BitsOfAByte/Wholist/releases/latest)
[![Licence](https://img.shields.io/github/license/BitsOfAByte/Wholist?color=blue)](https://github.com/BitsOfAByte/Wholist/blob/main/LICENSE)

**[Issues](https://github.com/BitsOfAByte/Wholist/issues) · [Pull Requests](https://github.com/BitsOfAByte/Wholist/pulls) · [Releases](https://github.com/BitsOfAByte/Wholist/releases/latest)**

</div>

---

## About

Wholist implements a nearby player list, inspired by the `/who` command from World of Warcraft. It provides a simple way to search for players nearby and do common actions such as examining, viewing adventure plates, finding them on the map and more.

## Features

- Minimalistic UI that can be scaled down to become a part of your everyday UI.
- Full localization support, using both game language and Dalamud's language settings where applicable.
- Automatic removal of "bot-like" players from the list.
- Built in context-menu for examining, targeting, viewing adventurer plates and more.
- Highlighting of your friends and party members in the list.
- Fully customizable colours for both job role colours and highlight colours.
- Integrates with other plugins via IPC ([learn more](https://github.com/BitsOfAByte/Wholist/blob/main/Wholist/IPC.md))

## Installation

You can install Wholist by using the Dalamud Plugin Installer. Do not manually download the plugin from this repository, or other 3rd party sources. If you are unsure about how to install plugins, please review Dalamud's FAQ.

## Configuration

Wholist has a built-in settings window that can be accessed by clicking the cog icon next in the plugin installer or by typing `/whosettings` in-game.

## Restrictions

Due to how Wholist fetches information about nearby players from the game it is impossible to increase how many players show on the "nearby players" list past what is available to the client.

It will also automatically hide the UI if you are partaking in Player vs Player content to prevent any competitive advantages that could come from using this plugin. This cannot be disabled.

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

Contributions are welcome as long as they follow Dalamud's rules for plugin creation and remain in-scope. Please use the [Commit Convention](https://github.com/BitsOfAByte/Wholist/blob/main/COMMIT_CONVENTION.md) when making commits to the repository.

If you want to help translate Wholist into your language, you can do so through the [Crowdin project](https://crwd.in/wholist). If the language you want to translate to is not available, please create an issue and it will be added.

## Licence

Wholist is licenced under the [AGPL-3.0 License](https://github.com/BitsOfAByte/Wholist/blob/main/LICENSE) and is maintained by [BitsOfAByte](https://github.com/BitsOfAByte)
