var Purchase = {

    Init: function () {
        document.getElementById("tablistHide").style.display = 'none';
        document.getElementById("Showall").style.display = 'none';
        var isOne = $("#one").hasClass('active');
        var isTwo = $("#two-tab").hasClass('active');
        var isThree = $("#three-tab").hasClass('active');
        var isforth = $("#forth-tab").hasClass('active');
        //Purchase.submitRating();
    },
    submitRating: function () {
        $("#cover-spin").show(0);
        //document.getElementById("resultTabBtn").style.display = 'none';
        var searchRateDto = {};

        if ($("#one").hasClass('active') == true) {
            searchRateDto.all = "Yes"
        }
        if ($("#forth-tab").hasClass('active') == true) {
            searchRateDto.loanProduct1 = "Yes"
        }

        if ($("#three-tab").hasClass('active') == true) {
            searchRateDto.loanProduct2 = "Yes"
        }
        if ($("#two-tab").hasClass('active') == true) {
            searchRateDto.loanProduct3 = "Yes"
        }
        searchRateDto.loanPurpose = document.getElementById("loanpurpose").value;
        searchRateDto.zipCode = $("#PurZipCode").val();
        searchRateDto.creditScore = $("#PurCreditScore").val();
        searchRateDto.propertyType = $("#propertyType").val();
        searchRateDto.propertyUsage = $("#propertyUsage").val();
        searchRateDto.purchasePrice = $("#purchasePrice").val();
        searchRateDto.downPayment = $("#down").val();
        searchRateDto.homeValue = $("#homeValue").val();
        searchRateDto.showAll = paging;
        //$("#divLoader").show();
        if (searchRateDto.loanPurpose == 1) {
            if (paging == 'three') {
                jQuery.ajax({
                    type: 'POST',
                    async: true,
                    url: '/LoanRate/IndexRate',
                    data: {
                        searchRateDto: searchRateDto,
                    },
                    success: function (data) {
                        if (data.length == 0) {
                            $('#rateDetails').html('');


                            $('#recordnotFound').text('Record Not Found.');
                            document.getElementById("recordnotFound").style.display = 'block';
                            $("#cover-spin").hide(0);
                        }
                        else {
                            $('#setCount').text(data.length);
                            document.getElementById("recordnotFound").style.display = 'none';
                            document.getElementById("tablistHide").style.display = 'block';
                            document.getElementById("Showall").style.display = 'block';
                            $('#rateDetails').html('');
                            var str = '';
                            var j = 0;
                            str = str + '<ul class="p-0 m-0">';
                            for (var i = 0; i < data.length; i++) {

                                str = str + '<li class="d-flex flex-column flex-md-row justify-content-between align-items-center">';
                                //str = str + '<div class="lenderImg"><img src="~/images/lender-img.svg"" /></div>';
                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].rate + ' / ' + data[i].apr;
                                str = str + '</h4><p>Estimated Rate/APR</p></div>';

                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].monthlyCost + '/mo';
                                str = str + '</h4><p>Monthly Payment</p></div>';

                                //str = str + '<div class="lenderContent"><h4>';
                                //str = str + '$0.00';
                                //str = str + '</h4><p>Points</p></div>';

                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].yearlyDesc;
                                str = str + '</h4></div></li>';

                            }

                            str = str + '</ul>';
                            $('#rateDetails').html(str);
                            $("#cover-spin").hide(0);
                        }
                    },
                });
            }

        }
        else {
            if (paging == 'three') {
                jQuery.ajax({
                    type: 'POST',
                    async: true,
                    url: '/LoanRate/IndexRate',
                    data: {
                        searchRateDto: searchRateDto,
                    },
                    success: function (data) {
                        if (data.length == 0) {
                            $('#rateDetails').html('');
                            $('#recordnotFound').text('Record Not Found.');
                            document.getElementById("recordnotFound").style.display = 'block';
                            $("#cover-spin").hide(0);
                        }
                        else {
                            $('#setCount').text(data.length);
                            document.getElementById("recordnotFound").style.display = 'none';
                            document.getElementById("tablistHide").style.display = 'block';
                            document.getElementById("Showall").style.display = 'block';
                            $('#rateDetails').html('');
                            var str = '';
                            var j = 0;
                            str = str + '<ul class="p-0 m-0">';
                            for (var i = 0; i < data.length; i++) {


                                str = str + '<li class="d-flex flex-column flex-md-row justify-content-between align-items-center">';
                                //str = str + '<div class="lenderImg"><img src="~/images/lender-img.svg"" /></div>';
                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].rate + ' / ' + data[i].apr;
                                str = str + '</h4><p>Estimated Rate/APR</p></div>';

                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].monthlyCost + '/mo';
                                str = str + '</h4><p>Monthly Payment</p></div>';

                                //str = str + '<div class="lenderContent"><h4>';
                                //str = str + '$0.00';
                                //str = str + '</h4><p>Points</p></div>';

                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].yearlyDesc;
                                str = str + '</h4></div></li>';
                            }
                            str = str + '</ul>';
                            $('#rateDetails').html(str);
                            $("#cover-spin").hide(0);
                        }
                    },
                });
            }
        }
    },
    ValidatePurchase: function () {
        var flag = true;
        var purZipCode = $('#PurZipCode').val().trim();
        if (purZipCode === null || purZipCode === "") {
            $('#errorPurZipCode').text('Please enter zip code.');
            document.getElementById("errorPurZipCode").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorPurZipCode").style.display = 'none';
        }
        var creditScore = $('#PurCreditScore').val().trim();
        if (creditScore === null || creditScore === "Select credit Score") {
            $('#errorPurCreditScore').text('Please select credit score.');
            document.getElementById("errorPurCreditScore").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorPurCreditScore").style.display = 'none';
        }
        var stringWithoutCommas = document.getElementById("homeValue").value.replace(/,/g, '');
        var hv = Number(stringWithoutCommas);
        if ($('#homeValue').val() == "" || hv > 0) {
            var loanPrice = $('#purchasePrice').val().trim();
            if (loanPrice === null || loanPrice === "" || loanPrice === "0") {
                $('#errorpurchasePrice').text('Please enter mortgage balance.');
                document.getElementById("errorpurchasePrice").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorpurchasePrice").style.display = 'none';
            }
            var homeValue = $('#homeValue').val().trim();
            if (homeValue === null || homeValue === "" || homeValue === "0") {
                $('#errorHomeValue').text('Please select home value.');
                document.getElementById("errorHomeValue").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorHomeValue").style.display = 'none';
            }
            if ($('#homeValue').val() == "" || hv > 0) {
                var purchasePrice1 = Number(document.getElementById("purchasePrice").value.replace(",", ""));
                var homeValue1 = Number(document.getElementById("homeValue").value.replace(",", ""));
                if (homeValue1 < purchasePrice1) {
                    $('#errorpurchasePrice').text('Mortgage value should be less than home value.');
                    document.getElementById("errorpurchasePrice").style.display = 'block';
                    flag = false;
                }
            }
        }
        else {
            var loanPrice = $('#purchasePrice').val().trim();
            if (loanPrice === null || loanPrice === "" || loanPrice === "0") {
                $('#errorpurchasePrice').text('Please enter purchase price.');
                document.getElementById("errorpurchasePrice").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorpurchasePrice").style.display = 'none';
            }
            var downP = $('#down').val().trim();
            if (downP === null || downP === "" || downP === "0") {
                $('#errordownPayment').text('Please enter down payment.');
                document.getElementById("errordownPayment").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errordownPayment").style.display = 'none';
            }

            var percentage = $('#percentage').val().trim();
            if (percentage === null || percentage === "" || percentage === "0") {
                $('#errorpercentage').text('Please enter percentage.');
                document.getElementById("errorpercentage").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorpercentage").style.display = 'none';
            }
            var percentage = $('#percentage').val().trim();
            if (percentage === null || percentage === "") {
                $('#errorpercentage').text('Please enter percentage.');
                document.getElementById("errorpercentage").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorpercentage").style.display = 'none';
            }
        }
        var propertyType = $('#propertyType').val();
        if (propertyType === null || propertyType === "Select property type") {
            $('#errorPropertyType').text('Please select property type.');
            document.getElementById("errorPropertyType").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorPropertyType").style.display = 'none';
        }
        var propertyUsage = $('#propertyUsage').val();
        if (propertyUsage === null || propertyUsage === "Select property usage") {
            $('#errorPropertyUsage').text('Please select property usage.');
            document.getElementById("errorPropertyUsage").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorPropertyUsage").style.display = 'none';
        }

        if (flag) {
            Purchase.submitRating();
        }
    },
}
var paging = "three";
$("#purchasePrice").keyup(function (event) {
    var number = document.getElementById("purchasePrice").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("purchasePrice").value = x1 + x2;
    document.getElementById("errorpurchasePrice").style.display = 'none';

    if (document.getElementById("percentage").value != null || document.getElementById("percentage").value != "") {
        var numVal1 = document.getElementById("purchasePrice").value.replace(/,/g, '');
        //var numVal1 = Number(document.getElementById("purchasePrice").value.replace(",", ""));
        var numVal2 = Number(document.getElementById("percentage").value) / 100;
        var totalValue = numVal1 - (numVal1 * numVal2)
        var downPayment = totalValue.toFixed(2)
        var totalvalue = numVal1 - downPayment;
        totalvalue += '';
        totalvalue = totalvalue.replace(/,/g, '');
        x = totalvalue.split('.');
        x3 = x[0];
        x4 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x3)) {
            x3 = x3.replace(rgx, '$1' + ',' + '$2');
        }
        document.getElementById("down").value = x3 + x4;
        document.getElementById("errorpercentage").style.display = 'none';
        document.getElementById("errordownPayment").style.display = 'none';
    }

    //Purchase.submitRating();
});

