var Communicationoptout = {
    Init: function () {


    },
}



function validateOPTOUTVal() {
    var flag = true;
    const cb1 = document.getElementById('one');
    const cb2 = document.getElementById('two');
    const cb3 = document.getElementById('three');
    const cb4 = document.getElementById('four');
    if (cb1.checked == true) {
        var address = $('#address').val().trim();
        if (address === null || address === "") {
            $('#erroraddress').text('Please enter the address.');
            document.getElementById("erroraddress").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("erroraddress").style.display = 'none';
        }
        var city = $('#city').val().trim();
        if (city === null || city === "") {
            $('#errorcity').text('Please enter the city name.');
            document.getElementById("errorcity").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorcity").style.display = 'none';
        }
        var State = $('#State').val().trim();
        if (State === null || State === "") {
            $('#errorstate').text('Please select state name.');
            document.getElementById("errorstate").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorstate").style.display = 'none';
        }
        var zipcode = $('#zipcode').val().trim();
        if (zipcode === null || zipcode === "") {
            $('#errorzipcode').text('Please enter zip code.');
            document.getElementById("errorzipcode").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorzipcode").style.display = 'none';
        }
    }
    else {
        document.getElementById("erroraddress").style.display = 'none';
        document.getElementById("errorstate").style.display = 'none';
        document.getElementById("errorzipcode").style.display = 'none';
        document.getElementById("errorcity").style.display = 'none';
    }
    if (cb2.checked == true) {
        var phonenumbercall = $('#phonenumbercall').val().trim();
        if (phonenumbercall === null || phonenumbercall === "") {
            $('#errorphonenumbercall').text('Please enter phone number.');
            document.getElementById("errorphonenumbercall").style.display = 'block';
            flag = false;
        }
        else {
            if (!validatePhoneNumber(phonenumbercall)) {
                $('#errorphonenumbercall').text('Please entervalid phone number.');
                document.getElementById("errorphonenumbercall").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorphonenumbercall").style.display = 'none';
            }

        }
    }
    else {
        document.getElementById("errorphonenumbercall").style.display = 'none';
    }
    if (cb3.checked == true) {
        var phonenumbertext = $('#phonenumbertext').val().trim();
        if (phonenumbertext === null || phonenumbertext === "") {
            $('#errorphonenumbertext').text('Please enter phone number.');
            document.getElementById("errorphonenumbertext").style.display = 'block';
            flag = false;
        }
        else {
            if (!validatePhoneNumber(phonenumbertext)) {
                $('#errorphonenumbertext').text('Please entervalid phone number.');
                document.getElementById("errorphonenumbertext").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorphonenumbertext").style.display = 'none';
            }
        }
    }
    else {
        document.getElementById("errorphonenumbertext").style.display = 'none';
    }
    if (cb4.checked == true) {
        var emailaddress = $('#emailaddress').val().trim();
        if (emailaddress === null || emailaddress === "") {
            $('#erroremailaddress').text('Please enter email address.');
            document.getElementById("erroremailaddress").style.display = 'block';
            flag = false;
        }
        else {

            if (!validateEmail(emailaddress)) {
                $('#erroremailaddress').text('Please enter valid email.');
                document.getElementById("erroremailaddress").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("erroremailaddress").style.display = 'none';
            }

        }
    }
    else {
        document.getElementById("erroremailaddress").style.display = 'none';
    }
    var Slct = $('#Slct').val().trim();
    if (Slct === null || Slct === "") {
        $('#errorSlct').text('Please select one why you’re opting out.');
        document.getElementById("errorSlct").style.display = 'block';
        flag = false;
    }
    else {

        document.getElementById("errorSlct").style.display = 'none';

    }


    return flag;
}
function validatePhoneNumber(input_str) {
    var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

    return re.test(input_str);
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
function ValidateContactMessage() {
    var flag = true;
    var MessageToFreddy = $('#MessageToFreddy').val().trim();
    if (MessageToFreddy === null || MessageToFreddy === "") {
        $('#errorMessageToFreddy').text('Please enter the message.');
        document.getElementById("errorMessageToFreddy").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorMessageToFreddy").style.display = 'none';
    }

    return flag;
}
function ValidateContactReview() {
    var flag = true;
    var reviewContactName = $('#reviewContactName').val().trim();
    if (reviewContactName === null || reviewContactName === "") {
        $('#errorreviewContactName').text('Please enter the name');
        document.getElementById("errorreviewContactName").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorreviewContactName").style.display = 'none';
    }
    var reviewPhoneNumber = $('#reviewPhoneNumber').val().trim();
    if (reviewPhoneNumber === null || reviewPhoneNumber === "") {
        $('#errorreviewreviewPhoneNumber').text('Please enter the phone number');
        document.getElementById("errorreviewreviewPhoneNumber").style.display = 'block';
        flag = false;
    }
    else {
        if (!validatePhoneNumber(reviewPhoneNumber)) {
            $('#errorreviewreviewPhoneNumber').text('Please enter valid phone number.');
            document.getElementById("errorreviewreviewPhoneNumber").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorreviewreviewPhoneNumber").style.display = 'none';
        }
    }
    var reviewEmail = $('#reviewEmail').val().trim();
    if (reviewEmail === null || reviewEmail === "") {
        $('#errorreviewEmail').text('Please enter the email');
        document.getElementById("errorreviewEmail").style.display = 'block';
        flag = false;
    }
    else {
        if (!validateEmail(reviewEmail)) {
            $('#errorreviewEmail').text('Please enter valid email.');
            document.getElementById("errorreviewEmail").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorreviewEmail").style.display = 'none';
        }
    }
    var reviewMessage = $('#reviewMessage').val().trim();
    if (reviewMessage === null || reviewMessage === "") {
        $('#errorreviewMessage').text('Please enter the message');
        document.getElementById("errorreviewMessage").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorreviewMessage").style.display = 'none';
    }
    return flag;
}
$("#emailaddress").focusout(function (event) {
    var emailaddress = $('#emailaddress').val().trim();
    if (emailaddress === null || emailaddress === "") {
        $('#erroremailaddress').text('Please enter email address.');
        document.getElementById("erroremailaddress").style.display = 'block';
        flag = false;
    }
    else {

        if (!validateEmail(emailaddress)) {
            $('#erroremailaddress').text('Please enter valid email.');
            document.getElementById("erroremailaddress").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("erroremailaddress").style.display = 'none';
        }

    }
});
$("#phonenumbercall").focusout(function (event) {
    var phonenumbercall = $('#phonenumbercall').val().trim();
    if (phonenumbercall === null || phonenumbercall === "") {
        $('#errorphonenumbercall').text('Please enter phone number.');
        document.getElementById("errorphonenumbercall").style.display = 'block';
        flag = false;
    }
    else {
        if (!validatePhoneNumber(phonenumbercall)) {
            $('#errorphonenumbercall').text('Please entervalid phone number.');
            document.getElementById("errorphonenumbercall").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorphonenumbercall").style.display = 'none';
        }

    }
});
$("#address").focusout(function (event) {
    document.getElementById("erroraddress").style.display = 'none';
});
$("#city").focusout(function (event) {
    document.getElementById("errorcity").style.display = 'none';
});
$("#State").focusout(function (event) {
    document.getElementById("errorstate").style.display = 'none';
});
$("#zipcode").focusout(function (event) {
    document.getElementById("errorzipcode").style.display = 'none';
});
$("#phonenumbertext").focusout(function (event) {
    var phonenumbertext = $('#phonenumbertext').val().trim();
    if (phonenumbertext === null || phonenumbertext === "") {
        $('#errorphonenumbertext').text('Please enter phone number.');
        document.getElementById("errorphonenumbertext").style.display = 'block';
        flag = false;
    }
    else {
        if (!validatePhoneNumber(phonenumbertext)) {
            $('#errorphonenumbertext').text('Please entervalid phone number.');
            document.getElementById("errorphonenumbertext").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorphonenumbertext").style.display = 'none';
        }
    }
});
$("#reviewMessage").focusout(function (event) {
    document.getElementById("errorreviewMessage").style.display = 'none';
});
$("#reviewPhoneNumber").focusout(function (event) {
    var reviewPhoneNumber = $('#reviewPhoneNumber').val().trim();
    if (reviewPhoneNumber === null || reviewPhoneNumber === "") {
        $('#errorreviewreviewPhoneNumber').text('Please enter the phone number');
        document.getElementById("errorreviewreviewPhoneNumber").style.display = 'block';

    }
    else {
        if (!validatePhoneNumber(reviewPhoneNumber)) {
            $('#errorreviewreviewPhoneNumber').text('Please enter valid phone number.');
            document.getElementById("errorreviewreviewPhoneNumber").style.display = 'block';

        }
        else {
            document.getElementById("errorreviewreviewPhoneNumber").style.display = 'none';
        }
    }
});
$("#reviewContactName").focusout(function (event) {
    document.getElementById("errorreviewContactName").style.display = 'none';
});
$("#MessageToFreddy").focusout(function (event) {
    document.getElementById("errorMessageToFreddy").style.display = 'none';
});
$("#reviewPhoneNumber").inputmask('999-999-9999');
$("#phonenumbercall").inputmask('999-999-9999');
$("#phonenumbertext").inputmask('999-999-9999');
Communicationoptout.Init();