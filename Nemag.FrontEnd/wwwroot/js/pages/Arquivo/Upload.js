function EfetuarArquivoUpload(input, progress, completeCallback) {
    //var inputArquivoLista = document.getElementById(input);

    //var fileLista = inputArquivoLista.files;

    var fileLista = $(input).prop('files');

    var formData = new FormData();

    formData.append("Token", loginToken);

    for (var i = 0; i < fileLista.length; i++) {
        formData.append("FormFileLista", fileLista[i]);
    }

    $.ajax(
        {
            url: apiUrl + "/Api/Arquivo/EfetuarArquivoUpload",
            data: formData,
            crossDomain: true,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) { },
            error: OnAjaxRequestError,
            complete: function (xhr, statusText) {
                OnAjaxComplete(xhr, completeCallback, undefined);
            },
            xhr: function () {
                var xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress",
                    function (evt) {
                        if (evt.lengthComputable) {
                            var progressValue = Math.round((evt.loaded / evt.total) * 100);

                            $(progress).css('width', progressValue + '%').attr('aria-valuenow', progressValue);
                        }
                    },
                    false);
                return xhr;
            }
        }
    );
}