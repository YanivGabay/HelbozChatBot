# HelbozChatBot Development Note

### Overview
This project involved creating a basic WhatsApp chatbot using C# .NET WebAPI and the Twilio API. To facilitate testing before deploying with an actual business phone number linked through Meta Dev Suite and registered with Twilio, I used `ngrok` in conjunction with Twilio's text sandbox.

### Key Implementation Steps
- **SandBox**: use twilio Sandbox without any linking a number to meta, we still need an API key and credit in our account.
- **Number**: we get a temporary number for 24 hours, than we or other people can send messages to that number, aswell as sending from the server.
- **Local Testing**: Utilize `ngrok` to expose a local server over the internet, making it accessible to Twilio for webhook interactions.






### Security Concern: Exposing API Key in Commit History
During development, I encountered a significant security issue where the `appsettings.json` file was mistakenly committed to Git, exposing sensitive API keys in the commit history. Here’s how I resolved this issue:

### Resolution Steps
1. **Problem Identification**: Realizing that the API keys were exposed in multiple commits, not just the current branch state.
2. **Solution Implementation**:
   - I used `git filter-branch`, a powerful tool for rewriting commit histories, to remove traces of `appsettings.json` from all commits.
   - **Command Used**:
     ```bash
     git filter-branch --force --index-filter "git rm --cached --ignore-unmatch appsettings.json" --prune-empty --tag-name-filter cat -- --all
     ```
   - This command specifically targets all branches and tags, removing `appsettings.json` and ensuring it does not appear in any part of the project's commit history.
3. **Post-Cleanup**: After cleaning the commit history, ensure `appsettings.json` is properly listed in `.gitignore` to prevent future exposures.

### Conclusion
This experience underscores the importance of vigilance with sensitive data in version control systems. Always verify that configuration files containing sensitive information like API keys are excluded from commits. If a mistake occurs, immediate action using tools like `git filter-branch` can help mitigate potential security risks.
