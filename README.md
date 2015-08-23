#CONCEPT + DESIGN DOCUMENT




*I Live, You Die!*<br>
Fast-Paced Monster Stealth/Survival





<b>GAME OVERVIEW:</b>

In the game you play as a monster trying to reach a nuclear reactor. The monster can violently take over human bodies and live within them to disguise itself, be safe around other humans and get closer to the reactor. However, if the monster is seen it is pretty much defenseless without the element of surprise and will surely die at the hands of the humans. 
At its core the gameplay is a fast-paced type of stealth. The player should feel very tense while playing due to the monster’s fragility. Moments of unexpected stress should be happening constantly and the player will need to scramble and think fast to survive.
The core mechanics and gameplay revolve around the monster’s ability to possess or discard humans while managing the awareness of other humans and the fact that the body they control is constantly decaying.

<b>GAME FLOW SUMMARY:</b>

The game begins with a title screen. After the player confirms they are ready to begin they are taken to a gameplay level.
When a level begins the player is given control of the monster at a predetermined start area. The objective in every level is simply to reach a different predetermined area that ends the level. The player will move around the level encountering enemies that will kill on sight if the player is detected. In order to avoid being found out the player must silently approach these same enemies, and without being detected take over their bodies using the take-over ability. By taking over humans at different times they will be able to overcome environmental obstacles and get by other humans.

<b>PROJECT SCOPE:</b>

Levels: 	1 tutorial level, 1 open ended action level 
Player Controlled Characters:	1
NPCs:			2 types
Abilities:			2 – take over and burst out

<b>GENERAL GAMEPLAY AND GAME MECHANICS:</b>

*Player Movement:*
Player movement needs to be tight and pretty quick. Not so fast that the player feels out of control but fast enough that when chaos ensues players need to rely on reflexes and quick thinking to survive. There shouldn’t be a lot of momentum in the movement, rather it should feel responsive and very tightly dependent on player input.
The movement is derived from the W, A, S, D keys.<br>
*Abilities:*<br>
The take-over ability is available to the player in both monster and human forms. When activated an animation plays out in the player character and if there are any humans in the direction the player is facing the monster character enters the human. Now the player controls that human character.<br>
Using the same basic rules, the player can also use the take-over ability while in control of a human. At this point the monster leaves the previous human and enters the newly acquired one.<br>
The burst-out ability can only be used while in control of a human body. This ability is not available while playing as the monster.<br>
When this ability is used the monster basically explodes out of the human body, reverting back to the original monster form.<br> When this ability is performed the explosion can kill other humans in a predetermined area around the player character.<br>
*Body Decay:*<br>
Players can’t control humans indefinitely. When a player inhabits a human body, the body begins decaying until it eventually disintegrates entirely, leaving the player exposed as the monster once again. This needs to be communicated to the player so they are aware of the urgency. However, the exact moment of decay should remain hidden to the player (no visible timers or bars) in order to enhance tension. Maybe sound cues…?<br>
<b>Enemy Behavior, Actions and Awareness:</b><br>
The enemy NPCs should have very simple behavior. Some might have scripted paths in which they move, others can simply be stationary in the level (asleep?).<br>
Enemies can kill the monster very rapidly once they become aware of it. The player should want to avoid any direct confrontation considering they don’t have many tools to defend themselves.<br>
Enemies should have a vision-cone in front of them (not visible to player) and depending on player actions and positions in relation to the vision cone, the enemy’s state of awareness changes.<br>
Enemies have three awareness levels: Unaware, Alarmed and Aggressive<br>
*Unaware:* In this state enemies are entirely unaware of the player’s presence. This is their default state. <br>
*Alarmed:* This state in reality is a transitional state. Alarmed state begins when a player is within an enemy’s vision cone. Behavior remains the same as in the ‘unaware’ state but a progress bar is displayed above the enemy and slowly begins to fill. Once the bar is filled the enemy goes into Aggressive state.<br>
If a player enters an enemy’s vision cone as the monster, the alarmed state last very little…  a second? And then transitions to aggressive.<br>
If a player enters an enemy’s vision cone while inside a human, the alarmed state lasts longer…    3 seconds?, before changing to aggressive.<br>
*Aggressive:* This state alters enemy behavior. Once an NPC enters this state it can’t go back to alarmed or unaware. The enemy will blindly rush towards the player character and will kill the player if it comes in contact with them.
