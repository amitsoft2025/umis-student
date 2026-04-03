
$(document).ready(function () {


    toggleMarksCalculation();


    //DataTable
    //debugger;
    var view = false;
   
    if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm)
    {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: inline;");
        $('#Percentage').attr('readonly', true);
        
        $('#Perr').html('Please fill Total Paper Marks and Total Obtain Marks ,Aggregate Percentage will automatic Calculate');

    }
    else
    {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: none;");
        //$('#Percentage').removeAttr("readonly");
        $('#Percentage').attr('readonly', true);
        $('#Perr').html('Please fill Total Paper Marks and Total Obtain Marks ,Aggregate Percentage will automatic Calculate');
       // $('#Perr').html('');
    }
    $('.subper').change(function () {
        //$(".subper").each(function () {
            debugger;
           // showAlert($(this).val());
            if ($(this).val() == "0" || $(this).val() == "") {
            showAlert('Please Enter Subject Percentage!!!');
                $(this).val('');
                $(this).focus();
            return;
        }
            else if (parseInt($(this).val()) > 99) {
            showAlert('Please Enter Subject Percentage less then 99 !!!');
                $(this).val('');
                $(this).focus();
            return;
        }
        //});
       
    });

   
    $('.MarksObtain').change(function () {
        //$(".subper").each(function () {
        debugger;
        var tr = $(this).closest("tr");
       var tm= tr.find("#TotalMarks").val();
        if ($(this).val() == "0" || $(this).val() == "") {
            showAlert('Please Enter obtained Marks !!!');
            tr.find("#SubjectPercentage").val('');
            $(this).val('');
            $(this).focus();
            return;
        }
        else if (parseInt($(this).val()) > tm) {
            showAlert('Obtained Marks should be less then Total marks !!!');
            $(this).val('');
            tr.find("#SubjectPercentage").val('');
            $(this).focus();
            return;
        }
        else {
            var per = (parseInt($(this).val()) * 100) / tm;
            var percentage = per.toPrecision(4);
            tr.find("#SubjectPercentage").val(percentage);
        }
        //});
        var subper = $('input[name="MarksObtain"]').map(function () {
            return this.value
        }).get();
        var TotalMarks = $('input[name="TotalMarks"]').map(function () {
            return this.value
        }).get();
        var count = 0;
        var aggper = 0.0;
        var totalm = 0;
        var Subject = "";
        var SubjectID = $('select[name="SubjectID"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < SubjectID.length; i++) {
            if (SubjectID[i] != "") {
                Subject += SubjectID[i] + ',';
            }

        }
        if ($('#SubjectLLID').val() != "") {
            Subject = Subject  + $('#SubjectLLID').val();

        }
        if ($('#SubjectNRBID').val() != "") {
            Subject = Subject + ',' + $('#SubjectNRBID').val();

        }
        if ($('#SubjectNBID').val() != "") {
            Subject = Subject + ',' + $('#SubjectNBID').val();

        }
        if ($('#SubjectExtraID').val() != "") {
            Subject = Subject + ',' + $('#SubjectExtraID').val();

        }
        var sublist = Subject.split(',');
        var Bper = 0.0;
        var Mper = 0.0;
        for (var k = 0; k < sublist.length; k++)
        {
            if (sublist[k] == 12)
            {
                 Mper = subper[k];
            }
            if (sublist[k] == 33) {
                 Bper = subper[k];
            }
        }
        var Cper = 0;
        if (Bper > Mper)
        {
            Cper = 12;
        }
        else {
            Cper = 33;
        }

   
        if ($('#SubjectExtraID').val() != "") {
            var subj = $('#SubjectExtraID').val();

        }
        if ($('#Qualification').val() == sci)
        {

            //if ($('#SubjectExtraID').val() != undefined) {
            //    if ($('#SubjectExtraID').val() != "") {

            //        for (var i = 0; i < subper.length; i++)
            //        {

            //            if (subper[i] != "")
            //            {
            //                if (sublist[i] != Cper) {

            //                    aggper = aggper + parseFloat(subper[i]);
            //                    totalm = totalm + parseInt(TotalMarks[i]);
            //                    count++;
            //                }
            //                //debugger;


            //                //showAlert(aggper);

            //            }
            //        }
            //    }
            //    else {

            //        for (var i = 0; i < 6; i++) {
            //            if (subper[i] != "") {
            //                //debugger;
            //                count++;
            //                aggper = aggper + parseFloat(subper[i]);
            //                totalm = totalm + parseInt(TotalMarks[i]);
            //                //showAlert(aggper);

            //            }
            //        }
            //    }
            //}
            //else {

            //    for (var i = 0; i < 6; i++) {
            //        if (subper[i] != "") {
            //            //debugger;
            //            count++;
            //            aggper = aggper + parseFloat(subper[i]);
            //            totalm = totalm + parseInt(TotalMarks[i]);
            //            //showAlert(aggper);

            //        }
            //    }
            //}

           



        }
        else
        {
            //for (var i = 0; i < 6; i++) {
            //    if (subper[i] != "") {
            //        //debugger;
            //        count++;
            //        aggper = aggper + parseFloat(subper[i]);
            //        totalm = totalm + parseInt(TotalMarks[i]);
            //        //showAlert(aggper);

            //    }
            //}
        }
       
        //var final = aggper*100 / totalm;
        //$('#Percentage').val(final.toPrecision(4));
       
      

    });
    
    $('#Qualification').change(function () {
        if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm) {
            $('#paperTotalMarks').attr('readonly', true);
            $('#paperTotalMarks').val('500');
        }
        else {
            //$('#paperTotalMarks').attr('readonly', true);
            //$('#paperTotalMarks').val('600');
		    $('#paperTotalMarks').removeAttr("readonly");//.attr('readonly', true);
            $('#paperTotalMarks').val('');
       
        }
       
        debugger;
        var res = $('#Qualification').val();
        if (res == "")
            res = 0;
        $.ajax({
            url: "/Student/Home/SubjectTable/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {

                console.log(result);
                if (result) {
                    //showAlert(result);
                    view = true;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: inline;");
                    $('#Percentage').attr('readonly', true);
                   // $('#Perr').html('Please fill subject detail ,Aggregate  Percentage will automatic Calculate');
                    $('#Perr').html('Please fill Total Paper Marks and Total Obtain Marks ,Aggregate Percentage will automatic Calculate');

                }
                else {
                    view = false;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: none;");
                    // $('#Percentage').removeAttr("readonly");
                    $('#Percentage').attr('readonly', true);
                   
                   // $('#Perr').html(' ');
                    $('#Perr').html('Please fill Total Paper Marks and Total Obtain Marks ,Aggregate Percentage will automatic Calculate');

                }
               
            }
        });

   });
   $('#boardtype').change(function () {
       //debugger;
       if ($('#boardtype').val() == 1)
       {
           $('#UniversityName').val('Bihar Board (BSEB  Board)');
       }
       else
       {
           $('#UniversityName').val('');
       }
   });
    $('.Subject').change(function () {

        debugger;
        var id = $('#Qualification').val();
        var res = $(this).val();
        //showAlert(res);
        var alltr = $(this).closest('tr').nextAll('tr').find('.Subject').html('<option value=' + "" + '>' + "--Select Subject--" + '</option>');
      
        var tr = $(this).closest("tr");
        var str = '';
        var SubjectID = $('select[name="SubjectID"]').map(function () {

            return this.value
        }).get();
        for (var i = 0; i < SubjectID.length; i++) {
            if (SubjectID[i] != "") {
                str += SubjectID[i] + ',';
            }
           
        }
        //showAlert(str);
        //tr.find('.Subject').find("option").remove();
        //tr.find('.Subject').append($("<option></option>").val("").html("Select Subject"));
        //tr.find('#compulsory1 ').find("option").remove();
        //tr.find('#compulsory1 ').append($("<option></option>").val("").html("--Select compulsory1--"));
        showloader();
      
        $.ajax({
            url: "/Student/Home/Subject_bindDanamic/",
            data: { id: id, res: str },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
               // hideloader();
                //debugger;
           
                    var str = '';
                    str += '<option value=' + "" + '>' + "--Select Subject--" + '</option>';
                for (var i = 0; i < result.length; i++) {

                    str += '<option value=' + result[i].ID + '>' + result[i].SubjectName + '</option>';
                    //tr.next('tr').find('.Subject ').html($("<option     />").val(result[i].ID).text(result[i].SubjectName));
                   
                    }
                tr.next('tr').find('.Subject ').html(str);
                   
                hideloader();

            },
            error: function (errormessage) {
                hideloader();
                showAlert(errormessage.responseText);
            }
            
        });
        
    });
    $('.Subject').change(function () {

        //debugger;
        var id = $('#Qualification').val();
        var res = $(this).val();
        
        var str = '';
        var SubjectID = $('select[name="SubjectID"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < SubjectID.length; i++) {
            if (SubjectID[i] != "") {
                str += SubjectID[i] + ',';
            }

        }      
        showloader();

        $.ajax({
            url: "/Student/Home/Subject_bindDanamic/",
            data: { id: id, res: str },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
               
                //debugger;

                var str = '';
                str += '<option value=' + "" + '>' + "--Select Extra Subject--" + '</option>';
                for (var i = 0; i < result.length; i++) {
                    str += '<option value=' + result[i].ID + '>' + result[i].SubjectName + '</option>';                   

                }
                $('#SubjectExtraID ').html(str);
                hideloader();

            },
            error: function (errormessage) {
                hideloader();
                showAlert(errormessage.responseText);
            }

        });

    });
});

