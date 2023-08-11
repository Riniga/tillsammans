function loadPersonalInformation()
{
    currentUser = JSON.parse(localStorage.getItem('currentUser'));
    document.getElementById("email").value = currentUser.email;
    document.getElementById("name").value = currentUser.fullname;
    document.getElementById("personnumber").value = currentUser.personnumber;
    document.getElementById("adress").value = currentUser.adress;
    document.getElementById("postalcode").value = currentUser.postalcode;
    document.getElementById("city").value = currentUser.city;
    document.getElementById("primaryphone").value = currentUser.primaryphone;
    document.getElementById("secondaryphone").value = currentUser.secondaryphone;
    document.getElementById("license").value = currentUser.license;
    document.getElementById("club").value = currentUser.club;
    document.getElementById("zone").value = currentUser.zone;
}

if (document.getElementById("PersonalInformationTab")) loadPersonalInformation(); 

function updateProfile()
{
    $('#cover-spin').show(0)
    currentUser = JSON.parse(localStorage.getItem('currentUser'));
    currentUser.personnumber = document.getElementById("personnumber").value;
    currentUser.adress = document.getElementById("adress").value;
    currentUser.postalcode = document.getElementById("postalcode").value;
    currentUser.city = document.getElementById("city").value;
    currentUser.primaryphone = document.getElementById("primaryphone").value;
    currentUser.secondaryphone = document.getElementById("secondaryphone").value;
    currentUser.club = document.getElementById("club").value;
    localStorage.setItem('currentUser', JSON.stringify(currentUser) );

    fetch(updateUserApiUrl,
    {
        method: 'post',
        headers: {'Content-Type': 'application/text' },
        body: JSON.stringify(currentUser)
    })
    .then(response => response.json())
    .then(data =>
    {
        $('#cover-spin').hide(0)
    })
    .catch((error) => {
        console.log(error);
    });

    $('#cover-spin').hide(0)            
}
