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
    document.getElementById("club").value = currentUser.club;
    document.getElementById("zone").value = currentUser.zone;
}

if (document.getElementById("PersonalInformationTab")) loadPersonalInformation(); 

$(document).ready(function () {
    var table = $('#refereetable').DataTable(
        {
            autoWidth: false,
            ajax: { url: 'http://localhost:7071/api/ReadAllUser', dataSrc: "" },
            columns: [
                { data: 'fullname' },
                { data: 'email' },
                { data: 'club' },
                { data: 'zone' }
            ],
        }
    );
});
