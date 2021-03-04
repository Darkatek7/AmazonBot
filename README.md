# AmazonDE_Bot
This is a Bot for checking and buying a Product on Amazon.de

### Windows only! 
### Be careful, this buys everything in your Cart!

## Install: (All Installers are in the Git Repository)
* Run "requirements.ps1" from the Installer folder in Powershell

## After installing Chrome Driver, Google Chrome and .Net Framework please open your Google Chrome Webbrowser and open https://www.amazon.de. 
* Now **_log into the website_** and make sure you click the stay signed in options.
* Change your Amazon language to german or english or else it won't work!

## Run Bot:
* Run the one of the installers from install folder. (Your Anti-Virus might give you a warning as the installer isn't signed)
* Then run the "AmazonDE_Bot" Program from the path you specified.

## Now the Bot should be running! Click on Check and Buy Product.
* The Bot should open your Chrome Browser. After Chrome was started, make sure it says you are logged in on Amazon! (If not, Login)
* The Bot will now continuously refresh the page and check if the Product is Available.

## When it is available, it will automaticly add the Product to your cart and go to the checkout. 

If you have never bought anything using chrome:
* All you have to do now is pause the Bot enter your Amazon password on the Chrome/Amazon.de website and click and buy Product!

Else:
* The Bot should have bought the product for you now.

## Settings:
* Settings can be changed on the fly. (except config.json)
* Enter your preferred delay in the textbox at the bottom.
* Select the checkboxes on the products you want to check for.

### Custom Product Settings:
* "custom_product_url": Amazon url of the product you wish to buy.
* "custom_product_simplicity": set this value to "complex" if your product doesn't have a hardcoded Amazon url (like the ps5), else leave it as "simple".
* "custom_product_title_": Replace PRODUCTNAMEOFPRODUCTTAB with the actual product names Tab. (this is only for complex products)

## Notes:
* When starting the Bot all Chrome Drivers and Chrome Browser Windows will be closed!
* When closing the Bot all Chrome Drivers and Chrome Browser Windows will be closed!

### If you encounter any problems feel free to ask for help in our Discord.
* https://discord.gg/Spak78KaEx

### I recommend not letting the bot run on standalone and keeping on eye on it.
