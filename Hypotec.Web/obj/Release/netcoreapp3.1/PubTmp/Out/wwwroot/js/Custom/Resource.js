var Resource = {
    Init: function () {

    },
}

$(document).ready(function () {
    $('#Description').summernote();
    $('#Header').summernote();
    $('#serachArticle').keyup();
});

$("#serachArticle").keyup(function (event) {

    var searchArticleModel = {};
    searchArticleModel.Description = $("#serachArticle").val();
    searchArticleModel.Tag = serachforcount

    //$("#divLoader").show();
    jQuery.ajax({
        type: 'POST',
        async: true,
        url: '/Resource/ResourceList',
        data: {
            searchArticleModel: searchArticleModel,
        },
        success: function (data) {
            if (data.length == 0) {
                $('#divResourceList').html('');
            }
            else {
                $('#divResourceList').html(data);
            }
        }
    });
});
var serachforcount = "All";
$("#myid li").click(function () {

    serachforcount = $(this).text();
    var searchArticleModel = {};
    searchArticleModel.Description = $("#serachArticle").val();
    searchArticleModel.Tag = serachforcount

    //$("#divLoader").show();
    jQuery.ajax({
        type: 'POST',
        async: true,
        url: '/Resource/ResourceList',
        data: {
            searchArticleModel: searchArticleModel,
        },
        success: function (data) {
            if (data.length == 0) {
                $('#divResourceList').html('');
            }
            else {
                $('#divResourceList').html(data);
            }
        }
    });
});

$("#Header").focusout(function (event) {
    document.getElementById("errorheader").style.display = 'none';
});
$("#Description").focusout(function (event) {
    document.getElementById("errorDescription").style.display = 'none';
});
$("#image").focusout(function (event) {
    document.getElementById("errorimage").style.display = 'none';
});

function Checkfiles() {
    var fup = fileName;
    var ext = fup.split('.').pop();
    if (ext == "jpg" || ext == "png" || ext == "jpeg") {
        return true;
    } else {
        return false;
    }
}
function validateResorceVal() {
    var flag = true;
    var Header = $('#Header').val().trim();
    if (Header === null || Header === "") {
        $('#errorheader').text('Please enter the header details.');
        document.getElementById("errorheader").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorheader").style.display = 'none';
    }
    var tag = $('#tag').val().trim();
    if (tag === null || tag === "Please Select Option") {
        $('#errortag').text('Please select one.');
        document.getElementById("errortag").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errortag").style.display = 'none';
    }
    var Description = $('#Description').val().trim();
    if (Description === null || Description === "") {
        $('#errorDescription').text('Please enter the description details.');
        document.getElementById("errorDescription").style.display = 'block';
        flag = false;
    }
    else {
        document.getElementById("errorDescription").style.display = 'none';
    }
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
    return flag;
}
var fileName = "";
$(".custom-file-input").on("change", function () {
    fileName = $(this).val().split("\\").pop();
    $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
});


