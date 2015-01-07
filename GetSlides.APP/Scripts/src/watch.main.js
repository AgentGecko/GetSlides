var GetSlides;
(function (GetSlides) {
    GetSlides.selectedPresentationUri;
    GetSlides.spin;

    function getUrl(pin) {
        GetSlides.router.getJson("/ws/geturi/" + pin, function (url) {
            if (url !== "Inactive pin, please try again.") {
                GetSlides.selectedPresentationUri = url;
                GetSlides.spin = pin;
                location.href = '#/watch/connect/';
            } else {
                console.log(url);
            }
        });
    }
    GetSlides.getUrl = getUrl;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=watch.main.js.map
