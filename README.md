# Generic-Survivor-Prototype-C#-
modular C# scripts and gameplay mechanics developed during my first Unity game prototypes.

Unity C# Mechanics Library — Generic Survivor Shooter

A collection of C# scripts and gameplay mechanics developed for my Generic Survivor Shooter prototype, built with Unity 6.

This repository documents the programming and gameplay systems I developed during my first week developing game prototypes. It serves as a practical showcase of the C# concepts, gameplay logic, and Unity systems I was able to implement in a complete playable prototype.

Project Overview
A horde-survival game inspired by top-down wave shooters.

The game features:
Progressive enemy difficulty
Level-based ability unlocks
Multiple enemy types
Automated enemy spawning
Player health and progression systems
Boss encounters
Win/Loss conditions
Basic UI and game state management

Core C# & Unity Systems
Clamped Dynamic Scaling
Implemented stat progression clamping to safely control the growth of player attributes such as:
Movement Speed
Maximum Health
Bullet Count
Projectile Velocity
This prevents values from exceeding predefined limits as the game progresses.

Milestone Ability Unlocks
Created a level-based progression system that unlocks player abilities at specific milestones:
Level 2 — Automated Turret 
Level 4 — Burst Heal 
Level 6 — Ultimate Ability Automated Wave & Spawner Logic

Implemented a time-based enemy spawning system.
The system progressively reduces enemy spawn intervals as the game progresses and supports multiple enemy archetypes with different movement speeds and health values.

Boss & Game State Management
Implemented a game state system responsible for:
Boss spawning after a set progression period
Monitoring the player's life state
Detecting boss defeat
Triggering Win/Loss states
Managing corresponding UI transitions

Script Overview Scripts/
Scripts/
├── PlayerController.cs
│   └── Handles player movement, stat scaling, leveling,
│       abilities, health, and player-related formulas.
│
├── BossController.cs
│   └── Handles boss movement, health, and projectiles.
│
├── BulletController.cs
│   └── Handles player bullet properties and behavior.
│
├── DenominatorController.cs
│   └── Spawns at 599 seconds and removes remaining enemies.
│       Removed enemies do not award additional score.
│
├── EnemyController.cs
│   └── Handles enemy movement, NavMesh navigation,
│       and enemy statistics.
│
├── TurretController.cs
│   └── Handles the automated turret ability activated
│       through the J key.
│
├── UltimateController.cs
│   └── Handles the ultimate ability activated through
│       the L key and its area damage behavior.
│
├── CameraFollow.cs
│   └── Keeps the camera following the player's position.
│
├── MenuManager.cs
│   └── Handles game UI and related time-management logic.
│
└── GameManager.cs
    └── Handles player score, health and skill UI,
        boss health, and background music.

What I Practiced 
Through this prototype, I practiced:
C# scripting
Object-oriented programming fundamentals
Unity MonoBehaviour scripting
Player movement and input handling
Health and damage systems
Experience and leveling systems
Stat clamping
Ability and skill systems
Enemy AI navigation using NavMesh
Coroutine-based game logic
Enemy spawning systems
Boss encounters
Game progression
Win/Loss state management
Basic UI management
Basic audio implementation

About This Project
This project was created as part of my first week of developing my own games.
The goal was not to create a polished commercial game, but to take the concepts I was learning and turn them into a complete playable prototype.
There are areas of the code and game architecture that I would improve as I continue learning, but I wanted to keep this repository as a record of my early development progress and the systems I was able to build independently.

Assets
The 3D assets and prefabs used in the prototype were created by me using Blender.

Future Improvements
Some systems I would improve in future iterations include:
More flexible player abilities and stat progression
More varied enemy behaviors
More advanced enemy progression
Additional projectile types
Power-up systems
More flexible input configuration
Improved game architecture and code organization
More advanced gameplay balancing

The playable version of the game is available on my itch.io page.
Itch.io: https://binawryyy.itch.io/generic-survivor-prototype
