rem Require: npm i -g azure-functions-core-tools@3 --unsafe-perm true
cd judotech.api 
start func start --csharp
cd ..

rem Require: npm install -g gulp
cd judotech.referee
gulp --environment development

rem Require: npm install -g live-server
start live-server --port=4145 public