function toggleMarksCalculation() {
    var selectedType = document.getElementById("marksType").value;
    var label = document.getElementById("obtainMarksLabel");
    var input = document.getElementById("paperMarksObtain");
    var totalMarksDiv = document.getElementById("totalMarksSection"); // ⬅️ wrap total marks input in this div

    if (selectedType === "cgpa") {
        label.innerHTML = "Enter CGPA <span class='starvalid'>*</span>";
        input.placeholder = "Enter CGPA";
        input.step = "0.01";
        input.min = "0";
        input.max = "10";
        input.type = "number";

        if (totalMarksDiv) totalMarksDiv.style.display = "none";
    } else if (selectedType === "marks") {
        label.innerHTML = "Enter Obtained Marks <span class='starvalid'>*</span>";
        input.placeholder = "Enter Marks";
        input.step = "1";
        input.min = "0";
        input.removeAttribute("max");
        input.type = "number";

        if (totalMarksDiv) totalMarksDiv.style.display = "block";
    } else {
        label.innerHTML = "Enter Value <span class='starvalid'>*</span>";
        input.placeholder = "Enter Value";
        if (totalMarksDiv) totalMarksDiv.style.display = "block";
    }

    // Clear percentage field
    document.getElementById("Percentage").value = "";
}



function toggleMarksType() {
    var marksType = $("#marksType").val();
    if (marksType === "cgpa") {
        $("#totalMarksDiv").hide(); // agar CGPA me total marks nahi chahiye
        $("#obtainMarksLabel").text("Enter CGPA");
    } else {
        $("#totalMarksDiv").show();
        $("#obtainMarksLabel").text("Enter Obtained Marks");
    }

    $("#paperMarksObtain").val("");
    $("#paperTotalMarks").val("");
    $("#Percentage").val("");
}



