$(document).ready(function () {
    var table = $("#reagents").DataTable({
        ajax: {
            url: "api/reagents",
            dataSrc: ""
        },
        columns: [
            {
                data: "Name"
            },
            {
                data: "FinalAmount",
                render: function (data, type, full, meta) {
                    return full.FinalAmount + " " + full.Unit.Name;
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Polish.json"
        }
    });
});