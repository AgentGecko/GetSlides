﻿/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />
/// <reference path="../typings/knockout.mapping/knockout.mapping.d.ts" />
/// <reference path="pdf.main.ts" />
/// <reference path="storage.ts" />
/// <reference path="presentations.main.ts" />
/// <reference path="websockets.main.ts" />
/// <reference path="router.ts" />
/// <reference path="../typings/sammyjs/sammyjs.d.ts" />

module GetSlides {

    var $pageContainer = $("#pageContainer")[0];
    export var app: Sammy.Application = Sammy();
    export var router: Router = new Router();
    export var storage: Storage = new Storage();
    export var pdfViewer: PdfViewer;
    export var viewmodel: any = {};

    $(window).resize(() => {
        resizeCanvas();
    });

    (<any>app).element_selector = '#pageContainer';

    app.get('#/logout/', (context: Sammy.EventContext) => {
        console.log("LOGOUT");
        $("#navbar-username").text("Signed in as Anon");
        storage.setItem(storage.keys['auth'], null);
        location.href = '#/login/';
    });

    app.get('#/login/', (context: Sammy.EventContext) => {
        context.partial('/Views/Login/Index.html', (partial: any) => {
            loginPing();
            navbarToggle(false);
        });
    });

    app.get('#/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Index.html', (partial: any) => {
            ping();
            navbarToggle(true);
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
            navbarToggle(true);
        });
    });

    app.get('#/watch/connect/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Watch/Index.html', (partial: any) => {
            navbarToggle(false);
            resizeCanvas();
            pdfViewer = new PdfViewer(selectedPresentationUri, "canvas", false, "ws://localhost:6316/api/ws/watch/" + spin);
        });
    });

    app.get('#/watch/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Watch/InsertPin.html', (partial: any) => {
            ping();
            navbarToggle(true);
        });
    });

    app.get('#/present/:id/', (context: Sammy.EventContext) => {
        context.partial('/Views/Presentation/Present/Index.html', (partial: any) => {
            ping();
            navbarToggle(true);
            resizeCanvas();
            router.getJsonAuth("/presentation/get/" + context.params["id"], storage.getItem(storage.keys['auth']), (presentation) => {
                router.getJsonAuth("/account/username", storage.getItem(storage.keys['auth']), (username) => {
                    console.log(presentation, username);
                    pdfViewer = new PdfViewer(presentation.presentationUri, "canvas", true, "ws://localhost:6316/api/ws/present/"+ presentation.id + "/username/" + username, presentation.id);
                });
                
            });
        });
    });


    app.run('#/login/');


    export function navbarToggle(login: boolean) {
        if (login) {
            $("#myPresentations").css("display", "block");
            $("#watchanon").css("display", "none");
            $("#watchlogin").css("display", "block");
            $("#settings").css("display", "block");
        } else {
            $("#myPresentations").css("display", "none");
            $("#watchanon").css("display", "block");
            $("#watchlogin").css("display", "none");
            $("#settings").css("display", "none");
        }
    }

    export function resizeCanvas() {

        var canvas = (<any>document).getElementById("canvas");
        canvas.height = window.innerHeight -120;
        canvas.width = this.canvas.height * 27/ 19;
        $("#canvas").css("margin-left", ((window.innerWidth - this.canvas.width) / 2));
    }

    export function login(username: string, password: string) {
        router.getToken(username, password, (error, data) => {
            console.log(error, data);
            if (data.access_token !== undefined) {
                storage.setItem(storage.keys['auth'], data.token_type + " " + data.access_token);
                console.log(data.token_type + " " + data.access_token);
                location.href = '#/';
            } else {
                console.log(data.error_description);
            }
        });
    }

    export function loginPing(callback?: Function) {
        console.log("loginPing");
        if (storage.getItem(storage.keys['auth']) !== undefined) {
            console.log(storage.getItem(storage.keys['auth']));
            router.getJsonAuth("/account/ping", storage.getItem(storage.keys['auth']), (data) => {
                if (data === "Ok") {
                    router.getJsonAuth("/account/username", storage.getItem(storage.keys['auth']), (data) => {
                        $("#navbar-username").text("Signed in as " + data);
                        location.href = '#/';
                    });
                    console.log("loginPingOk");
                } else if (data !== "Ok") {
                    console.log("loginPingNotOk");
                }
            });
        } else {
            console.log("auth empty");
        }
    }

    export function ping(callback?: Function) {
        console.log("ping");
        if (storage.getItem(storage.keys['auth']) !== undefined) {
            console.log(storage.getItem(storage.keys['auth']));
            router.getJsonAuth("/account/ping", storage.getItem(storage.keys['auth']), (data) => {
                if (data === "Ok") {
                    console.log("pingOk");

                    router.getJsonAuth("/account/username", storage.getItem(storage.keys['auth']), (data) => {
                        $("#navbar-username").text("Signed in as " + data);
                    });
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