function percheck() {
    if (parseInt($("#Percentage").val()) > 99)
    {
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
function percheck1() {
    var type = $("#marksType").val();
    var paperTotalMarks = $("#paperTotalMarks").val();
    var paperMarksObtain = $("#paperMarksObtain").val();
    var Percentage = 0;

    if (type === "marks") {
        if (parseInt(paperMarksObtain) > parseInt(paperTotalMarks)) {
            showAlert("Obtain Marks should not exceed Total Marks!");
            $("#paperMarksObtain").val("");
            $("#Percentage").val("");
            return;
        }
        Percentage = (paperMarksObtain * 100.0 / paperTotalMarks);
    } else if (type === "cgpa") {
        if (parseFloat(paperMarksObtain) > 10) {
            showAlert("CGPA should not exceed 10.");
            $("#paperMarksObtain").val("");
            $("#Percentage").val("");
            return;
        }
        Percentage = parseFloat(paperMarksObtain) * 9.5;
    }

    if (!isNaN(Percentage)) {
        $("#Percentage").val(parseFloat(Percentage).toFixed(2));
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
    $("#paperMarksObtain ").val("");
    $("#paperTotalMarks ").val("");
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
    //debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png",".pdf"];
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


                        // break;
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
    var paperMarksObtain = $("#paperMarksObtain ").val();
    var paperTotalMarks = $("#paperTotalMarks ").val();
    var file1 = $("#file").val();
    var RollNo = $("#RollNo").val();
    var boardtype = $("#boardtype").val();
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
    if (boardtype == "")
    {
        showAlert('Please Select Board Type  !!');
        return;
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
    //if ($('#Qualification').val() != art || $('#Qualification').val() != sci || $('#Qualification').val() != comm)
    //{
    //    //debugger;
    //    if (Percentage == "0" || Percentage == "") {

    //        //$('#Perr').html("Please Enter percentage  !!");
    //        showAlert('Please Enter Percentage  !!');
    //        return;
    //    }
    //}

    if (PassingYear == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        showAlert('Please select  Passing Year  !!');
        return;
    }
    //if (paperTotalMarks == "") {

    //    //$('#Berr').html("Please select a Qualification  !!");
    //    showAlert('Please Enter Total Paper Marks  !!');
    //    return;
    //}
    if (paperMarksObtain == "") {

        //$('#Berr').html("Please select a Qualification  !!");
        showAlert('Please Enter Total Obtain Marks  !!');
        return;
    }
    

   
    //if (parseInt($("#Percentage").val()) > 100) {
        
    //    showAlert('Aggregate  Percentage should be less then 100 ');
    //    // $("#btn").attr("style", "display: none;");
    //    return;
    //}
    if (parseInt($("#Percentage").val()) < 0)
    {

        showAlert('Aggregate  Percentage should be greater than 0 ');
         return;
    }
    if (Percentage == "0" || Percentage == "") {

                //$('#Perr').html("Please Enter percentage  !!");
                showAlert('Please Enter Percentage  !!');
                return;
     }
    //console.log(obj);
    if ($('#Qualification').val() != 1)
    {
        var subper = $('input[name="SubjectPercentage"]').map(function () {
            return this.value
        }).get();
        var SubjectID = "";
        var SubjectID11 = $('select[name="SubjectID"]').map(function () {

            return this.value
        }).get();
        for (var i = 0; i < SubjectID11.length; i++)
        {
            var j = i + 1;
            if (j == 1)
                str1 = "First";
            else if (j == 2)
                str1 = "Second";
            else if (j == 3)
                str1 = "Third";

            if (SubjectID11[i] != "") {
                SubjectID += SubjectID11[i] + ',';
            }
            else {

                showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
                return;

            }


        }
        if ($('#SubjectLLID').val() != "") {
            SubjectID = SubjectID + $('#SubjectLLID').val() + ",";

        }
        else {
            showAlert('Please select  LL Subject !!');
            return;
        }
        if ($('#SubjectNRBID').val() != "") {
            SubjectID = SubjectID + $('#SubjectNRBID').val() + ',';

        }
        else {
            showAlert('Please select  NRB Subject !!');
            return;
        }
        if ($('#SubjectNBID').val() != "") {
            SubjectID = SubjectID + $('#SubjectNBID').val();

        }
        else {
            showAlert('Please select  MB Subject !!');
            return;
        }
        if ($('#SubjectExtraID').val() != undefined)
        {
            if ($('#SubjectExtraID').val() != "") {
                SubjectID = SubjectID + ',' + $('#SubjectExtraID').val();
            }
        }
        var SubID = $('input[name="ID"]').map(function () {

            return this.value
        }).get();
        var TotalMarks = $('input[name="TotalMarks"]').map(function () {

            return this.value
        }).get();
        var MarksObtain = $('input[name="MarksObtain"]').map(function () {

            return this.value
        }).get();
        var no = $('input[name="SubjectPercentage"]');
        var listObj = [];
        var view;
        // debugger;

        var sublist = SubjectID.split(',');
        if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm)
        {
            view = true;
            var str1 = '';

            if ($('#SubjectExtraID').val() != undefined)
            {
                if ($('#SubjectExtraID').val() != "") {
                    for (var i = 0; i < sublist.length ; i++) {
                        var j = i + 1;
                        if (j == 1)
                            str1 = "First";
                        else if (j == 2)
                            str1 = "Second";
                        else if (j == 3)
                            str1 = "Third";
                        else if (j == 4)
                            str1 = "Fourth";
                        else if (j == 5)
                            str1 = "Fifth";
                        else if (j == 6)
                            str1 = "Sixth";
                        else if (j == 7)
                            str1 = "Seventh";
                        if (SubjectID11[i] == "") {
                            showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
                            return;
                        }
                        if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
                            showAlert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
                            return;
                        }
                        if (MarksObtain[i] == "" || MarksObtain[i] == "0") {
                            showAlert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
                            return;
                        }
                    }
                }
                else {
                    for (var i = 0; i < sublist.length ; i++) {
                        var j = i + 1;
                        if (j == 1)
                            str1 = "First";
                        else if (j == 2)
                            str1 = "Second";
                        else if (j == 3)
                            str1 = "Third";
                        else if (j == 4)
                            str1 = "Fourth";
                        else if (j == 5)
                            str1 = "Fifth";
                        else if (j == 6)
                            str1 = "Sixth";

                        if (SubjectID11[i] == "") {
                            showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
                            return;
                        }
                        if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
                            showAlert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
                            return;
                        }
                        if (MarksObtain[i] == "" || MarksObtain[i] == "0") {
                            showAlert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
                            return;
                        }
                    }
                }
            }
            else {
                for (var i = 0; i < sublist.length ; i++) {
                    var j = i + 1;
                    if (j == 1)
                        str1 = "First";
                    else if (j == 2)
                        str1 = "Second";
                    else if (j == 3)
                        str1 = "Third";
                    else if (j == 4)
                        str1 = "Fourth";
                    else if (j == 5)
                        str1 = "Fifth";
                    else if (j == 6)
                        str1 = "Sixth";

                    if (SubjectID11[i] == "") {
                        showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
                        return;
                    }
                    if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
                        showAlert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
                        return;
                    }
                    if (MarksObtain[i] == "" || MarksObtain[i] == "0") {
                        showAlert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
                        return;
                    }
                }
            }


        }
    }
    res = 0;


    //showloader();

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
        fileData.append('boardtype', boardtype);
        fileData.append('RollNo', RollNo);
        fileData.append('EncriptedID', ID);
        fileData.append('sublist', sublist);
        fileData.append('subper', subper);
        fileData.append('TotalMarks', TotalMarks);
        fileData.append('MarksObtain', MarksObtain);
        fileData.append('SubID', SubID);
        fileData.append('paperTotalMarks', $("#paperTotalMarks").val());
        fileData.append('paperMarksObtain', $("#paperMarksObtain").val());
        fileData.append('marksType', $("#marksType").val());
        //return;
        showloader();
        $.ajax({
            url: '/Student/Home/AddNewQualification',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {

                // console.log(result);
                if (result.Status == true) {
                    //debugger;
                    hideloader();
                    showAlert(result.Msg);
                    //window.location.reload();
                    window.location = '/Student/Home/PreviousYearQualification';
                  
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
//function submitapplication() {
//   debugger;
//    var ID = $("#hid").val();
//    //showAlert(ID);
//    var Qualification = $("#Qualification").val();
//    var UniversityName = $("#UniversityName").val();
//    var Percentage = $("#Percentage").val();
//    var PassingYear = $("#PassingYear").val();
//    var hfile = $("#hfile").val();
//    var file1 = $("#file").val();
//    var RollNo = $("#RollNo").val();
//    var FileURl;
//    var file;
//    if ($("#file").val() == null) {

//        FileURl = $("#hfile").val();

//        file = $(".link").attr("href");
//    }
//    else {
//        var str = $("#file").val().split('\\');
//        FileURl = str.pop();
//        file = $(".link").attr("href");
//    }


//    //showAlert(file);
//    if (Qualification == "") {

//        //$('#Berr').html("Please select a Qualification  !!");
//        showAlert('Please select Qualification  !!');
//        return;
//    }
//    if (ID == "0") {
//        if (file1 == "") {
//            showAlert("Please Upload Document");
//            return;
//        }
//    }
//    if (UniversityName == "") {

//        //$('#Uerr').html("Please Enter University Name  !!");
//        showAlert('Please Enter Board/University Name  !!');
//        return;
//    }
//    if (RollNo == "") {

//        // $('#Yerr').html("Please select a Passing year  !!");
//        showAlert('Please Enter  Roll Number  !!');
//        return;
//    }
//    if ($('#Qualification').val() != art || $('#Qualification').val() != sci || $('#Qualification').val() != comm) {
//        //debugger;
//        if (Percentage == "0" || Percentage == "") {

//            //$('#Perr').html("Please Enter percentage  !!");
//            showAlert('Please Enter Percentage  !!');
//            return;
//        }
//    }
  
//    if (PassingYear == "") {

//        // $('#Yerr').html("Please select a Passing year  !!");
//        showAlert('Please select  Passing Year  !!');
//        return;
//    }
    
    
//    var obj = {
//        ID: ID,
//        QualicationType: Qualification,
//        Board_UniversityName: UniversityName,
//        Percentage: Percentage,
//        PassingYear: PassingYear,
//        FileURl: FileURl,
//        file: file,
//        hfile: hfile,
//        RollNo: RollNo,
//        EncriptedID:ID
//    };
//    //console.log(obj);
//    if ($('#Qualification').val() != 1)
//    {
//        var subper = $('input[name="SubjectPercentage"]').map(function () {
//            return this.value
//        }).get();
//        var SubjectID = "";
//        var SubjectID11 = $('select[name="SubjectID"]').map(function () {

//            return this.value
//        }).get();
//        for (var i = 0; i < SubjectID11.length; i++)
//        {
//            var j = i + 1;
//            if (j == 1)
//                str1 = "First";
//            else if (j == 2)
//                str1 = "Second";
//            else if (j == 3)
//                str1 = "Third";
           
//            if (SubjectID11[i] != "")
//            {
//                SubjectID += SubjectID11[i] + ',';
//            }
//            else
//            {
                
//                    showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
//                    return;
                
//            }


//        }
//        if ($('#SubjectLLID').val() != "")
//        {
//            SubjectID = SubjectID + $('#SubjectLLID').val()+",";

//        }
//        else {
//            showAlert('Please select  LL Subject !!');
//            return;
//        }
//        if ($('#SubjectNRBID').val() != "") {
//            SubjectID = SubjectID  + $('#SubjectNRBID').val()+ ',';

//        }
//        else {
//            showAlert('Please select  NRB Subject !!');
//            return;
//        }
//        if ($('#SubjectNBID').val() != "") {
//            SubjectID = SubjectID  + $('#SubjectNBID').val();

//        }
//        else
//        {
//            showAlert('Please select  MB Subject !!');
//            return;
//        }
//        if ($('#SubjectExtraID').val() != undefined)
//        {
//            if ($('#SubjectExtraID').val() != "")
//            {
//                SubjectID = SubjectID+',' + $('#SubjectExtraID').val() ;
//            }
//        }



//        var SubID = $('input[name="ID"]').map(function () {

//            return this.value
//        }).get();
//        var TotalMarks = $('input[name="TotalMarks"]').map(function () {

//            return this.value
//        }).get();
//        var MarksObtain = $('input[name="MarksObtain"]').map(function () {

//            return this.value
//        }).get();
//        var no = $('input[name="SubjectPercentage"]');
//        var listObj = [];
//        var view;
//        debugger;

//        var sublist = SubjectID.split(',');
//        if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm)
//        {
//            view = true;
//            var str1 = '';
          
//            if ($('#SubjectExtraID').val() != undefined )
//            {
//                if ($('#SubjectExtraID').val() != "")
//                {
//                    for (var i = 0; i < sublist.length ; i++)
//                    {
//                        var j = i + 1;
//                        if (j == 1)
//                            str1 = "First";
//                        else if (j == 2)
//                            str1 = "Second";
//                        else if (j == 3)
//                            str1 = "Third";
//                        else if (j == 4)
//                            str1 = "Fourth";
//                        else if (j == 5)
//                            str1 = "Fifth";
//                        else if (j == 6)
//                            str1 = "Sixth";
//                        else if (j == 7)
//                            str1 = "Seventh";
//                        if (SubjectID11[i] == "")
//                        {
//                            showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
//                            return;
//                        }
//                        if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
//                            showAlert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
//                            return;
//                        }
//                        if (MarksObtain[i] == "" || MarksObtain[i] == "0")
//                        {
//                            showAlert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
//                            return;
//                        }
//                    }
//                }
//                else
//                {
//                    for (var i = 0; i < sublist.length ; i++)
//                    {
//                        var j = i + 1;
//                        if (j == 1)
//                            str1 = "First";
//                        else if (j == 2)
//                            str1 = "Second";
//                        else if (j == 3)
//                            str1 = "Third";
//                        else if (j == 4)
//                            str1 = "Fourth";
//                        else if (j == 5)
//                            str1 = "Fifth";
//                        else if (j == 6)
//                            str1 = "Sixth";

//                        if (SubjectID11[i] == "") {
//                            showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
//                            return;
//                        }
//                        if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
//                            showAlert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
//                            return;
//                        }
//                        if (MarksObtain[i] == "" || MarksObtain[i] == "0") {
//                            showAlert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
//                            return;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                for (var i = 0; i < sublist.length ; i++) {
//                    var j = i + 1;
//                    if (j == 1)
//                        str1 = "First";
//                    else if (j == 2)
//                        str1 = "Second";
//                    else if (j == 3)
//                        str1 = "Third";
//                    else if (j == 4)
//                        str1 = "Fourth";
//                    else if (j == 5)
//                        str1 = "Fifth";
//                    else if (j == 6)
//                        str1 = "Sixth";
                    
//                    if (SubjectID11[i] == "") {
//                        showAlert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
//                        return;
//                    }
//                    if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
//                        showAlert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
//                        return;
//                    }
//                    if (MarksObtain[i] == "" || MarksObtain[i] == "0") {
//                        showAlert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
//                        return;
//                    }
//                }
//            }


//        }
//    }
//        res = 0;
  
   
//    showloader();

//    $.ajax({
//        url: "/Student/Home/AddNewQualification",
//        data: JSON.stringify(obj),
//        type: "POST",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (result) {
//            //console.log(result);
//            hideloader();
//            if (result.Status) {
//                if (view)
//                {
//                    //if ($('#SubjectExtraID').val() != "") {

//                        for (var i = 0; i < sublist.length; i++) {

//                            debugger;
//                            //console.log(sublist[i]);
//                            var obj1 = {

//                                ID: SubID[i],
//                                QualificationMasterID: result.ScopeIdentity,
//                                SubjectID: sublist[i],
//                                SubjectPercentage: subper[i],
//                                TotalMarks: TotalMarks[i],
//                                MarksObtain: MarksObtain[i]
//                            };
//                            listObj.push(obj1);
//                        }
//                    //}
//                    //else {
//                    //    for (var i = 0; i < sublist.length-1; i++) {

//                    //        debugger;
//                    //        //console.log(sublist[i]);
//                    //        var obj1 = {

//                    //            ID: SubID[i],
//                    //            QualificationMasterID: result.ScopeIdentity,
//                    //            SubjectID: sublist[i],
//                    //            SubjectPercentage: subper[i],
//                    //            TotalMarks: TotalMarks[i],
//                    //            MarksObtain: MarksObtain[i]
//                    //        };
//                    //        listObj.push(obj1);
//                    //    }
//                    //}
//                    showloader();
//                    debugger;
//                    $.ajax({
//                        url: "/Student/Home/SubjectDetails",
//                        data: JSON.stringify(listObj),
//                        type: "POST",
//                        contentType: "application/json;charset=utf-8",
//                        dataType: "json",
//                        success: function (res) {
//                            debugger;
//                            hideloader();
//                            if (res == 1) {
//                                if (SubID.length > 0) {
                                    
//                                    showAlert(" Subject Details  Updated successfully");
//                                }
//                                else {

//                                    showAlert(" Subject Details added successfully");
//                                }
//                                showloader();
//                                location.replace('/Student/Home/PreviousyearQualification');
//                            }
//                            else {
//                                showAlert("Could not added Subject Details ");
//                            }
//                        },
//                        error: function (errormessage) {
//                            hideloader();
//                            showAlert(errormessage.responseText);
//                        }
//                    });
//                }
//                else {
//                    showAlert(result.Msg);
//                    hideloader();
//                    location.replace('/Student/Home/PreviousyearQualification');
//                }
//            }
//            else {
//                hideloader();
//                showAlert(result.Msg);
//               // location.replace('/Student/Home/StudentQualification');
//            }
//            //location.replace('/Student/Home/PreviousyearQualification');
//            // window.location.reload();

//        },
//        error: function (errormessage) {
            
//            hideloader();
//            showAlert(errormessage.responseText);
//        }


//    });
    
   
//}

