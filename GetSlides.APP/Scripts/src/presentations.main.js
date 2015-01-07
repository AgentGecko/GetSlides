var GetSlides;
(function (GetSlides) {
    GetSlides.selectedPresentationId;

    function enableFileUploader() {
        $("#file_uploader").change(function () {
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
    GetSlides.enableFileUploader = enableFileUploader;

    function uploadPdf(event) {
        var inputData = document.getElementById('file_uploader').files;
        var formData = new window.FormData($('form')[0]);
        var name = $("#upload_status").text();
        var newText = $("#file_uploader").val().split("\\")[2];
        console.log(inputData, formData, name, newText);
        var url = '/Presentation/upload/';
        GetSlides.router.uploadPdf(url, GetSlides.storage.getItem(GetSlides.storage.keys['auth']), formData, function (data) {
            var vdata = { "presentations": data };

            //updateViewModel(vdata);
            $("#upload_status").text("No file chosen.");
            $("#upload_btn").css("display", "none");
            location.reload();
        });
    }
    GetSlides.uploadPdf = uploadPdf;
})(GetSlides || (GetSlides = {}));
//# sourceMappingURL=presentations.main.js.map
