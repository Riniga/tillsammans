class User {
    constructor(username, name) {
      this.username = username;
      this.name = name;
    }
}

function signIn()
{
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    
    

    fetch("http://localhost:7071/api/Login",
        {
            method: 'post',
            headers: {
                'Content-Type': 'application/text',
            },
            body: "{ username: '" + username + "', password: '" + password + "'  }"
        })
        .then(response => response.json())
        .then(data =>
        {
            console.log("token: "+data.token);
            if (data.token!="00000000-0000-0000-0000-000000000000") 
            {
                
                SuccessfullLogin(data);
                window.location.href = "/start.html";
            }
            else  
                ShowFailedLogin();

        })
        .catch((error) => {
            console.log(error);
            ShowFailedLogin();
        });
}

function signOut()
{
    localStorage.removeItem('currentUser');
    
    ResetLoginUI();
}

function SuccessfullLogin(user)
{
    const userObject = new User(user.username, user.fullName);
    localStorage.setItem('currentUser', JSON.stringify(userObject) );
    ResetLoginUI();
}

function ShowFailedLogin()
{
    document.getElementById("username").setAttribute('aria-invalid', 'true');
    document.getElementById("password").setAttribute('aria-invalid', 'true');
    document.getElementById("failedlogin").style.visibility = "visible";
}

function ResetLoginUI()
{

    currentUser = localStorage.getItem('currentUser');
    var loginItem = document.getElementById("login");
    var logoutItem = document.getElementById("logout");

    if (currentUser)
    {
        document.getElementById("userFirstName").innerHTML=JSON.parse(currentUser).name;
        loginItem.setAttribute('hidden', 'true');
        logoutItem.removeAttribute('hidden');
    }
    else
    {
        if (window.location.pathname!='/') window.location.href = "/";
        logoutItem.setAttribute('hidden', 'true');
        loginItem.removeAttribute('hidden');
    }

    document.getElementById("username").setAttribute('aria-invalid', 'false');
    document.getElementById("password").setAttribute('aria-invalid', 'false');
    document.getElementById("failedlogin").style.visibility = "hidden";
}

ResetLoginUI(); 