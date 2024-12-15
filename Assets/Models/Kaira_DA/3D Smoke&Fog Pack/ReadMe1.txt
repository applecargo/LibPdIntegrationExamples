How to Use
1. Attaching to Player Character
By default, the Ambiance Fog prefab can automatically follow the player. If you forget to assign the player character, it will automatically find the object tagged as Player in the scene and follow it. However, to ensure proper functionality, manually link your player object if possible.

Select the Ambiance Fog prefab in your scene.

Drag and drop your Player object into the Player field of the prefab inspector.

If this step is skipped, the fog will automatically search for an object tagged "Player" and follow that object.

2. Wind Settings
You can easily control the fog’s movement by adjusting the wind values in the inspector. These parameters allow you to define wind velocity along the X, Y, and Z axes.

X Axis Wind: Controls horizontal movement.
Y Axis Wind: Controls vertical movement (height changes).
Z Axis Wind: Controls depth movement (towards or away from the camera).
3. Customizing Fog Color
Each fog effect comes with customizable color options. Simply adjust the fogColor field in the inspector to match your scene’s atmosphere.

4. FogSettingsLocals Script
All prefabs come with an attached FogSettingsLocals script, which provides additional customization options. You can tweak various settings to adjust:

Density: Control the thickness of the fog.
Color: Adjust the color to fit the mood of your scene.
Wind Dynamics: Define how the fog reacts to wind.
To use this:

Select the prefab in the scene or hierarchy.
In the inspector, find the FogSettingsLocals component.
Customize the available parameters (density, color, wind direction, etc.).
5. Prefabs Overview
Ambiance Fog: Creates atmospheric fog that follows the player and adds immersion.
Local Small Fog: For smaller, confined fog zones, useful in localized areas.
Large Fog: For expansive areas or entire levels.
Fog Bomb: Creates an explosion of fog that disperses dynamically, great for action scenes.
Special Fog: For specialized effects that combine different fog behaviors.
Installation Instructions
Import the 3D Smoke & Fog Pack into your Unity project.
Drag the desired fog prefab from the Assets folder into your scene.
Customize the fog’s appearance and behavior using the FogSettingsLocals script.
Troubleshooting
Fog Not Following the Player: Ensure that the player object is either manually assigned or tagged as "Player".
Fog Not Visible: Check the color and opacity settings to ensure the fog is not too transparent or blending into the environment.
Unexpected Fog Movement: Make sure the wind settings (X, Y, Z) are not set too high, causing unrealistic behavior.
License
This package is free to use in both personal and commercial projects. Please credit Kaira Digital Arts when using this asset.