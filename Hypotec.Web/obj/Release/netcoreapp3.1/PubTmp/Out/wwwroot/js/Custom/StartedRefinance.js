var StartedRefinance = {
    Init: function () {
        document.getElementById("div1r").style.display = 'none';
        document.getElementById("divpagesecondR").style.display = 'none';
        document.getElementById("openProp").style.display = 'none';
        document.getElementById("divaptunit").style.display = 'none';
        document.getElementById("divpagesecondThird").style.display = 'none';



    },

}
const cb19 = document.getElementById('exampleRadios19');
const cb20 = document.getElementById('exampleRadios20');
const cb21 = document.getElementById('exampleRadios21');
const cb22 = document.getElementById('exampleRadios22');
const cb23 = document.getElementById('exampleRadios23');

const cb24 = document.getElementById('exampleRadios24');
const cb25 = document.getElementById('exampleRadios25');
const cb26 = document.getElementById('exampleRadios26');

const cb27 = document.getElementById('exampleRadios27');
const cb28 = document.getElementById('exampleRadios28');
const cb29 = document.getElementById('exampleRadios29');
const cb30 = document.getElementById('exampleRadios30');
$("#explainWord").keyup(function (event) {
    if (cb23.checked == true) {
        if ($('#explainWord').val().trim() != "") {
            $('#btnNextR').prop("disabled", false);
        }
    }

})
$("#addPropertyRateAdd").keyup(function (event) {

    if ($('#addPropertyRateAdd').val().trim() != "") {
        $('#btnNextPager2').prop("disabled", false);
        document.getElementById("openProp").style.display = 'block';
        document.getElementById("divaptunit").style.display = 'block';
        if ($('#City').val().trim() != "") {
            if ($('#State').val().trim() != "") {
                if ($('#zipcode').val().trim() != "") {
                    $('#btnNextPager2').prop("disabled", false);
                }
            }
        }
    }

})
$("#addPropertyRateAdd").focusout(function (event) {
    if ($('#addPropertyRateAdd').val().trim() != "") {
        if ($('#City').val().trim() != "") {
            if ($('#State').val().trim() != "") {
                if ($('#zipcode').val().trim() != "") {
                    $('#btnNextPager2').prop("disabled", false);
                }
            }
        }
    }
});
$("#City").keyup(function (event) {

    if ($('#addPropertyRateAdd').val().trim() != "") {
        if ($('#City').val().trim() != "") {
            if ($('#State').val().trim() != "") {
                if ($('#zipcode').val().trim() != "") {
                    $('#btnNextPager2').prop("disabled", false);
                }
            }
        }
    }

})
$("#State").keyup(function (event) {

    if ($('#addPropertyRateAdd').val().trim() != "") {
        if ($('#City').val().trim() != "") {
            if ($('#State').val().trim() != "") {
                if ($('#zipcode').val().trim() != "") {
                    $('#btnNextPager2').prop("disabled", false);
                }
            }
        }
    }

})
$("#zipcode").keyup(function (event) {

    if ($('#addPropertyRateAdd').val().trim() != "") {
        if ($('#City').val().trim() != "") {
            if ($('#State').val().trim() != "") {
                if ($('#zipcode').val().trim() != "") {
                    $('#btnNextPager2').prop("disabled", false);
                }
            }
        }
    }

})

//$("#propaddress").keyup(function (event) {
//    if (cb23.checked == true) {
//        if ($('#propaddress').val().trim() != "") {
//            $('#btnNextR').prop("disabled", false);
//        }

//    }
//})
$(document).on("change", ".form-check-input", function () {

    if (cb19.checked == true || cb20.checked == true || cb21.checked == true || cb22.checked == true) {
        $('#btnNextR').prop("disabled", false);
    }

    if (cb23.checked == true) {
        document.getElementById("div1r").style.display = 'block';
        if ($('#propaddress').val().trim() != "") {
            $('#btnNextR').prop("disabled", false);
        }
    }
    else {
        document.getElementById("div1r").style.display = 'none';
    }

    if (cb26.checked == true || cb24.checked == true || cb25.checked == true) {
        if (cb30.checked == true || cb27.checked == true || cb28.checked == true || cb29.checked == true) {
            $('#btnNextPageToRefinance').prop("disabled", false);
        }
    }
    // Here you will get the current selected/checked radio option value
})
function scrollToTop() {
    // Scroll to top logic
    window.scrollTo(0, 0);
}
function NextPageR() {
    document.getElementById("divpagefirstr").style.display = 'none';
    document.getElementById("divpagesecondR").style.display = 'block';
    document.getElementById("divpagesecondThird").style.display = 'none';
    scrollToTop();

}
function NextPager2() {
    document.getElementById("divpagefirstr").style.display = 'none';
    document.getElementById("divpagesecondR").style.display = 'none';
    document.getElementById("divpagesecondThird").style.display = 'block';
    scrollToTop()

}
function PreviousPage1() {
    document.getElementById("divpagefirstr").style.display = 'block';
    document.getElementById("divpagesecondR").style.display = 'none';
    document.getElementById("divpagesecondThird").style.display = 'none';
    scrollToTop()

}
function PreviousPage2() {
    document.getElementById("divpagefirstr").style.display = 'none';
    document.getElementById("divpagesecondR").style.display = 'block';
    document.getElementById("divpagesecondThird").style.display = 'none';
    scrollToTop()

}
StartedRefinance.Init();

