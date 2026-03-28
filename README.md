# Snake MVVM Project

![Coverage](https://img.shields.io/badge/Coverage-18%25-orange) [![Core Unit Tests](https://github.com/dev-92/Snake/actions/workflows/RunSnakeCoreTests.yml/badge.svg)](https://github.com/dev-92/Snake/actions/workflows/RunSnakeCoreTests.yml)

A modern implementation of a **Snake-inspired game** with multiple collectible prey items, designed as my first **MVVM (Model-View-ViewModel) project**.  
This project was created **as a learning project** to take my first steps in **design patterns and software architecture**, with an emphasis on **clean code** and **testability**.  

<img width="800" height="950" alt="{C3EA87D9-5745-4308-B3E4-7F990F40D2DA}" src="https://github.com/user-attachments/assets/a5a89439-f632-4715-b97f-13e2d66263bc" />


## 1 Features

- Snake-inspired gameplay
- Multiple **collectible prey items**, each with unique effects  
- Fully structured using the **MVVM architecture**  
- Emphasis on **clean code principles** for maintainability  
- Implementation of common **design patterns**  
- Project is **testable** due to separation of UI and core logic  
- **Unit tests** for main core logic

## 2 Overview
- As there is no explanation implemented for now here are some further informations
### Controls
- Steering is provided by arrow keys
- By pressing escape the game will be paused (Start-Game Butt)
### Prey
- The score of collecting prey will be multiplied by the current speed factor (in both directions)
- <img width="30" height="30" alt="cherry" src="https://github.com/user-attachments/assets/86eb21d3-bf01-49c9-aa4a-fcb2fa6d5bba" /> Current speed factor *= 1.5 , base score = 3
- <img width="30" height="30" alt="apple" src="https://github.com/user-attachments/assets/cbca9668-08ec-4ece-bbba-7cdf4f746a51" /> Current speed factor *= -1.5, base score = -2
- <img width="30" height="30" alt="bomb" src="https://github.com/user-attachments/assets/5a68261b-43bf-4c18-9fdd-7a31cd27de27" /> Base score = -10 
- <img width="30" height="30" alt="mouse" src="https://github.com/user-attachments/assets/c576c3ef-f68c-4a84-933d-fee4c5887732" /> Base score = 1, +1 to tail
- <img width="30" height="30" alt="duck" src="https://github.com/user-attachments/assets/503a889d-f40d-4fa2-ab6b-bb66f958d36e" /> Base score = 2, +2 to tail **Quack !**
- <img width="30" height="30" alt="rabbit" src="https://github.com/user-attachments/assets/d3ae9bfc-77c2-4e8c-b4bc-8255b532a149" /> Base score = 3, +3 to tail

### Infoboard
- <img width="30" height="30" alt="speed" src="https://github.com/user-attachments/assets/5e121172-41f1-4ea2-8cdc-716e0ec00edf" /> Current speed factor
- <img width="30" height="30" alt="score" src="https://github.com/user-attachments/assets/2c403afd-eb56-4902-aeb1-b628b25e390f" /> Current score
- <img width="30" height="30" alt="length" src="https://github.com/user-attachments/assets/bfa59ce8-913e-483f-afc0-859c32f1b76e" /> Current length of snake
- <img width="30" height="30" alt="crown" src="https://github.com/user-attachments/assets/d2d843d6-9d47-411e-92e4-3e27eeba726b" /> Highscore (not implemented yet)


## 3 Possible Future Features
(As this is a project to primarily work on architecture, clean code and testing they may not be implemented)
- Automatic **tagging** of releases (not yet implemented)
- Add advanced designs to main menu & game over screen
- Fill settings menu
- Variable gameboard size (amount of fields)
- Highscore tracking
- Controls overview
- Infoboard symbols overview
- Collectable items (prey) effect overview

## 4 Known issues
- After starting a new game the "Start Game" button should switch to "Continue" in case the game is paused and not game over
- One unit test ist currently failing from time-to-time

## 5 Architecture
The project follows the **MVVM pattern**:

- **Model**: Handles game state, scoring, collectibles, and logic  
- **ViewModel**: Bridges the Model and View, exposing data in a testable way  
- **View**: User interface, responsible for rendering the game  

This separation ensures **testability**

## 6 Testing
- Core game logic is separated from UI for easier unit testing  
- Automated tests to run on every push  

## 7 License
This project is licensed under the MIT License - see LICENSE file for details.

## 8 Third-Party Assets

The following media are subject to separate licenses:

- **Pixabay**: All images and sounds used in this project are free to use, modify, and include in projects, but cannot be sold standalone. [Pixabay License](https://pixabay.com/service/license/)
