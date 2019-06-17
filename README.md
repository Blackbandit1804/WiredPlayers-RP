<h3>INFO!</h3>
<pre>
Ich möchte euch darauf aufmerksam machen das dies NICHT mein Script ist! 
dieses Script muss man leider absofort bei dem Programmierer kaufen weshalb
ich diese Version (v1.1.0) erneut hochlade.
</pre>
<pre>
That not my resource! this resource is from Xabi https://rage.mp/profile/15952-xabi/
</pre>

# WiredPlayers RolePlay Server
WiredPlayers is a RolePlay project made for RAGE Multiplayer, it uses C# as main server language and JavaScript for client-side scripts. I started with it back in March 2017 and I'm still upgrading its functionality with suggestions received from people using this gamemode.

## Getting Started

### Prerequisites

* [RAGE Multiplayer](https://cdn.gtanet.work/RAGE_Multiplayer.zip) - The client to login into the server
* [Bridge plugin](https://cdn.gtanet.work/bridge-package.zip) - The plugin allowing use to use C# server-side
* [MySQL Server](https://dev.mysql.com/downloads/mysql/) - The database to store the data
* [.NET Core SDK](https://www.microsoft.com/net/download) - The SDK to develop C# resources
* Any client you want to access the MySQL database

**Note:** This project has only been tested under Windows environments

### Installing
1. Install the .msi file that comes into RAGE Multiplayer's .zip file
2. Execute the **updater.exe** located on the root folder where you installed RAGE Multiplayer
3. Unzip the Bridge plugin into the folder called **server-files** replacing the files if needed
4. Get all the files from this GitHub and place them into the same folder as before, replacing the files you're asked for
5. Make sure your router has opened 22005 UDP port and 22006 TCP/IP
6. Open your MySQL client and execute the **wprp.sql** script under **server-files** folder
7. Import to Visual Studio the **WiredPlayers.csproj** file, located on the following path:
**%RAGEMP Installed folder%/server-files/bridge/resources/WiredPlayers/**
8. Change the database connection settings under **meta.xml** located on the following path: 
**%RAGEMP Installed folder%/server-files/bridge/resources/WiredPlayers/**
9. Make sure your solution has linked the **gtanetwork.api** and **MySql.Data** Nugets, if not, add them
10. On Visual Studio, clean and build the solution in order to generate the required **WiredPlayers.dll** library
11. Execute the **server.exe** located under the **server-files** folder
12. Log into your server and enjoy it


If you followed all this steps, you should be able to login with your newly registered account
