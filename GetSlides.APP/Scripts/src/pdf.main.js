/// <reference path="../typings/pdf/pdf.d.ts" />
var GetSlides;
(function (GetSlides) {
    var PdfViewer = (function () {
        function PdfViewer(adress, canvasId) {
            this.pages = {};
            this.scale = 1;
            this.canvas = document.getElementById(canvasId);
            this.canvas.height = 500;
            this.canvas.width = 500;
            this.getPdf(adress);
        }
        PdfViewer.prototype.getPdf = function (adress) {
            var _this = this;
            PDFJS.getDocument(adress).then(function (pdf) {
                _this.pdf = pdf;
            });
        };

        PdfViewer.prototype.getPage = function (pageNumber) {
            var _this = this;
            if (this.pdf !== undefined && this.pdf !== null) {
                if (pageNumber > 0 && pageNumber <= this.pdf.numPages) {
                    this.pdf.getPage(pageNumber).then(function (page) {
                        _this.pages[pageNumber] = page;
                    });
                }
            }
        };

        PdfViewer.prototype.renderPage = function (pageNumber) {
            var viewport = this.pages[pageNumber].getViewport(this.scale);
            var context = this.canvas.getContext('2d');
            var renderContext = {
                canvasContext: context,
                viewport: viewport
            };

            this.pages[pageNumber].render(renderContext);
        };
        return PdfViewer;
    })();
    GetSlides.PdfViewer = PdfViewer;
})(GetSlides || (GetSlides = {}));
//$(document).ready(() => {
//    PDFJS.getDocument('/PDF/k.pdf').then((pdf: PDFDocumentProxy) => {
//        console.log(pdf);
//        pdf.getPage(3).then((page: PDFPageProxy) => {
//            console.log(page);
//            var scale = 1;
//            var viewport = page.getViewport(scale);
//            var canvas: HTMLCanvasElement = (<any>document).getElementById('canvas');
//            var context = canvas.getContext('2d');
//            canvas.height = viewport.height;
//            canvas.width = viewport.width;
//            var renderContext = {
//                canvasContext: context,
//                viewport: viewport
//            };
//            page.render(renderContext);
//        });
//    });
//});
//# sourceMappingURL=pdf.main.js.map
