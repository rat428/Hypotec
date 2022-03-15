var Started = {
    Init: function () {
        document.getElementById("div1").style.display = 'none';
        document.getElementById("div2").style.display = 'none';
        document.getElementById("div3").style.display = 'none';
        document.getElementById("div4").style.display = 'none';
        document.getElementById("divpagesecond").style.display = 'none';


    },

}
function scrollToTop() {
    // Scroll to top logic
    window.scrollTo(0, 0);
}
const cb1 = document.getElementById('exampleRadios1');
const cb2 = document.getElementById('exampleRadios2');
const cb3 = document.getElementById('exampleRadios3');
const cb4 = document.getElementById('exampleRadios4');
const cb5 = document.getElementById('exampleRadios5');
const cb6 = document.getElementById('exampleRadios6');
const cb7 = document.getElementById('exampleRadios7');
const cb8 = document.getElementById('exampleRadios8');
const cb9 = document.getElementById('exampleRadios9');
const cb10 = document.getElementById('exampleRadios10');
const cb11 = document.getElementById('exampleRadios11');
const cb12 = document.getElementById('exampleRadios12');
const cb13 = document.getElementById('exampleRadios13');
const cb14 = document.getElementById('exampleRadios14');
const cb15 = document.getElementById('exampleRadios15');
const cb16 = document.getElementById('exampleRadios16');
const cb17 = document.getElementById('exampleRadios17');
const cb18 = document.getElementById('exampleRadios18');
$("#zipcode").keyup(function (event) {
    if (cb1.checked == true || cb2.checked == true || cb3.checked == true) {
        if (cb5.checked == true || cb6.checked == true || cb7.checked == true || cb8.checked == true) {
            if (cb9.checked == true || cb10.checked == true || cb11.checked == true || cb12.checked == true) {
                if ($('#zipcode').val() != "") {
                    $('#btnNext').prop("disabled", false);
                }

            }
        }
    }
})
$("#propaddress").keyup(function (event) {
    if (cb4.checked == true) {
        if ($('#propaddress').val() != "") {
            $('#btnNext').prop("disabled", false);
        }

    }
})
$(document).on("change", ".form-check-input1", function () {

    if (cb1.checked == true || cb2.checked == true || cb3.checked == true) {
        document.getElementById("div1").style.display = 'block';
        document.getElementById("div2").style.display = 'block';
        document.getElementById("div3").style.display = 'block';
    }
    else {
        document.getElementById("div1").style.display = 'none';
        document.getElementById("div2").style.display = 'none';
        document.getElementById("div3").style.display = 'none';
    }
    if (cb4.checked == true) {
        document.getElementById("div4").style.display = 'block';
    }
    else {
        document.getElementById("div4").style.display = 'none';
    }
    if (cb1.checked == true || cb2.checked == true || cb3.checked == true) {
        if (cb5.checked == true || cb6.checked == true || cb7.checked == true || cb8.checked == true) {
            if (cb9.checked == true || cb10.checked == true || cb11.checked == true || cb12.checked == true) {

                $('#btnNext').prop("disabled", false);


            }
        }
    }
    if (cb4.checked == true) {
        if ($('#address').val().trim() != "") {
            $('#btnNext').prop("disabled", false);
        }

    }
    if (cb13.checked == true || cb14.checked == true || cb15.checked == true) {
        if (cb16.checked == true || cb17.checked == true || cb18.checked == true) {
            $('#btnNextPage').prop("disabled", false);
        }
    }
    // Here you will get the current selected/checked radio option value
})

function NextPage() {
    document.getElementById("divpagefirst").style.display = 'none';
    document.getElementById("divpagesecond").style.display = 'block';
    scrollToTop();

}
function PreviousPage() {
    document.getElementById("divpagefirst").style.display = 'block';
    document.getElementById("divpagesecond").style.display = 'none';
    scrollToTop();
}
Started.Init();

