
$(document).ready(function () {

});


function percheck() {
    if (parseInt($("#Percentage").val()) > 99) {
        $("#Percentage").val("");
        showAlert('Percentage should be less then 100 ');
        // $("#btn").attr("style", "display: none;");
        return;
    }
    else {
        $('#btn').removeAttr("style");
        $("#btn").attr("style", "display: inline;");
    }
    if (parseInt($("#Percentage").val()) <= 0) {
        $("#Percentage").val("");
        showAlert('Percentage should be greater then 0 ');

        return;
    }


}

function Onlynumericvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9.]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}
function OnlyIntvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}


function resetapplication() {
    $("#Qualification").val("");
    $("#UniversityName").val("");
    $("#Percentage").val("");
    $("#PassingYear").val("");
    $("#file").val("");
    $("#RollNo").val("");
    $("#append-big-btn").val("");
}

check = function () {
    debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".pdf"];
    var arrInputs = document.getElementsByTagName("input");
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            var sFileNamesize = document.getElementById('file').files[0].size;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions.length; j++) {
                    var sCurExtension = _validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                    }
                }
                if (!blnValid) {
                    oInput.value = "";
                    document.getElementById("append-big-btn").value = "";
                    showAlert("Sorry, file is invalid ");
                    return false;
                }
                else {
                    if (sFileNamesize > 500000) {
                        oInput.value = "";
                        document.getElementById("append-big-btn").value = "";
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
    debugger;
    var ID = $("#hid").val();
    //showAlert(ID);
    var Qualification = $("#Qualification").val();
    var UniversityName = $("#UniversityName").val();
    var Percentage = $("#Percentage").val();
    var PassingYear = $("#PassingYear").val();
    var hfile = $("#hfile").val();
    var file1 = $("#file").val();
    var RollNo = $("#RollNo").val();
    var FileURl;
    var file;
    if ($("#file").val() == null) {

        FileURl = $("#hfile").val();

        file = $(".link").attr("href");
    }
    else {
        var str = $("#file").val().split('\\');
        FileURl = str.pop();
        file = $(".link").attr("href");
    }


    //showAlert(file);
    if (Qualification == "") {

        //$('#Berr').html("Please select a Qualification  !!");
        showAlert('Please select Qualification  !!');
        return;
    }
    if (ID == "0") {
        if (file1 == "") {
            showAlert("Please Upload Document");
            return;
        }
    }
    if (UniversityName == "") {

        //$('#Uerr').html("Please Enter University Name  !!");
        showAlert('Please Enter Board/University Name  !!');
        return;
    }
    if (RollNo == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        showAlert('Please Enter  Roll Number  !!');
        return;
    }
    if ($('#Qualification').val() != art || $('#Qualification').val() != sci || $('#Qualification').val() != comm) {
        //debugger;
        if (Percentage == "0" || Percentage == "") {

            //$('#Perr').html("Please Enter percentage  !!");
            showAlert('Please Enter Percentage  !!');
            return;
        }
    }

    if (PassingYear == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        showAlert('Please select  Passing Year  !!');
        return;
    }
    res = 0;
    showloader();
    var EnID = $('#EnID').val();
    if (window.FormData !== undefined) {
        var fileUpload = $("#file").get(0);
        var files = fileUpload.files;
        var fileData = new FormData();
        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                fileData.append("file", files[i]);
            }
        }
        else {
            //showAlert($('#hfile').val());
            fileData.append("hfile", $('#hfile').val());

        }

        // debugger;
        fileData.append('ID', ID);
        fileData.append('Qualification', Qualification);
        fileData.append('UniversityName', UniversityName);
        fileData.append('Percentage', Percentage);
        fileData.append('PassingYear', PassingYear);
        fileData.append('RollNo', RollNo);

        showloader();
        $.ajax({
            url: '/StudentBEd/HomeB/AddNewQualification',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {

                // console.log(result);
                if (result.Status == true) {

                    hideloader();
                    showAlert(result.Msg);
                    //window.location.reload();
                    //window.location = '/College/Home/PreviousYearQualificationManualAd/?id=' + EnID;
                    location.replace('/StudentBEd/HomeB/PreviousyearQualificationO');
                }
                else {

                    showAlert(result.Msg);
                    hideloader();
                }
                //return false;
            },
            error: function (err) {
                // debugger;
                showAlert(err.statusText);
                hideloader();
                return false;
            }
        });
    }
    else {
        showAlert("FormData is not supported.");

        return false;
    }


}