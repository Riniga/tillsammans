
$(document).ready(function () {
    var table = $('#refereetable').DataTable(
        {
            autoWidth: false,
            ajax: { url: readAllUsersApiUrl, dataSrc: "" },
            columns: [
                { data: 'fullname' },
                { data: 'email' },
                { data: 'club' },
                { data: 'zone' }
            ],
        }
    );
});
