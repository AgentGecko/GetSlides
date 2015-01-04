/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />

module GetSlides {
    var app: Sammy.Application = Sammy();

    app.get('#/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Index.html', (partial: any) => {

        });
    });

    app.get('#/About/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Present/Index.html', (partial: any) => {

        });
    });

    app.get('#/Watch/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Watch/Index.html', (partial: any) => {

        });
    });

    app.get('#/Account/', (context: Sammy.EventContext) => {
        context.partial('/Views/Settings/Index.html', (partial: any) => {

        });
    });

    app.run('#/');

}