# This Far and No Further

A small 2D arcade game made at **VAF Talent Jam 2026** (September 7–9, 2026) in Viborg, Denmark.

You play **Kjeld**, a priest standing in the middle of Viborg Cathedral, keeping the flames from the door. Fire comes flying in from every direction – aim your shield with the mouse and block it, build up your combo, and call down a divine blessing when things get too hot.

> Play it in the browser on itch.io: *https://mekkermb.itch.io/this-far-and-no-further*

## How to play

| Input | Action |
|---|---|
| Mouse | Aims the shield towards the cursor |
| W / A / S / D | Skill checks – press the right key when the ring hits the center |
| Space | "Nuke" / divine blessing – burns away every flame (has a cooldown) |
| Esc | Pause |

- Every flame you block gives points and +1 combo. The higher the combo, the more points per flame.
- Skill checks give a bonus multiplier (Perfect / Great / Good) – miss one and your combo resets.
- You have a limited number of lives. Getting hit gives you a couple of seconds of invincibility.
- High scores are saved locally to a leaderboard file and shown in the main menu and on the leaderboard scene.

## Tech

- **Unity 6** (6000.6.0f1), Universal Render Pipeline (2D)
- Unity Input System (action map in `Assets/InputActionsMap.inputactions`)
- UI Toolkit for menus and HUD, TextMesh Pro for text
- Object pooling for flames (`ObjectPooler` + `FireSpawner`)
- Custom shader for the nuke effect
- Built for WebGL

### Project structure

```
Assets/
  Art/          Sprites, sprite sheets and animations (Kjeld, flames, background, UI)
  Prefabs/      Flame prefab, among others
  Scenes/       MainMenu, Game, LeaderBoardScene
  Scripts/      Game logic (see below)
  Shaders/      Nuke shader
  Sound/        Music, SFX and AudioMixer
  UI/           UXML files for menus, HUD and game over
```

Key scripts:

- `PlayerController` – score, combo, health and i-frames
- `MouseHandler` / `ShieldScript` – shield direction and collision with fire
- `FireSpawner` / `FireScript` / `ObjectPooler` – spawning and movement of flames
- `Nuke` / `Ring` – the ultimate ability and its visual ring
- `SkillCheck` – WASD timing events
- `BarManager` – health and ult bars
- `LeaderboardHandler` – save/load high scores as JSON
- `MainMenu`, `PauseMenu`, `GameOverMenu` – menu flow

## Run it yourself

1. Clone the repo.
2. Open the folder in Unity Hub with Unity **6000.6.0f1** (or a newer Unity 6).
3. Open `Assets/Scenes/MainMenu.unity` and press Play.

## The team

| Name | Role |
|---|---|
| Mads Bisgaard (Mekkermb) | Programming, animation, audio, build & itch.io |
| Rasmus Jensen | Programming (shield, fire, nuke, combo, skill checks, leaderboard, shader) |
| Steffan Condamine (DerpGuts) | Menus/UI, sound and voice lines |
| Emil Mau (Robolamp) | Art – Kjeld, flames, background, health/ult bars |