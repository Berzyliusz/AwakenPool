How to add new ball types:
1. Make a prefab variant out of ball base.
2. Assign material or mesh you need.
3. Assign value of Point For Hit
4. Place and use in the scene.

How to add new levels:
1. Duplicate "Level 1 Setup" prefab. ( Check out "Level 2 Setup")
2. Place balls the way you want to.
3. Place obstacles if needed.
4. Place hitable posts where needed. Set "Hitable" tag on the hitable elements.
5. Fill in the data on GameSetup component - plug playable ball, cue, balls, and fiddle with the params

Notes:
1. As for this excersise there is no "level managment" so to play new level, place it in the scene,
and plug into GameStarter script as the current level.
2. Camera controlls are skipped entirely due to lack of design - 
in the future player may control the camera, or we can add "camera position" to each level,
so we can see larger levels etc.