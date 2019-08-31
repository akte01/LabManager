$(document).ready(function () {
    $('#datetimepicker2').datetimepicker({
        format: 'YYYY-MM-DD',
        locale: 'pl',
        minDate: new Date(),
        sideBySide: true
    });
});