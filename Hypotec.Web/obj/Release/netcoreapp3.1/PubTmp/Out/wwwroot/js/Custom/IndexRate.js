var IndexRate = {

    Init: function () {

        IndexRate.submitRating();
        window.scrollTo(0, 0);
    },
    submitRating: function () {
        //document.getElementById("resultTabBtn").style.display = 'none';
        var searchRateDto = {};
        searchRateDto.all = "Yes"
        searchRateDto.loanPurpose = 0;
        searchRateDto.zipCode = "94123"
        searchRateDto.firstTimeBuyer = "1";
        searchRateDto.creditScore = "720"
        searchRateDto.propertyType = 0;
        searchRateDto.propertyUsage = 0;
        searchRateDto.purchasePrice = "500,000";
        searchRateDto.downPayment = "100,000";
        searchRateDto.showAll = paging;
        searchRateDto.indaxRate = "IndexPage";
        //$("#divLoader").show();

        jQuery.ajax({
            type: 'POST',
            async: true,
            url: '/Home/IndexRate',
            data: {
                searchRateDto: searchRateDto,
            },
            success: function (data) {
                if (data.length == 0) {
                    $('#rateDetails1').html('');
                }
                else {
                    $('#rateDetails1').html('');
                    var str = '';
                    var j = 0;
                    str = str + '<ul class="p-0 m-0">';
                    for (var i = 0; i < data.length; i++) {
                        if (paging == 'three') {
                            j++;
                            if (j < 10) {
                                str = str + '<li style="725px" class="d-flex flex-column flex-md-row justify-content-between align-items-center">';
                                //str = str + '<div class="lenderImg"><img src="~/images/lender-img.svg"" /></div>';
                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].yearlyDesc;
                                str = str + '</h4></div>';
                                str = str + '<div class="lenderContent"><h4>';
                                str = str + data[i].rate + ' / ' + data[i].apr;
                                str = str + '</h4><p>Estimated Rate/APR</p></div>';

                                //str = str + '<div class="lenderContent"><h4>';
                                //str = str + data[i].monthlyCost + '/mo';
                                //str = str + '</h4><p>Monthly Payment</p></div>';

                                //str = str + '<div class="lenderContent"><h4>';
                                //str = str + '$0.00';
                                //str = str + '</h4><p>Points</p></div>';

                                str = str + '</li>';

                            }
                        }

                    }
                    str = str + '</ul>';
                    $('#rateDetails1').html(str);
                }
            },
        });
    },
}
var paging = "three";
$(".eyeShowHides").click(function () {

    $(this).toggleClass("fas fa-eye");
    var input = $("[id='" + $(this).attr("toggle") + "']");

    if (input.attr("type") === "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
})
$('.testimonial-section ').owlCarousel({
    loop: true,
    margin: 10,
    nav: false,
    dots: true,
    autoplay: true,
    autoplayTimeout: 20000,
    responsiveClass: true,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 1
        },
        1000: {
            items: 1
        }
    }
})
IndexRate.Init();

