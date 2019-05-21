$(document).ready(function () {
    var table = $("#equipment").DataTable({
        dom: 'lBfrtip',
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
            },            {

                data: "EquipmentLocation.Name"
            },
            {
                data: "Comment"
            },
            {
                data: "Id",
                render: function (data) {
                    return "<a href='Equipment/Edit/" + data + "'><span class='glyphicon glyphicon-pencil'></span></a>";
                }
            },
            {
                data: "Id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-equipment-id=" + data + "><span class='glyphicon glyphicon-trash'></button>";
                }
            }
        ],
        buttons: [
            {
                extend: 'pdfHtml5',
                text: 'Generuj raport PDF',
                className: "customPDFbutton",
                exportOptions: {
                    columns: [0, 1, 2, 3]
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Polish.json"
        },
    });

    table.buttons().container()
        .appendTo('#example_wrapper .col-sm-6:eq(0)');

    $("#equipment").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Czy napewno chcesz usunąć wybrany sprzęt?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/equipment/" + button.attr("data-equipment-id"),
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    });

    $("#AddConsumptionForm").submit(function () {
        $.ajax({
            url: 'reagents/AddConsumption',
            type: "POST",
            data: $("#AddConsumptionForm").serialize()
        });
        return false;
    });
});