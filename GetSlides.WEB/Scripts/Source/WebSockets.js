/// <reference path="../typings/pdf.d.ts" />
var GetSlides;
(function (GetSlides) {
    (function (WebSocketsModule) {
        var WebSocketAPI = (function () {
            function WebSocketAPI() {
            }
            WebSocketAPI.prototype.Initialize = function (user, data) {
                this.url = "wss://localhost:44301/api/WebSocket?user=" + user + "&data=" + data;
            };

            WebSocketAPI.prototype.WS = function () {
                this.websocket = new WebSocket(this.url);
                this.websocket.onopen = function (evt) {
                    this.OnOpen(evt);
                };
                this.websocket.onclose = function (evt) {
                    this.OnClose(evt);
                };
                this.websocket.onerror = function (evt) {
                    this.OnError(evt);
                };
                this.websocket.onmessage = function (evt) {
                    this.OnMessage(evt);
                };
            };

            WebSocketAPI.prototype.OnOpen = function (evt) {
                console.log("WebSocket connection is open.");
            };
            WebSocketAPI.prototype.OnClose = function (evt) {
                console.log("WebSocket connection is closed.");
            };
            WebSocketAPI.prototype.OnError = function (evt) {
                console.log("An error has occured.");
            };
            WebSocketAPI.prototype.OnMessage = function (evt) {
                //Get evt.data page
            };
            return WebSocketAPI;
        })();
        WebSocketsModule.WebSocketAPI = WebSocketAPI;

        var PresentationControls = (function () {
            function PresentationControls() {
            }
            PresentationControls.prototype.loadWorkerURL = function (url) {
                if (this.cachedWorker) {
                    return this.cachedWorker;
                }

                this.cachedWorker = PDFJS.createPromiseCapability();
                var xmlhttp;
                xmlhttp = new XMLHttpRequest();

                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        var workerJSBlob = new Blob([xmlhttp.responseText], {
                            type: "text/javascript"
                        });
                        this.cachedWorker.resolve(window.URL.createObjectURL(workerJSBlob));
                    }
                };
                xmlhttp.open("GET", url, true);
                xmlhttp.send();
                return this.cachedWorker.promise;
            };

            PresentationControls.prototype._getWebWorker = function () {
                return this.loadWorkerURL('../../../Scripts/pdf.worker.js').then(function (blob) {
                    PDFJS.workerSrc = blob;
                    return PDFJS;
                });
            };
            PresentationControls.prototype.openPdf = function (url) {
                return this._getWebWorker().then(function (PDFJS) {
                    return PDFJS.getDocument(url);
                });
            };
            PresentationControls.prototype.getPage = function (page) {
                this.openPdf('https://localhost:44301/PDF/prez.pdf').then(function (pdf) {
                    pdf.getPage(1).then(function (page) {
                        var scale = 1.5;
                        var viewport = page.getViewport(scale);

                        this.canvas = document.getElementById('the-canvas');
                        var context = this.canvas.getContext('2d');
                        this.canvas.height = viewport.height;
                        this.canvas.width = viewport.width;

                        var renderContext = {
                            canvasContext: context,
                            viewport: viewport
                        };
                        page.render(renderContext);
                    });
                });
            };
            return PresentationControls;
        })();
        WebSocketsModule.PresentationControls = PresentationControls;
    })(GetSlides.WebSocketsModule || (GetSlides.WebSocketsModule = {}));
    var WebSocketsModule = GetSlides.WebSocketsModule;
})(GetSlides || (GetSlides = {}));

(function () {
    var presentationcontrols = new GetSlides.WebSocketsModule.PresentationControls();
    // presentationcontrols.getPage(1);
});
//# sourceMappingURL=WebSockets.js.map
