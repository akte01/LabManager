$(document).ready(function () {
    document.getElementById('reagentId').checked = false;
    document.getElementById('equipmentId').checked = false;
    document.getElementById('solutionId').checked = false;
    document.getElementById('solidId').checked = false;
});

function showROrEInput() {
    if (document.getElementById('reagentId').checked) {
        document.getElementById('reagentInput').style.display = 'block';
        validateReagentInput();
    }
    else document.getElementById('reagentInput').style.display = 'none';

    if (document.getElementById('equipmentId').checked) {
        document.getElementById('equipmentInput').style.display = 'block';
        document.getElementById('solutionId').checked = false;
        document.getElementById('solidId').checked = false;
        document.getElementById('solutionInput').style.display = 'none';
        document.getElementById('solidInput').style.display = 'none';
        validateEquipmentInput();
    }
    else document.getElementById('equipmentInput').style.display = 'none';

    setValueAsPlaceholder();
}

function showSolutionOrSolidInput() {
    if (document.getElementById('solutionId').checked) {
        document.getElementById('solutionInput').style.display = 'block';
        validateSolutionInput();
    }
    else document.getElementById('solutionInput').style.display = 'none';

    if (document.getElementById('solidId').checked) {
        document.getElementById('solidInput').style.display = 'block';
        validateSolidInput();
    }
    else document.getElementById('solidInput').style.display = 'none';
}