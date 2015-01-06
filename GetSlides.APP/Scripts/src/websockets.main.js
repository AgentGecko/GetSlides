var GetSlides;
(function (GetSlides) {
    var WebSocketHandler = (function () {
        function WebSocketHandler(uri, isSubject, onOpen, onClose, onMessage, onError, id) {
            var _this = this;
            this.isSubject = false;
            this.uri = uri;
            this.isSubject = isSubject;
            if (isSubject)
                this.id = id;
            this.websocket = new WebSocket(uri);
            this.websocket.onopen = function (event) {
                _this.onOpen(event, onOpen);
            };
            this.websocket.onclose = function (event) {
            };
            this.websocket.onmessage = function (event) {
            };
            this.websocket.onerror = function (event) {
            };
        }
        WebSocketHandler.prototype.onOpen = function (event, callback) {
            var _this = this;
            console.log("Open", event);
            if (this.isSubject) {
                GetSlides.router.getJsonAuth("/ws/present/getpin/" + this.id, GetSlides.storage.getItem(GetSlides.storage.keys['auth']), function (data) {
                    _this.pin = data;
                    callback(data);
                });
            }
        };

        WebSocketHandler.prototype.onMessage = function (event, callback) {
            console.log("Message", event);
            callback(event.data);
        };

        WebSocketHandler.prototype.onClose = function (event, callback) {
            console.log("Close", event);
            callback();
        };

        WebSocketHandler.prototype.onError = function (event, callback) {
            console.log("Error", event);
            callback();
        };

        WebSocketHandler.prototype.send = function (data) {
            if (this.isSubject)
                this.websocket.send(data);
        };
        return WebSocketHandler;
    })();
    GetSlides.WebSocketHandler = WebSocketHandler;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=websockets.main.js.map
