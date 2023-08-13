<div align="center">
  <h1>
    - FBLA 2023 Game Simulation :video_game: -
  </h1>
</div>

<br>

<p align="center">
  | &nbsp &nbsp &nbsp Kyle Smith &nbsp &nbsp &nbsp |
</p>

<br>

![Main Menu](./GitAssets/thumbNailGame.png?raw=true "Main Menu")

<br>

This file will show the main parts of the game simulation "Word Sorcerer." Within the file are the current sections:

<ul>
  <li><a href="#game-about">Game About 📜</a></li>
  <li><a href="#game-requirements">Game Requirements 📑</a></li>
  <li><a href="#game-download">Game Download 🔧</a></li>
  <li><a href="#game-inner-workings">Game Inner Workings :gear:</a></li>
  <li><a href="#game-recognition">Game Recognition :sparkles:</a></li>
  <li><a href="#game-future-developments">Game Future Developments :ballot_box_with_check:</a></li>
</ul>

<br>

<a name="game-about"></a>
<h2>Game About 📜</h2>
<br>

This game is based on the popular mobile game <a href="https://www.nytimes.com/games/wordle/index.html">`Wordle`</a> with a little bit of inspiration of Final Fantasy VI's attacking system for the boss battle.
<br>
<br>
|
<br>
<br>
Within the game, you will find that the forest world of the main character is being invaded by an evil wizard and the only way to save your world is to use *Magical Word Magic* to defeat the wizard!

<br>
<br>

<a name="game-requirements"></a>
<h2>Game Requirements 📑</h2>
<br>

This game meets the requirements illustrated in the guidlines here: <a href="https://www.fbla-pbl.org/fbla-topics/">FBLA GUIDELINES</a>

- [x] The game should be an executable game, either through the Internet or through a local installation.
  - Game can be played through an installation.
- [x] The game should contain a scoreboard.
  - Game contains a scoreboard that can be viewed at any time.
- [x] The game should contain a leaderboard and celebratory messages.
  - Game contains a live leaderboard so you can see other player's scores. Player receives a message when entering their name for the leaderboard.
- [x] The game should have a minimum of three levels.
  - Game's difficulty increases slightly after defeating the boss.
- [x] The game should have an instructional display.
  - Players can view how to play at the beginning of the play.

<br>
<br>

<a name="game-download"></a>
<h2>Game Download 🔧</h2>
<br>

Steps for downloading and executing the game:

  - [x] Game is compatible with `Windows`, `Linux`, and `MacOS`
   1. First Method:
      - Go to the branch <a href="https://github.com/CodeAPretzel/FBLA-2023-Game-Simulation/tree/pull-game">`pull-game`</a> and pull the .zip of the entire branch into your local system using the `Code` menu in the top-right corner.
      - `Extract all` from the .zip.
      - Open the folder and go to the executable `Word Sorcerer Setup_x86-x64.exe` and execute it.
      - Then follow the instructions on your screen.
      - Finally, you can play!
  
   2. Second Method:
      - Make a `git clone request` from the folder.
      - Will require <a href="https://git-scm.com/book/en/v2/Getting-Started-Installing-Git">Git</a>.
      - Use the following request to pull the `Word Sorcerer` folder and execute `Word Sorcerer Setup_x86-x64.exe`:
      <br>
      
      ```
      git clone -b pull-game https://github.com/CodeAPretzel/FBLA-2023-Game-Simulation.git
      ```

<br>
<br>

<a name="game-inner-workings"></a>
<h2>Game Inner Workings :gear:</h2>
<br>

One of the core factors in <i>Word Sorcerer</i> is the leaderboard. Using <a href="https://lootlocker.com/?gclid=CjwKCAjwue6hBhBVEiwA9YTx8EQW2c-GlN_UVjrRAPVk0_OmgoPnOADHqUkqmBgvlgIe3FeFSEqXvhoCGpMQAvD_BwE">`Lootlocker's`</a> API system, we are able to manage a live server that will continue to update the leaderboard, giving the most recent results of players.
<br>
<br>

<h3>
  <p align="center">
    - &nbsp Illustration of Response &nbsp -
  </p>
</h3>
<br>

<b>
  <p align="center">
    Lootlocker API &nbsp --> &nbsp Server Requests API Keys &nbsp --> &nbsp Set and Display Metadata Gained to Player
  </p>
</b>

<br>
<br>

<a name="game-recognition"></a>
<h2>Game Recognition :sparkles:</h2>
<br>

Some of the assets used in the game belong to the owner of source and are compatible with <a href="https://creativecommons.org/licenses/by/4.0/">`Attribution 4.0 International (CC BY 4.0)`</a> or <a href="https://creativecommons.org/licenses/by-nc/4.0/">`Attribution-NonCommercial 4.0 International (CC BY-NC 4.0)`</a>. Credit to these authors are provided within the game's credit section.
<!--><li></li><br>

--> Licensing is under the GNU Affero General Public License v3.0.
<a href="https://github.com/CodeAPretzel/FBLA-2023-Game-Simulation/blob/main/LICENSE">`More Info`</a>

<br>
<br>

<a name="game-future-developments"></a>
<h2>Game Future Developments :ballot_box_with_check:</h2>
<br>

- [x] Fixing Errors for the Leaderboard
- [x] Improving User Experience in the Game's Environment
- [x] Improving Composition on Music and SFX
- [ ] Adding Polishing Details for Boss Battle
- [ ] Adding Polishing Details for the Wordle Aspect
- [ ] Generalization of Multiple Levels and Experiences
- [ ] Adding Game Modes and Activites within the Game