$("#percentage").keyup(function (event) {
   var numVal1 = document.getElementById("purchasePrice").value.replace(/,/g, '');
    //var numVal1 = Number(document.getElementById("purchasePrice").value.replace(",", ""));
    var numVal2 = Number(document.getElementById("percentage").value.replace("%","")) / 100;
    var totalValue = numVal1 - (numVal1 * numVal2)
    var downPayment = totalValue.toFixed(2)
    var number = numVal1 - downPayment;
    number += '';
    number = number.replace(/,/g, '');
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("down").value = x1 + x2;
    document.getElementById("errorpercentage").style.display = 'none';
    document.getElementById("errordownPayment").style.display = 'none';
    //downpayment();
    //Purchase.submitRating();
});
var downpp = "0";
$("#down").keyup(function (event) {
    var nStr;
    var pp = document.getElementById("purchasePrice").value.split(',');
    for (var i = 0; i < pp.length; i++) {
        nStr += pp[i];

    }
    var pPos1 = Number(nStr.replace("undefined", ""));
    var dStr;
    var dp = document.getElementById("down").value.split(',');
    for (var i = 0; i < dp.length; i++) {
        dStr += dp[i];

    }
    var pEarned1 = Number(dStr.replace("undefined", ""));

    if (pPos1 > pEarned1) {
        var ppStr;
        var pp1 = document.getElementById("purchasePrice").value.split(',');
        for (var i = 0; i < pp1.length; i++) {
            ppStr += pp1[i];

        }
        var pPos = Number(ppStr.replace("undefined", ""));

        var dpStr;
        var dp1 = document.getElementById("down").value.split(',');
        for (var i = 0; i < dp1.length; i++) {
            dpStr += dp1[i];

        }
        var pEarned = Number(dpStr.replace("undefined", ""));

        downpp = pEarned;

        var perc = "";
        if (isNaN(pPos) || isNaN(pEarned)) {
            perc = " ";
        } else {
            perc = ((pEarned / pPos) * 100).toFixed(2);
            var length = perc.toString().length;
            if (length > 5) {
                return false;

            }
            $("#percentage").mask('99.99');
            $('#percentage').val(perc+"%");
        }
    }
    else {
        if (isNaN(pEarned1)) {
            $('#down').val("");
            $('#percentage').val("");
        }
        else {
            $('#down').val(downpp);
        }
        return false;
    }
    var number = document.getElementById("down").value;
    number += '';
    number = number.replace(",", "");
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    document.getElementById("down").value = x1 + x2;
    document.getElementById("errordownPayment").style.display = 'none';
});
//$("#downPayment").keyup(function (event) {
//    debugger;
//    var pPos = parseInt(document.getElementById("purchasePrice").value.replace(",", ""));
//    var pEarned = parseInt($('#downPayment').val.replace(",", ""));
//    var perc = "";
//    if (isNaN(pPos) || isNaN(pEarned)) {
//        perc = " ";
//    } else {
//        perc = ((pEarned / pPos) * 100).toFixed(3);
//    }
//    $('#percentage').val(perc);
//    document.getElementById("errordownPayment").style.display = 'none';
//    //Purchase.submitRating();
//});
$("#PurZipCode").keyup(function (event) {
    document.getElementById("errorPurZipCode").style.display = 'none';
    //Purchase.submitRating();
});
$("#PurCreditScore").change(function () {
    document.getElementById("errorPurCreditScore").style.display = 'none';
    //Purchase.submitRating();
});
$("#propertyType").change(function () {
    document.getElementById("errorPropertyType").style.display = 'none';
    //Purchase.submitRating();
});

