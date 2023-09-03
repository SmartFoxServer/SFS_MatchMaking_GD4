# SmartFoxServer Example Projects for Godot 4.x

This series of **C# examples** built for the Godot 4 engine have been developed with **Godot Mono 4.0.3**, but the concepts and the code to interact with the SFS2X API are valid for any version of Godot 4.x (unless otherwise noted).

Each of the tutorials in this series examine a single example, describing its objectives, **offering an insight into the SmartFoxServer features** it wants to highlight. This project includes all the assets required to compile and test the example (both client and — if existing — server side). If necessary, code excerpts are provided in the tutorial itself (see online documentation link below), in order to better explain the approach that was followed to implement a specific feature. At the bottom of the tutorial, additional resources are linked if available.

The tutorials have an increasing complexity, from basic server connection to a complete game with authoritative server code.

Specifically, the examples will showcase:

* basic connection with optional protocol encryption
* room management
* buddy list management
* **game rooms and match-making**
* authoritative server in a turn-based game

The Godot examples provided have been tested for exporting as native executables for Windows and macOS. At the time of writing this article (June 2023) Godot Mono does not yet support exporting for mobile platform or the browser.

# SFS_MatchMaking_GD4
This example expands those described in the Lobby: Basics and Lobby: Buddies tutorials by adding a match-making system, to help users find the right Game Room to join based on their skill, and invitations, to let friends meet and play with each other.

In this document we assume that you already went through the previous tutorials, where we explained the subdivision of the application into three scenes, how to create a GlobalManager class to share the connection to SmartFoxServer among scenes and how to implement and manage a buddy list.

<p align="center"> 
<img width="720" alt="connector-login" src="https://github.com/SmartFoxServer/SFS_LobbyBasics_GD4/assets/30838007/3001b9ec-4cc0-4316-9fe2-eaeecdc60a60">
 </p>h


## Setup & run
In order to setup and run the example, follow these steps:

1. unzip the examples package;
2. launch the Godot, click on the Import button and navigate to the Connector folder;
3. click the **Build button** in the top right corner of the Godot editor before running the example;

The client's C# code is in the Godot project's *res://scripts* folder, while the SmartFoxServer 2X client API DLLs are in the *res:// folder*.

## Online Tutorial and Documentation
The code for this example is divided into multiple classes contained in the res://scripts folder. The folder contains three <name>GlobalManager scripts which are attached to the control nodes in their respective scenes.
All managers are basic Godot C# scripts implementing the **_Ready**, **_Process** and **_Notification** methods where needed. They also contain the listeners for the events fired by UI components (i.e. buttons), some helper methods and the listeners for SmartFoxServer's client API events.

There is also a script called **GlobalManager** which is a singleton class, and holds a reference to the SmartFox class instance to share the client-server connection among the project's multiple scenes.

<p align="center"> 
<img width="720" alt="connector-login" src="https://github.com/SmartFoxServer/SFS_LobbyBasics_GD4/assets/30838007/c559cfd9-d0af-41f4-866e-65323a279c25">
 </p>

To learn more about this template and how it is configured for establishing a connection and handling basic SmartFoxServer events, go to the online documentation and tutorials linked below.

**SmartFoxServer Example Documentation**   

http://docs2x.smartfoxserver.com/ExamplesGodot/lobby-basics

This online documentation includes:
* Shared Connection and Login
* Joining a Game Room
* Exchanging in-game Chat Messages
  
 and further **Resource Links**

http://docs2x.smartfoxserver.com/ExamplesGodot/introduction

 <p align="center"> 
<img width="400" alt="connector-login" src="https://github.com/SmartFoxServer/SFS_Connector_GD4/assets/30838007/a8f025fb-5bc0-4ca6-8ce0-8ec808565303">
 </p>

