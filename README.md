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
- Enemy AI foundation: FSM-based behaviour system
- Multiple enemy types with unique behaviours:
  - Silver Swarms - swarm intelligence with coordination
  - Vein Devourers - solitary aggressive hunters
  - Soldiers - tactical enemy units
  - Aborigines (Melee & Ranged variants) - tribal faction with team mechanics
- Points of Interest (POI) system with faction control
- Async enemy spawning system with dynamic factories

---

## Architectural Focus

The project prioritizes **code structure and extensibility** over visual complexity.

Key principles:
- Clear separation of data, logic, and input
- Minimal coupling between gameplay systems
- Predictable and scalable architecture
- Designed for future expansion (weapons, enemies, abilities, UI)

---

## Technical Documentation

- **[Design & development wiki](https://www.notion.so/Home-Page-2e7b1d042691803b9ca8f44b374dd507)** (Notion)

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

### Enemy AI System (FSM)
- **Multiple Enemy Types:**
  - Silver Swarms: Swarm behavior with collective intelligence
  - Soldiers: Basic tactical units
  - Vein Devourers: Solitary hunters with extended patrol behavior
  - Aborigines: Tribal enemies with team coordination
    - Melee variant: Close-range combat specialists
    - Ranged variant: Support units with conditional attack logic
  
- **AI Architecture:**
  - Finite State Machine (FSM) using EnemyFsmState pattern
  - Context-based decision making (HumanoidAiContext, etc.)
  - Shared states through inheritance (Idle, Patrol, Alert, Combat, Reposition)
  - Type-specific overrides for unique behavior
  - Wired through Zenject for clean dependency injection

### Spawning & World System
- **Dynamic Enemy Spawning:**
  - Async spawning with UniTask integration
  - Enemy spawner factories for factory pattern implementation
  - Configurable spawner instances per enemy type
  
- **Points of Interest (POI):**
  - Configurable spawn, patrol, and reposition points
  - Type-based location control (e.g., AboriginesCamp - only occupied by aborigines)
  - Support for multiple faction interactions
  - Struct-based point collections for performance

### Enemy Combat System
- **Type-specific combat components** (abstract base with implementations)
- **Layer mask filtering** for attack detection
- **Inventory integration** for mech/player damage calculations
- **Movement controller** with request queuing when agent is disabled

---

## 🛠 Tech Stack

- **Unity**
- **C#**
- **UniTask**
- **Zenject**
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
  <sub>[Task tracking](https://yougile.com/board/bx0d6ibecvi1)</sub>

- [x] **[v0.2 - Enemies AI & Behaviour](https://github.com/Hol1k/dwarf-machine/releases/tag/v0.2.0)**

  Basic enemy logic, states, and interactions.
  <sub>[Task tracking](https://yougile.com/board/t2oo1aab3rr1)</sub>

- [ ] **v0.3 - Level Objects & First Game Level** *(in progress)*

  Environmental objects and the first playable level.

- [ ] **v0.4 - All Character Mechanics**  

  Finalization of player and mech core mechanics. 

- [ ] **v0.5 - Multiplayer**  

  Experimental multiplayer implementation.

- [ ] **v0.6 - Dungeon Balance**  

  Difficulty curves and gameplay pacing.

- [ ] **v0.7 - Art Design**  

  Visual pass and stylistic consistency.

- [ ] **v0.8 - Sound Design**  

  Sound effects and audio feedback.

- [ ] **v0.9 - UX / UI**  

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
