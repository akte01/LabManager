calculate = function () {
    var initialAmount = document.getElementById('initialAmount').value;
    initialAmount = initialAmount.replace(',', '.')
    var consumedAmount = document.getElementById('consumedAmount').value;
    consumedAmount = consumedAmount.replace(',', '.')
    document.getElementById('finalAmount').value = (parseFloat(initialAmount) - parseFloat(consumedAmount));
    var finalAmount = document.getElementById('finalAmount').value;
    document.getElementById('finalAmount').value = finalAmount.replace('.', ',');
}