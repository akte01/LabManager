$(document).ready(function () {
    $('#datetimepicker2').datetimepicker({
        format: 'YYYY-MM-DD',
        locale: 'pl',
        defaultDate: moment(),
        minDate: new Date(),
        sideBySide: true
    });
});