# AdvancedProg2VS

### Run the project:

#### Run the reviews site:
1. pull the git files to a MVC project
2. execute the program and open chrome

#### Run the Rest-Api:
1. pull the git files to a Web-Rest-Api project
2. execute the program

#### Run client-side (ReactJS):
1. Clone the files to a react project.
2. Navigate in the command line to the project path.
3. Activate the next commands: 1. (npm install bootstrap) 2. (npm i jquery) 3. (npm i bootstrap-icons) 4. (npm install @microsoft/signalr) those are the packages (bootstrap, bootstrap icons, jquery, signalR).
4. Edit the ServerUrl.js file to match the Rest-Api URL.
5. Write "npm start".
6. Open google chrome and navigate (in the url) to "http://localhost:3000".


### Cryptography in the code:
For us to know which user is currently addressing the API, we gave the client Json Web Token (JWT) uppon registration/login which he sends to the API in each authorized request, then, we extract his user name from the token (decrypt the token) and handle his request.
To maintain the user's safety, we encrypted their passwords in the database with an asymmetrical encryption method (SHA256).
