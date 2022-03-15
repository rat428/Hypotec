var Advisor = {
    Init: function () {
        initialize();
    },
}

$(document).ready(function () {
    //SerachAdvisor();
    $('#Subjects_dropdown').multiselect({
        includeSelectAllOption: true,
    });
    $("#Call").inputmask('999-999-9999');
    $("#Text").inputmask('999-999-9999');
    $("#Office").inputmask('999-999-9999');
    $("#Ext").inputmask('9999');
});
//function SerachAdvisor() {

//    var serachAdvisor = $("#hdnShortStateName").val() + "," + $("#hdnLongStateName").val() + "," + $("#serachArea").val();
//    var longitude = $("#hdnlongitude").val();
//    var latitude = $("#hdnlatitude").val();
//    //var shortStateName = $("#hdnShortStateName").val();
//    var radius = $("#radius").val();
//    var result = $("#result").val();
//    //$("#divLoader").show();
//    jQuery.ajax({
//        type: 'POST',
//        async: true,
//        url: '/Advisor/AdvisorList',
//        data: {
//            serachAdvisor: serachAdvisor,
//            longitude: longitude,
//            latitude: latitude,
//            radius: radius,
//            recordCount: result
//        },
//        success: function (data) {
//            if (data.length == 0) {
//                $('#divAdvisorContent').html('');
//            }
//            else {
//                $('#divAdvisorContent').html(data);
//            }
//        }
//    });
//}

