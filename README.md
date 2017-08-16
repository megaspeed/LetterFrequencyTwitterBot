# LetterFrequencyTwitterBot
LetterFrequencyTwitterBot is a console .Net application. 
It computes letter's frequency statistic for last 5 messages from specified account 
and prints statistic on console and tweets it on behalf of application twitter account.

# Getting Started
Before start app fill twitter account credentials in TwitterBot\BotConsole\App.config
```
  <appSettings>
    <!--Fill twitter account credentials-->
    <add key="consumerKey" value="" />
    <add key="consumerSecret" value="" />
    <add key="accessToken" value="" />
    <add key="accessTokenSecret" value="" />
  </appSettings>
```
# Built With
- TweetMoaSharp
- Ninject
- Newtonsoft.Json
