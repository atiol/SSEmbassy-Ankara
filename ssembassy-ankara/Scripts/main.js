//$(document).ready(function() {
//});
jQuery.noConflict();
(function ($) {
    (function () {
        // sidenav
        $('.sidenav').sidenav();

        // dropdown trigger
        $('.dropdown-trigger').dropdown({
            coverTrigger: false,
            hover: true,
            constrainWidth: false
        });

        // modal
        $('.modal').modal();

        // homepage carousel
        $('.carousel').carousel({
            fullwidth: true,
            indicators: true
        });
        setInterval(function () {
            $('.carousel').carousel('next');
        }, 3000);

        // collapsible
        $('.collapsible').collapsible();

        // .dropdown content
        $('.dropdown-content li>a').css("width", "auto !important");
    });
})(jQuery);