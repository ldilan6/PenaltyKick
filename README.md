# PenaltyKick

Android APK found at this google drive link. 
---------

https://drive.google.com/open?id=1nHAnHZvDXfyYZYhlHRFmTsYMCMjg4PCA

Download file to your computer and then add it to your phone (folder) via a usb cable.

Go to Phone settings, Security, and have the unknown sources button checked. 

Should be able to open the apk and play the game now. The game is played in landscape view. 

iOS users follow these steps to download the game.
--------------------
Must have a Mac computer to do so. Connect your device via a usb cable.

Start off by downloading Unity. 

https://store.unity.com/#plans-individual

The free version is fine. Download with the Unity Hub included. 

Once you have Unity downloaded, you must download the game engine. The specific version you will need is found here, https://unity3d.com/unity/alpha/2019.3.0a2

Click on the install with Hub option. 

Now you must add the iOS module. Do this by opening the Unity Hub then click on the Installs tab, there click on your games engines three dots. Click on add modules, then check iOS build support, click done and install.

Next step is to download the latest version of Xcode. https://apps.apple.com/us/app/xcode/id497799835?mt=12

Once those two software packages are downloaded, you will then download this project ZIP to your computer and save it to whatever folder you wish. If the joystick happens to be out of position for you, please download this zip from my google drive. Use this zip instead of downloading the zip from Github. Continue the instructions using this folder as the "zip" folder. https://drive.google.com/open?id=1cboj_cZJv5__F6zPL4u-rOCIsvWtsVMr

Now to add an Apple ID to Xcode. Open Xcode, from the menu bar at the top of the screen choose Xcode>Preferences. From the preferences window. 

Choose Accounts at the top of the window. To add your Apple ID, click the plus sign at the bottom left corner and choose Add Apple ID. 

Enter you Apple ID and password.

Select your Apple ID from the list.

Find the zip folder you downloaded from Github in your downloads folder and extract it. Once it is extracted you can add it to Unity. Add it by clicking on the Add button in the projects tab of Unity Hub. 

Now to open the project in Unity, you must open Unity Hub. And in the projects tab click on the add button.

Once the project is open, then open the build settings from the top menu (file> Build Settings).

Highlight iOS from the list of platforms on the left and choose Switch Platform at the botton of the window. 

Next open the Player settings in the inspector panel, (Edit>Project settings> Player)

Expand the section at the bottom called Other Settings, and enter your chosen bundle identifier where it says Bundle Identifier.

For the bundle identifier use the format "com.companyname.gametitlexxxxxx" where xxxxxxx is just a combination of numbers. 

Next open build settings from the top menu (File> Build settings). Click Build to build. 

Click the down arrow in the top right of the prompt to expand it, then click New Folder. 

When prompted to choose a name, enter "Builds" and click Create. This will create a new folder called Builds in the root directory for your project. 

In the text input field marked Save As, enter "iOS" and click Save.

Once Unity buils the Xcode project, a finder window will open at the project's location. 

Double click the .xcodeproj file to open the project with Xcode.

In the top left, select Unity-iPhone to view the project settings. It will open with the General tab selected.

Under the topmost section called Identity, you may see a warning and a button that says Fix Issue. Click the Fix Issue button if needed. Otherwise continue. 

A popup will appear, showing details of any teams that have been added to Xcode. Make sure that the correct team is shown in the dropdown.

Click choose to instruct Xcode to download any required certificates and generate a provisioning profile. The warning will then disappear. 

Click on Unity-iphone in the left corner and under signings and capabilities enable automatic signings. Then select your Apple ID team from the dropdown for teams.

Next select build settings under the same menu of Unity-iphone and under project document select your current version of xcode under project format. 

On your device, go to Settings> Display and Brightness> Auto-lock. Disable locking by selecting never.

In the top left of the Xcode interface, click Run (the "play" button). You should see the app when Xcode finishes building. The game is played in landscape view. 

If needed do the following. 

Enable Developer mode by choosing Enable, and enter your password when prompted. 

On your device, go to settings > General > Device Management > Developer App> app name

Choose your Apple ID, then choose Trust. 







