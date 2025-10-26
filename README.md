# 🚀 Rocket Lander 2D — Physics-Based Gameplay & Level Progression System

## Overview
**Rocket Lander 2D** is a physics-based 2D game developed in **Unity**, where the player controls a rocket that must navigate through a series of handcrafted levels filled with gravitational forces, hazards, and challenges.  
The project emphasizes **realistic physics**, **modular and reusable code**, and **clean architectural design**, serving as a showcase of advanced gameplay programming principles.

---

## 🧩 Core Features
- 🪐 **Physics-Driven Movement** — The rocket’s movement relies entirely on Rigidbody2D physics, producing smooth, realistic thrust and inertia.
- 🌌 **Gravity & Force Zones** — Custom black holes, attractors, and directional force zones that dynamically affect player control.
- ⛽ **Fuel Mechanics** — Real-time fuel usage with pickups for refueling.
- 💎 **Pickups & Collectibles** — Coins and power-ups with reusable, event-driven logic.
- 📊 **Dynamic UI** — Displays live velocity, fuel, and level status.
- 💥 **Collision Feedback** — Crashing ends the round, while safe landings trigger success sequences.
- 🔊 **Immersive Sound Design** — Layered sound effects for thrust, collisions, pickups, and environmental ambience.
- ✨ **Shader-Based Visuals** — Custom visual effects to enhance atmosphere and readability.
- 🎯 **Level Progression System** — Handcrafted levels controlled by a fully coded system that handles success/failure, restarts, and advancement.

---

## 🧠 Technical Highlights
- **Clean, modular C# architecture** designed for scalability and reusability.
- Separation of gameplay logic into independent managers (forces, sound, UI, levels).
- **Event-driven communication** between systems for loose coupling and easy maintenance.
- Use of **ScriptableObjects** to manage data-driven force configurations.
- Centralized **Level Manager** controlling progression and state transitions.

---

## ⚙️ Tools & Technologies
- **Engine:** Unity  
- **Language:** C#  
- **Systems:** Rigidbody2D Physics, ScriptableObjects, Event System  
- **Audio:** Unity AudioSource (trigger-based playback)  
- **UI:** Unity Canvas (real-time feedback)  
- **Version Control:** Git + GitHub  

---

## 🚀 Gameplay Loop
1. Control the rocket using thrusters and physics-based input.  
2. Avoid obstacles, gravity wells, and fuel depletion.  
3. Land successfully to complete the level and move to the next.  
4. Crashing resets the current level.  
5. Repeat until all levels are completed.

---

## 📸 Screenshots & Media
*(Add gameplay GIFs or screenshots, e.g. `/Media/rocket_ui.gif`, `/Media/gravity_zone.gif`)*

---

## 🧩 Future Additions
- Additional levels and challenges.
- Expanded sound and particle feedback.
- More visual polish (camera shake, motion trails, etc.).

---

## 👤 Author
**Darío Calderón Tornero**  
2D/3D Videogame Programmer | Unity Developer  

[Portfolio Website](https://dariocalderondev.wordpress.com)  
[GitHub Profile](https://github.com/DarioCalderonTornero)

---
