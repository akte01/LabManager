$(document).ready(function () {
    var table = $("#equipment").DataTable({
        ajax: {
            url: "api/equipment",
            dataSrc: ""
        },
        columns: [
            {
                data: "Name"
            },
            {
                data: "Amount"
            },
            {
                data: "EquipmentLocation.Name"
            },
            {
                data: "Comment"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Polish.json"
        }
    });
});