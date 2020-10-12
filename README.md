# Memory Game W\AI And UI Invluded (Windows Form)

# The Memory Game w\AI
# Goals:
• Implementation of work with object-oriented departments and programming
• Work with arrays / collections departments
• Using the string class
• Use of external Dll (assembly)
# Knowledge required:
• Work with arrays / collections departments
Access Modifiers, Constructors, Properties, Indexers
• Using the string class
• Use of external Dll (assembly)

# The Exercise:
You need to implement the memory game (relatively primitively) when the console is the user interface.
The program will first ask the user for the desired board size (number of columns and number of rows).
The initial state will be a symmetrically sized panel of X rows of columns at the request of the user.
The program will not allow the selection of an odd number of squares (e.g. 5X5).
At each stage the user will be asked to select a slot in the board to reveal the signal that is in the above box.
At this point the user will be asked to select another box in which he thinks the additional letter T is located. The slot
That you selected will be exposed in a similar way to where the first slot was exposed (the board will be drawn again)

# the progress of the game
1. The user is asked to enter his name (he is the first player).
2. The user is asked to decide whether the game is for two players or against the computer.
3. If he chooses the option of two players, he is asked to enter the name of the player
The second.
4. The user is asked to determine the height and width of the board (minimum 4x4 maximum 6x6 and mandatory quantity
Double squares).
5. At the beginning of the rotation the matrix is empty and will be drawn in this way (in the case of 6x4).
6 Each player in turn will be asked to choose the first slot he wants to reveal. If the selection is invalid a suitable message will be given and so on until it is selected a slot
Valid (valid slot is a slot hidden within the board boundaries. Any other input is considered invalid
And a notice must be issued explaining the reason for its illegality).
7. After the player selects a square, clear the screen, draw the board as it is after
That we have exposed the signal in the relevant box.
8. At this point the player must select the slot that he considers to be a match, according to the instructions in sections 6 and 7-9. If the user has managed to reveal a matching pair, the pair will remain exposed until the end of the game, the player will receive
Point, and the queue will remain with him for new input under the previous section (i.e. returns to section 6).
10. If the user reveals an mismatched pair, the mismatched pair will remain exposed for 2 seconds and after 2 seconds the two slots selected will be empty again and the turn goes to the other player for input.
(I.e. returns to section 6).
11. Board Mode After the first player selects squares E3 and: B4
12 When the last pair is revealed, the game is over and a victory is determined for the player with the greater number of exposures (an appropriate message will be issued that includes the status of the points between the players. Each player receives points according to
The number of his exposures).
13. After the end of a game, the user will be asked if he wants to start another round. If so, return to section 4.
If not, a message will be displayed and the program will end.
14. Starting from section 6, at each stage the game can be retired by selecting "Q" instead of a square
(General retirement from the software).

# Windows, controls, events and memory

# Goals
• Assimilation of work with events and delegations
• First window application
# Knowledge required
• Event and delegation technology
• Create a basic window application and add controls
# The exercise
Realize the memory game only this time to the operating system "Windows."
The main goal is to experiment with writing a first window app.
The software will display a first window that will allow the user to determine the size of the board in the game.

Clicking the Friend a Against button will activate the text box of the other player's name
m_TextBoxFriend.Enabled = true;
And change the text in the button to Computer Against and then with the next click the situation will return to normal
As in the picture above.
• Pressing the purple button will change the text in the button to the next possible size for the clipboard, scrolling
That is, clicking on the button will show the user the sizes:
4x4, 4x5, 4x6, 5x4, 5x6, 6x4, 6x5, 6x6, 4x4, 4x5, ..
• Pressing the Start button or the X button will close the window and cause the main window to appear.
After closing the settings window, the game window will appear, with buttons arranged according to the size you selected.
The user, and three Labels with different background colors (different background color for each player) as shown in the image
(This is the board mode after the user selects a board of, 6x4 Dana found 2 pairs, Amir found 1 and now Dana's turn)
• The size of the window will be adjusted to the size of the panel and the additional controls in the window.
Pressing a gray button will display the letter "hidden behind" and the background of the button will be
Depending on the background set for the player currently in turn. If the click is the second click and a pair is discovered, the buttons will remain visible and the turn will remain of the player who discovered it. Otherwise the buttons will be hidden back
And the turn will pass to the other player.
• Each time the scoring pair is discovered it will be updated accordingly as shown in the picture.
• Pressing a visible button does nothing.
• At the end of the game (draw / win) a message will be displayed using MessageBox.ShowDialog showing the
The result and asks if you want another round. If you do not want another round, the window will close and the app
Ends. If so, the board resets and begins another round with the same settings.
