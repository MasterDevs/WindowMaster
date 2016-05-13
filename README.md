# WindowMaster

The WindowMaster library is a managed wrapper around User32.dll to access information about particular windows on screen as well as creating a global keyboard hook. It also defines a HotKey action which will consists of a set of HotKey Combos (Modifier & Key) and a reference to a delegate that will be fired once that HotKey is pressed.

![ScreenShot.png](http://download-codeplex.sec.s-msft.com/Download?ProjectName=windowmaster&DownloadId=266070)

The WindowMaster Tray application is an implementation of WindowMaster lib. It's a small tray application that lets you define some window actions that will be performed when the HotKey(s) is pressed. Below is a list of currently supported actions. (Any actions not listed here are still in development so please use at your own risk. Newly added actions are marked in bold.)

* Dock Window - Places a window to a part of the current screen and re-sizes the window to a percentage of the working area of the screen.
* Dock & Move Window - Performs the same function as {"WinKey+L / WinKey+R"} on Windows 7
* Maximize Window
* Media Key - Map any HotKey to Play/Pause, Stop, Previous, Next, Volume Up/Down, Mute
* Minimize Window
* Minimize Window to System Tray (Restore window by clicking on Tray Icon)
* Move Window - Moves the foreground window to the next screen.
* Restore Window
* Restore Window Down - If window is maximized, it will be placed in its' normal state. If it's in normal state, it will be minimized.
* Restore Window Up - If window is minimized, it will be placed in its' normal state. If it's in normal state, it will be maximized.
* Show Active Actions
* Start A Process or bring a currently running process to the foreground 
* Stretch Window - Stretches a window horizontally or vertically (depending on setting). 
