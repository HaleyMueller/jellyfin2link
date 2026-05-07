// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ModalMaker(url, title, data, showImmediatly) {
    var modalID = makeid(8);

    $.ajax({
        url: url,
        type: 'GET',
        contentType: 'application/json',
        data: data,
        success: function (data) {
            var s = `<div class="modal fade" id="${modalID}" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h1 class="modal-title fs-5" id="exampleModalLabel">${title}</h1>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">`;
            s += data;
            s += `</div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                <button type="button" class="btn btn-primary" onclick="$('#${modalID} form').submit();">Save changes</button>
                            </div>
                        </div>
                    </div>
                </div>`;

            $("body").append(s);

            $(`#${modalID} form input[type="submit"]`).hide();
            $(`#${modalID} form button[type="submit"]`).hide();

            //$(`${modalID} form`).on('submit', function (e) {
            //    e.preventDefault();
            //});

            //$(`#${modalID} form`).submit(function (e) {
            //    e.preventDefault();
            //    SubmitFormV3(`#${modalID} form`, false, 'NULL');
            //});

            if (showImmediatly == true) {
                var aa = new bootstrap.Modal(document.getElementById(modalID), {});
                aa.show();
                console.log("ahh")
                //$(`#${modalID}`).addClass("show");
                //$(`#${modalID}`).show();
            }
            //$(id).html(s);
        },
        error: function (request, error) {
            alert("Request: " + JSON.stringify(request));
        }
    });



    return "#" + modalID;
}

function makeid(length) {
    let result = '';
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    const charactersLength = characters.length;
    let counter = 0;
    while (counter < length) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
        counter += 1;
    }
    return result;
}

function SubmitFormV3(formID, clearFields, successFunctionName) {
    var form = $(formID)[0];

    var hasAlertArray = [];
    HideAlerts(formID, hasAlertArray);

    var spinnerCheck = $(`${formID}-spinner`)[0];

    if (spinnerCheck == null) {
        var arp = `<div class="d-flex justify-content-center" id='${form.id}-spinner'>
                      <div class="spinner-border" role="status">
                        <span class="visually-hidden">Loading...</span>
                      </div>
                    </div>`;
        $(formID).append(arp);
    }

    $(`${formID}-spinner`).show();
    $(`${formID}-spinner`).removeClass("hidden");;

    $.ajax({
        url: form.action,
        type: form.method,
        data: new FormData(form),
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            $(`${formID}-spinner`).hide();
            $(`${formID}-spinner`).addClass("hidden");;
            $(formID).find($("[name$='_error']")).hide();

            $(formID).find($(":input")).removeClass("is-invalid");

            if (clearFields == true) { //THIS CAN CLEAR HIDDEN FIELDS
                $(formID).find($(":input")).val("");
            }

            $(formID).find($("[name$='_error']")).hide();

            //Process Alert Messages
            var errorMessage = "";
            var infoMessage = "";
            var successMessage = "";
            var warningMessage = "";
            result.notifications.forEach(function (i) {
                if (i.fieldName == "" && i.notificationType == 0) {
                    infoMessage = infoMessage + i.message + "\r\n";
                }
                if (i.fieldName == "" && i.notificationType == 1) {
                    warningMessage = warningMessage + i.message + "\r\n";
                }
                if (i.fieldName == "" && i.notificationType == 2) {
                    errorMessage = errorMessage + i.message + "\r\n";
                }
                if (i.fieldName == "" && i.notificationType == 3) {
                    successMessage = successMessage + i.message + "\r\n";
                }
            });

            if (infoMessage != "") {
                DisplayAlert(formID, "info", infoMessage);
                hasAlertArray.push("info");
            }
            if (warningMessage != "") {
                DisplayAlert(formID, "warning", warningMessage);
                hasAlertArray.push("warning");
            }
            if (errorMessage != "") {
                DisplayAlert(formID, "danger", errorMessage);
                hasAlertArray.push("danger");
            }
            if (successMessage != "") {
                DisplayAlert(formID, "success", successMessage);
                hasAlertArray.push("success");
            }

            HideAlerts(formID, hasAlertArray);

            //Process field errors
            if (result.hasError) {
                result.notifications.forEach(function (i) {
                    DisplayError(formID, i);
                });

                ShowRedStatus("An error has occurred.");
            } else {
                if (successFunctionName != "NULL") {
                    window[successFunctionName]();
                }

                ShowGreenStatus("Success!");
            }
        },
        error: function (result) {
            $(`${formID}-spinner`).hide();
            $(`${formID}-spinner`).addClass("hidden");;
            DisplayAlert(formID, "danger", 'Application is currently offline. Please try again later.');

            ShowRedStatus("Connection failed.");
        }
    });

    $(formID + ' [data-clear-onsubmit="true"]').val("");
}

function DisplayAlert(formID, bootstrapType, msg) {

    var alertCheck = $(formID + ` .alert-${bootstrapType}`)[0];

    if (alertCheck == null) {
        var alertHTML = `<div class="alert alert-${bootstrapType}" role="alert" style="display:none;"></div>`;
        $(formID).prepend(alertHTML);
    }

    var alert = $(formID + ` .alert-${bootstrapType}`);
    alert.html(msg);
    alert.show();
}

function HideAlerts(formID, hasAlertArray) {

    console.log('hiding alert array', hasAlertArray)

    $(formID + ` .alert`).each(function (i, e) {

        var hasAlert = false;
        for (var ii = 0; ii < hasAlertArray.length; ii++) {
            var check = $($(e)[0]).hasClass("alert-" + hasAlertArray[ii]);

            console.log(hasAlertArray[ii], $($(e)[0]), check);

            if (check == true) {
                hasAlert = true;
                console.log('hasAlert', $(e))
            }
        }

        if (hasAlert == false) {
            console.log('hiding', $(e))
            $(e).hide();
        }
    });
}

function DisplayError(formID, validationMessage) {
    console.log("display error", formID, validationMessage);

    if (validationMessage.fieldName == "" || validationMessage.fieldName == null) {

    } else {
        var alertCheck = $(formID + ` [name='${validationMessage.fieldName}_error']`)[0];
        if (alertCheck == null) {
            var alertHTML = `<span name="${validationMessage.fieldName}_error" style="display:none; color:red"></span>`;

            $(`${formID} [name='${validationMessage.fieldName}']`).parent().append(alertHTML);
        }

        $(formID + " [name='" + validationMessage.fieldName + "']").addClass('is-invalid');

        $(formID + ` [name='${validationMessage.fieldName}_error']`).html(validationMessage.message)
        $(formID + ` [name='${validationMessage.fieldName}_error']`).show();
    }
}

function ShowGreenStatus(message) {
    $("#success-message").text(message).show().delay(3000).slideUp(1000).fadeOut(10000);
}

function ShowRedStatus(message) {
    $("#error-message").text(message).show().delay(3000).slideUp(1000).fadeOut(10000);
}