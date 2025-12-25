# Dwarf Machine

**Gameplay prototype focused on scalable architecture and systems design.**

This project was created to explore production-like development practices in Unity rather than to build a content-heavy game.  
The main goal is to design clean, extensible gameplay systems that can be easily expanded over time.

---

## Current Prototype Features

- Player movement and interaction
- Mountable combat mech
- Separate movement logic for player and mech
- Basic melee combat
- Configurable mech stats
- Enemy AI foundation: FSM-based behaviour system (WIP)

---

## Architectural Focus

The project prioritizes **code structure and extensibility** over visual complexity.

Key principles:
- Clear separation of data, logic, and input
- Minimal coupling between gameplay systems
- Predictable and scalable architecture
- Designed for future expansion (weapons, enemies, abilities, UI)

---

## Implemented Systems

### Input Handling
- Input logic is separated using a strategy-based approach
- Allows switching input behaviour without modifying gameplay logic

### Movement System
- Independent movement controllers for player and mech
- Shared interfaces where behaviour overlaps

### Stats & Configuration
- Entities parameters are defined via configuration (ScriptableObjects)
- No hardcoded gameplay values
- Designed to scale for upgrades and balance iterations

### Dependency Injection
- Core systems are wired through DI
- Reduces tight coupling and improves maintainability

### Enemy AI (FSM)
- Finite State Machine foundation for enemy behaviour
- Context object stores runtime data for AI decisions
- State IDs and factory approach to support multiple enemy types
- Wired through Zenject installer for clean dependencies

---

## 🛠 Tech Stack

- **Unity**
- **C#**
- Scriptable configurations
- Production-oriented Git workflow (feature-driven commits, versioning)

---

## Project Status

This is an **actively evolving prototype**.

Current focus:
- strengthening core gameplay systems
- keeping architecture clean and readable
- avoiding premature content and visual complexity

---

## Development Roadmap

The project follows a **version-based development roadmap**.  
Each milestone represents a focused development stage and will be **released as a tagged version in this repository**.

The roadmap reflects a system-first approach:  
core gameplay → mechanics → meta systems → presentation.

### Planned Milestones
[Latest Release](https://github.com/Hol1k/dwarf-machine/releases/latest)

- [x] **[v0.1 - Player Inputs](https://github.com/Hol1k/dwarf-machine/releases/tag/v0.1.0)**

  Core input handling and control schemes.

- [ ] **v0.2 - Enemies AI & Behaviour** *(in progress)*

  Basic enemy logic, states, and interactions.

- [ ] **v0.3 - Level Objects & First Game Level**  

  Environmental objects and the first playable level.

- [ ] **v0.4 - All Character Mechanics**  

  Finalization of player and mech core mechanics. 

- [ ] **v0.5 - Multiplayer**  

  Experimental multiplayer implementation.

- [ ] **v0.6 - Hub**  

  Central hub for navigation and progression.

- [ ] **v0.7 - Upgrading Mechanics**  

  Progression systems and upgrade logic.

- [ ] **v0.8 - Meta Balance**  

  Global balance tuning and meta-level systems.

- [ ] **v0.9 - Dungeon Balance**  

  Difficulty curves and gameplay pacing.

- [ ] **v0.10 - Art Design**  

  Visual pass and stylistic consistency.

- [ ] **v0.11 - Sound Design**  

  Sound effects and audio feedback.

- [ ] **v0.12 - UX / UI**  

  User interface and overall usability improvements.

- [ ] **Release - Demo Build**  

  Public demo showcasing the core gameplay loop.

---

## Devlog

Development progress, design decisions, and intermediate results are documented in the devlog:

**Telegram Devlog (RU):**  
https://t.me/hol1kDev

---

## Notes

This project is intentionally minimal in visuals and content.  
Its value lies in **how the systems are built**, not in how much content is present.

The prototype is designed as a foundation for further gameplay and technical experimentation.
