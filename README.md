# Snake MVVM Project

A modern implementation of the classic **Snake game**, designed as my first **MVVM (Model-View-ViewModel) project**. This project emphasizes **clean code, design patterns, unit testing**, and a robust **Git workflow**.

## Features

- Classic Snake gameplay with smooth controls
- Fully structured using the **MVVM architecture**
- Emphasis on **clean code principles** for maintainability
- Implementation of common **design patterns**
- **Unit tests** covering core logic and game mechanics
- Automated **unit testing** triggered on push
- Automatic **tagging** of releases

## Architecture

The project follows the **MVVM pattern**:

- **Model**: Handles game state, scoring, and logic
- **ViewModel**: Bridges the Model and View, exposing data in a testable way
- **View**: User interface, responsible for rendering the game

This separation ensures **testability** and **scalability**.

## Testing

- Unit tests cover core game mechanics and logic
- Automated tests run on every push to ensure code stability
- Test framework: `[Insert your testing framework here, e.g., NUnit, JUnit, PyTest]`

## Git Workflow

- Feature branches for development
- Pull requests for code review
- Automated tests on push using CI/CD
- Automatic tagging of releases for version management
