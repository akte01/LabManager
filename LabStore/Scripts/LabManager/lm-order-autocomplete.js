var reagents = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    remote: {
        url: '/api/reagents?query=%QUERY',
        wildcard: '%QUERY'
    }
});
$('#reagent').typeahead({
    minLength: 3,
    highlight: true
}, {
        name: 'reagents',
        display: 'Name',
        source: reagents
    });

var equipmentList = new Bloodhound({
    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
    queryTokenizer: Bloodhound.tokenizers.whitespace,
    remote: {
        url: '/api/equipment?query=%QUERY',
        wildcard: '%QUERY'
    }
});
$('#equipment').typeahead({
    minLength: 3,
    highlight: true
}, {
        name: 'equipment',
        display: 'Name',
        source: equipmentList
    });