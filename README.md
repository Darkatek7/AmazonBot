# PS5_Bot_AmazonDE
This is a Bot for checking and buying a PS5 Digital Edition on Amazon.de

(You can also get this to work for PS5 - Disc Edition. Just follow the steps at the bottom of the page)

### Windows only! 
### Be careful, this buys everything in your Cart!

## Install: (All Installers are in the Git Repository)
* Copy **_chromedriver.exe_ to _"C:/WebDriver/"_** or it won't work!
* Now Install **_Google Chrome_**.
* Now install **_Visual Studio Community 2019_**: https://visualstudio.microsoft.com/de/downloads/ and
* install **.Net Framework 4.7.2** from: https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-web-installer 

## After installing Chrome Driver, Google Chrome and .Net Framework please open your Google Chrome Webbrowser and open https://www.amazon.de. 
* Now **_log into the website_** and make sure you click the stay signed in options.

## Before you run the Bot, make sure you have all Chrome Drivers and Chrome Browser-Windows closed!

## Run Bot:
Method 1: 
* Run the "setup.exe" file from the installer folder. (Your Anti-Virus might give you a warning as the installer isn't signed)
* Then run the "PS5_Bot.exe" file from the path you specified.

Method 2: (offers customization, Disc Version, etc) 
* Now run the Visual Studio Solution (**_PS5_Bot.sln_**) and click on **Start** button in top Center Screen.(Or customize if you wish to)

## Now the Bot should be running! Click on Check and Buy PS5. (Make sure you only click the Button once!)
* The bot should open your Chrome Browser. After Chrome was started, make sure it says you are logged in on Amazon! (If not, Login)
* The Bot will now continuously refresh the page and check if the PS5 is Available.

## When it is available, it will automaticly add it to your cart and go to the checkout. 

If you have never bought anything using chrome:
* All you have to do now is close the Bot enter your Amazon password on the Chrome/Amazon.de website and click and buy Product!

Else:
* The Bot should have bought the product for you now.

### If you encounter any problem create an Issue on Github.

### I recommend not letting the bot run on standalone and keeping on eye on it.


## PS5 - Disc Version:
To get it to work with the Disc Version you need to change the following code in BrowserLogic/CheckIfProductIsAvailable:
```
IWebElement productTab = GetElementUsingXPath(
                "/html/body/div[2]/div[2]/div[5]/div[5]/div[4]/div[30]/div[1]/div/form/div/ul/li[7]");
```
To this Code:
```
IWebElement productTab = GetElementUsingXPath(
                "/html/body/div[2]/div[2]/div[5]/div[5]/div[4]/div[30]/div[1]/div/form/div/ul/li[6]");
```
