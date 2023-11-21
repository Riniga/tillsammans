rem Require: npm i -g azure-functions-core-tools@3 --unsafe-perm true
cd tillsammans.api 
start func start --csharp
cd ..

rem Require: npm install -g gulp
cd tillsammans.web
gulp

rem Require: npm install -g live-server
start live-server --port=4145 tillsammans.web\public