function ValidateAdvisor() {
    var flag = true;
    var Name = $('#UserName').val().trim();
    if (Name === null || Name === "") {
        $('#errorUserName').text('Please enter the name.');
        document.getElementById("errorUserName").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorName").style.display = 'none';
    }
    var RegistrationNumber = $('#RegistrationNumber').val().trim();
    if (RegistrationNumber === null || RegistrationNumber === "") {
        $('#errorRegistrationNumber').text('Please enter registration number.');
        document.getElementById("errorRegistrationNumber").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorRegistrationNumber").style.display = 'none';
    }
    var Ext = $('#Ext').val().trim();
    if (Ext === null || Ext === "") {
        $('#errorExt').text('Please enter ext. number.');
        document.getElementById("errorExt").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorExt").style.display = 'none';
    }
    var LicenseState = $("#Subjects_dropdown").val();
    if (LicenseState.length == 0) {
        $('#errorLicenseState').text('Please enter the license state.');
        document.getElementById("errorLicenseState").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorLicenseState").style.display = 'none';
    }
    //var Address = $('#address').val().trim();
    //if (Address === null || Address === "") {
    //    $('#errorAddress').text('Please enter the Address.');
    //    document.getElementById("errorAddress").style.display = 'block';
    //    flag = false;
    //}
    //else {
    //    document.getElementById("errorAddress").style.display = 'none';
    //}
    var About = $('#About').val().trim();
    if (About === null || About === "") {
        $('#errorAbout').text('Please enter the about.');
        document.getElementById("errorAbout").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorAbout").style.display = 'none';
    }
    var email = $('#Email').val().trim();
    if (email === null || email === "") {

        $('#errorEmail').text('Please enter the email.');
        document.getElementById("errorEmail").style.display = 'block';
        flag = false;
    }
    else {
        if (!validateEmail(email)) {
            $('#errorEmail').text('Please enter the valid email.');
            document.getElementById("errorEmail").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorEmail").style.display = 'none';

        }

    }
    var Office = $('#Office').val().trim();
    if (Office === null || Office === "") {
        $('#errorOffice').text('Please enter the office number.');
        document.getElementById("errorOffice").style.display = 'block';
        flag = false;
    }
    else {

        if (!validatePhoneNumber(Office)) {
            $('#errorOffice').text('Please enter the valid office number.');
            document.getElementById("errorOffice").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorOffice").style.display = 'none';
        }

    }
    var Text = $('#Text').val().trim();
    if (Text === null || Text === "") {
        $('#errorText').text('Please enter the text number.');
        document.getElementById("errorText").style.display = 'block';
        flag = false;
    }
    else {

        if (!validatePhoneNumber(Text)) {
            $('#errorPhone').text('Please enter the valid office number.');
            document.getElementById("errorText").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorText").style.display = 'none';
        }

    }
    var Call = $('#Call').val().trim();
    if (Call === null || Call === "") {
        $('#errorCall').text('Please enter the call number.');
        document.getElementById("errorCall").style.display = 'block';
        flag = false;
    }
    else {

        if (!validatePhoneNumber(Text)) {
            $('#errorCall').text('Please enter the valid call number.');
            document.getElementById("errorCall").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorCall").style.display = 'none';
        }

    }
    var iadvisoridmage = $('#advisorid').val().trim();
    if (iadvisoridmage === null || iadvisoridmage === "") {
        var image = $('#image').val().trim();
        if (image === null || image === "") {
            $('#errorimage').text('Please attache a image.');
            document.getElementById("errorimage").style.display = 'block';
            flag = false;
        }
        else {
            if (!Checkfiles()) {
                $('#errorimage').text('Please attach image only(jpg,png,jpeg).');
                document.getElementById("errorimage").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorimage").style.display = 'none';
            }
        }
    }
    return flag;
}
function validatePhoneNumber(number) {
    var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

    return re.test(number);
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
function Checkfiles() {
    var fup = fileName;
    var ext = fup.split('.').pop();
    if (ext == "jpg" || ext == "png" || ext == "jpeg") {
        return true;
    } else {
        return false;
    }
}
$("#Name").focusout(function (event) {
    document.getElementById("errorName").style.display = 'none';
});
$("#Email").focusout(function (event) {
    var email = $('#Email').val().trim();
    if (email === null || email === "") {

        $('#errorEmail').text('Please enter the email.');
        document.getElementById("errorEmail").style.display = 'block';

    }
    else {
        if (!validateEmail(email)) {
            $('#errorEmail').text('Please enter the valid email.');
            document.getElementById("errorEmail").style.display = 'block';

        }
        else {
            document.getElementById("errorEmail").style.display = 'none';

        }

    }
});
$("#Call").focusout(function (event) {
    var Call = $('#Call').val().trim();
    if (Call === null || Call === "") {
        $('#errorCall').text('Please enter the call number.');
        document.getElementById("errorCall").style.display = 'block';

    }
    else {

        if (!validatePhoneNumber(Call)) {
            $('#errorCall').text('Please enter the valid call number.');
            document.getElementById("errorCall").style.display = 'block';

        }
        else {
            document.getElementById("errorCall").style.display = 'none';
        }

    }
});
$("#Text").focusout(function (event) {
    var Text = $('#Text').val().trim();
    if (Text === null || Text === "") {
        $('#errorText').text('Please enter the text number.');
        document.getElementById("errorText").style.display = 'block';

    }
    else {

        if (!validatePhoneNumber(Text)) {
            $('#errorPhone').text('Please enter the valid office number.');
            document.getElementById("errorText").style.display = 'block';

        }
        else {
            document.getElementById("errorText").style.display = 'none';
        }

    }
});
$("#Office").focusout(function (event) {
    var Office = $('#Office').val().trim();
    if (Office === null || Office === "") {
        $('#errorOffice').text('Please enter the office number.');
        document.getElementById("errorOffice").style.display = 'block';

    }
    else {

        if (!validatePhoneNumber(Office)) {
            $('#errorPhone').text('Please enter the valid office number.');
            document.getElementById("errorOffice").style.display = 'block';
        }
        else {
            document.getElementById("errorOffice").style.display = 'none';
        }

    }
});
$("#About").focusout(function (event) {
    document.getElementById("errorAbout").style.display = 'none';
});
//$("#address").focusout(function (event) {
//    document.getElementById("errorAddress").style.display = 'none';
//});
$("#Ext").focusout(function (event) {
    document.getElementById("errorExt").style.display = 'none';
});
$("#RegistrationNumber").focusout(function (event) {
    document.getElementById("errorRegistrationNumber").style.display = 'none';
});
$("#LicenseState").focusout(function (event) {
    document.getElementById("errorLicenseState").style.display = 'none';
});
$("#image").focusout(function (event) {
    document.getElementById("errorImage").style.display = 'none';
});
var fileName = "";
$(".custom-file-input").on("change", function () {
    fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});





