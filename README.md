# Endless Runner 3D - Unity Project

## Overview
This is a 3D Endless Runner game built using Unity. The game focuses on player movement, dynamic environment generation, obstacle spawning, and user interface (UI) integration. The primary goal of the project is to learn and implement core game development concepts such as object movement, game mechanics, score tracking, and UI handling in Unity.

---

## Project Features

### **Core Game Mechanics:**
- **Player Movement:** The player character automatically moves forward, and the player can control the character's movement to avoid obstacles by moving left or right.
- **Obstacle Spawning:** Obstacles are generated dynamically as the player moves forward, increasing the game's difficulty as the player progresses.
- **Dynamic Path Generation:** The game continuously generates a path that the player follows, providing a dynamic and never-ending game environment.
- **Game Over Mechanism:** When the player collides with an obstacle, the game ends, and a Game Over screen is displayed.
- **Score Tracking:** The player's score increases as they continue to run, and the score is displayed on the screen. The best (high) score is saved and displayed as a reference for future sessions.

### **User Interface (UI):**
- **Score Display:** A running score is shown at the top of the screen, updating as the player progresses.
- **High Score Display:** The highest score (best record) is saved and displayed on the screen. The game will remember the high score across sessions.
- **Game Over UI:** After the player dies, a Game Over screen appears with the option to restart the game.

### **Graphics and Assets:**
- The game uses free assets available in the Unity Asset Store, such as character models, textures, and environment assets, to create a visually engaging environment.
- The environment is procedurally generated with obstacles placed dynamically along the player's path.

---

## How to Play

1. **Start the Game:** Press the "Start" button to begin playing.
2. **Player Controls:** 
   - **Arrow keys** or **A/D** to move the character left and right.
   - **Escape** to pause the game or initiate a game over screen.
3. **Game Over and Restart:** 
   - When the player collides with an obstacle, the game ends.
   - The best score is saved and shown.
   - Press **"R"** to restart the game and try again.
   - Press **"H"** to reset the HighScore and try again.

---


## Game Logic & Key Components

### **GameManager:**
The core script of the game is the `GameManager`, which handles:
- **Score and High Score Management:** Tracks the player's current score and updates the high score if a new record is achieved.
- **Obstacle Spawning:** The `GameManager` periodically spawns obstacles for the player to avoid.
- **Game Over Handling:** Detects when the player dies and triggers the Game Over screen.
- **Scene Transitions:** Handles switching between different scenes, such as the main menu and game-over screen.

### **FollowTransform:**
The `FollowTransform` script ensures that certain objects (like the camera or UI elements) follow the player’s movement on specified axes, providing a smooth visual experience.

### **PlayerMovement:**
Handles the movement of the player character, including:
- **Automatic forward movement.**
- **Player-controlled lateral movement (left and right).**

---

## Challenges Faced During Development
During the development of this game, several challenges were encountered, including:
- **Procedural Level Generation:** Creating a seamless and dynamic environment for the player to run through without visible seams or interruptions.
- **Obstacle Detection and Collisions:** Ensuring that the player could reliably detect and avoid obstacles, which required fine-tuning collision detection and player movement.
- **Game Over Handling:** Implementing a smooth transition between the gameplay and the Game Over screen, ensuring the player's progress is saved and the high score is updated.
- **UI Integration:** Properly linking the UI elements such as score, high score, and restart buttons, and ensuring they update dynamically during the game.

---

## Additional Features to Add
- **Sound Effects and Music:** Adding background music and sound effects for player actions like jumping and colliding with obstacles.

---

## Conclusion
This Endless Runner game is a great starting point for learning game development in Unity. The game features a straightforward mechanic with dynamic environment generation, obstacle avoidance, and score tracking. Through this project, key game development concepts were learned, including player movement, UI integration, and procedural content generation.

---

