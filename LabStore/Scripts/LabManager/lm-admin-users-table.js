$(document).ready(function () {
    var table = $("#users").DataTable({
        ajax: {
            url: "api/users",
            dataSrc: ""
        },
        columns: [
            {
                data: "Name"
            },
            {
                data: "Surname"
            },
            {
                data: "Email"
            },
            {
                data: "Id",
                render: function (data) {
                    return "<a href='users/edit/" + data + "'>Edytuj</a>";
                }
            },
            {
                data: "Id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-user-id=" + data + ">Usuń</button>";
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Polish.json"
        }
    });

    $("#users").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Czy napewno chcesz usunąć wybranego użytkownika?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/users/" + button.attr("data-user-id"),
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });

    });
});