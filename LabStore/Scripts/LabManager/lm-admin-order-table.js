$(document).ready(function () {
    var table = $("#orders").DataTable({
        dom: 'lBfrtip',
        ajax: {
            url: "api/orders",
            dataSrc: ""
        },
        columns: [
            {
                data: "Date",
                render: function (data) {
                    var dateTime = data.toString();
                    var date = dateTime.substr(0, 10);
                    return date;
                }
            },
            {
                data: "User.Surname",
                render: function (data, type, full, meta) {
                    return full.User.Name + " " + full.User.Surname;
                }
            },
            {
                data: "Content",
                render: function (data) {
                    var content = data.split(/:|;/);
                    var splitedContent = [];
                    for (let i = 0; i < content.length; i++) {
                        splitedContent += (content[i] + ";<br>");
                    }
                    var result = splitedContent.replace("Roztwory;", "<strong>Roztwory:</strong>");
                    result = result.replace("Odczynniki stałe;", "<strong>Odczynniki stałe:</strong>");
                    result = result.replace("Sprzęt;", "<strong>Sprzęt:</strong>");
                    result = result.slice(0, -5);
                    return result;
                }
            },
            {
                data: "DateFor",
                render: function (data) {
                    var dateTime = data.toString();
                    var date = dateTime.substr(0, 10);
                    return date;
                }
            },
            {
                data: "Grade"
            },
            {
                data: "Comment",
                render: function (data) {
                    result = data.split("Edytowany:").join("<span style='color:#e95420'>Edytowany:</span>");
                    return result;
                }
            },
            {
                data: "Status.Name",
                render: function (data, type, full, meta) {
                    return "<form id='changeStatus' method='post' action='orders/ChangeStatus/'>"
                        + "<div class='changeStatusForm'><select class='form-control' name='StatusId'>"
                        + "<option value='' selected disabled hidden>" + full.Status.Name + "</option>"
                        + "<option value='1'>W przygotowaniu</option>"
                        + "<option value='2'>Zrealizowano</option>"
                        + "</select >"
                        + "<input type = 'hidden' name = 'Id' value = '" + full.Id + "'></div > "
                        + "<button type='submit' class='btn btn-primary' >Zmień status</span ></button ></form > ";
                },
                width: "100px"
            },
            {
                data: "Id",
                render: function (data) {
                    return "<a href='Orders/Edit/" + data + "'><span class='glyphicon glyphicon-pencil'></span></a>";
                }
            },
            {
                data: "Id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-order-id=" + data + "><span class='glyphicon glyphicon-trash'></button>";
                }
            }
        ],
        buttons: [
            {
                extend: 'pdfHtml5',
                text: 'Generuj raport PDF',
                className: "customPDFbutton",
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7]
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Polish.json"
        },
    });

    table.buttons().container()
        .appendTo('#example_wrapper .col-sm-6:eq(0)');

    $("#orders").on("click", ".js-delete", function () {
        var button = $(this);
        bootbox.confirm("Czy napewno chcesz usunąć wybrane zamówienie?", function (result) {
            if (result) {
                $.ajax({
                    url: "/api/orders/" + button.attr("data-order-id"),
                    method: "DELETE",
                    success: function () {
                        table.row(button.parents("tr")).remove().draw();
                    }
                });
            }
        });
    });

    $("#changeStatus").submit(function () {
        $.ajax({
            url: 'reagents/ChangeStatus',
            type: "POST",
            data: $("#changeStatus").serialize()
        });
        return false;
    });
});