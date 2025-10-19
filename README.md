# Snake MVVM Project

![Coverage](https://img.shields.io/badge/coverage-18%25-orange) [![PassingTests](https://github.com/dev-92/Snake/actions/workflows/RunSnakeCoreTests.yml/badge.svg)](https://github.com/dev-92/Snake/actions/workflows/RunSnakeCoreTests.yml)

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

## 2 Planned Features
- Automatic **tagging** of releases (not yet implemented)
- Redesign main menu & game over screens
- Settings menu
- Highscore tracking  

## 3 Architecture

The project follows the **MVVM pattern**:

- **Model**: Handles game state, scoring, collectibles, and logic  
- **ViewModel**: Bridges the Model and View, exposing data in a testable way  
- **View**: User interface, responsible for rendering the game  

This separation ensures **testability**

## 4 Testing

- Core game logic is separated from UI for easier unit testing  
- Automated tests to run on every push  

## 5 License

This project is licensed under the MIT License - see LICENSE file for details.

## 6 Third-Party Assets

The following media are subject to separate licenses:

- **Pixabay**: All images and sounds used in this project are free to use, modify, and include in projects, but cannot be sold standalone. [Pixabay License](https://pixabay.com/service/license/)
- Future: CI/CD with automated tests and release tagging  

