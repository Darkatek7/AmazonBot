# Amazon_Bot
This is a Bot for checking and buying a Product on Amazon

### Windows only! 
### Be careful, this buys everything in your Cart!

## Install: (All Installers are in the Git Repository)
* Run "requirements.ps1" from the Installer/Requirements folder in Powershell (or have both .Net 5 and Google Chrome V89.0.4389.114 installed)
* Run the installer (setup.exe) from Installers/Setup Files folder. (Your Anti-Virus might give you a warning as the installer isn't signed)

## Please open your Google Chrome Webbrowser and open the amazon website
* Open your chrome browser and make sure in the to right corner (under profiles) it says guest or not signed in.
* Now **_log into the Amazon website_** and make sure you click the stay signed in options.
* Change your Amazon language to german or english or complex products will not work!

## Run Bot:
* Run "Amazon_Bot" Program from the path you specified.(or Desktop / Start Menu)

## Now the Bot should be running!
* Click on Run checking and buying.
* The Bot should open your Chrome Browser (if you did not enable headless mode). After Chrome was started, make sure it says you are logged in on Amazon! (If not, Login)
* The Bot will now continuously refresh the page and check if the Product is Available and then buy it if it meets you price requirements.

## Checking a Product:
* When checking and buying a product for the first time, it takes about one minute to create a chromeprofile for that item. Don't close the program while creating it and wait until the chrome windows open! Otherwise you have delete the product and add it again in your product settings window.

## When it is available, it will automaticly add the Product to your cart and go to the checkout. 
* Sometimes Amazon asks for your password again! Just open your chrome website and enter your Amazon password manually. (If you are on headless mode it won't buy your product!)

## Settings:
* Settings can be changed on the fly.
* Enter your preferred refresh delay in the textbox at the bottom left.
* Enter maximum waiting delay for a button to appear on the website in the bottom right corner.
* Click Edit Products to add and remove products.
* Enable headless mode if you wish to hide the chromewindow.

## Edit Products
* When adding new Products, make sure the Product name is exactly what it says on Amazon (eg: PS5, PS5 - Digital Edition) or complex products won't work.
* If you want to check for a product make sure you enabled the checkbox!
* You can also enter a wishlist url to buy everything that is in that wishlist or just one product of it if you set the quantity to 1. (wishlist has to be of your own Account).
* Leave max price blank or set it to 0 to ignore max price. Note: max price works for the most products, not all of them. Try it out first!

## Notes:
* When starting the Bot all Chrome Drivers and Chrome Browser Windows will be closed!
* When closing the Bot all Chrome Drivers and Chrome Browser Windows will be closed!

### If you encounter any problems feel free to ask for help in our Discord.
* https://discord.gg/Spak78KaEx

### I recommend not letting the bot run on standalone and keeping on eye on it.
