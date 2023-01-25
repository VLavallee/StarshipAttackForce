using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ideas : MonoBehaviour
{
    /* CURRENT PROGRESS / WHAT IS LEFT TO DO
     * 5 BOSSES ARE IN GAME AND WORKING. 2 BOSSES NEED NEW WEAPONS THAT WILL ALSO BE USED FOR WEAPON UPGRADES FOR THE PLAYER. 1 BOSS NEEDS A WEAPON MAKEOVER WITH PLAYER UPGRADE AS WELL. 
     * 5 SHIPS ARE MOSTLY WORKING. ALL HAVE THEIR OWN DEFAULT WEAPONS THAT WORK. UPGRADES WORK. MISSILES AND THE BEAM WEAPON NEED TO BE CHECKED FOR ANY THAT MAY BE MISSING SOME IMPLEMENTATION.
     * SHIELD SYSTEM WORKS AS INTENDED ON ONE SHIP, NEED TO COPY OVER TO ALL OTHER SHIPS.
     * 1 ENEMY SHIP WORKS FINE. NEED TO UPDATE THE 3 REMAINING ENEMY SHIPS WITH WEAPONS AND PUT INTO GAME.
     * MORE WAYPOINTS ARE NEEDED FOR THE ENEMIES TO TRAVEL ON. 
     * MUSIC NEEDS TO BE MADE FOR MENU AND MAIN GAME, SFX NEED TO BE MADE FOR SHOOTING, POWERUPS ETC.
     * 
     * TIME ESTIMATE: 
     * 
     * 1.5 DAYS - UPGRADE BOSS WEAPONS TO COMPLETE
     * 1 DAY - FINISH PLAYER SHIP WEAPON SETUPS FOR COMPLETED WEAPONS                
     * 1 DAY - FINISH IMPLEMENTING SHIELD SYSTEM AND WAYPOINTS
     * 2 DAYS - UPDATE 3 ENEMY SHIPS WITH NEW SHOOTING TYPES
     * 1 WEEK - MUSIC FOR MENU AND MAIN GAME
     * 3 DAYS - SFX FOR SHOOTING, POWERUPS ETC.
     * 
     * DC ship, ult DC ship - photon
     * Ext , ult Ext ship - blade
     * saucer, ult Saucer - saucer proj
     * 
     * Main weapons: photon, prism, saucer proj, blade
     * photon - no special effect
     * prism - no special effect
     * saucer - no special effect
     * blade - no special effect
     * 
     * Alt weapons: fireball, ice bullet, shock sphere
     * fireball - small explosions that do extra damage
     * ice bullet - kills create ice explosions
     * shock sphere - projectiles fork into two projectiles
     * 
     * special weapons: homing missile, drone, beam, shield projectile, proximity mine
     * homing missile - auto tracks enemies
     * drone - additional shooting units that follow the player
     * beam - high damage laser that shoots through all targets
     * shield projectile - spinning projectiles that cannot be destroyed
     * proximity mine - launched mines that detonate after time or collision
     * 
     * 
     * 
     * 
     * CANCELLED \ DELAYED - Have a combo system in place for destroying enemies. Set it up with a float that counts each time an ememy is destroyed. If the float is low enough (say within 1 second)
     * it can add a multiplier to the score system that will stay until the threshold is reached again. For example destroy 2 enemies within 1 second and achieve combo 2x multiplier
     * for those two enemies, then destroy another enemy within 1 second and that combo is increased to 3x, and so on. 
     * 
     * Allow ships to be unlocked with two different methods. Playing the game and earning currency will allow you to purchase some ships, with others unlocked with achievements.
     * If in game currency was available in the shop for real money, this could be a way to allow the player to spend on the game without cheapening the experience. As an example, 
     * you could spend 25,000 credits to buy a fancy new ship. This money could be earned by playing the game for a while or by buying in game currency with real money. 
     * There could be another ship that cannot be purchased with any currency, and is only unlocked after beating the game on insane difficulty using the starter ship. 
     * Skins or color changes could also be purchased with in game currency, giving the player a chance to customize their favourites. 
     * 
     * CANCELLED \ DELAYED - Level system 
     * Each level is comprised of 10 waves of enemies, with some also incorporating space debris into the design. 
     * Some levels could have bosses, have different backgrounds and enemies etc.
     * 
     * Infinite system
     * One continuous level that goes on until the player dies. It will slowly increase in diffuculty until it becomes impossible to go further. In game currency would be
     * earned at a different rate than the normal level system to even out the economy of the game. The infinite system would use randomization to keep the game interesting. 
     * 
     * 
     * Game Architecture Plan
     * Have a large number of paths and waves and create a script that will randomize the enemies, paths, and waves, including bosses. Boss 1 will start with 1000 hp for example, 
     * the next boss that spawns will have increased hp, same with the enemies. This will keep the game interesting on playthroughs and will continue to increase in difficulty until
     * it becomes impossible to progress due to the high damage/ life of enemies. This will help to incentivize the player to upgrade their ships and/ or purchase new ships / upgrades.
     * 
     * To keep the paths from repeating, the script should create a new list of all the available paths at the start of the game, and each time a path is used it will be removed from
     * the list of available paths. When the path list reaches zero it will start over and the process will begin again.
     * 
     * To keep two different paths from going on top of each other more than necessary, there should be 2 to 3 seperate path lists, with each list containing paths that are contained to one
     * area or less likely to interfere with paths from the other list. This will be tedious work but may be necessary, depending how the playthroughs go. 
     * 
     * To keep the player changing and upgrading their weapon throughout the game certain enemies should be resistant to certain damages.
     * 
     * 
     * 
     */
}
