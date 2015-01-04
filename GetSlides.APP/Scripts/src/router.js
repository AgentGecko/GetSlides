var GetSlides;
(function (GetSlides) {
    var Router = (function () {
        function Router() {
            this.baseAdress = "/api";
        }
        Router.prototype.getJsonAuth = function (adress, token, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", token);
                }
            }).done(function (data) {
                callback(null, data);
            }).fail(function (jqXhr, textStatus, errorThrown) {
                callback(errorThrown, null);
            });
        };

        Router.prototype.postJsonAuth = function (adress, token, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'POST',
                accepts: "application/json",
                dataType: "application/json"
            }).done(function (data) {
                callback(null, data);
            }).fail(function (jqXhr, textStatus, errorThrown) {
                callback(errorThrown, null);
            });
        };

        Router.prototype.getJson = function (adress, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json"
            }).done(function (data) {
                callback(null, data);
            }).fail(function (jqXhr, textStatus, errorThrown) {
                callback(errorThrown, null);
            });
        };

        Router.prototype.postJson = function (adress, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json"
            }).done(function (data) {
                callback(null, data);
            }).fail(function (jqXhr, textStatus, errorThrown) {
                callback(errorThrown, null);
            });
        };

        Router.prototype.getToken = function (username, password, callback) {
            var data = { grant_type: "password", username: username, password: password };
            $.ajax({
                url: "/token",
                type: 'POST',
                accepts: "application/json",
                dataType: "application/json",
                data: data
            }).always(function (response, error) {
                callback(error, JSON.parse(response.responseText));
            });
        };
        return Router;
    })();
    GetSlides.Router = Router;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=router.js.map
