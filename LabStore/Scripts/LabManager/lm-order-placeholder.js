function setValueAsPlaceholder() {
    if (document.getElementById('reagentId').checked || document.getElementById('equipmentId').checked) {
        if (document.getElementById('dateFor').placeholder != "2000-01-01 00:00:00" && document.getElementById('grade').placeholder != "") {
            document.getElementById('dateFor').value = document.getElementById('dateFor').placeholder
            document.getElementById('grade').value = document.getElementById('grade').placeholder
            document.getElementById('comment').value = document.getElementById('comment').placeholder
        }
    }
};