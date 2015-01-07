/// <reference path="../typings/pdf/pdf.d.ts" />

module GetSlides {


    export class PdfViewer
    {
        public presentationId: number;
        public pdf: PDFDocumentProxy;
        public canvas: HTMLCanvasElement;
        public scale: number;
        public currentPage: number = 0;
        public webSocketHandler: WebSocketHandler;
        public pages: { [pageNumber: number]: PDFPageProxy; } = {};

        constructor(adress: string, canvasId: string, isSubject: boolean, wsUri: string, presentationId?: number) {
            this.scale = 1;
            this.canvas = (<any>document).getElementById(canvasId);
            this.load(adress);
            if (presentationId != null) {
                this.webSocketHandler = new WebSocketHandler(wsUri, isSubject, this.OnWsOpen, this.OnWsClose, this.goToPage, this.OnWsError, presentationId);
                this.presentationId = presentationId;
            } else {
                this.webSocketHandler = new WebSocketHandler(wsUri, isSubject, this.OnWsOpen, this.OnWsClose, this.goToPage, this.OnWsError);
            }
            
        }

        public goToPage(pageNum1: string) {
            if (pageNum1 === "CLOSE") GetSlides.pdfViewer.stop();
            else {
                var pageNum = parseInt(pageNum1);
                console.log(pageNum, GetSlides);
            if (pageNum > 0 && pageNum < GetSlides.pdfViewer.pdf.numPages) {
                GetSlides.pdfViewer.currentPage = pageNum;
                console.log(pageNum, GetSlides);
                GetSlides.pdfViewer.getPage(GetSlides.pdfViewer.currentPage, () => {
                    GetSlides.pdfViewer.renderPage(GetSlides.pdfViewer.currentPage);
                });
            } 
            }
        }

        public nextPage() {
            if (this.currentPage < this.pdf.numPages) {
                this.currentPage++;
                this.webSocketHandler.send(this.currentPage);
                this.getPage(this.currentPage, () => {
                    this.renderPage(this.currentPage);
                });
            }
        }

        public prevPage() {
            if (this.currentPage > 1) {
                this.currentPage--;
                this.webSocketHandler.send(this.currentPage);
                this.getPage(this.currentPage, () => {
                    console.log("aaa");
                    this.renderPage(this.currentPage);
                });
            }
        }

        public stop() {
            this.webSocketHandler.websocket.close();
            location.href = '#/';
        }

        public updatePageNumber() {
            $("#pageNum").text(this.currentPage + "/" + this.pdf.numPages);
        }

        public OnWsOpen() {
        }

        public OnWsClose() {
            
        }

        public OnWsMessage() {
            
        }

        public OnWsError() {
            
        }

        public load(adress: string) {
            this.getPdf(adress, () => {
                this.getPage(1, () => {
                    this.renderPage(1);
                });
            });
        }

        public getPdf(adress: string, callback?: Function) {
            PDFJS.getDocument(adress).then((pdf: PDFDocumentProxy) => {
                this.pdf = pdf;
                if(callback !== undefined)callback();
            });
        }

        public getPage(pageNumber: number, callback?: Function) {
            if (this.pdf !== undefined && this.pdf !== null) {
                if (pageNumber > 0 && pageNumber <= this.pdf.numPages) {
                    if (this.pages[pageNumber] === undefined) {
                        this.pdf.getPage(pageNumber).then((page: PDFPageProxy) => {
                            this.pages[pageNumber] = page;
                            this.currentPage = pageNumber;
                            if (callback !== undefined) callback();
                        });
                    } else {
                        this.currentPage = pageNumber;
                        if (callback !== undefined) callback();
                    }
                }
            }
        }

        public renderPage(pageNumber: number, callback?: Function) {

            var viewport = this.pages[pageNumber].getViewport(this.scale);
            var context = this.canvas.getContext('2d');
            var renderContext = {
                canvasContext: context,
                viewport: viewport
            };

            this.pages[pageNumber].render(renderContext);
            this.updatePageNumber();
            if (callback !== undefined) callback();
        }
    }

}