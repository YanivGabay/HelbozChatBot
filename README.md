# HelbozChatBot

First try using c# .net webapi
and twilio api to create a basic whatsapp chatbot.

you need to register a businses phone number linked in
meta dev suite, and registered through twilio, but we can
use:
ngrok and twilio text sandbox to test it.















small note to myself:


had a big problem, with exposing the api key throguh appsettings.json
through multiply commits
the problem is , even if you ignore it after, there is still a git history, which 
the api key is exposes.
the BEST solution, was to use git-filter-branch, in the following way:
git filter-branch --force --index-filter "git rm --cached --ignore-unmatch appsettings.json" --prune-empty --tag-name-filter cat -- --all 
basiclly, it went over all the commits which include appsettings.json, and delete them from there and also rewritten the commit (without the appsettings)