$("#homeValue").keyup(function () {
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
    //Purchase.submitRating();
});
$("#propertyUsage").change(function () {
    document.getElementById("errorPropertyUsage").style.display = 'none';
    //Purchase.submitRating();
});
$('#one').on('click', function first_click() {
    if ($("#one").hasClass('active') == false) {
        $("#one").addClass("active");
        $("#two-tab").removeClass("active");
        $("#three-tab").removeClass("active");
        $("#forth-tab").removeClass("active");
    }
    else {
        $("#one").removeClass("active");
    }
    Purchase.submitRating();
});
$('#two-tab').on('click', function first_click() {
    if ($("#two-tab").hasClass('active') == false) {
        $("#two-tab").addClass("active");
        $("#one").removeClass("active");
        $("#three-tab").removeClass("active");
        $("#forth-tab").removeClass("active");
    }
    else {
        $("#two-tab").removeClass("active");

    }
    Purchase.submitRating();
});
$('#three-tab').on('click', function first_click() {
    if ($("#three-tab").hasClass('active') == false) {
        $("#three-tab").addClass("active");
        $("#two-tab").removeClass("active");
        $("#one").removeClass("active");
        $("#forth-tab").removeClass("active");
    }
    else {
        $("#three-tab").removeClass("active");

    }
    Purchase.submitRating();
});
$('#forth-tab').on('click', function first_click() {
    if ($("#forth-tab").hasClass('active') == false) {
        $("#forth-tab").addClass("active");
        $("#two-tab").removeClass("active");
        $("#one").removeClass("active");
        $("#three-tab").removeClass("active");

    }
    else {
        $("#forth-tab").removeClass("active");

    }
    Purchase.submitRating();
});
//$('#showAlls').on('click', function first_click() {
//    paging = "yes";
//    Purchase.submitRating();
//});
$('#getResult').on('click', function first_click() {
    Purchase.ValidatePurchase();


});
$("#purchasePrice").mask('000000000000000000000');
$("#down").mask('000000000000000000000');
$("#homeValue").mask('000000000000000000000');
$("#percentage").mask('99.99');
//var options = {
//    'onKeyPress': function (cep, e, field, options) {
//        var masks = ['00000', '0000Z'];
//        mask = (cep == '0000') ? masks[1] : masks[0];
//        $('#zipcode').mask(mask, options);
//    },
//    'clearIfNotMatch': true,
//    'translation': {
//        'Z': {
//            pattern: /[1-9]/
//        }
//    }
//};

//$('#PurZipCode').mask("000999", options);
$("#PurZipCode").mask('000000');
//$("#PurZipCode").mask("000000");
Purchase.Init();

