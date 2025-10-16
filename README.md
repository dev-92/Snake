# Snake MVVM Project

![Coverage](https://img.shields.io/badge/coverage-8%25-orange) 

A modern implementation of a **Snake-inspired game** with multiple collectible prey items, designed as my first **MVVM (Model-View-ViewModel) project**.  
This project was created **as a learning project** to take my first steps in **design patterns and software architecture**, with an emphasis on **clean code** and **testability**.  

<img width="800" height="950" alt="Snake Screenshot" src="https://github.com/user-attachments/assets/9e34b63b-0e59-46fa-951a-db4534d9cd58" />

## Features

- Snake-inspired gameplay with **smooth controls**  
- Multiple **collectible prey items**, each with unique effects  
- Fully structured using the **MVVM architecture**  
- Emphasis on **clean code principles** for maintainability  
- Implementation of common **design patterns**  
- Project is **testable** due to separation of UI and core logic  
- **Unit testing prepared** (currently only one basic test is available)  

### Planned Features
- Automated **unit testing** triggered on push (not yet implemented)  
- Automatic **tagging** of releases (not yet implemented)  
- Game over mechanics  
- Title screen  
- Highscore tracking  

---

## Architecture

The project follows the **MVVM pattern**:

- **Model**: Handles game state, scoring, collectibles, and logic  
- **ViewModel**: Bridges the Model and View, exposing data in a testable way  
- **View**: User interface, responsible for rendering the game  

This separation ensures **testability** and **scalability**.

---

## Testing

- Core game logic is separated from UI for easier unit testing  
- **Currently only one unit test is available**  
- Future: Automated tests to run on every push  

---

## License

This project is licensed under the MIT License - see LICENSE file for details.

## Third-Party Assets

The following media are subject to separate licenses:

- **Pixabay**: All images and sounds used in this project are free to use, modify, and include in projects, but cannot be sold standalone. [Pixabay License](https://pixabay.com/service/license/)
- Future: CI/CD with automated tests and release tagging  

