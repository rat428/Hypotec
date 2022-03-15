var PurcahseCalculator = {
    Init: function () {
    },
    GetPurcahseCalculator: function () {
        $("#cover-spin").show(0);
        var SearchCalculatorDto = {};
        SearchCalculatorDto.CurrentLoanBalanceAmount = $("#homeValue").val();
        SearchCalculatorDto.DownPayment = $("#downPayment").val();
        SearchCalculatorDto.LoanTerm = $("#loanTerm").val();
        SearchCalculatorDto.YearlyInsurance = $("#yearlyInsurance").val();
        SearchCalculatorDto.InterestRate = $("#interestRate").val();
        SearchCalculatorDto.YearlyTaxes = $("#yearlyTaxes").val();
        SearchCalculatorDto.MonthlyHomeownersAssociation = $("#monthlyHomeownersAssociation").val();
        jQuery.ajax({
            type: 'POST',
            async: true,
            url: '/Calculator/PurcahseCalculator',
            data: {
                searchCalculatorDto: SearchCalculatorDto,
            },
            success: function (data) {
                if (data.length == 0) {
                    $('#purchaseDetails').html('');
                    $('#recordnotFound').text('Record Not Found.');
                    document.getElementById("recordnotFound").style.display = 'block';
                    $("#cover-spin").hide(0);
                }
                else {
                    $('#purchaseDetails').html('');
                    document.getElementById("recordnotFound").style.display = 'none';
                    var str = '';
                    var str = '<div class="col-md-12 p-0"><div class="mortgageCalculatorResult"><h4>We Recommend These Options For You</h4><div class="row" id="divrender">';
                    for (var i = 0; i < data.length; i++) {
                        str = str + '<div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 col-large" ><div class="resultBoxArea" >';
                        str = str + '<div class="rBoxHeader d-flex justify-content-between">';

                        str = str + '<h4 class="p-0">Monthly Payment <span class="blue-text resultRate">';
                        str = str + '$' + data[i].monthlyCost;
                        str = str + '</span></h4>';
                        str = str + '<div class="dropdown">';
                        str = str + '<button class="btn dropdown-toggle p-0" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
                        str = str + "View Closing Costs";
                        str = str + '</button>';
                        str = str + '<div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">';
                        str = str + '<div class="gContentRate">';
                        str = str + '<ul class="m-0">';
                        str = str + '<li>Net Fees <span>';
                        str = str + '$' + data[i].netFees;
                        str = str + '</span></li>';
                        str = str + '<li>VA Funding Fee <span>';
                        str = str + '$' + 0.00;
                        str = str + '</span></li>';
                        str = str + '<li>Mortgage Insurance <span>';
                        str = str + '$' + 0.00;
                        str = str + '</span></li>';
                        str = str + '</ul>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '<div class="rBoxContent row">';
                        str = str + '<div class="imgList col-md-4  col-lg-4 col-xl-12">';
                        str = str + '<div class="gImage"';
                        str = str + 'id=';
                        str = str + 'chartContainer' + i.toString();
                        str = str + '>';

                        //'+ i.toString() + ' > ';
                        str = str + '</div>';
                        str = str + '<div class="gContentList">';
                        str = str + '<ul class="m-0">';
                        str = str + '<li><div><span class="fColor"></span>';
                        str = str + 'Principal And Interest';
                        str = str + '</div> <h5>';
                        str = str + '$' + data[i].monthlyCost;
                        str = str + '</h5></li>';
                        str = str + '<li><div><span class="sColor"></span>';
                        str = str + 'Taxes';
                        str = str + '</div> <h5>';
                        str = str + '$' + 0.00;
                        str = str + '</h5></li>';
                        str = str + '<li><div><span class="tColor"></span>';
                        str = str + 'Insurance';
                        str = str + '</div> <h5>';
                        str = str + '$' + 0.00;
                        str = str + '</h5></li>';
                        str = str + '<li><div><span class="ftColor"></span>';
                        str = str + 'PMI';
                        str = str + '</div> <h5>';
                        str = str + '$' + 0.00;
                        str = str + '</h5></li>';
                        str = str + '</ul>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '<div class="gContentRate col-md-12 col-lg-12 col-xl-12">';
                        str = str + '<div class="sContent">';

                        if (data[i].saveYear == null) {
                            str = str + '<span>You Will Save</span>';
                            str = str + '</div>';
                            str = str + '<h3 class="blue-text">';
                            str = str + '$' + data[i].savingAmount;
                            str = str + '<small> Per Month</small></h3>';
                        }
                        else {
                            str = str + '<span>You will shorten your term by</span>';
                            str = str + '</div>';
                            str = str + '<h3 class="blue-text">';
                            str = str + data[i].saveYear + " Years ";
                        }

                        str = str + '<ul class="m-0">';
                        str = str + '<li>Term <span>';
                        str = str + data[i].productDesc;
                        str = str + '</span></li>';
                        str = str + '<li>Rate/APR <span>';
                        str = str + data[i].rate + ' / ' + data[i].apr;
                        str = str + '</span></li>';
                        str = str + '<li>Monthly Payment <span>';
                        str = str + '$' + data[i].monthlyCost;
                        str = str + '</span></li>';
                        str = str + '</ul>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '<div class="rBoxFooter">';
                        str = str + "<a href='../account/started' class='btn'>Get Your Custom Rate</a>";
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';

                    }
                    str = str + '</div>';
                    str = str + '</div>';
                    str = str + '</div>';
                    $('#purchaseDetails').html(str);
                    // Pie Chart Section Start
                    for (var i = 0; i < data.length; i++) {

                        var chart = new CanvasJS.Chart("chartContainer" + i.toString(),
                            {
                                title: {
                                    text: ""
                                },
                                legend: {
                                    maxWidth: 10,
                                    itemWidth: 10
                                },
                                data: [
                                    {
                                        type: "doughnut",
                                        showInLegend: false,
                                        legendText: "{indexLabel}",
                                        dataPoints: [
                                            { y: data[i].monthlyCost, indexLabel: "Principal And Interest" },
                                            { y: 0, indexLabel: "Insurance" },
                                            { y: 0, indexLabel: "Taxes" },
                                            { y: 0, indexLabel: "PMI" },
                                        ]
                                    }
                                ]
                            });
                        chart.render();
                    }
                    $("#cover-spin").hide(0);
                }
                // Pie Chart Section End

                //for (var i = 0; i < data.length; i++) {
                //    RefinanceCalculator.PieChart();
                //}
            },

        });
    },
    ValidatePurcahseCalculator: function () {
        var flag = true;
        var homeValue = $('#homeValue').val().trim();
        if (homeValue === null || homeValue === "" || homeValue === "0") {
            $('#errorHomeValue').text('Please enter home value.');
            document.getElementById("errorHomeValue").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorHomeValue").style.display = 'none';
        }
        var downPayment = $('#downPayment').val().trim();
        if (downPayment === null || downPayment === "" || downPayment === "0") {
            $('#errorDownPayment').text('Please enter down payment.');
            document.getElementById("errorDownPayment").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorDownPayment").style.display = 'none';
        }
        var loanTerm = $('#loanTerm').val().trim();
        if (loanTerm === null || loanTerm === "Select One") {
            $('#errorLoanTerm').text('Please enter loan term.');
            document.getElementById("errorLoanTerm").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorLoanTerm").style.display = 'none';
        }
        var interestRate = $('#interestRate').val().trim();
        if (interestRate === null || interestRate === "") {
            $('#errorInterestRate').text('Please enter interest rate.');
            document.getElementById("errorInterestRate").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorInterestRate").style.display = 'none';
        }
        var yearlyTaxes = $('#yearlyTaxes').val().trim();
        if (yearlyTaxes === null || yearlyTaxes === "") {
            $('#errorYearlyTaxes').text('Please enter taxes.');
            document.getElementById("errorYearlyTaxes").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorYearlyTaxes").style.display = 'none';
        }
        var yearlyInsurance = $('#yearlyInsurance').val().trim();
        if (yearlyInsurance === null || yearlyInsurance === "") {
            $('#errorYearlyInsurance').text('Please enter insurance.');
            document.getElementById("errorYearlyInsurance").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorYearlyInsurance").style.display = 'none';
        }
        var monthlyHomeownersAssociation = $('#monthlyHomeownersAssociation').val().trim();
        if (monthlyHomeownersAssociation === null || monthlyHomeownersAssociation === "") {
            $('#errorMonthlyHomeownersAssociation').text('Please enter monthly homeowners association.');
            document.getElementById("errorMonthlyHomeownersAssociation").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorMonthlyHomeownersAssociation").style.display = 'none';
        }

        if (flag) {
            PurcahseCalculator.GetPurcahseCalculator();
        }
    },
}
$("#homeValue").focusout(function (event) {
    var flag = true;
    var homeValue = $('#homeValue').val().trim();
    if (homeValue === null || homeValue === "" || homeValue === "0") {
        $('#errorHomeValue').text('Please enter home value.');
        document.getElementById("errorHomeValue").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorHomeValue").style.display = 'none';
    }
});
$("#downPayment").focusout(function (event) {
    var flag = true;
    var downPayment = $('#downPayment').val().trim();
    if (downPayment === null || downPayment === "" || downPayment === "0") {
        $('#errorDownPayment').text('Please enter down payment.');
        document.getElementById("errorDownPayment").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorDownPayment").style.display = 'none';
    }
});
$("#interestRate").focusout(function (event) {
    var flag = true;
    var interestRate = $('#interestRate').val().trim();
    if (interestRate === null || interestRate === "") {
        $('#errorInterestRate').text('Please enter interest rate.');
        document.getElementById("errorInterestRate").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorInterestRate").style.display = 'none';
    }
});
$("#yearlyTaxes").focusout(function (event) {
    var flag = true;
    var yearlyTaxes = $('#yearlyTaxes').val().trim();
    if (yearlyTaxes === null || yearlyTaxes === "") {
        $('#errorYearlyTaxes').text('Please enter taxes.');
        document.getElementById("errorYearlyTaxes").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorYearlyTaxes").style.display = 'none';
    }
});
$("#yearlyInsurance").focusout(function (event) {
    var flag = true;
    var yearlyInsurance = $('#yearlyInsurance').val().trim();
    if (yearlyInsurance === null || yearlyInsurance === "") {
        $('#errorYearlyInsurance').text('Please enter insurance.');
        document.getElementById("errorYearlyInsurance").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorYearlyInsurance").style.display = 'none';
    }
});
$("#monthlyHomeownersAssociation").focusout(function (event) {
    var flag = true;
    var monthlyHomeownersAssociation = $('#monthlyHomeownersAssociation').val().trim();
    if (monthlyHomeownersAssociation === null || monthlyHomeownersAssociation === "") {
        $('#errorMonthlyHomeownersAssociation').text('Please enter monthly homeowners association.');
        document.getElementById("errorMonthlyHomeownersAssociation").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorMonthlyHomeownersAssociation").style.display = 'none';
    }
});
$("#homeValue").keyup(function (event) {
    var number = document.getElementById("homeValue").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("homeValue").value = x1 + x2;
    document.getElementById("errorHomeValue").style.display = 'none';
});
$("#downPayment").keyup(function (event) {
    var number = document.getElementById("downPayment").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("downPayment").value = x1 + x2;
    document.getElementById("errorDownPayment").style.display = 'none';
});
$("#loanTerm").change(function (event) {
    document.getElementById("errorLoanTerm").style.display = 'none';
});
$("#interestRate").keyup(function (event) {
    document.getElementById("errorInterestRate").style.display = 'none';
});
$("#yearlyTaxes").keyup(function (event) {
    var number = document.getElementById("yearlyTaxes").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("yearlyTaxes").value = x1 + x2;
    document.getElementById("errorYearlyTaxes").style.display = 'none';
});
$("#yearlyInsurance").keyup(function (event) {
    var number = document.getElementById("yearlyInsurance").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("yearlyInsurance").value = x1 + x2;
    document.getElementById("errorYearlyInsurance").style.display = 'none';
});

$("#monthlyHomeownersAssociation").change(function (event) {
    var number = document.getElementById("monthlyHomeownersAssociation").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("monthlyHomeownersAssociation").value = x1 + x2;
    document.getElementById("errorMonthlyHomeownersAssociation").style.display = 'none';
});
function GetPurcahseResult() {
    PurcahseCalculator.ValidatePurcahseCalculator();
}
$("#purchasePrice").mask('000000000000000000000');
$("#downPayment").mask('000000000000000000000');
$("#homeValue").mask('000000000000000000000');
$("#yearlyTaxes").mask('000000000000000000000');
$("#yearlyInsurance").mask('000000000000000000000');
$("#monthlyHomeownersAssociation").mask('000000000000000000000');
$("#interestRate").mask('99.99');

PurcahseCalculator.Init();