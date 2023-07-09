class User {
    constructor(email, fullname, password) {
      this.email = email;
      this.fullname = fullname;
      this.password = password;
    }
}

class Login {
    constructor(email, token) {
      this.email = email;
      this.token = token;
    }
}

function signIn()
{
    var email = document.getElementById("username").value;
    var password = document.getElementById("password").value;
    const userObject = new User(email,"", password);

    fetch(apihostname + "/api/Login",
        {
            method: 'post',
            headers: {'Content-Type': 'application/text' },
            body: JSON.stringify(userObject) 
        })
        .then(response => response.json())
        .then(data =>
        {
            console.log("token: "+data.token);
            if (data.token!="00000000-0000-0000-0000-000000000000") 
            {
                
                SuccessfullLogin(data);
            }
            else  
                ShowFailedLogin();

        })
        .catch((error) => {
            console.log(error);
            ShowFailedLogin();
        });
}


function SuccessfullLogin(login)
{
    localStorage.setItem('currentLogin', JSON.stringify(login) );
    setCurrentUser(login.email);
}

function setCurrentUser(email)
{
    fetch(apihostname + "/api/ReadUser?email=" + email,
        {
            method: 'get',
            headers: {'Content-Type': 'application/text' }
        })
        .then(response => response.json())
        .then(data =>
        {
            localStorage.setItem('currentUser', JSON.stringify(data) );
            window.location.href = "/start.html";
        })
        .catch((error) => {
            console.log(error);
        });
}


function signOut()
{
    currentLogin = localStorage.getItem('currentLogin');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('currentLogin');

    fetch(apihostname + "/api/Logout",
        {
            method: 'post',
            headers: {'Content-Type': 'application/text' },
            body: currentLogin 
        })
        .then(response => response.json())
        .then(data =>
        {
            window.location.href = "/";
        })
        .catch((error) => {
            console.log(error);
        });

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
        document.getElementById("userFirstName").innerHTML=JSON.parse(currentUser).fullname;
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