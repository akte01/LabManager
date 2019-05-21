$(document).ready(function () {
    var table = $("#reagents").DataTable({
        dom: 'lBfrtip',
        ajax: {
            url: "api/reagents",
            dataSrc: ""
        },
        columns: [
            {
                data: "Name"
            },
            {
                data: "InitialAmount",
                render: function (data, type, full, meta) {
                    return full.InitialAmount + " " + full.Unit.Name;
                }
            },
            {
                data: "ConsumedAmount",
                render: function (data, type, full, meta) {
                    return full.ConsumedAmount + " " + full.Unit.Name;
                }
            },
            {
                data: "FinalAmount",
                render: function (data, type, full, meta) {
                    return full.FinalAmount + " " + full.Unit.Name;
                }
            },
            {
                data: "StorageLocation.Name"
            },
            {
                data: "Comment"
            },
            {
                data: "Id",
                render: function (data) {
                    return "<a href='Reagents/Edit/" + data + "'><span class='glyphicon glyphicon-pencil'></span></a>";
                }
            },
            {
                data: "Id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-reagent-id=" + data + "><span class='glyphicon glyphicon-trash'></button>";
                }
            },
            {
                data: "Id",
                render: function (data) {
                    return "<form id='AddConsumptionForm' method='post' action='reagents/AddConsumption/'>"
                        + "<input class='form-control' id ='addConsump' type = 'text' size = '6' name = 'ConsumedAmount' pattern='[0-9]+([,][0-9]+)?' title ='Tylko wartości liczbowe i przecinki są akceptowane!'>"
                        + "<input type = 'hidden' name = 'Id' value = '" + data + "'> <button type='submit'class='btn-link'><span class='glyphicon glyphicon-plus'></span ></button ></form>";
                },
                width: "90px"
            }
        ],
        buttons: [
            {
                extend: 'pdfHtml5',
                text: 'Generuj raport PDF',
                className: "customPDFbutton",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5]
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Polish.json"
        },
    });

    table.buttons().container()
        .appendTo('#example_wrapper .col-sm-6:eq(0)');

    $("#reagents").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Czy napewno chcesz usunąć wybrany odczynnik?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/reagents/" + button.attr("data-reagent-id"),
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