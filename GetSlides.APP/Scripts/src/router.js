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
            }).always(function (data) {
                callback(JSON.parse(data.responseText));
            });
        };

        Router.prototype.postJsonAuth = function (adress, token, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'POST',
                accepts: "application/json",
                dataType: "application/json",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", token);
                }
            }).always(function (data) {
                callback(JSON.parse(data.responseText));
            });
        };

        Router.prototype.getJson = function (adress, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json"
            }).always(function (data) {
                callback(JSON.parse(data.responseText));
            });
        };

        Router.prototype.postJson = function (adress, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'GET',
                accepts: "application/json",
                dataType: "application/json"
            }).done(function (data) {
                callback(JSON.parse(data.responseText));
            });
        };

        Router.prototype.uploadPdf = function (adress, token, formData, callback) {
            $.ajax({
                url: this.baseAdress + adress,
                type: 'POST',
                cache: false,
                data: formData,
                contentType: false,
                processData: false,
                crossDomain: true,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("Authorization", token);
                }
            }).always(function (data) {
                callback(data);
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
