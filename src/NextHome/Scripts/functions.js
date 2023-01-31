// ------------------------------------------------------------------------------ //
// Global Variables
// ------------------------------------------------------------------------------ //
var domainUrl = 'https://nexthome-egy.com';

// ------------------------------------------------------------------------------ //
// Toggle Search
// ------------------------------------------------------------------------------ //

$(".input-group-addon.close-search").on("click", function () {
    $(".top-search").slideUp();
});

function showTopSearch() {
    $(".top-search").slideToggle();
}

function openSearchUnits(searchUrl) {

    var queryParams = [];
    $('[name^=search_]').toArray().forEach(function (item) {
        queryParams.push($(item).attr('name').substring(7).toLowerCase() + '=' + $(item).val());
    });
    //console.log(searchUrl);
    //window.location.assign(domainUrl +'/'+ searchUrl +'?'+ queryParams.join('&'));
    window.open(domainUrl + searchUrl + '?' + queryParams.join('&'), '_self');
}



$(document).ready(function () {

    $("#SendUs-Form,#ContactUs-Form,#PopupRequest-Form").validate({
        rules: {
            FullName: {
                required: true
            },
            MobileNo: {
                required: true
            },
            Message: {
                required: true
            }
        },
        messages: {

            FullName: {
                required: ""
            },
            MobileNo: {
                required: ""
            },
            Message: {
                required: ""
            }
        }
    });
})

function SendRequest_OnBegin() {
    $('#request_message').waitMe({
        effect: 'bounce',
        color: '#669941'
    });
}

function SendRequest_OnSuccess(data) {

    if (data == 'Ok') {
        $('#request_message').hide();
        $('#request_sent').show();
        $('#request_message').waitMe("hide");

    }
    else {
    }
}

function SendRequest_Failure(e) {
    console.log(e);
}

function showRequestFrom() {
    $('#request_message').find('[name="FullName"]').val('');
    $('#request_message').find('[name="MobileNo"]').val('');
    $('#request_message').find('[name="Message"]').val('');

    $('#request_sent').hide();
    $('#request_message').show();
}



function PopupRequest_OnBegin() {
    $('#popup_message').waitMe({
        effect: 'bounce',
        color: '#669941'
    });
}

function PopupRequest_OnSuccess(data) {

    if (data == 'Ok') {
        $('#popup_message').hide();
        $('#popup_sent').show();
        $('#popup_message').waitMe("hide");

    }
    else {
    }
}

function PopupRequest_Failure(e) {
    console.log(e);
}

function showPopupFrom() {
    $('#popup_message').find('[name="FullName"]').val('');
    $('#popup_message').find('[name="MobileNo"]').val('');
    $('#popup_message').find('[name="Message"]').val('');

    $('#popup_sent').hide();
    $('#popup_message').show();
}

var popup_ProjName = null;
function showRequestModal(unitName) {
    showPopupFrom();
    if (unitName) {
        if (!popup_ProjName) {
            popup_ProjName = $('#popup_message').find('[name="ProjectName"]').val();
        }

        $('#popup_message').find('[name="ProjectName"]').val(popup_ProjName + ' - ' + unitName);
    }
    $('#popumodal_request').modal('show');
}

/* Overlay Form
============================================== */
$(function () {
    $('.btn-ask').toArray().forEach(function (item) {
        attachAskAction(item);
    });
})

function attachAskAction(item) {

    $(item).click(function (e) {
        if ($(item).attr('inlineform') == true) {
            var image = $($(item).parents(".overlay-item")[0]).find(".image")[0];
            var overlayForm = $(image).find(".overlay-form")[0];
            if ($(overlayForm).css('top') != '0px') {
                $(image).children('.overlay').css('display', 'none');
                $(overlayForm).css('top', '0px');
            }
            else {
                $(image).children('.overlay').css('display', 'block');
                $(overlayForm).css('top', '100%');
            }
        }
        else {
            showRequestModal($(item).attr('unitname'));
        }
    });

}

