# 🏡 Raspberry Pi Mumble Intercom: Hardware and Configuration

This document details the hardware components and software configuration required for the Murmur server and the Mumble client nodes.

---

## 1. Mumble Server (Murmur) Hardware
The server is responsible for routing all audio traffic and managing user/channel settings. A higher-spec Raspberry Pi is recommended for stability.

| Component | Quantity | Description | RPi Model |
| :--- | :--- | :--- | :--- |
| **Raspberry Pi** | 1 | Murmur server. Pi **3B+ or 4** recommended. | Server |
| **MicroSD Card (32GB)** | 1 | Class 10/U1 for OS (Raspberry Pi OS Lite) and Murmur data. | Server |
| **Power Supply** | 1 | Official RPi power supply (e.g., 5.1V, 3.0A for Pi 4). | Server |
| **Ethernet Cable** | 1 | **Recommended** for stable server operation. | Server |

---

## 2. Mumble Node (Client) Hardware
Each node is a room-based intercom station.

| Component | Quantity (per node) | Description | RPi Model |
| :--- | :--- | :--- | :--- |
| **Raspberry Pi Zero W** | N | Cost-effective and low-power client hardware. | Client Node |
| **MicroSD Card (16GB)** | N | Class 10/U1 for OS and the C# client application. | Client Node |
| **Small Touchscreen** | N | **3.5" or 5" DSI/SPI/HDMI touchscreen**. Used for recipient selection and PTT. | Client Node |
| **Combined USB Mic/Speaker** | N | A **USB all-in-one speakerphone** or a small USB sound card paired with a speaker and mic. Look for built-in **echo cancellation**. | Client Node |
| **Power Supply** | N | Official RPi Zero power supply (5V, 1.2A or greater). | Client Node |

---

## 3. Murmur Server Configuration Instructions

These steps assume **Raspberry Pi OS Lite** is installed on the server Pi and it is connected to the local network (obtain its IP, e.g., `192.168.1.50`).

### Step 3.1: Installation and Initial Setup

1.  **SSH into the Server Pi** and update the package list:
    ```bash
    sudo apt update
    sudo apt install mumble-server
    ```
2.  **Run Initial Configuration:**
    ```bash
    sudo dpkg-reconfigure mumble-server
    ```
    * Set **Autostart** to 'Yes'.
    * Set a strong **SuperUser** password (required for administration).

### Step 3.2: Configure `mumble-server.ini`

1.  Edit the Murmur configuration file:
    ```bash
    sudo nano /etc/mumble-server.ini
    ```
2.  Set the following (adjust as needed):
    ```ini
    # Set max users
    users=10
    # Set a friendly name for the server
    registerName=House_Intercom
    # Keep the default port unless you have a conflict
    port=64738
    # Deny entry to root to force users to an admin-created channel (optional)
    # allowRoot=false
    ```
3.  Save and exit.

### Step 3.3: Restart and Channel Setup

1.  **Restart Murmur** to apply changes:
    ```bash
    sudo service mumble-server restart
    ```
2.  **Admin Setup (Via PC/Mobile Mumble Client):**
    * Use a standard Mumble client to connect as **SuperUser** with the set password.
    * Right-click the **Root** channel and create the required channels:
        * One primary channel: **"All"** (Channel ID 1 or other). This is the mandatory broadcast channel.
        * Individual channels for selective calls: **"Kitchen," "Bedroom," "Office,"** etc.
    * Register a standard user (e.g., `Intercom_Node`) and note the username/password for client configuration.
