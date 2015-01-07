module GetSlides {

    export var selectedPresentationUri: string;
    export var spin: string;

    export function getUrl(pin: string) {
        router.getJson("/ws/geturi/" + pin, (url) => {
            if (url !== "Inactive pin, please try again.") {
                GetSlides.selectedPresentationUri = url;
                GetSlides.spin = pin;
                location.href = '#/watch/connect/';
            } else {
                console.log(url);
            }
        });
    }
    
} 