/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />

module GetSlides {

    var $pageContainer = $("#pageContainer")[0];
    export var app: Sammy.Application = Sammy();
    export var router: Router = new Router();
    export var storage: Storage = new Storage();
    export var pdfViewer: PdfViewer;
    export var viewmodel: any = {};

    (<any>app).element_selector = '#pageContainer';

    app.get('#/logout/', (context: Sammy.EventContext) => {
        $("#navbar-username").text("Signed in as Anon");
        storage.setItem(storage.keys['auth'], null);
        location.href = '#/login/';
    });

    app.get('#/login/', (context: Sammy.EventContext) => {
        context.partial('/Views/Login/Index.html', (partial: any) => {
            loginPing();
            
        });
    });

    app.get('#/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Index.html', (partial: any) => {
            ping();
            router.getJsonAuth("/presentation/get", storage.getItem(storage.keys['auth']), (data) => {
                var vdata = { "presentations": data };
                updateViewModel(vdata);
                enableFileUploader();
            });
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

    app.get('#/watch/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Watch/InsertPin.html', (partial: any) => {
            ping();
        });
    });

    app.get('#/present/:id/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Present/Index.html', (partial: any) => {
            ping();
        });
    });


    app.run('#/login/');

    export function login(username: string, password: string) {
        router.getToken(username, password, (error, data) => {
            console.log(error, data);
            if (data.access_token !== undefined) {
                storage.setItem(storage.keys['auth'], data.token_type + " " + data.access_token);
                router.getJsonAuth("/account/username", storage.getItem(storage.keys['auth']), (data) => {
                    $("#navbar-username").text("Signed in as " + data);
                });
                console.log(data.token_type + " " + data.access_token);
                location.href = '#/';
            } else {
                console.log(data.error_description);
            }
        });
    }

    export function loginPing() {
        console.log("loginPing");
        if (storage.getItem(storage.keys['auth']) !== undefined) {
            console.log(storage.getItem(storage.keys['auth']));
            router.getJsonAuth("/account/ping", storage.getItem(storage.keys['auth']), (data) => {
                if (data === "Ok") {
                    console.log("loginPingOk");
                    location.href = '#/';
                } else if (data !== "Ok") {
                    console.log("loginPingNotOk");
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
            router.getJsonAuth("/account/ping", storage.getItem(storage.keys['auth']), (data) => {
                if (data === "Ok") {
                    console.log("pingOk");
                } else if (data !== "Ok") {
                    console.log("pingNotOk");
                    location.href = '#/login/';
                }
            });
        } else {
            console.log("auth empty");
        }
    }

    export function updateViewModel(data) {
        if(viewmodel.presentations != undefined)
        ko.cleanNode($pageContainer);
        viewmodel = ko.mapping.fromJS(data);
        ko.applyBindings(viewmodel, $pageContainer);
    }


}