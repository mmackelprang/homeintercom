# 🚀 Mumble Intercom Project Plan

This document outlines the phased plan for developing the **Mumble Node Controller** software in modern C# (targeting .NET 8) for the Raspberry Pi Zero W intercom endpoints. The client software will use the **MumbleSharp** library.

---

## Phase 1: Core Connectivity and Basic Audio (Weeks 1-2)

**Goal:** Establish a stable, auto-connecting Mumble client with functional audio input/output.

| Task ID | Task Description | Deliverables & Metrics | Status |
| :--- | :--- | :--- | :--- |
| **1.1** | **Setup Client Environment** | RPi Zero W running RPi OS, **.NET 8 Runtime** installed, USB audio recognized (`aplay -l`, `arecord -l` check). | ⏳ Pending hardware |
| **1.2** | **Initial C# Project & Dependencies** | C# project created (targeting `net8.0`), **MumbleSharp** and **NAudio** NuGet packages installed. | ✅ **COMPLETE** |
| **1.3** | **Mumble Connection Logic** | `MumbleIntercomController` class implemented. Client successfully **connects** to the Murmur server IP (e.g., 192.168.1.50). | ✅ **COMPLETE** (Architecture) |
| **1.4** | **Seamless Connection/Reconnection** | Code logic to automatically join the server on launch and **re-establish connection** if dropped. Logs show successful connection status. | ✅ **COMPLETE** |
| **1.5** | **Automatic Channel Join** | Upon connection, the client successfully searches for and **joins the "All" channel** using its channel name. | ⚠️ Architecture ready, requires server |
| **1.6** | **Audio Capture & Transmission (PTT POC)** | **NAudio** configured to capture mic input. Implement PTT function that **Opus encodes** and transmits voice data to the current channel. | ✅ **COMPLETE** |
| **1.7** | **Audio Playback** | Implement handling of **received voice packets** from Murmur and play them back through **NAudio** to the speaker. | ✅ **COMPLETE** |

---

## Phase 2: Touchscreen UI and Intercom Logic (Weeks 3-4)

**Goal:** Implement the touchscreen UI, user/channel display, and the required recipient selection logic.

| Task ID | Task Description | Deliverables & Metrics |
| :--- | :--- | :--- |
| **2.1** | **UI Framework Setup** | Initial C# **Avalonia UI** project setup for cross-platform GUI. UI displays properly on the RPi Zero W touchscreen. |
| **2.2** | **Basic Status UI** | Display the Node's name (e.g., "Kitchen"), connection status (Online/Offline), and a primary "All" **Push-To-Talk (PTT)** button. |
| **2.3** | **User/Channel Tracking** | `IMumbleProtocol` events implemented to dynamically update a list of **connected users and available channels** received from the server. |
| **2.4** | **Recipient Selection UI** | Create a scrollable list/modal on the touchscreen to allow the user to select one of the following: **"All"** (broadcast), **individual User** (whisper/private message). or selected **set of individual users** |
| **2.5** | **PTT Logic Implementation** | PTT button press uses the selected recipient: **Broadcast to "All" channel** OR send a **Whisper (Private Message)** to the individual User or set of selected users. |
| **2.6** | **Intercom Flow Optimization** | Implement logic to automatically mute speaker output during PTT transmission to prevent feedback/echo *if hardware echo cancellation is insufficient*. |

---

## Phase 3: Finalization, Auto-Discovery, and Deployment (Weeks 5-6)

**Goal:** Polishing, implementing seamless node management, and creating a robust final deployment package.

| Task ID | Task Description | Deliverables & Metrics |
| :--- | :--- | :--- |
| **3.1** | **Murmur Server Discovery** | Implement **mDNS/Bonjour** or a simple **UDP broadcast client** to automatically discover the Murmur server's IP address on the local network. *If this proves unstable on the Zero W, revert to a fixed IP in config.* |
| **3.2** | **Audio Quality/Volume Control** | UI elements added for local **speaker volume control** (e.g., using **ALSA P/Invoke**). Fine-tune Opus encoding parameters for lowest latency. |
| **3.3** | **Notification Sounds** | Add a distinct audio notification (a "Roger Beep") upon PTT press/release. Add a different chime for incoming **Whisper/Private Messages**. |
| **3.4** | **Resource Optimization** | Profile the C# application for CPU/Memory usage on the Pi Zero W and optimize the UI and background tasks for minimal overhead. |
| **3.5** | **Systemd Service Setup** | Create a **Systemd service file** (`intercom.service`) to ensure the C# client application starts automatically as a background daemon upon Raspberry Pi boot. |
| **3.6** | **Final Casing & Documentation** | Assemble hardware into final casing and complete user/admin documentation. |
