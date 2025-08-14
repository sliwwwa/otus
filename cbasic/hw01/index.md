I. Project's name

Dad's childhood



II. Roles

 - player
 - gamemaker
 - moderator
 - administrator



III. Roles' functionality

player:

-opens the API
-enters his name if he does so for the first time
  -loads his last save if he did that before
  -saves his progress whenever he wants
-progresses through the story
  -creates his character of different classes, abilities and perks
  -enters the canon or made by gamemakers levels
  -gets answered questions, answers them
  -goes different ways and gets different endings according to his moves
  -finds upgrades for his character by exploring and fighting more
  -tracks his character's health, resistance and other stats
  -gears his character up to withstand the enemies' attacks and be stronger comparing to them
-contacts support in a special chat when in these circumstances
  -finds a bug or a defect
  -needs basic guidance to the game from reliable people
  -wants to make a suggestion
-can also help other people get knowledge about the game

gamemaker:

-codes, creates levels and other content
  -tests it for bugs and defects
  -sends the files of code to moderators for consideration
  -the process of filtering by moderators goes
  -the content is approved into the game
    -the gamemaker is happy
  -the content is disapproved
    -gets sent the files back to remove the glitches found

moderator:

-receives content from gamemakers for consideration
  -tests it to be glitchless and safe to play
  -approves it
    -sends it to the programmers to code the content into the game
  -disapproves it
    -sends it back to a gamemaker to remake it
    -if violates the rules of content making, restricts to do so for a period of time or forever
-monitors the behaviour of the community
  -kicks or bans players if the rules of behaviour are violated
  -can see each players progress in the game
-counts as support which players contact in certain situations mentioned above


administrator:

-monitors the behaviour of the community, moderators and other admins
  -kicks or bans mentioned people if needed
  -promotes or demotes moderators and other admins if deserved so
-monitors the state of the game and reports it to the programmer



IV. Main functionality

player:

-creates a customizable character and progresses through the story
-saves and loads his progress
-can leave feedback and contact support

gamemaker:

-submits his creations to moderators
-a minor change, or a huge reimagining

moderator:

-moderates the incoming and existing game content, same with players
-works as support

administrator

-manages roles and other admins' position
-monitors players and the state of the game



V.UseCases

----------UseCase001----------

Unauthenticated user (role not defined - logged in to the bot for the first time)

------Хепиpath------

1.
Bot:
"Greetings, stranger! To start the game, enter "/start". See the menu for complete instructions. After you enter this command, we will remember you. "

2.
User:
"/start"

3.
Bot:
"What is your name?"

4.
User authenticated by TelegramID:
"sliwwwa"

5.
Bot:
"Hi, sliwwwa! Welcome to the game!
To secure your account in the game, create a password."

6.
Bot:
"The password must contain at least 8 characters and include at least one special character (~!@#$%^&*), one uppercase letter, one lowercase letter and one number
Enter your password:"

7.
User authenticated by TelegramID:
"*****************"

8.1. Password validation was successful
Bot:
"Repeat password:"

9.
User authenticated by TelegramID:
"*****************"

10.1. Passwords matched
Bot:
"Your password has been accepted. It will be requested from you each time you enter the game in order to secure your account.
You have been assigned the role of [Player]. You can start the game with the command "/startgame". If you want to offer a new level for the game or become a moderator, write the command "/knockknockneo"."

11.
System:
The user + role + password bundle is saved in the application.



------Alternative scenarios------

8.2. Password validation failed (password does not meet the requirements)
Bot:
"Password does not meet the requirements, enter a new password."

Go to step 6.



10.2.1. Passwords do not match (alternative scenario 10.2 works 3 times, in case of three-time password mismatch, go to step 5)
Bot:
"Passwords do not match, re-enter:"

10.2.2.
User authenticated by TelegramID:
"*****************"

10.2.3. Passwords matched

Go to step 10.1





----------UseCase002----------

Authenticated and authorized user (role defined - [Player])

------ Happy path ------

1.
Bot:
"Hi, sliwwwa! For better experience, enter your password.
If you forgot your password, write to the administrator. "

2.
User authenticated by TelegramID:
"*****************"

3.1. Password accepted.
Bot:
"Password accepted. Enjoy the game!
If you want to continue a saved game, select the "/continuegame" command.
If you want to start a new game, select the "/startgame" command.
Remember that if you choose a new game, the saved progress will be lost. "

4.
User authenticated by TelegramID, validated by password, authorized as [Player]:
"/startgame"

5.
Bot:
"Let's give your character a name:"

6.
User:
"Slava"

7.
Bot:
"Character class:

 - Melee;
 - Ranger;
 - Mage;
 - Summoner.

Choose one."



------Alternative scenarios------


3.2.1. Password not accepted the first and second time.
Bot:
"You didn't guess the password, try again:"



3.2.2. Password not accepted for the third time.
Bot:
"You did not guess the password for the third time. Take a 5-minute break and try again."
