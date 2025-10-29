# 🚀 Rocket Lander 2D — Physics-Based Gameplay, Level Progression & Gamepad Haptics

## Overview
**Rocket Lander 2D** is a physics-based game built with **Unity (C#)** where you steer a rocket through a series of **handcrafted levels** filled with force fields, hazards and tight fuel management.  
The project emphasizes **clean, scalable architecture**, **robust input handling** (keyboard + **gamepad with haptics**) and strong player feedback through UI, audio and visuals.

---

## 🧩 Core Features
- 🪐 **Physics-Driven Movement** — Motion handled via `Rigidbody2D` (thrust + torque) for smooth, physical control.
- 🌌 **Gravity & Force Zones** — Black holes, attractors and directional fields affecting trajectory.
- 🎯 **Level Progression System** — Hand-authored levels managed by code (win → advance, crash → retry).
- ⛽ **Fuel Mechanics & Pickups** — Real-time fuel consumption + refuel items; coins/collectibles with reusable logic.
- 📊 **Live UI Feedback** — Real-time **speed**, **fuel** and **state** display.
- 🔊 **Immersive Audio** — SFX for thrust, collisions, pickups and landings.
- ✨ **Shader/FX Polish** — Custom visuals to enhance readability and atmosphere.

### 🎮 Input System (Key Strength)
- 🕹️ **Clean & scalable input architecture** (Unity Input System).
- 🔄 **Runtime device detection**: seamlessly switches between **keyboard/mouse** and **gamepad**.
- 🤲 **Full gamepad support** with **haptic feedback/rumble** on key events (start, crash, landing, low fuel).
- 🧼 Decoupled input → gameplay via events for maintainability and easy extension.

---

## 🧠 Technical Highlights
- Modular C# architecture with **event-driven** communication (loose coupling).
- **ScriptableObjects** for data-driven force configurations.
- Separate managers for **game flow**, **UI**, **audio** and **forces**.
- Level controller handling success/failure, state transitions and scoring hooks.

---

## ⚙️ Tech Stack
- **Engine:** Unity  
- **Language:** C#  
- **Systems:** Rigidbody2D, ScriptableObjects, Unity Input System  
- **Audio:** AudioSource (+ event triggers)  
- **UI:** Unity Canvas  
- **Platforms:** Windows (keyboard + gamepad)

---

## 🎮 Controls
**Keyboard**
- `W / Up` — Thrust
- `A / Left` — Rotate left
- `D / Right` — Rotate right

**Gamepad**
- `A / South` — Thrust
- `Left Stick` — Rotate (or `LB/RB` if configured)
- **Haptics** — Rumble on crash/landing/low fuel/start

> Device switching happens **in real time**; UI and handling adapt accordingly.

---

## 🚀 Gameplay Loop
1. Apply thrust and rotation to navigate safely.  
2. Manage fuel, avoid hazards and use force zones to your advantage.  
3. **Land successfully → next level**.  
4. **Crash → retry current level**.  
5. Clear all handcrafted stages.

---

## 📸 Media



https://github.com/user-attachments/assets/fee5702a-8f00-4a04-8a36-d976fec5f85f


---

## 🧩 Future Work
- More handcrafted levels and difficulty tuning.
- Extra visual polish (camera shake/motion trails).
- Additional SFX layers and contextual haptics.

---

## 📝 How to Run
1. Clone the repository  
2. Open with **Unity 2022.3 LTS** (or your target LTS)  
3. Open scene: `Scenes/Game.unity`  
4. Press **Play** (connect a gamepad to test haptics)

---

## 👤 Author
**Darío Calderón Tornero** — 2D/3D Videogame Programmer (Unity)  
Portfolio · Itch.io · LinkedIn · GitHub

