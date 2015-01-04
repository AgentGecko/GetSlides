/// <reference path="../typings/pdf/pdf.d.ts" />

module GetSlides {
    
    export class PdfViewer
    {
        public pdf: PDFDocumentProxy;
        public canvas: HTMLCanvasElement;
        public scale: number;
        public pages: { [pageNumber: number]: PDFPageProxy; } = {};

        constructor(adress: string, canvasId: string) {
            this.scale = 1;
            this.canvas = (<any>document).getElementById(canvasId);
            this.canvas.height = 500;
            this.canvas.width = 500;
            this.getPdf(adress);
        }

        public getPdf(adress: string) {
            PDFJS.getDocument(adress).then((pdf: PDFDocumentProxy) => {
                this.pdf = pdf;
            });
        }

        public getPage(pageNumber: number) {
            if (this.pdf !== undefined && this.pdf !== null) {
                if (pageNumber > 0 && pageNumber <= this.pdf.numPages) {
                    this.pdf.getPage(pageNumber).then((page: PDFPageProxy) => {
                        this.pages[pageNumber] = page;
                    });
                }
            }
        }

        public renderPage(pageNumber: number) {

            var viewport = this.pages[pageNumber].getViewport(this.scale);
            var context = this.canvas.getContext('2d');
            var renderContext = {
                canvasContext: context,
                viewport: viewport
            };

            this.pages[pageNumber].render(renderContext);
        }
    }

}

$(document).ready(() => {
    PDFJS.getDocument('/PDF/k.pdf').then((pdf: PDFDocumentProxy) => {
        console.log(pdf);
        pdf.getPage(3).then((page: PDFPageProxy) => {
            console.log(page);

            var scale = 1;
            var viewport = page.getViewport(scale);

            var canvas: HTMLCanvasElement = (<any>document).getElementById('canvas');
            var context = canvas.getContext('2d');
            canvas.height = viewport.height;
            canvas.width = viewport.width;

            var renderContext = {
                canvasContext: context,
                viewport: viewport
            };

            page.render(renderContext);
        });
    });
});