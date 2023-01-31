/* Loader
============================================== */
$(window).on("load", function () {
    "use strict";
    $(".loader").fadeOut(800);
});

jQuery(function ($) {
    "use strict";

    /* Back to top
    ============================================== */
    var amountScrolled = 700;
    var backBtn = $("a.back-to");
    $(window).on("scroll", function () {
        if ($(window).scrollTop() > amountScrolled) {
            backBtn.fadeIn("slow");
        } else {
            backBtn.fadeOut("slow");
        }
    });
    backBtn.on("click", function () {
        $("html, body").animate({
            scrollTop: 0
        }, 700);
        return false;
    });



    /* Puch Menu
    ============================================== */
    var $menuLeft = $(".pushmenu-left");
    var $menuRight = $(".pushmenu-right");
    var $toggleleft = $("#menu_bars.left");
    var $toggleright = $("#menu_bars.right");
    var pushbody = $(".pushmenu-push");
    $toggleleft.on("click", function () {
        $(this).toggleClass("active");
        pushbody.toggleClass("pushmenu-push-toright");
        $menuLeft.toggleClass("pushmenu-open");
        return false;
    });
    $toggleright.on("click", function () {
        $(this).toggleClass("active");
        pushbody.toggleClass("pushmenu-push-toleft");
        $menuRight.toggleClass("pushmenu-open");
        return false;
    });


    /* Push DropDowns 
    ============================================== */
    var side_drop = $(".push_nav .dropdown");
    side_drop.on("show.bs.dropdown", function (e) {
        $(this).find(".dropdown-menu").first().stop(true, true).slideDown();
    });
    side_drop.on("hide.bs.dropdown", function (e) {
        $(this).find(".dropdown-menu").first().stop(true, true).slideUp();
    });

    $('.selectpicker').selectpicker({
        size: 4
    });


    /* Skills Bar
    ============================================== */
    $(".skills li").each(function () {
        $(this).appear(function () {
            $(this).animate({
                opacity: 1,
                left: "0px"
            }, 800);
            var b = jQuery(this).find(".progress-bar").attr("data-width");
            $(this).find(".progress-bar").animate({
                width: b + "%"
            }, 1300, "linear");
        });
    });


    /* Rang slider
    ============================================== */
    $(".selectbox").selectbox();
    $(".nstSlider").nstSlider({
        "left_grip_selector": ".leftGrip",
        "right_grip_selector": ".rightGrip",
        "value_bar_selector": ".bar",
        "value_changed_callback": function (cause, leftValue, rightValue) {
            $(this).parent().find(".leftLabel").text(leftValue);
            $(this).parent().find(".rightLabel").text(rightValue);
        }
    });


    /* Fancybox
    ============================================== */
    //$(".fancybox").fancybox({
    //    openEffect: 'elastic',
    //    openSpeed: 650,
    //    closeEffect: 'fade',
    //    closeClick: true,
    //});


    /* Check Box
    ============================================== */
    $('input[name="radio-btn"]').wrap('<div class="radio-btn"><i></i></div>');
    $(".radio-btn").on('click', function () {
        var _this = $(this),
            block = _this.parent().parent();
        block.find('input:radio').attr('checked', false);
        block.find(".radio-btn").removeClass('checkedRadio');
        _this.addClass('checkedRadio');
        _this.find('input:radio').attr('checked', true);
    });
    $('input[name="check-box"]').wrap('<div class="check-box"><i></i></div>');
    $.fn.toggleCheckbox = function () {
        this.attr('checked', !this.attr('checked'));
    }
    $('.check-box').on('click', function () {
        $(this).find(':checkbox').toggleCheckbox();
        $(this).toggleClass('checkedBox');
    });



    /* Team popup
    ============================================== */
    $('a[href=".team"]').on("click", function (event) {
        event.preventDefault();
        $("#team").addClass("open");
        $("#team > form > input[type='search']").focus();
    });
    $("#team, #team button.close").on("click keyup", function (event) {
        if (event.target == this || event.target.className == 'close' || event.keyCode == 27) {
            $(this).removeClass('open');
        }
    });


    /* Equalise columns
    ============================================== */
    $(".item").each(function () {
        var highestBox = 0;
        $(".col-md-6", this).each(function () {
            if ($(this).height() > highestBox) {
                highestBox = $(this).height();
            }
        });
        $(".col-md-6", this).height(highestBox);
    });


    /* Toggle button for more options
    ============================================== */
    $(".show-hide-detail").hide();
    $(".show-hide-detail:first").show();
    $(".show-hide-btn a").on('click', function () {
        var thid_data = $(this).attr('data-id');
        $(".show-hide-btn a").removeClass('active');
        $(this).addClass('active');
        if (!$("#" + thid_data).is(":visible")) {
            $(".show-hide-detail").hide();
            $("#" + thid_data).show();
        }
    });


    /* Vertical center
     ============================================== */
    var verticalCenterHeight = function () {
        var screenHeight = $(window).height();
        $(".vertical-center").each(function () {
            $(this).css('top', ($(this).parent().height() - $(this).height()) / 2);
        });
    }
    window.onload = verticalCenterHeight;
    window.onresize = verticalCenterHeight;


    /* Left Menu
    ============================================== */
    $(document).ready(function () {
        var lastId, $targetLink = $('.block-menu a[href^="#"]', document.body);
        $($targetLink, document.body).on('click', function (e) {
            $targetLink.removeClass('active');
            $(this).addClass('active');
            e.preventDefault();
            var target = this.hash;
            var $target = $(target);
            var pointer = $target.offset().top - 120;
            $('html, body').stop().animate({
                'scrollTop': pointer
            }, 900, 'swing', function () { });
        });
        var lastId, topMenu = $(".block-menu", document.body),
            topMenuHeight = 160,
            menuItems = topMenu.find('a[href^="#"]');
        var scrollItems = menuItems.map(function () {
            var item = $($(this).attr("href"));
            if (item.length) {
                return item;
            }
        });
        $(window).scroll(function () {

            if (topMenu.hasClass('affix')) {
                var fromTop = $(this).scrollTop() + topMenuHeight;
                var cur = scrollItems.map(function () {
                    if ($(this).offset().top < fromTop)
                        return this;
                });
                cur = cur[cur.length - 1];
                var id = cur && cur.length ? cur[0].id : "";
                if (lastId !== id) {
                    lastId = id;
                    menuItems.removeClass("active");
                    menuItems.filter("[href=#" + id + "]").addClass("active");
                }
            }
        });
    });


    /* Owl all sliders
    ============================================== */
    $("#partner_slider").owlCarousel({
        autoPlay: 4000,
        items: 6,
        navigation: false,
        pagination: false,
        itemsDesktop: [1199, 4],
        itemsDesktopSmall: [979, 4]
    });

    $("#about_single").owlCarousel({
        autoPlay: true,
        singleItem: true,
        navigation: false,
    });

    $("#agent-2-slider, #agent-3-slider, #agent-4-slider").owlCarousel({
        autoPlay: 3000,
        singleItem: true,
        pagination: false,
        navigation: true,
        navigationText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
    });

    var owl = $("#banner_eig_slider");
    owl.owlCarousel({
        autoPlay: 3000,
        navigation: false,
        singleItem: true,
        pagination: false,
        transitionStyle: "fade"
    });

    //$("#gallery_slider").owlCarousel({
    //    autoPlay: true,
    //    items: 3,
    //    pagination: false,
    //    navigation: true,
    //    navigationText: [
    //        "<i class='fa fa-angle-left'></i>",
    //        "<i class='fa fa-angle-right'></i>"
    //    ],
    //    itemsMobile: [480, 1],
    //});

//Moved to layout for rtl of navigationText
    //$("#property-2-slider, #nav_slider").owlCarousel({
    //    //autoPlay: 3000,
    //    autoPlay: false,
    //    items: 3,
    //    pagination: false,
    //    navigation: true,
    //    navigationText: [
    //        "<i class='fa fa-angle-left'></i>",
    //        "<i class='fa fa-angle-right'></i>"
    //    ],
    //    itemsDesktopSmall: [1024, 2],
    //    itemsTablet: [768, 2],
    //    itemsMobile: [479, 1],
    //});

    $('#main-slider.carousel').carousel({
        interval: 8000,
        singleItem: true,
        transitionStyle: "fade"
    });

    $("#property-listing-slider, #ide_team").owlCarousel({
        autoPlay: 3000,
        items: 2,
        pagination: false,
        navigation: true,
        navigationText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
    });

    $("#partner_slider_2").owlCarousel({
        autoPlay: 4000,
        items: 6,
        navigation: false,
        pagination: true,
        itemsDesktop: [1199, 4],
        itemsDesktopSmall: [979, 4]
    });

    var sync1 = $("#property-d-1");
    var sync2 = $("#property-d-1-2");

    sync1.owlCarousel({
        autoPlay: 3000,
        singleItem: true,
        slideSpeed: 1000,
        navigation: true,
        pagination: false,
        navigationText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
        afterAction: syncPosition,
        responsiveRefreshRate: 200,
    });
    $("#owl-testimonial-1").owlCarousel({
        autoPlay: true,
        items: 1,
        navigation: false,
    });

    sync2.owlCarousel({
        items: 5,
        itemsDesktop: [1199, 10],
        itemsDesktopSmall: [979, 10],
        itemsTablet: [768, 8],
        itemsMobile: [479, 4],
        pagination: false,
        responsiveRefreshRate: 100,
        afterInit: function (el) {
            el.find(".owl-item").eq(0).addClass("synced");
        }
    });

    function syncPosition(el) {
        var current = this.currentItem;
        $("#property-d-1-2")
            .find(".owl-item")
            .removeClass("synced")
            .eq(current)
            .addClass("synced")
        if ($("#property-d-1-2").data("owlCarousel") !== undefined) {
            center(current)
        }
    }

    $("#property-d-1-2").on("click", ".owl-item", function (e) {
        e.preventDefault();
        var number = $(this).data("owlItem");
        sync1.trigger("owl.goTo", number);
    });

    function center(number) {
        var sync2visible = sync2.data("owlCarousel").owl.visibleItems;
        var num = number;
        var found = false;
        for (var i in sync2visible) {
            if (num === sync2visible[i]) {
                var found = true;
            }
        }
        if (found === false) {
            if (num > sync2visible[sync2visible.length - 1]) {
                sync2.trigger("owl.goTo", num - sync2visible.length + 2)
            } else {
                if (num - 1 === -1) {
                    num = 0;
                }
                sync2.trigger("owl.goTo", num);
            }
        } else if (num === sync2visible[sync2visible.length - 1]) {
            sync2.trigger("owl.goTo", sync2visible[1])
        } else if (num === sync2visible[0]) {
            sync2.trigger("owl.goTo", num - 1)
        }
    }

    $("#property-1-slider, #listing_slider").owlCarousel({
        autoPlay: false,
        items: 2,
        pagination: false,
        navigation: true,
        navigationText: [
            "<i class='fa fa-angle-left'></i>",
            "<i class='fa fa-angle-right'></i>"
        ],
        itemsMobile: [480, 1],
    });

    $("#our-agent-slider").owlCarousel({
        autoPlay: 3000,
        items: 3,
        pagination: true,
        navigation: false,
        itemsDesktop: [1199, 3],
        itemsDesktopSmall: [979, 2],
        itemsTablet: [768, 2],
        itemsMobile: [480, 1],
    });

    $("#our-partner-slider").owlCarousel({
        autoPlay: 3000,
        items: 5,
        navigation: false,
        pagination: false,
        itemsDesktop: [1199, 3],
        itemsDesktopSmall: [979, 3]
    });

    $("#about_slider").owlCarousel({
        autoPlay: true, //Set AutoPlay to 3 seconds
        singleItem: true,
        navigation: false,
        pagination: true,
        itemsDesktop: [1199, 1],
        itemsDesktopSmall: [320, 1]
    });



     /* Revolution
    ============================================== */
	
    initSliderItem();

    var revapi = jQuery("#rev_full").revolution({
        sliderType: "standard",
        scrollbarDrag: "true",
        spinner: "on",
        sliderLayout: window.innerWidth > 480 ? "fullscreen" : "auto",
        minHeight: window.innerWidth < 1000 ? (Math.ceil(window.innerHeight * 0.5) + 'px') : "",
        delay: 3000,
        navigation: {
            arrows: {
                enable: true
            }
        },
        //responsiveLevels: [1240, 1024, 778, 480],
        //gridwidth: [1240, 1024, 778, 480],
        //gridheight: [400, 768, 960, 720],

        ///* [DESKTOP, LAPTOP, TABLET, SMARTPHONE] */
        //visibilityLevels: [1240, 1024, 1024, 480],
    });

    function initSliderItem () {
        var items = $('.tp-resizeme');

        items.toArray().forEach(function (item) {
            if (window.innerWidth > 480) {
                $(item).attr('data-x', 'left');
                $(item).attr('data-hoffset', '10');
                $(item).attr('data-y', 'bottom');
                $(item).attr('data-voffset', '0');
                $(item).attr('data-width', '600');// '300']"
                $(item).attr('data-height', 'auto');
                $(item).attr('data-whitespace', 'normal');
                //$(item).attr('data-visibility="['on', 'on', 'on', 'on']"
                $(item).attr('data-fontsize', '16'); //'24']"
                $(item).attr('data-lineheight', '10');
            }
            else {
                $(item).attr('data-x', 'center');
                //$(item).attr('data-hoffset', '120');
                $(item).attr('data-y', 'bottom');
                //$(item).attr('data-voffset', '20');
                $(item).attr('data-width', '300px');// '300']"
                $(item).attr('data-height', 'auto');
                $(item).attr('data-whitespace', 'normal');
                //$(item).attr('data-visibility="['on', 'on', 'on', 'on']"
                $(item).attr('data-fontsize', '24'); //'24']"
                $(item).attr('data-lineheight', '10');
            }
        })
        
    }

    /* Masonry
    ============================================== */

    $('#project-grid').masonry({
        itemSelector: '.grid-item', // use a separate class for itemSelector, other than .col-
        columnWidth: '.grid-sizer',
        percentPosition: true
    });


    /* Cubeportfolio
    ============================================== */
    $("#portfolio").cubeportfolio({
        filters: "#portfolio-filter",
        layoutMode: "grid",
        defaultFilter: "*",
        animationType: 'slideDelay',
        gapHorizontal: 30,
        gapVertical: 30,
        gridAdjustment: "responsive",
        lightboxDelegate: ".cbp-lightbox",
        lightboxGallery: true,
    });

    $("#nospace").cubeportfolio({
        filters: "#nospace-filter",
        layoutMode: "grid",
        defaultFilter: "*",
        animationType: "slideDelay",
        gapHorizontal: 0,
        gapVertical: 0,
        gridAdjustment: 'responsive',
        mediaQueries: [{
            width: 1500,
            cols: 3
        }, {
            width: 1100,
            cols: 3
        }, {
            width: 800,
            cols: 3
        }, {
            width: 480,
            cols: 2
        }, {
            width: 320,
            cols: 1
        }],
        lightboxDelegate: ".cbp-lightbox",
        lightboxGallery: true,
    });

    $('#js-grid-masonry').cubeportfolio({
        layoutMode: 'grid',
        gapHorizontal: 50,
        gapVertical: 20,
        gridAdjustment: 'responsive',
        mediaQueries: [{
            width: 1500,
            cols: 3
        }, {
            width: 1100,
            cols: 3
        }, {
            width: 800,
            cols: 3
        }, {
            width: 480,
            cols: 2
        }, {
            width: 320,
            cols: 1
        }],
    });



    /* Fun Fact
    ============================================== */
    $(".number-counters").appear(function () {
        $(".number-counters [data-to]").each(function () {
            var e = $(this).attr("data-to");
            $(this).delay(6e3).countTo({
                from: 1,
                to: e,
                speed: 3000,
                refreshInterval: 50
            })
        })
    });



    /* Typewriter
    ============================================== */
    $('#typewriter').typewriter({
        prefix: "Next homes provides ",
        text: ["best clients", "best stellar service", "simple question", "home valutation"],
        typeDelay: 90,
        waitingTime: 1500,
        blinkSpeed: 200
    });


    /* Parallaxie
    ============================================== */
    $(".parallaxie").parallaxie({
        speed: 0.6,
        offset: 0,
    });


});




