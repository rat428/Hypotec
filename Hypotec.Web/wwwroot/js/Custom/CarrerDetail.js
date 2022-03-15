var CarrerDetail = {

    Init: function () {

    },
}
function validatePhoneNumber(input_str) {
    var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

    return re.test(input_str);
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
$("#firstName").focusout(function (event) {
    document.getElementById("errorfirstName").style.display = 'none';
});
$("#email").focusout(function (event) {
    var email = $('#email').val().trim();
    if (email === null || email === "") {

        $('#erroremail').text('Please enter the email.');
        document.getElementById("erroremail").style.display = 'block';
    }
    else {
        if (!validateEmail(email)) {
            $('#erroremail').text('Please enter the valid email.');
            document.getElementById("erroremail").style.display = 'block';
        }
        else {
            document.getElementById("erroremail").style.display = 'none';

        }

    }
});
$("#lastName").focusout(function (event) {
    document.getElementById("errorlastName").style.display = 'none';
});
$("#primaryPhone").focusout(function (event) {
    var primaryPhone = $('#primaryPhone').val().trim();
    if (primaryPhone === null || primaryPhone === "") {
        $('#errorprimaryPhone').text('Please enter the primary phone.');
        document.getElementById("errorprimaryPhone").style.display = 'block';
    }
    else {
        if (!validatePhoneNumber(primaryPhone)) {
            $('#errorPhone').text('Please enter the valid primary phone.');
            document.getElementById("errorprimaryPhone").style.display = 'block';
        }
        else {
            document.getElementById("errorprimaryPhone").style.display = 'none';
        }

    }
});
$("#Attachment").focusout(function (event) {
    document.getElementById("errorcustomFile").style.display = 'none';
});
var fileName = "";
$(".custom-file-input").on("change", function () {
    fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});
$("#primaryPhone").inputmask('999-999-9999');
function Checkfiles() {
    var fup = fileName;
    var ext = fup.split('.').pop();
    if (ext == "pdf" /*|| ext == "docx" || ext == "doc"*/) {
        return true;
    } else {
        return false;
    }
}
function validateDetails() {
    var flag = true;
    var firstName = $('#firstName').val().trim();
    if (firstName === null || firstName === "") {
        $('#errorfirstName').text('Please enter the first name.');
        document.getElementById("errorfirstName").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorfirstName").style.display = 'none';
    }

    var primaryPhone = $('#primaryPhone').val().trim();
    if (primaryPhone === null || primaryPhone === "") {
        $('#errorprimaryPhone').text('Please enter the primary phone.');
        document.getElementById("errorprimaryPhone").style.display = 'block';
        flag = false;
    }
    else {

        if (!validatePhoneNumber(primaryPhone)) {
            $('#errorprimaryPhone').text('Please enter the valid primary phone.');
            document.getElementById("errorprimaryPhone").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorprimaryPhone").style.display = 'none';
        }

    }
    var email = $('#email').val().trim();
    if (email === null || email === "") {

        $('#erroremail').text('Please enter the email.');
        document.getElementById("erroremail").style.display = 'block';
        flag = false;
    }
    else {
        if (!validateEmail(email)) {
            $('#erroremail').text('Please enter the valid email.');
            document.getElementById("erroremail").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("erroremail").style.display = 'none';

        }

    }
    var lastName = $('#lastName').val().trim();
    if (lastName === null || lastName === "") {
        $('#errorlastName').text('Please enter the last name.');
        document.getElementById("errorlastName").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorlastName").style.display = 'none';
    }
    var customFile = $('#Attachment').val().trim();
    if (customFile === null || customFile === "") {
        $('#errorcustomFile').text('Please attache a file.');
        document.getElementById("errorcustomFile").style.display = 'block';
        flag = false;
    }
    else {
        if (!Checkfiles()) {
            $('#errorcustomFile').text('Please upload only pdf file.');
            document.getElementById("errorcustomFile").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorcustomFile").style.display = 'none';
        }
    }
    return flag;
}
CarrerDetail.Init();

