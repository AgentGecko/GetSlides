/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />

module GetSlides {


    export var app: Sammy.Application = Sammy();
    export var router: Router = new Router();
    export var storage: Storage = new Storage();
    export var pdfViewer: PdfViewer;

    (<any>app).element_selector = '#pageContainer';

    app.get('#/login', (context: Sammy.EventContext) => {
        context.partial('/Views/Login/Index.html', (partial: any) => {
            console.log("login");
            loginPing();
        });
    });

    app.get('#/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Index.html', (partial: any) => {
            ping();
        });
    });


    app.get('#/account/', (context: Sammy.EventContext) => {
        context.partial('/Views/Settings/Index.html', (partial: any) => {
            ping();
        });
    });

    app.get('#/watch/:id/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Watch/Index.html', (partial: any) => {
            ping();
        });
    });

    app.get('#/present/:id/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Present/Index.html', (partial: any) => {
            ping();
        });
    });


    app.run('#/login');

    export function login(username: string, password: string) {
        router.getToken(username, password, (error, data) => {
            console.log(error, data);
            storage.setItem(storage.keys['auth'], data.token_type + " " + data.access_token);
            console.log(data.token_type + " " + data.access_token);
            location.href = '#/';
        });
    }

    export function loginPing() {
        console.log("loginPing");
        if (storage[storage.keys['auth']] !== undefined) {
            console.log(storage.getItem(storage.keys['auth']));
            router.getJsonAuth("/account/ping", storage.getItem(storage.keys['auth']), (error, data) => {
                if (error === null) {
                    location.href = '#/';
                }
            });
        } else {
            console.log("auth empty");
        }
    }

    export function ping() {
        console.log("ping");
        if (storage.getItem(storage.keys['auth']) !== undefined) {
            console.log(storage.getItem(storage.keys['auth']));
            router.getJsonAuth("/account/ping", storage.getItem(storage.keys['auth']), (error, data) => {
                if (error !== null) {
                    console.log(error);
                    location.href = '#/login/';
                }
            });
        } else {
            console.log("auth empty");
        }
    }

}