/* Timeline
============================================== */
$(function () {
    window.sr = ScrollReveal();
    if ($(window).width() < 768) {
        if ($('.timeline-content').hasClass('js--fadeInLeft')) {
            $('.timeline-content').removeClass('js--fadeInLeft').addClass('js--fadeInRight');
        }
        sr.reveal('.js--fadeInRight', {
            origin: 'right',
            distance: '300px',
            easing: 'ease-in-out',
            duration: 800,
        });
    } else {
        sr.reveal('.js--fadeInLeft', {
            origin: 'left',
            distance: '300px',
            easing: 'ease-in-out',
            duration: 800,
        });
        sr.reveal('.js--fadeInRight', {
            origin: 'right',
            distance: '300px',
            easing: 'ease-in-out',
            duration: 800,
        });
    }
    sr.reveal('.js--fadeInLeft', {
        origin: 'left',
        distance: '300px',
        easing: 'ease-in-out',
        duration: 800,
    });
    sr.reveal('.js--fadeInRight', {
        origin: 'right',
        distance: '300px',
        easing: 'ease-in-out',
        duration: 800,
    });
});



/* Loan Calculator
============================================== */
function myFunction() {
    var loan = $('#amount').val(),
        month = $('#months').val(),
        int = $('#interest').val(),
        years = $('#years').val(),
        down = $('#down').val(),
        amount = parseInt(loan),
        months = parseInt(month),
        down = parseInt(down),
        annInterest = parseFloat(int),
        monInt = annInterest / 1200,
        calculation = ((monInt + (monInt / (Math.pow((1 + monInt), months) - 1))) * (amount - (down || 0))).toFixed(2);
    document.getElementById("output").innerHTML = calculation;
}
$(function () {
    var month = $(this).val(),
        doneTypingInterval = 500,
        months = parseInt(month),
        typingTimer;

    $('#months').keyup(function () {
        month = $(this).val();
        months = parseInt(month);

        clearTimeout(typingTimer);
        if (month) {
            typingTimer = setTimeout(doneTyping, doneTypingInterval);
        }
    });

    function doneTyping() {
        $('#years').val(months / 12);
    }
})
$(function () {
    var month = $(this).val(),
        doneTypingInterval = 500,
        months = parseInt(month),
        typingTimer;

    $('#months').keyup(function () {
        month = $(this).val();
        months = parseInt(month);

        clearTimeout(typingTimer);
        if (month) {
            typingTimer = setTimeout(doneTyping, doneTypingInterval);
        }
    });

    function doneTyping() {
        $('#years').val(months / 12);
    }
})
$(function () {
    var year = $(this).val(),
        doneTypingInterval = 500,
        years = parseInt(year),
        typingTimer;

    $('#years').keyup(function () {
        year = $(this).val();
        myears = parseInt(year);

        clearTimeout(typingTimer);
        if (year) {
            typingTimer = setTimeout(doneTyping, doneTypingInterval);
        }
    });

    function doneTyping() {
        $('#months').val(year * 12);
    }
})



/* Wow
============================================== */
new WOW().init();



/* Date picker
============================================== */
$('.datepicker').datepicker();
