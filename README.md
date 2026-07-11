# Descent

A 2D top-down roguelike dungeon crawler built in Unity for CPSC 491: Senior Capstone Project at California State University, Fullerton.

**Team:** Inigo Zulueta, Christian Barajas, Jasper Liong, Mary Ann Ndoria  
**Instructor:** Dr. Marc Velasco  
**Engine:** Unity 6000.3.5f2  
**Platform:** Windows (StandaloneWindows64)

---

## About the Game

Descent is a 2D roguelike dungeon crawler where the player navigates procedurally generated dungeons, fights enemies, collects power-ups, and tries to survive as long as possible. Each run features a unique map layout generated at runtime using a space-partitioning and random-walk algorithm.

### Core Features

- **Procedural Dungeon Generation** — Rooms are placed using a grid-partition algorithm and connected via randomized corridors. No two runs are the same.
- **Combat System** — Attack enemies in range using `E` or `Space`. Enemies pursue the player and deal damage on contact with a cooldown.
- **Health System** — Both the player and enemies have world-space health bars that update in real time.
- **Power-Ups** — Collect damage boost pickups (animated flasks) scattered throughout the dungeon to increase attack power.
- **XP System** — Defeating enemies awards XP, laying the groundwork for future progression mechanics.
- **Game Over & Restart** — When the player dies, the game pauses and presents a Game Over menu with options to restart or return to the main menu.
- **Options Menu** — Adjust master volume, screen resolution (1280×720, 1600×900, 1920×1080), and fullscreen mode. Settings persist via `PlayerPrefs`.
- **Background Music** — Continuous background music plays from the title screen and persists across menu navigation without duplication.

---

## How to Run the Game

### Open in Unity Editor

**Requirements:**
- Unity **6000.3.5f2** (or compatible Unity 6 version)
- Unity modules: **Universal Render Pipeline (URP)**, **2D Tilemap Editor**, **TextMeshPro**

**Steps:**

1. Clone the repository:
   ```
   git clone https://github.com/inigomz/Final-Project-CPSC-491.git
   ```
2. Open **Unity Hub**.
3. Click **Open** and navigate to the `Descent/` folder inside the cloned repo.
4. Let Unity import assets (this may take a few minutes on first open).
5. In the **Project** window, go to `Assets/Scenes/` and open `TitleMenuScene`.
6. Press the **Play** button to start the game in the editor.

> **Note:** If Unity opens in Safe Mode due to compile errors, click **Ignore** and allow the project to fully load before entering Play Mode.

---

## Controls

| Action | Key |
|---|---|
| Move | `W A S D` or Arrow Keys |
| Attack | `E` or `Space` |
| Navigate Menus | Mouse |

---

## Scenes

| Scene | Description |
|---|---|
| `TitleMenuScene` | Main menu — New Game, Resume, Options, Quit |
| `OptionsMenu` | Volume, resolution, and fullscreen settings |
| `Test_room` | Primary gameplay scene with dungeon, enemies, and power-ups |

---

## Project Structure

```
Descent/
├── Assets/
│   ├── Scripts/          # All C# gameplay scripts
│   ├── Scenes/           # Unity scene files
│   ├── PlayerCharacter/  # Player sprites and animations
│   ├── Resources/        # Prefabs and runtime-loaded assets
│   └── Settings/         # URP and render pipeline settings
├── ProjectSettings/
└── ...
.github/
└── workflows/
    ├── unity-build.yml         # CI build pipeline (Windows x64)
    └── check-core-scripts.yml  # Verifies required scripts exist on merge
```

---

## Scripts Overview

| Script | Purpose |
|---|---|
| `DungeonGenerator.cs` | Procedural map generation (partition + random walk) |
| `PlayerAttack.cs` | Range-based attack on `E`/`Space` input |
| `PlayerHealth.cs` | Player health, damage handling, death trigger |
| `EnemyAI.cs` | Enemy pursuit and contact damage with cooldown |
| `EnemyHealth.cs` | Enemy health, death, XP reward on kill |
| `PlayerXP.cs` | Tracks and adds XP when enemies die |
| `GameManager.cs` | Singleton — resets player state on New Game |
| `PlayerSpawner.cs` | Spawns one player at scene start |
| `MainMenuUI.cs` | Title menu button handlers and BGM initialization |
| `OptionsMenu.cs` | Volume, resolution, and fullscreen controls |
| `SettingsManager.cs` | Persists settings to JSON across sessions |
| `SettingsData.cs` | Data class for serialized settings values |

---

## CI/CD

The project uses GitHub Actions for continuous integration.

- **`unity-build.yml`** — Triggered on push and pull request to `main`. Builds the project for Windows x64 using `game-ci/unity-builder` and uploads the artifact.
- **`check-core-scripts.yml`** — Verifies that all required gameplay scripts exist in `Descent/Assets/Scripts/` before a merge is accepted.
- **CodeQL** — Static analysis for code security scanning.

CI results and build history: [GitHub Actions](https://github.com/inigomz/Final-Project-CPSC-491/actions)  
Bug and issue tracking: [GitHub Issues](https://github.com/inigomz/Final-Project-CPSC-491/issues)

---

## Known Issues

- Player and NPC models clip through **obstacle tiles** (stone boulders) — collision box not yet configured for the obstacle tilemap layer (TC-049, TC-050).
- **Fade transitions** between scenes are not yet implemented — scene changes are instantaneous (TC-023).
- **Options preferences** are not fully persisted via `UserPrefs.ini` in all cases (TC-025).
- When loading a saved game, the **tilemap regenerates** instead of restoring the saved layout (TC-051).
- Some UI buttons (**New Start**, **Options**) do not consistently trigger audio feedback — unrelated to the volume system; likely a UI event binding issue.

---

## Testing

The team follows a five-stage testing flow: **Analyze → Build → Test → Review → Publish**.

- Manual playtesting is the primary validation method for gameplay systems.
- Each developer tests their feature locally before submitting a pull request.
- All pull requests require at least one peer review and approval before merging.
- Over 50 test cases have been developed and executed across three sprints, covering combat, health, UI, scene transitions, audio, and collision systems.

See the full test case documentation in the [Game Design Document](https://docs.google.com/document/d/1eGPRH4ldziAnm1u-NpCPTW1WANdgroQVC3HUbJqbe2M/edit?usp=sharing).

---

## Documentation

Full design document (testing plans, implementation details, CI/CD writeups, operations):  
[Descent — Game Design Document](https://docs.google.com/document/d/1eGPRH4ldziAnm1u-NpCPTW1WANdgroQVC3HUbJqbe2M/edit?usp=sharing)

---

## Team

| Name | Role |
|---|---|
| Inigo Zulueta | Procedural dungeon generation, CI/CD pipeline, bug fixes, QA testing |
| Christian Barajas | Main menu, options menu, New Game system, health bars, Game Over UI, QA testing, bug fixes |
| Jasper Liong | Player movement, combat, enemy AI, XP system, power-ups, QA Testing, bug fixes |
| Mary Ann Ndoria | Settings system, audio persistence, volume controls, QA validation |

---

Built with love at California State University, Fullerton
