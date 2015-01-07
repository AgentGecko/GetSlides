/// <reference path="../typings/pdf/pdf.d.ts" />
var GetSlides;
(function (GetSlides) {
    var PdfViewer = (function () {
        function PdfViewer(adress, canvasId, isSubject, wsUri, presentationId) {
            this.currentPage = 0;
            this.pages = {};
            this.scale = 1;
            this.canvas = document.getElementById(canvasId);
            this.load(adress);
            if (presentationId != null) {
                this.webSocketHandler = new GetSlides.WebSocketHandler(wsUri, isSubject, this.OnWsOpen, this.OnWsClose, this.goToPage, this.OnWsError, presentationId);
                this.presentationId = presentationId;
            } else {
                this.webSocketHandler = new GetSlides.WebSocketHandler(wsUri, isSubject, this.OnWsOpen, this.OnWsClose, this.goToPage, this.OnWsError);
            }
        }
        PdfViewer.prototype.goToPage = function (pageNum1) {
            if (pageNum1 === "CLOSE")
                GetSlides.pdfViewer.stop();
            else {
                var pageNum = parseInt(pageNum1);
                console.log(pageNum, GetSlides);
                if (pageNum > 0 && pageNum < GetSlides.pdfViewer.pdf.numPages) {
                    GetSlides.pdfViewer.currentPage = pageNum;
                    console.log(pageNum, GetSlides);
                    GetSlides.pdfViewer.getPage(GetSlides.pdfViewer.currentPage, function () {
                        GetSlides.pdfViewer.renderPage(GetSlides.pdfViewer.currentPage);
                    });
                }
            }
        };

        PdfViewer.prototype.nextPage = function () {
            var _this = this;
            if (this.currentPage < this.pdf.numPages) {
                this.currentPage++;
                this.webSocketHandler.send(this.currentPage);
                this.getPage(this.currentPage, function () {
                    _this.renderPage(_this.currentPage);
                });
            }
        };

        PdfViewer.prototype.prevPage = function () {
            var _this = this;
            if (this.currentPage > 1) {
                this.currentPage--;
                this.webSocketHandler.send(this.currentPage);
                this.getPage(this.currentPage, function () {
                    console.log("aaa");
                    _this.renderPage(_this.currentPage);
                });
            }
        };

        PdfViewer.prototype.stop = function () {
            this.webSocketHandler.websocket.close();
            location.href = '#/';
        };

        PdfViewer.prototype.updatePageNumber = function () {
            $("#pageNum").text(this.currentPage + "/" + this.pdf.numPages);
        };

        PdfViewer.prototype.OnWsOpen = function () {
        };

        PdfViewer.prototype.OnWsClose = function () {
        };

        PdfViewer.prototype.OnWsMessage = function () {
        };

        PdfViewer.prototype.OnWsError = function () {
        };

        PdfViewer.prototype.load = function (adress) {
            var _this = this;
            this.getPdf(adress, function () {
                _this.getPage(1, function () {
                    _this.renderPage(1);
                });
            });
        };

        PdfViewer.prototype.getPdf = function (adress, callback) {
            var _this = this;
            PDFJS.getDocument(adress).then(function (pdf) {
                _this.pdf = pdf;
                if (callback !== undefined)
                    callback();
            });
        };

        PdfViewer.prototype.getPage = function (pageNumber, callback) {
            var _this = this;
            if (this.pdf !== undefined && this.pdf !== null) {
                if (pageNumber > 0 && pageNumber <= this.pdf.numPages) {
                    if (this.pages[pageNumber] === undefined) {
                        this.pdf.getPage(pageNumber).then(function (page) {
                            _this.pages[pageNumber] = page;
                            _this.currentPage = pageNumber;
                            if (callback !== undefined)
                                callback();
                        });
                    } else {
                        this.currentPage = pageNumber;
                        if (callback !== undefined)
                            callback();
                    }
                }
            }
        };

        PdfViewer.prototype.renderPage = function (pageNumber, callback) {
            var viewport = this.pages[pageNumber].getViewport(this.scale);
            var context = this.canvas.getContext('2d');
            var renderContext = {
                canvasContext: context,
                viewport: viewport
            };

            this.pages[pageNumber].render(renderContext);
            this.updatePageNumber();
            if (callback !== undefined)
                callback();
        };
        return PdfViewer;
    })();
    GetSlides.PdfViewer = PdfViewer;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=pdf.main.js.map
