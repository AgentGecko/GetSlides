/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />
var GetSlides;
(function (GetSlides) {
    GetSlides.app = Sammy();
    GetSlides.router = new GetSlides.Router();
    GetSlides.storage = new GetSlides.Storage();
    GetSlides.pdfViewer;

    GetSlides.app.element_selector = '#pageContainer';

    GetSlides.app.get('#/', function (context) {
        context.partial('/Views/Presentation/Index.html', function (partial) {
        });
    });

    GetSlides.app.get('#/account/', function (context) {
        context.partial('/Views/Settings/Index.html', function (partial) {
        });
    });

    GetSlides.app.get('#/watch/:id/', function (context) {
        context.partial('/Views/Presentation/Watch/Index.html', function (partial) {
        });
    });

    GetSlides.app.get('#/present/:id/', function (context) {
        context.partial('/Views/Presentation/Present/Index.html', function (partial) {
        });
    });

    GetSlides.app.run('#/');
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=main.js.map
