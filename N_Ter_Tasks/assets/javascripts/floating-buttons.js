$('.floating-container').hover(function () {
    //$('.qa-action-button').addClass('display');
}, function () {
    $('.qa-action-button').removeClass('display');
    $('.quick-access-tooltip').removeClass('show');
    $('.quick-access').removeClass('open');
});
$('.quick-access').hover(function () {
    $('.quick-access').addClass('open');
    $('.quick-access-tooltip').addClass('show');
    $('.qa-action-button').addClass('display');
});