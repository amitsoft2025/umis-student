function Add() {
    var month = 03;
    var ID = $("#hid").val();
    var current = $("#current").html();
    //var id = parseInt(ID) + 1;
    //showAlert(ID);
    var Session = current.split('-');
    var sess = [];
    var newSession = '';
    var year = '';
    for (var i = 0; i < Session.length; i++) {
        sess[i] = parseInt(Session[i]) + 1;
        // showAlert(sess[i]);   
        if (i < 1) {
            //debugger;
            newSession += sess[i].toString() + '-';
            year = sess[i].toString();
        }
        else {
            newSession += sess[i].toString();
        }

    }

    // showAlert(year);
    var obj = {
        Session: newSession,
        year: year,
        nID: ID
    };
    console.log(obj);
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = mm + '/' + dd + '/' + yyyy;
    console.log(today);
    debugger;

    if (yyyy == year  ) {
        if (mm == month) {
            $.ajax({
                type: "GET",
                url: "/Administrator/Home/AddNewSession",
                data: obj,
                dataType: "json",
                success: function (data) {
                    debugger;
                    if (data.status == true) {
                        $('#current').html(data.Session);
                        console.log(data);
                        showAlert(data.Msg);
                    }
                    else {
                        showAlert(data.Msg);
                    }
                },
                error: function () {
                    showAlert("error");
                }
            });
        }
        else {
            showAlert('Cannot add new session otherthan March Month');
        }
       
    }
    else {
        if (parseInt(yyyy) <= parseInt(year)) {
            showAlert('you have already updated new session for this year,Cannot add new session in the middle of current session');
        }
        else {
            showAlert('Cannot add new session in the middle of current session');
        }

    }
}