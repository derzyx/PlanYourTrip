//https://tutorial.eyehunts.com/js/prevent-form-submission-on-enter-key-press-example-code/

document.querySelectorAll("form").forEach(function (item) {
    item.onkeypress = function (e) {
        var key = e.charCode || e.keyCode || 0;
        if (key == 13) {
            e.preventDefault();
        }
    }
});