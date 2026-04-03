
﻿function resetapplication() {
    $("#Document").val("") ;   
    $("#append-big-btn").val("");
}
function encodeImagetoBase64(element) {
    //debugger;
    var file = element.files[0];

    var reader = new FileReader();

    reader.onloadend = function () {

        $(".link").attr("href", reader.result);

        $(".link").text(reader.result);

    }

    reader.readAsDataURL(file);

}
check = function () {

    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementsByTagName("input");
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            var sFileNamesize = document.getElementById('file').files[0].size;;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions.length; j++) {
                    var sCurExtension = _validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                        // break;
                    }


                }


                if (!blnValid) {
                    oInput.value = "";
                    document.getElementById('append-big-btn').value = "";
                    showAlert("Sorry, file is invalid ");
                    return false;
                }
                else {
                    if (sFileNamesize > 512000) {
                        oInput.value = "";
                        document.getElementById('append-big-btn').value = "";
                        showAlert("File is too big!");
                        break;
                    }
                }

            }
            var fileSize = document.getElementById('file').files[0].size;


        }
    }

    return true;
}
function submitapplication() {
   
    var ID = $("#hid").val();
    //showAlert(ID);
    var Document = $("#Document").val();  
    var hfile = $("#hfile").val();
    var file = $("#file").val();
    var FileURl = '';
    var file1 = '';
    if ($("#file").val() == null) {
         FileURl = $("#hfile").val();

         file1 = $(".link").attr("href");
    }
    else {
        var str = $("#file").val().split('\\');
         FileURl = str.pop();
         file1 = $(".link").attr("href");
    }

    if (Document == "") {
        showAlert("Please Select Document Type");
         return;
    }
    if (ID== "0") {
        if (file== "") {
            showAlert("Please Upload Document");
            return;
        }
    }
    var obj = {
        ID: ID,
        DocumentType: Document,       
        FileName: FileURl,
        file: file1,
        hfile: hfile
    };
    //console.log(obj)
    showloader();
    $.ajax({
        url: "/Student/PG/DocumentSave",
        data: JSON.stringify(obj),
        enctype: 'multipart/form-data',
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            debugger;
            hideloader();
            showAlert(result);
               location.replace('/Student/PG/DocumentUpload');
        },
        error: function (errormessage) {
            hideloader();
            showAlert(errormessage.responseText);
        }
    });
}