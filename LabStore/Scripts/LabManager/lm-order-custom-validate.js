function addNotEqualRule() {
    $.validator.addMethod("notEqual", function (value, element, param) {
        return this.optional(element) || value != '0';
    });
}

function addRegexRule() {
    $.validator.addMethod("regex", function (value, element, regexp) {
        var re = new RegExp(regexp);
        return this.optional(element) || re.test(value);
    });
}
function validateEquipmentInput() {
    $("#equipment").rules("add", {
        required: true,
        messages: {
            required: "Proszę wprowadzić nazwę sprzętu",
        }
    });
    $("#equipmentAmount").rules("add", {
        notEqual: '0',
        regex: "^[0-9]*$",
        messages: {
            notEqual: "Proszę wprowadzić ilość sprzętu",
            regex: "Proszę wproadzić liczbę całkowitą"
        }
    });
    addNotEqualRule();
    addRegexRule();
}

function validateReagentInput() {
    $("#reagent").rules("add", {
        required: true,
        messages: {
            required: "Proszę wprowadzić nazwę odczynnika",
        }
    });
    $("#solutionId").rules("add", {
        required: true,
        messages: {
            required: "Proszę zaznaczyć kategorię odczynnika"
        }
    });
}

function validateSolutionInput() {
    $("#concentration").rules("add", {
        notEqual: '0',
        regex: "^[0-9,]*$",
        messages: {
            notEqual: "Proszę wprowadzić stężenie roztworu",
            regex: "Proszę wprowadzić ',' zamiast '.'"
        }
    });
    $("#concentrationUnit").rules("add", {
        required: true,
        messages: {
            required: "Proszę wybrać jednostkę stężenia"
        }
    });
    $("#volume").rules("add", {
        notEqual: '0',
        regex: "^[0-9]*$",
        messages: {
            notEqual: "Proszę wprowadzić objętość roztworu",
            regex: "Proszę wproadzić liczbę całkowitą"
        }
    });
    addNotEqualRule();
    addRegexRule();
}

function validateSolidInput() {
    $("#unit").rules("add", {
        required: true,
        messages: {
            required: "Proszę wybrać jednostkę",
        }
    });
    $("#reagentAmount").rules("add", {
        notEqual: '0',
        regex: "^[0-9,]*$",
        messages: {
            notEqual: "Proszę wprowadzić ilość odczynnika",
            regex: "Proszę wprowadzić ',' zamiast '.'"
        }
    });
    addNotEqualRule();
    addRegexRule();
}