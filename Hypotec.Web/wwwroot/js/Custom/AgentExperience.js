var AgentExperience = {
    Init: function () {

    },
  
    SendMailToSlot: function () {
        var slotBookModel = {};
        slotBookModel.Id = slotid;
        slotBookModel.Name = $("#name").val();
        slotBookModel.Time = $("#lblSlot").text();
        slotBookModel.Phone = $("#phone").val();
        slotBookModel.Email = $("#email").val();
        jQuery.ajax({
            type: 'POST',
            async: true,
            url: '/FindAndExpert/AgentSlotBooking',
            data: {
                slotBookModel: slotBookModel,
            },
            success: function (data) {
                window.location.href = data.redirect;
            }
        });
    },

    SendMailAgent: function () {
        var slotBookModel = {};
        slotBookModel.Name = $("#name").val();
        slotBookModel.Time = $("#lblSlot").text();
        slotBookModel.Phone = $("#phone").val();
        slotBookModel.Email = $("#email").val();
        slotBookModel.AreasCovered = $("#areasCovered").val();
        slotBookModel.TellUs = $("#tellUs").val();
        jQuery.ajax({
            type: 'POST',
            async: false,
            url: '/AgentExperience/AgentExperience',
            data: {
                slotBookModel: slotBookModel,
            },
            success: function (data) {

                location = data.redirect;
            }
        });
    },
    ValidateAgentExperience: function () {
        var flag = true;
        var name = $('#name').val().trim();
        if (name === null || name === "") {
            $('#errorName').text('Please enter the name.');
            document.getElementById("errorName").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorName").style.display = 'none';
        }

        var phone = $('#phone').val().trim();
        if (phone === null || phone === "") {
            $('#errorPhone').text('Please enter the phone number.');
            document.getElementById("errorPhone").style.display = 'block';
            flag = false;
        }
        else {

            if (!validateNumber(phone)) {
                $('#errorPhone').text('Please enter the valid phone number.');
                document.getElementById("errorPhone").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorPhone").style.display = 'none';
            }

        }
        var email = $('#email').val().trim();
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
        var areasCovered = $('#areasCovered').val().trim();
        if (areasCovered === null || areasCovered === "") {
            $('#errorAreasCovered').text('Please enter the areas covered.');
            document.getElementById("errorAreasCovered").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorAreasCovered").style.display = 'none';
        }
        var tellUs = $('#tellUs').val().trim();
        if (tellUs === null || tellUs === "") {
            $('#errorTellUs').text('Please enter the areas covered.');
            document.getElementById("errorTellUs").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorTellUs").style.display = 'none';
        }
        if (flag) {
            AgentExperience.SendMailAgent();
        }
    },
    ValidateSendMailToSlot: function () {
        var flag = true;
        var name = $('#name').val().trim();
        if (name === null || name === "") {
            $('#errorName').text('Please enter the name.');
            document.getElementById("errorName").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorName").style.display = 'none';
        }

        var phone = $('#phone').val().trim();
        if (phone === null || phone === "") {
            $('#errorPhone').text('Please enter the phone number.');
            document.getElementById("errorPhone").style.display = 'block';
            flag = false;
        }
        else {

            if (!validateNumber(phone)) {
                $('#errorPhone').text('Please enter the valid phone number.');
                document.getElementById("errorPhone").style.display = 'block';
                flag = false;
            }
            else {
                document.getElementById("errorPhone").style.display = 'none';
            }

        }
        var email = $('#email').val().trim();
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

        if (flag) {
            AgentExperience.SendMailToSlot();
        }
    },
}
$(document).ready(function () {
    $("#phone").inputmask('999-999-9999');
});
function validateNumber(input_str) {
    var re = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;

    return re.test(input_str);
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
$("#name").focusout(function (event) {
    document.getElementById("errorName").style.display = 'none';
});
$("#email").focusout(function (event) {
    var email = $('#email').val().trim();
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
});
$("#phone").focusout(function (event) {
    var phone = $('#phone').val().trim();
    if (phone === null || phone === "") {
        $('#errorPhone').text('Please enter the phone number.');
        document.getElementById("errorPhone").style.display = 'block';
        flag = false;
    }
    else {
        if (!validateNumber(phone)) {
            $('#errorPhone').text('Please enter the valid phone number.');
            document.getElementById("errorPhone").style.display = 'block';
            flag = false;
        }
        else {
            document.getElementById("errorPhone").style.display = 'none';
        }

    }
});
$("#areasCovered").focusout(function (event) {
    document.getElementById("errorAreasCovered").style.display = 'none';
});
$("#tellUs").focusout(function (event) {
    document.getElementById("errorTellUs").style.display = 'none';
});

var slotid = "";
function setslotvalue(slot) {

    var slotValue = $(slot).val();
    var dayName = $(slot).attr("dayname");

    slotid = $(slot).attr("hdnSlotId");

    var slotDate = $('#' + dayName).text();

    $('#hdnSlot').val(dayName + " " + slotValue);
    $('#lblSlot').text(slotDate + " " + dayName + " " + slotValue);
    $('#btnslotnext').prop("disabled", false);
}
function SendMailtest() {

    AgentExperience.ValidateSendMailToSlot();
}
function SendMailAgengtEx1233() {
    AgentExperience.ValidateAgentExperience();
}
//AgentExperience.Init();

