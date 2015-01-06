/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />

module GetSlides {


    export var app: Sammy.Application = Sammy();
    export var router: Router = new Router();
    export var storage: Storage = new Storage();
    export var pdfViewer: PdfViewer;

    (<any>app).element_selector = '#pageContainer';


    app.get('#/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Index.html', (partial: any) => {

        });
    });


    app.get('#/account/', (context: Sammy.EventContext) => {
        context.partial('/Views/Settings/Index.html', (partial: any) => {

        });
    });

    app.get('#/watch/:id/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Watch/Index.html', (partial: any) => {

        });
    });

    app.get('#/present/:id/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Present/Index.html', (partial: any) => {

        });
    });


    app.run('#/');

}