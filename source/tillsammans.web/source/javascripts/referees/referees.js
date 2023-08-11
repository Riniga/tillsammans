
$(document).ready(function () {
    var table = $('#refereetable').DataTable(
        {
            autoWidth: false,
            ajax: { url: readAllUsersApiUrl, dataSrc: "" },
            columns: [
                { data: 'fullname' },
                { data: 'license' },
                { data: 'zone' },
                { data: 'club' },
                { data: 'email' }
            ],
        }
    );
});
