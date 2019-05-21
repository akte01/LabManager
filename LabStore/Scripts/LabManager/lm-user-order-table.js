$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    var table = $("#orders").DataTable({
        ajax: {
            url: "/api/orders",
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
            },
            {
                data: "Id",
                render: function (data, type, full, meta) {
                    if (full.Status.Name == "Zrealizowano")
                        return "<span class='glyphicon glyphicon-pencil disabledEditor' data-toggle='tooltip'"
                            + "data-placement='bottom' title='Nie można edytować zrealizowanego zamówienia'></span>";
                    else return "<a href='Orders/Edit/" + data + "'>"
                       +  "<span class='glyphicon glyphicon-pencil'></span></a> ";
                }
            },
            {
                data: "Id",
                render: function (data) {
                    return "<button class='btn-link js-delete' data-order-id=" + data + "><span class='glyphicon glyphicon-trash'></button>";
                }
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Polish.json"
        },
    });

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
});