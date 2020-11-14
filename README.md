# RP-EchoVR
RP-EchoVR is a simple program which updates your Discord rich-presence for the VR-game "Echo Arena", it aims to be efficient and reliable. 
* Can be used as a standalone app
* Comes with a Windows Service to start the app automatically when Echo is launched
* Includes a PowerShell script to install the service 
* No need to change any paths, fast & easy setup

## How to install
At first download and extract the .zip folder.
<details><summary><b>standalone version</b></summary>

1. place the folder in your desired directory (e.g. "Program Files").
2. whenever you want to play echo, start "RP_EchoVR.exe" (the file with the little disk icon)
3. a console window should appear, if there's **no** message everything is fine
4. have fun in echo and don't forget to close the program when you stop playing

### tips
here are two tips which may help you if you don't want to use the service-version

* If you want to have a link on your desktop, go to the executable, right-click > send to > Desktop
* You can also place a link in the start menu, so it will launch the app directly after you logged in <br>
    press "win + R" and enter the following ```shell:startup```, now you can paste the link in here, right-click > properties > Run "Minimized"

</details>
<details><summary><b>service version</b></summary>

1. place the folder in your desired directory (e.g. "Program Files").
2. open PowerShell **with administrator privileges** and make sure the right execution policy is set<br>
    (this policy prompts you to confirm every step of the script)
    ```
    Set-ExecutionPolicy Unrestricted
    ```
    (this policy allows the script to run without any restrictions)
    ```
    Set-ExecutionPolicy Bypass 
    ```
    you need **one** of these to run the script
3. enter the path to the folder using cd 
    (example)
    ```
    cd "C:\Program Files\RP-EchoVR\"
    ```
    you can use the tab-key to autocomplete
4. enter the following command
    ```
    .\service-install.ps1
    ```
5. follow the instructions in PowerShell
6. if there's a green message saying "finished" you installed the service successfully 
7. before you close PowerShell type in
    ```
    Set-ExecutionPolicy Default
    ```
    to reset the policy to default values
8. now the service will run in the background and open/ close the app automatically as soon as you start/ stop playing echo

</details>
  
## How it works

### standalone version
If you start the program it will check for data from the Echo API (which is available on ```127.0.0.1:6721/session```), the received data is formatted and presented in your status via Discords API. 
This process will loop as long as you don't close the program with a refresh-rate of 20 checks per second.

| pros                 | cons                 |
|:--------------------:|:--------------------:|
| runs when needed     | start/stop manually  |
| reliable             | console window       |

### service version
Since the executable is the same it works exactly the same as the standalone version, the difference is the service. It will run until you either shut the pc down or stop the service manually and check every 5 seconds if echo has been started. If yes, it will start the main application in a background service (no window, less memory usage). 

| pros                 | cons                 |
|:--------------------:|:--------------------:|
| fully automatic      | service stays active |
| background process   |                      |

## Need help?
You can always [join my discord server](https://discord.gg/7cxBkBr/ "invite link") if you have any questions, concerns or requests.
If you don't want to have another server in your list, you can also send me a direct message, my discord-username is: x_gaming#4786
If you like the application or have an idea on how to improve it - let me know :D

## External ressources
* [ExecutionPolicy](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.core/about/about_execution_policies?view=powershell-7.1/ "ExecutionPolicy")
* [Discords rich presence](https://discord.com/developers/docs/game-sdk/ "Discords rich presence")
* [Documentation of the Echo API](https://github.com/Ajedi32/echovr_api_docs/ "(unofficial) documentation of the Echo API")
