$(function () {

    var rootUrl = window.location.protocol + '//' + window.location.host;

    var model = {
        PolicyNumber: null,
        EffectiveDate: null,
        ExpireDate: null,
        FirstName: null,
        LastName: null,
        Street: null,
        City: null,
        State: null,
        Zip: null,
        SelectedRiskConstruction: null,
        YearBuilt:  null,
        BuildingStreet: null,
        BuildingCity: null,
        BuildingState: null,
        BuildingZip: null
    };

    $("#addPolicyFormModal").dialog({
        autoOpen: false,
        modal: true,
        width: 650,
        height: 600,
        open: function (event, ui) {
            $('#addPolicyFormModal').css('overflow', 'hidden');
        },
        show: {
            effect: "blind",
            duration: 1000
        },
        buttons: [
            {
                id: "cancelAddBtn",
                text: "Cancel",
                click: function () {
                    $('#addPolicyFormModal').dialog('close');
                }
            },
            {
                id: "submitAddBtn",
                text: "Submit",
                click: addPolicy
            }
        ]
    });

    function addPolicy() {
        $("#cancelAddBtn").button("disable");
        $("#submitAddBtn").button("disable");

        var $formFields = $(":text");
        var valid = true;

        model.SelectedRiskConstruction = $("#SelectedRiskConstruction option:selected").val();

        $.each($formFields, function (_, item) {
            model[item.name] = item.value;

            var itemValid = validate($(item));
            valid = valid && itemValid;

            if (!itemValid) {
                $(item).addClass("ui-state-error");
            } else {
                $(item).removeClass("ui-state-error");
            }
        });

        if (valid) {
            $.ajax(
                {
                    url: rootUrl + '/InsurancePolicy/Add',
                    type: "POST",
                    data: JSON.stringify(model),
                    processData: false,
                    contentType: "application/json",
                    success: function (data, textStatus, jqXHR) {
                        var result = JSON.parse(data);
                        if (result.HasError) {
                            handleError(result.ErrorMessage);
                        } else {
                            $('#addPolicyFormModal').dialog('close');
                            location.reload();
                        }

                        enableFormButtons();
                    },
                    error: function (jqXHR, error) {
                        handleError(jqXHR.status, jqXHR.responseText);
                        enableFormButtons();
                    }
                });
        } else {
            enableFormButtons();
        }
        return valid;
    }

    function handleError(incoming, errorMessage) {
        var msg = '';
        if (incoming === 0) {
            msg = 'Not connect.\n Verify Network.';
        } else if (incoming === 404) {
            msg = 'Requested page not found. [404]';
        } else if (incoming === 500) {
            msg = 'Internal Server Error [500].';
        } else if (errorMessage) {
            msg = errorMessage;
        } else {
            msg = incoming;
        }

        $('#validationError').text(msg);
        $('#validationError').addClass('alert alert-danger');
    }

    function enableFormButtons() {
        $("#cancelAddBtn").button("enable");
        $("#submitAddBtn").button("enable");
    }

    function validate(selector) {
        if (selector.val() < 1) {
            selector.addClass("ui-state-error");
            return false;
        } else {
            return true;
        }
    }

    $("#addBtn").on("click", function (e) {

        e.preventDefault();

        $("#addPolicyFormModal").html("");

        $("#addPolicyFormModal").dialog("option", "title", "Loading...").dialog("open");

        $("#addPolicyFormModal").load(rootUrl + '/insurancepolicy/add', function () {
            $("#addPolicyFormModal").dialog("option", "title", "Add a policy");
            $("#SelectedRiskConstruction").selectmenu();
        });

    });

});