# Descent

Descent is a 2D top down roguelike dungeon crawler created in Unity for the CPSC 491 Senior Capstone Project at California State University, Fullerton.

**Team:** Inigo Zulueta, Christian Barajas, Jasper Liong, and Mary Ann Ndoria  
**Instructor:** Dr. Marc Velasco  
**Engine:** Unity 6000.3.5f2  
**Platform:** Windows

## About the Game

Explore a newly generated dungeon in every run, battle pursuing enemies, collect upgrades, and survive for as long as you can. Dungeon layouts are created at runtime with space partitioning and random walk generation, so each attempt offers a different path through the game.

### Core Features

* **Procedural dungeon generation:** Rooms are arranged on a grid and joined by randomized corridors.
* **Combat:** Attack nearby enemies with `E` or `Space`. Enemies chase the player and cause contact damage after a short cooldown.
* **Health:** The player and enemies have world space health bars that update immediately when damage is dealt.
* **Power ups:** Animated flask pickups placed throughout the dungeon increase the player's attack damage.
* **Experience:** Defeated enemies award experience points that can support future progression features.
* **Game over menu:** When the player dies, the game pauses and offers the choice to restart or return to the main menu.
* **Options:** Players can adjust master volume, resolution, and fullscreen mode. Settings are saved between sessions.
* **Music:** Background music continues across menu scenes without restarting or playing duplicate tracks.

## How to Run the Game

### Play a Built Version

1. Download the latest build artifact from [GitHub Actions](https://github.com/inigomz/Final%2DProject%2DCPSC%2D491/actions).
2. Extract the downloaded archive.
3. Run `Descent.exe`.

### Open the Project in Unity

You will need Unity **6000.3.5f2**, or a compatible Unity 6 release, with Universal Render Pipeline, 2D Tilemap Editor, and TextMeshPro installed.

1. Clone the repository from `https://github.com/inigomz/Final%2DProject%2DCPSC%2D491.git`.
2. Open Unity Hub.
3. Select **Open**, then choose the `Descent/` folder inside the repository.
4. Allow Unity to import the assets. The first import may take several minutes.
5. In the Project window, open `Assets/Scenes/TitleMenuScene`.
6. Select **Play** to launch the game in the editor.

If Unity enters Safe Mode because of compilation errors, select **Ignore** and let the project finish loading before entering Play Mode.

## Controls

* Move with `W`, `A`, `S`, `D`, or the arrow keys.
* Attack with `E` or `Space`.
* Navigate menus with the mouse.

## Scenes

* `TitleMenuScene` contains the main menu, including New Game, Resume, Options, and Quit.
* `OptionsMenu` contains volume, resolution, and fullscreen settings.
* `Test_room` is the primary gameplay scene containing the dungeon, enemies, and power ups.

## Project Structure

The Unity project is stored in `Descent/`. Gameplay code is under `Assets/Scripts/`, scenes are under `Assets/Scenes/`, and project configuration is under `ProjectSettings/`. GitHub Actions workflows are stored in `.github/workflows/`.

## Main Scripts

* `DungeonGenerator.cs` creates the procedural map.
* `PlayerAttack.cs` handles player attacks and input.
* `PlayerHealth.cs` manages player health, damage, and death.
* `EnemyAI.cs` controls enemy pursuit and contact damage.
* `EnemyHealth.cs` manages enemy health, death, and experience rewards.
* `PlayerXP.cs` records experience earned from defeated enemies.
* `GameManager.cs` resets player state when a new game begins.
* `PlayerSpawner.cs` creates the player when a scene starts.
* `MainMenuUI.cs` handles title menu actions and music initialization.
* `OptionsMenu.cs` controls volume, resolution, and fullscreen options.
* `SettingsManager.cs` saves settings between sessions.
* `SettingsData.cs` defines the saved settings data.

## Continuous Integration

GitHub Actions builds the project for Windows, checks that required gameplay scripts are present, and runs CodeQL security analysis. Pull requests require a peer review before they can be merged.

View [build results](https://github.com/inigomz/Final%2DProject%2DCPSC%2D491/actions) or report a problem through [GitHub Issues](https://github.com/inigomz/Final%2DProject%2DCPSC%2D491/issues).

## Known Issues

* Player and nonplayer character models can clip through stone obstacle tiles because collision has not yet been configured for that tilemap layer.
* Scene changes happen immediately because fade transitions have not been implemented.
* Some option preferences are not saved consistently.
* Loading a saved game generates a new tilemap instead of restoring the saved layout.
* Some menu buttons do not always play audio feedback because of a likely interface event binding issue.

## Testing

The team uses a five stage process: **Analyze → Build → Test → Review → Publish**. Each developer tests their work locally before opening a pull request, and every pull request requires at least one peer review. More than 50 test cases across three sprints cover combat, health, menus, scene transitions, audio, and collision behavior.

Detailed test cases, implementation notes, and design information are available in the [Game Design Document](https://docs.google.com/document/d/1eGPRH4ldziAnm1u%2DNpCPTW1WANdgroQVC3HUbJqbe2M/edit?usp=sharing).

## Team Contributions

* **Inigo Zulueta:** Procedural dungeon generation, continuous integration, bug fixes, and testing.
* **Christian Barajas:** Main menu, options menu, new game flow, health bars, game over interface, bug fixes, and testing.
* **Jasper Liong:** Player movement, combat, enemy behavior, experience system, power ups, bug fixes, and testing.
* **Mary Ann Ndoria:** Settings, persistent audio, volume controls, and quality assurance.

Built with care at California State University, Fullerton.