/* Wow
============================================== */
$(function () {
    //var effects = ["bounce", "pulse", "rubberBand", "shake", "swing", "tada", "wobble", "bounceIn", "bounceInDown", "bounceInLeft", "bounceInRight", "bounceInUp", "fadeIn", "fadeInDown", "fadeInDownBig", "fadeInLeft", "fadeInLeftBig", "fadeInRight", "fadeInRightBig", "fadeInUp", "fadeInUpBig", "flip", "lightSpeedIn", "rotateIn", "rotateInDownLeft", "rotateInDownRight", "rotateInUpLeft", "rotateInUpRight", "rollIn", "zoomIn", "zoomInDown", "zoomInLeft", "zoomInRight", "zoomInUp"];
    //var effects = ["bounce", "pulse", "swing", "bounceIn", "bounceInDown", "bounceInLeft", "bounceInRight", "bounceInUp", "fadeIn", "fadeInDown", "fadeInDownBig", "fadeInLeft", "fadeInLeftBig", "fadeInRight", "fadeInRightBig", "fadeInUp", "fadeInUpBig", "rotateInDownLeft", "rotateInDownRight", "rotateInUpLeft", "rotateInUpRight", "rollIn", "zoomIn"];
    var effects = ["fadeInDown"];
    $('.wow').toArray().forEach(function (item) {
        var effect = effects[Math.round(Math.random() * (effects.length - 1))];
        $(item).addClass(effect);
    })

    new WOW().init();
})

/* Fun Fact
    ============================================== */
var numCounted = false;
$(".number-counters").appear();
$(".number-counters").on('appear', function () {
    if (!numCounted) {
        $(".number-counters [data-to]").each(function () {
            var e = $(this).attr("data-to");
            $(this).delay(6e3).countTo({
                from: 1,
                to: e,
                speed: 3000,
                refreshInterval: 50
            })
        })
        numCounted = true;
    }
});

/* upload CV
    ============================================== */

function uploadCV(form) {
    var formFields = $(form)[0];
    // From Validation Begin ------------------------- 
    for (var i = 0; i < formFields.length; i++) {
        if (!formFields[i].validity.valid)
            $(formFields[i]).addClass('error');
    }
    if (!formFields.checkValidity())
        return;
    // From Validation End -------------------------
    var data = new FormData(formFields);
    var reqPanel = $(form).parents('[rel=request]').toArray()[0];
    var resPanel = $(reqPanel).parent().children('[rel=response]').toArray()[0];
    $(reqPanel).waitMe({
        effect: 'bounce',
        color: '#669941'
    });
    $.ajax({
        type: "POST",
        enctype: 'multipart/form-data', // Important!
        url: domainUrl + '/umbraco/Surface/Contact/UploadCV', // Important!
        data: data,
        processData: false, // Important!
        contentType: false, // Important!
        cache: false,
        success: function (res) {
            if (res == 'Ok') {
                $(reqPanel).hide();
                $(resPanel).show();
                $(reqPanel).waitMe("hide");
            }
            else {
                console.error(res);
            }
        }
    });
}

function hide_element(element) {
    $('#' + element).hide();
}


/* initialize Offer
    ============================================== */
function initOfferModal(imageUrl, linkUrl, offerName, showAfter, hideAfter) {

    if (showAfter == -1)
        showAfter = 3000;

    if (hideAfter != -1)
        hideAfter += showAfter;

    if (linkUrl == null)
        linkUrl = '#.';

    var modalOffer = $('#modalOffer');
    $(modalOffer).attr('offer_name', offerName);

    var imgOffer = $(modalOffer).find('img').toArray()[0];
    $(imgOffer).attr('src', imageUrl);

    //var lnkOffer = $(modalOffer).children('a').toArray()[0];
    $(imgOffer).attr('href', linkUrl);

    $(imgOffer).on('click', function () {
        if (linkUrl && linkUrl != '') {
            window.open(linkUrl, '_blank');
        }
        else {
            var projName = $('#popup_message').find('[name="ProjectName"]');
            $(projName).val($(projName).val() + ' - ' + offerName);
            $('#popumodal_request').modal('show');
        }
        $(modalOffer).fadeToggle('slow');
    });

    setTimeout(function () {
        if (imageUrl != '')
            $(modalOffer).fadeToggle('slow');
    }, showAfter);

    setTimeout(function () {
        if (hideAfter != -1)
            $(modalOffer).fadeToggle('slow');
    }, hideAfter);

}

/* Fancybox
============================================== */
$(".fancybox-offer").fancybox({
    openEffect: 'elastic',
    openSpeed: 650,
    closeEffect: 'fade',
    closeClick: true,
    arrows: true,
    keyboard: true,
    clickContent: function (current, event) {
        if (!event)
            return;
        //return current.type === "image" ? "zoom" : false;
        var offer = $(document).find("img[offer][src='" + current.src + "']").toArray()[0];
        var linkUrl = $(offer).attr('link-url');
        var offerName = $(offer).attr('offer-name');
        if (linkUrl && linkUrl != '') {
            window.open(linkUrl, '_blank');
        }
        else {
            var projName = $('#popup_message').find('[name="ProjectName"]');
            //$(projName).val($(projName).val() + ' - ' + offerName);
            $(projName).val(offerName);
            //jQuery.fancybox.getInstance().close();
            $('#popumodal_request').modal('show');
        }
        return true;
    },
});