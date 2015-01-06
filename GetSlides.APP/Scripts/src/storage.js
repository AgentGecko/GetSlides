var GetSlides;
(function (GetSlides) {
    var Storage = (function () {
        function Storage() {
            this.keys = {
                "pin": "__PIN",
                "auth": "__AUTH"
            };
            this.isLocal = false;
            this.isSession = false;
            this.isObject = false;
            this.dictionary = {};
            if (typeof (window.localStorage) !== "undefined") {
                this.isLocal = true;
            } else if (typeof (sessionStorage) !== "undefined") {
                this.isSession = true;
            } else {
                this.isObject = true;
            }
        }
        Storage.prototype.setItem = function (key, value) {
            if (this.isLocal) {
                window.localStorage.setItem(key, value);
            } else if (this.isSession) {
                sessionStorage.setItem(key, value);
            } else if (this.isObject) {
                this.dictionary[key] = value;
            }
        };

        Storage.prototype.getItem = function (key) {
            if (this.isLocal) {
                return window.localStorage.getItem(key);
            } else if (this.isSession) {
                return sessionStorage.getItem(key);
            } else if (this.isObject) {
                return this.dictionary[key];
            }
            return null;
        };
        return Storage;
    })();
    GetSlides.Storage = Storage;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=storage.js.map
