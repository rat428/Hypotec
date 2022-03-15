var RefinanceCalculator = {
    Init: function () {
        document.getElementById("divMonthlyPayment").style.display = 'none';
        document.getElementById("divPitieRadios").style.display = 'none';
        document.getElementById("divRemainingTerm").style.display = 'none';
        document.getElementById("divCashOut").style.display = 'none';
        document.getElementById("divYearlyInsurance").style.display = 'none';
        document.getElementById("Showall").style.display = 'none';
    },
    GetRefinanceCalculator: function () {
        $("#cover-spin").show(0);
        var SearchCalculatorDto = {};
        const cb19 = document.getElementById('exampleRadios1');
        SearchCalculatorDto.includeVAY = cb19.checked;
        SearchCalculatorDto.RefinanceGoal = document.getElementById("refinanceGoal").value;
        SearchCalculatorDto.CurrentLoanBalanceAmount = $("#currentLoanBalanceAmount").val();
        SearchCalculatorDto.MonthlyPayment = $("#monthlyPayment").val();
        SearchCalculatorDto.YearlyTaxes = $("#yearlyTaxes").val();
        SearchCalculatorDto.YearlyInsurance = $("#yearlyInsurance").val();
        SearchCalculatorDto.RemainingTerm = $("#remainingTerm").val();
        SearchCalculatorDto.CashOut = $("#cashOut").val();
        SearchCalculatorDto.HomeValue = $("#homeValue").val();
        SearchCalculatorDto.ZipCode = $("#zipCode").val();
        SearchCalculatorDto.Fico = $("#fico").val();
        jQuery.ajax({
            type: 'POST',
            async: true,
            url: '/Calculator/RefinanceCalculator',
            data: {
                searchCalculatorDto: SearchCalculatorDto,
            },
            success: function (data) {

                if (data.length == 0) {
                    $('#refinanceDetails').html('');
                    $('#recordnotFound').text('Record Not Found.');
                    document.getElementById("recordnotFound").style.display = 'block';
                    $("#cover-spin").hide(0);
                }
                else {
                    document.getElementById("recordnotFound").style.display = 'none';
                    document.getElementById("Showall").style.display = 'block';
                    var str = '';
                    var str = '<div class="col-md-12 p-0"><div class="mortgageCalculatorResult"><h4>We Recommend These Options For You</h4><div class="row" id="divrender">';
                    for (var i = 0; i < data.length; i++) {
                        if (data.length > 1) {
                            str = str + '<div class="col-md-4 col-xs-12 col-sm-12 col-lg-4 col-large" ><div class="resultBoxArea" >';
                        }
                        else {
                            str = str + '<div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 col-large" ><div class="resultBoxArea" >';
                        }
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
                        str = str + '$' + data[i].vaFunding;
                        str = str + '</span></li>';
                        str = str + '<li>Mortgage Insurance <span>';
                        str = str + '$' + data[i].mortgageInsurence;
                        str = str + '</span></li>';
                        str = str + '</ul>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '</div>';

                        str = str + '<div class="rBoxContent row">';

                        str = str + '<div class="imgList col-md-4  col-lg-4 col-xl-12">';

                        str = str + '<div class="gImage" style="height: 300px; width: 100%;"';
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
                        str = str + '$' + data[i].pmi;
                        str = str + '</h5></li>';
                        str = str + '</ul>';
                        str = str + '</div>';
                        str = str + '</div>';
                        str = str + '<div class="gContentRate col-md-12 col-lg-12 col-xl-12">';
                        str = str + '<div class="sContent">';

                        if (data[i].saveYear != null) {

                            str = str + '<span>You will shorten your term by</span>';
                            str = str + '</div>';
                            str = str + '<h3 class="blue-text">';
                            str = str + data[i].saveYear + " Years ";

                        }
                        else if (data[i].cashout != null) {
                            str = str + '<span>You Will Get</span>';
                            str = str + '</div>';
                            str = str + '<h3 class="blue-text">';
                            str = str + '$' + data[i].cashout;
                            str = str + '</h3>';
                        }
                        else {
                            str = str + '<span>You Will Save</span>';
                            str = str + '</div>';
                            str = str + '<h3 class="blue-text">';
                            str = str + '$' + data[i].savingAmount;
                            str = str + '<small> Per Month</small></h3>';
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
                    $('#refinanceDetails').html(str);
                    // Pie Chart Section Start
                    for (var i = 0; i < data.length; i++) {

                        var chart = new CanvasJS.Chart("chartContainer" + i.toString(),
                            {
                    
                                title: {
                                    text: "Monthly Payment  $" + data[i].monthlyCost,
                                    verticalAlign: "center",
                                    fontSize: 16,
                                    fontColor: "black",
                                    margin: 15,
                                    padding: 5,
                                    //fontWeight: "bolt",
                                    fontFamily: "arial",
                                    //borderThickness: 2,
                                    //cornerRadius: 6,
                                    wrap: false,
                                    dockInsidePlotArea: true
                                },
                                legend: {
                                    maxWidth: 10,
                                    itemWidth: 10

                                },

                                data: [

                                    {
                                        type: "doughnut",
                                        showInLegend: "false",
                                        legendText: "false",
                                        labelFontSize: "10",
                                        dataPoints: [
                                            { y: data[i].monthlyCost },
                                            { y: 0 },
                                            { y: 0 },
                                            { y: data[i].pmi },
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
    ValidateRefinanceCalculator: function () {

        var flag = true;
        var refinanceGoal = $('#refinanceGoal').val().trim();
        if (refinanceGoal === null || refinanceGoal === "Select one") {
            $('#errorRefinanceGoal').text('Please select one.');
            document.getElementById("errorRefinanceGoal").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorRefinanceGoal").style.display = 'none';
        }
        var currentLoanBalanceAmount = $('#currentLoanBalanceAmount').val().trim();
        if (currentLoanBalanceAmount === null || currentLoanBalanceAmount === "") {
            $('#errorCurrentLoanBalanceAmount').text('Please enter mortgage balance.');
            document.getElementById("errorCurrentLoanBalanceAmount").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorCurrentLoanBalanceAmount").style.display = 'none';
        }
        if ($('#refinanceGoal').val() === "LowerPayment") {
            var monthlyPayment = $('#monthlyPayment').val().trim();
            if (monthlyPayment === null || monthlyPayment === "") {
                $('#errorMonthlyPayment').text('Please enter current monthly payment.');
                document.getElementById("errorMonthlyPayment").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorMonthlyPayment").style.display = 'none';
            }
            if (radiobuttonval == "Yes") {
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

            }
            else {
                document.getElementById("errorYearlyInsurance").style.display = 'none';
                document.getElementById("errorYearlyTaxes").style.display = 'none';
            }

        }
        else if ($('#refinanceGoal').val() === "ShortenTerm") {
            var remainingTerm = $('#remainingTerm').val().trim();
            if (remainingTerm === null || remainingTerm === "") {
                $('#errorRemainingTerm').text('Please enter remaining term.');
                document.getElementById("errorRemainingTerm").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorRemainingTerm").style.display = 'none';
            }
        }
        else {
            var cashOut = $('#cashOut').val().trim();
            if (cashOut === null || cashOut === "") {
                $('#errorCashOut').text('Please enter cash out.');
                document.getElementById("errorCashOut").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorCashOut").style.display = 'none';
            }
        }
        var homeValue = $('#homeValue').val().trim();
        if (homeValue === null || homeValue === "") {
            $('#errorHomeValue').text('Please enter home value.');
            document.getElementById("errorHomeValue").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorHomeValue").style.display = 'none';
        }
        var zipCode = $('#zipCode').val().trim();
        if (zipCode === null || zipCode === "") {
            $('#errorZipCode').text('Please enter zip code.');
            document.getElementById("errorZipCode").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorZipCode").style.display = 'none';
        }
        var fico = $('#fico').val().trim();
        if (fico === null || fico === "Select credit Score") {
            $('#errorFico').text('Please select credit score.');
            document.getElementById("errorFico").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorFico").style.display = 'none';
        }
        if (flag) {
            RefinanceCalculator.GetRefinanceCalculator();
        }
    },
}

$("#fico").change(function (event) {
    document.getElementById("errorFico").style.display = 'none';
});
$("#zipCode").keyup(function (event) {
    document.getElementById("errorZipCode").style.display = 'none';
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
$("#cashOut").keyup(function (event) {
    var number = document.getElementById("cashOut").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("cashOut").value = x1 + x2;
    document.getElementById("errorCashOut").style.display = 'none';
});
$("#remainingTerm").keyup(function (event) {
    document.getElementById("errorRemainingTerm").style.display = 'none';
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
$("#currentLoanBalanceAmount").keyup(function (event) {
    var number = document.getElementById("currentLoanBalanceAmount").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("currentLoanBalanceAmount").value = x1 + x2;
    document.getElementById("errorCurrentLoanBalanceAmount").style.display = 'none';
});
$("#monthlyPayment").keyup(function (event) {
    var number = document.getElementById("monthlyPayment").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("monthlyPayment").value = x1 + x2;
    document.getElementById("errorMonthlyPayment").style.display = 'none';
});
$("#refinanceGoal").change(function () {
    document.getElementById("errorRefinanceGoal").style.display = 'none';
});

$("#refinanceGoal").change(function () {

    document.getElementById("errorRefinanceGoal").style.display = 'none';
    var refinanceGoal = $("#refinanceGoal").val();
    if (refinanceGoal == "LowerPayment") {
        document.getElementById("divMonthlyPayment").style.display = 'block';
        document.getElementById("divPitieRadios").style.display = 'block';
        document.getElementById("divRemainingTerm").style.display = 'none';
        document.getElementById("divCashOut").style.display = 'none';
    }
    else if (refinanceGoal == "ShortenTerm") {
        document.getElementById("divRemainingTerm").style.display = 'block';
        document.getElementById("divMonthlyPayment").style.display = 'none';
        document.getElementById("divCashOut").style.display = 'none';
        document.getElementById("divPitieRadios").style.display = 'none';
        document.getElementById("divYearlyInsurance").style.display = 'none';

    }
    else if (refinanceGoal == "CashOut") {
        document.getElementById("divCashOut").style.display = 'block';
        document.getElementById("divRemainingTerm").style.display = 'none';
        document.getElementById("divMonthlyPayment").style.display = 'none';
        document.getElementById("divPitieRadios").style.display = 'none';
        document.getElementById("divYearlyInsurance").style.display = 'none';
    }
});
var radiobuttonval = "No";
function handleClick(myRadio) {
    debugger;
    if (myRadio.value == "No") {

        radiobuttonval = myRadio.value;
        document.getElementById("divYearlyInsurance").style.display = 'none';
        document.getElementById("errorYearlyInsurance").style.display = 'none';
        document.getElementById("errorYearlyTaxes").style.display = 'none';
    }
    if (myRadio.value == "Yes") {
        radiobuttonval = myRadio.value;
        document.getElementById("divYearlyInsurance").style.display = 'block';
    }
}
function GetRefinanceResult() {
    RefinanceCalculator.ValidateRefinanceCalculator();


}
$("#currentLoanBalanceAmount").mask('000000000000000000000');
$("#monthlyPayment").mask('000000000000000000000');
$("#homeValue").mask('000000000000000000000');
$("#yearlyTaxes").mask('000000000000000000000');
$("#yearlyInsurance").mask('000000000000000000000');
$("#cashOut").mask('000000000000000000000');
$("#zipCode").mask('000000');
RefinanceCalculator.Init();