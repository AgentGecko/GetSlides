/// <reference path="../typings/pdf.d.ts" />
module GetSlides {
    export module WebSocketsModule {

        export class WebSocketAPI {
            url: string;
            websocket: WebSocket;

            public Initialize(user: string, data: string) {
                this.url = "wss://localhost:44301/api/WebSocket?user=" + user + "&data=" + data;
            }

            public WS() {
                this.websocket = new WebSocket(this.url);
                this.websocket.onopen = function (evt) { this.OnOpen(evt); };
                this.websocket.onclose = function (evt) { this.OnClose(evt); };
                this.websocket.onerror = function (evt) { this.OnError(evt); };
                this.websocket.onmessage = function (evt) { this.OnMessage(evt); };
            }

            public OnOpen(evt: Event) {
                console.log("WebSocket connection is open.");
            }
            public OnClose(evt: Event) {
                console.log("WebSocket connection is closed.");
            }
            public OnError(evt: Event) {
                console.log("An error has occured.");
            }
            public OnMessage(evt: Event) {
                //Get evt.data page
            }

        }

        export class PresentationControls {
            cachedWorker: PDFPromise<any>;
            canvas: HTMLCanvasElement;
            

            constructor() {
            }

            public loadWorkerURL(url)  {
                if (this.cachedWorker) { return this.cachedWorker; }

                this.cachedWorker = PDFJS.createPromiseCapability();
                var xmlhttp;
                xmlhttp = new XMLHttpRequest();

                xmlhttp.onreadystatechange = function () {
                    if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                        var workerJSBlob = new Blob([xmlhttp.responseText], {
                            type: "text/javascript"
                        });
                        this.cachedWorker.resolve((<any>window).URL.createObjectURL(workerJSBlob));
                    }
                };
                xmlhttp.open("GET", url, true);
                xmlhttp.send();
                return (<PDFPromise<any>>this.cachedWorker.promise);
            }

            private _getWebWorker() {
                return this.loadWorkerURL('../../../Scripts/pdf.worker.js').then(function (blob) {
                    PDFJS.workerSrc = blob;
                    return PDFJS;
                });
            }
            public openPdf(url) {
                return this._getWebWorker().then(function (PDFJS) {
                    return PDFJS.getDocument(url);
                });
            }
            public getPage(page: string) {

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
            }

        }
    }
}

() => {
    var presentationcontrols = new GetSlides.WebSocketsModule.PresentationControls();
   // presentationcontrols.getPage(1);
};