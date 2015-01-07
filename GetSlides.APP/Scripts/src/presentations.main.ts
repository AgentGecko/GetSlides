module GetSlides {

    export var selectedPresentationId: number;

    export function enableFileUploader() {
        $("#file_uploader").change(() => {
            var newText = $("#file_uploader").val();
            newText = newText.split("\\")[2];
            if (newText.indexOf('.pdf') >= 0) {
                $("#upload_status").text(newText);
                $("#upload_btn").css("display", "inline");
            } else {
                $("#upload_status").text("Error: The format is not supported.");
            }
        });
    }

    export function uploadPdf(event) {
        
            var inputData = (<any>document.getElementById('file_uploader')).files;
            var formData = new (<any>window).FormData($('form')[0]);
            var name = $("#upload_status").text();
            var newText = $("#file_uploader").val().split("\\")[2];
            console.log(inputData, formData, name, newText);
            var url = '/Presentation/upload/';
            router.uploadPdf(url, storage.getItem(storage.keys['auth']), formData, (data) => {
                var vdata = { "presentations": data };
                //updateViewModel(vdata);
                $("#upload_status").text("No file chosen.");
                $("#upload_btn").css("display", "none");
                location.reload();
            });
    }

}



